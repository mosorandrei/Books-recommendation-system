import React, { useState, useEffect, useContext } from "react";

import UserCard from "../../components/user-card/UserCard";
import "../pages.scss";
import { getAllMembers } from "../../services/fetch-functions";
import Button from "../../components/button/button";
import { AuthContext } from "../../hooks/auth-context";

function Users() {
  const {
    state: { accessToken, user },
  } = useContext(AuthContext);
  const [users, setUsers] = useState([]);

  useEffect(() => {
    getAllMembers(accessToken)
      .then((result) => {
        setUsers(result);
      })
      .catch((error) => {
        console.log(error);
      });
  }, [accessToken, user]);

  const handleBlockUser = () => {};
  const handleUnblockUser = () => {};

  return (
    <div className="books">
      <h2>All users</h2>
      <div className="row">
        {users.map((user) => (
          <div key={user.username}>
            <UserCard {...user}>
              <div className="double-button">
                <Button
                  style="outlined"
                  color="red"
                  size="L"
                  onClick={() => handleBlockUser()}
                >
                  Block
                </Button>
                <Button
                  style="contained"
                  color="green"
                  size="L"
                  onClick={() => handleUnblockUser()}
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
