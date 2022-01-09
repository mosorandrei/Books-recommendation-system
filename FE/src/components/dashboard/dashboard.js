import React, { useState, useRef, useContext } from "react";
import { Link } from "react-router-dom";
import PropType from "prop-types";

import "./dashboard.scss";
import Avatar from "../avatar/avatar";
import Navigation from "../navigation/navigation";
import Level from "../level/level";
import { USER_TYPES, AUTH_ACTIONS } from "../../utils/constants";
import { getNewAccessToken } from "../../services/fetch-functions";
import useInterval from "../../hooks/use-interval";
import Button from "../button/button";
import Modal from "../modal/modal";
import { AuthContext } from "../../hooks/auth-context";

function Dashboard({ onChangeDisplayMode, displayMode, children }) {
  const {
    state: { expireTime, accessToken, user },
    dispatch,
  } = useContext(AuthContext);
  const endSessionModalRef = useRef();
  const [toEndSession, setToEndSession] = useState(true);

  function handleContinueSession(e) {
    getNewAccessToken(accessToken).then((response) => {
      dispatch({
        type: AUTH_ACTIONS.SET_ACCESS_TOKEN,
        payload: {
          accessToken: response.token,
        },
      });
    });
    endSessionModalRef.current.closeModal();
    setToEndSession(false);
  }

  useInterval(() => {
    endSessionModalRef.current.openModal();
  }, (expireTime - 30) * 1000);

  useInterval(() => {
    if (toEndSession) {
      dispatch({
        type: "LOGOUT_USER",
      });
    } else {
      setToEndSession(true);
    }
  }, expireTime * 1000);

  const switchButton =
    displayMode === USER_TYPES.USER ? (
      <Link
        onClick={() => onChangeDisplayMode()}
        to={`/${USER_TYPES.ADMIN}/library`}
      >
        Switch to {USER_TYPES.ADMIN}
      </Link>
    ) : (
      <Link
        onClick={() => onChangeDisplayMode()}
        to={`/${USER_TYPES.USER}/discover`}
      >
        Switch to {USER_TYPES.USER}
      </Link>
    );

  const handleLogoutButton = () => {
    dispatch({
      type: AUTH_ACTIONS.LOGOUT_USER,
    });
  };

  return (
    <>
      <div className="dashboard">
        <div className="lhp">
          <Avatar name={user.fullName} avatarLink={user?.avatar} />

          <Level
            alreadyRead={user?.numberOfReads}
            allBooks={user?.numberOfReads + user?.numberOfReadings}
          />

          <Navigation displayMode={displayMode} />

          <div className="lhp-bottom">
            {user.isAdmin ? switchButton : undefined}
            <a onClick={handleLogoutButton}>Sign out</a>
          </div>
        </div>
        <div className="rhp">{children}</div>
      </div>
      <Modal
        title="Your session is about to expire. &#13; Do you want to continue?"
        ref={endSessionModalRef}
      >
        <Button
          type="button"
          style="contained"
          color="purple"
          size="L"
          onClick={(e) => handleContinueSession(e)}
        >
          Yes
        </Button>
      </Modal>
    </>
  );
}

Dashboard.propTypes = {
  onChangeDisplayMode: PropType.func,
  displayMode: PropType.oneOf([USER_TYPES.USER, USER_TYPES.ADMIN]),
  children: PropType.node,
};

export default Dashboard;
