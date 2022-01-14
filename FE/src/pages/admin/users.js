import React, { useState, useEffect, useContext } from "react";

import UserCard from "../../components/user-card/UserCard";
import "../pages.scss";
import {
  getAllMembers,
  blockUser,
  unblockUser,
} from "../../services/fetch-functions";
import Button from "../../components/button/button";
import { AuthContext } from "../../hooks/auth-context";

function Users() {
  const {
    state: { accessToken, user },
  } = useContext(AuthContext);
  const [users, setUsers] = useState([]);
  const [usersStatus, setUsersStatus] = useState([]);

  useEffect(() => {
    accessToken &&
      getAllMembers(accessToken)
        .then((result) => {
          setUsers(result);
        })
        .catch((error) => {
          console.log(error);
        });
  }, [accessToken, user]);

  useEffect(() => {
    if (users) {
      let tempStatus = [];
      users.map((user) => {
        tempStatus.push({
          id: user.id,
          isBlocked: user.isBlocked,
        });
      });
      setUsersStatus(tempStatus);
    }
  }, [users]);

  const getStatusForUser = (id) => {
    let isBlocked;
    users &&
      usersStatus.map((user) => {
        if (user.id === id) {
          isBlocked = user.isBlocked;
        }
      });
    return isBlocked;
  };

  const handleBlockUser = (id) => {
    blockUser(id, accessToken).catch((error) => {
      console.log(error);
    });
    let tempStatus = usersStatus;
    tempStatus = tempStatus.map((user) => {
      if (user.id == id) {
        user.isBlocked = 1;
      }
      return user;
    });
    setUsersStatus(tempStatus);
  };

  const handleUnblockUser = (id) => {
    unblockUser(id, accessToken).catch((error) => {
      console.log(error);
    });
    let tempStatus = usersStatus;
    tempStatus = tempStatus.map((user) => {
      if (user.id == id) {
        user.isBlocked = 0;
      }
      return user;
    });
    setUsersStatus(tempStatus);
  };

  return (
    <div className="books">
      <h2>All users</h2>
      <div className="row">
        {users.map((user) => (
          <div key={user.id}>
            <UserCard {...user}>
              <div className="double-button">
                <Button
                  style="outlined"
                  color="red"
                  size="L"
                  onClick={() => handleBlockUser(user.id)}
                  disabled={getStatusForUser(user.id)}
                >
                  Block
                </Button>
                <Button
                  style="contained"
                  color="green"
                  size="L"
                  onClick={() => handleUnblockUser(user.id)}
                  disabled={!getStatusForUser(user.id)}
                >
                  Unblock
                </Button>
              </div>
            </UserCard>
          </div>
        ))}
      </div>
    </div>
  );
}

export default Users;
