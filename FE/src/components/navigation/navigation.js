import React from "react";
import { NavLink } from "react-router-dom";

import "./navigation.scss";
import { USER_TYPES } from "../../utils/constants";
import { userRoutes, adminRoutes } from "../../routes";

function Navigation({ displayMode }) {
  const userListItems = (
    <nav>
      <ul className="nav-list">
        {userRoutes.map(userRoute => (
          <li key={userRoute.id}>
            <NavLink
              to={userRoute.path}
              activeClassName="selected"
              exact
              className="link"
            >
              {userRoute.name}
            </NavLink>
          </li>
        ))}
      </ul>
    </nav>
  );

  const adminListItems = (
    <nav>
      <ul className="nav-list">
        {adminRoutes.map(adminRoute => (
          <li key={adminRoute.id}>
            <NavLink
              to={adminRoute.path}
              activeClassName="selected"
              exact
              className="link"
            >
              {adminRoute.name}
            </NavLink>
          </li>
        ))}
      </ul>
    </nav>
  );

  return displayMode === USER_TYPES.USER ? userListItems : adminListItems;
}

export default Navigation;
