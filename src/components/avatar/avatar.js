import React from "react";
import "./avatar.scss";

import defaultAvatar from "../../assets/default-avatar.png";

function Avatar({ avatarLink, name = "John Doe", username = "" }) {
  return (
    <div className="information">
      <img className="avatar" src={avatarLink || defaultAvatar} />
      <div>
        <p className="name">{name}</p>
        {username && <p className="username"> {username} </p>}
      </div>
    </div>
  );
}

export default Avatar;
