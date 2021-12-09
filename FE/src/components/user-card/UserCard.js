import React from "react";
import PropTypes from "prop-types";

import Avatar from "../avatar/avatar";
import "./user-card.scss";

function UserCard({ username, email, firstName, lastName, children }) {
  return (
    <div className={"card"}>
      <Avatar
        avatarLink={undefined}
        name={firstName + " " + lastName}
        username={username}
      ></Avatar>

      <a class="email" href="mailto:contact@test.com">
        {email}
      </a>

      {children}
    </div>
  );
}

UserCard.propTypes = {
  username: PropTypes.string,
  email: PropTypes.string,
  firstName: PropTypes.string,
  lastName: PropTypes.node,
};

export default UserCard;
