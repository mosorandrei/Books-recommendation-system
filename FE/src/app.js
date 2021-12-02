import {
  BrowserRouter as Router,
  Switch as Routes,
  Route,
  Redirect as Navigate,
} from "react-router-dom";
import React, { useContext, useState } from "react";

import ConditionalWrapper from "./components/conditional-wrapper/conditional-wrapper";
import Dashboard from "./components/dashboard/dashboard";
import { USER_TYPES } from "./utils/constants";
import { userRoutes, adminRoutes } from "./routes";
import BookDetails from "./pages/user/book-details/book-details";
import Auth from "./pages/auth/auth";
import { AuthContext } from "./hooks/auth-context";

function App() {

  const { isAdmin, accessToken } = useContext(AuthContext);

  const [displayMode, setDisplayMode] = useState(
    localStorage.getItem("displayMode") || USER_TYPES.USER
  );

  function changeDisplayMode() {
    const toggledDisplayMode =
      displayMode === USER_TYPES.ADMIN ? USER_TYPES.USER : USER_TYPES.ADMIN;
    setDisplayMode(toggledDisplayMode);
    localStorage.setItem("displayMode", toggledDisplayMode);
  }

  return (
    <Router>
      <ConditionalWrapper
        condition={accessToken !== undefined
          || (localStorage.getItem("accessToken") !== undefined
            && localStorage.getItem("accessToken") !== null)}
        wrapper={children => (
          <Dashboard
            onChangeDisplayMode={changeDisplayMode}
            displayMode={displayMode}
          >
            {children}
          </Dashboard>
        )}
      >
        <div className="app">
          <Routes>
            <Route path="/" exact>
              {accessToken !== undefined
                || (localStorage.getItem("accessToken") !== undefined
                  && localStorage.getItem("accessToken") !== null) ? <Navigate to="/user/discover" /> : <Auth />}
            </Route>

            {userRoutes.map(userRoute => {
              const UserRouteComponent = userRoute.element;
              return (
                <Route key={userRoute.id} path={userRoute.path} exact>
                  {(localStorage.getItem("accessToken") !== "undefined"
                    && localStorage.getItem("accessToken") !== undefined
                    && localStorage.getItem("accessToken") !== null) ? (
                    <UserRouteComponent />
                  ) : (
                    <Navigate to="/" />
                  )}
                </Route>
              );
            })}

            {adminRoutes.map(adminRoute => {
              const AdminRouteComponent = adminRoute.element;
              return (
                <Route key={adminRoute.id} path={adminRoute.path} exact>
                  {isAdmin
                    || (localStorage.getItem("isAdmin") !== "undefined"
                    && localStorage.getItem("isAdmin") !== undefined
                      && localStorage.getItem("isAdmin") !== null
                      && localStorage.getItem("isAdmin") === "1") ? (
                    <AdminRouteComponent />
                  ) : (
                    <Navigate to="/" />
                  )}
                </Route>
              );
            })}

            <Route path="/user/book/:id" exact>
              {accessToken !== undefined
                || (localStorage.getItem("accessToken") !== undefined
                  && localStorage.getItem("accessToken") !== null) ? (
                <BookDetails />
              ) : (
                <Navigate to="/" />
              )}
            </Route>

            <Route render={() => <Navigate to="/" />} />
          </Routes>
        </div>
      </ConditionalWrapper>
    </Router>
  );
}

export default App;
