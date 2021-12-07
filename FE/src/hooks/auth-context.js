import React, { createContext, useEffect, useReducer } from "react";

import { getUser } from "../services/fetch-functions";
import { AUTH_ACTIONS } from "../utils/constants";

const initialState = {
    user: {},
    isAdmin: undefined,
    accessToken: undefined,
    expireTime: undefined
};

const reducer = (state, action) => {
    // eslint-disable-next-line default-case
    switch (action.type) {
        case AUTH_ACTIONS.LOGIN_USER:
            localStorage.setItem("accessToken", action.payload.accessToken);
            localStorage.setItem("expireTime", action.payload.expireTime);
            localStorage.setItem("isAdmin", action.payload.user.isAdmin);
            return {
                ...state,
                user: action.payload.user,
                isAdmin: action.payload.user.isAdmin,
                accessToken: action.payload.accessToken,
                expireTime: action.payload.expireTime
            };
        case AUTH_ACTIONS.LOGOUT_USER:
            localStorage.clear();
            return {
                user: {},
                accessToken: undefined,
                expireTime: undefined
            };
        case AUTH_ACTIONS.SET_ACCESS_TOKEN:
            localStorage.setItem("accessToken", action.payload.accessToken);
            return {
                ...state,
                accessToken: action.payload.accessToken
            };
        case AUTH_ACTIONS.SET_EXPIRE_TIME:
            localStorage.setItem("expireTime", action.payload.expireTime);
            return {
                ...state,
                expireTime: action.payload.expireTime
            };
        case AUTH_ACTIONS.UPDATE_USER_INFORMATION:
            localStorage.setItem("isAdmin", action.payload.user.isAdmin);
            return {
                ...state,
                user: action.payload.user
            };
        case AUTH_ACTIONS.UPDATE_USER_TYPE:
            localStorage.setItem("isAdmin", action.payload.isAdmin);
            return {
                ...state,
                isAdmin: action.payload.isAdmin
            };
    }
};

const AuthProvider = ({ children }) => {
    const [state, dispatch] = useReducer(reducer, initialState);

    useEffect(() => {
        const localAccessToken = localStorage.getItem("accessToken");
        const localExpireTime = localStorage.getItem("expireTime");
        const localIsAdmin = localStorage.getItem("isAdmin");

        if (localIsAdmin !== "undefined" && localIsAdmin !== null) {
            dispatch({
                type: AUTH_ACTIONS.SET_ACCESS_TOKEN,
                payload: {
                    isAdmin: localIsAdmin
                }
            });
        }
        if (localAccessToken !== "undefined" && localAccessToken !== null) {
            dispatch({
                type: AUTH_ACTIONS.SET_ACCESS_TOKEN,
                payload: {
                    accessToken: localAccessToken
                }
            });
        }
        if (localExpireTime !== "undefined" && localExpireTime !== null) {
            dispatch({
                type: AUTH_ACTIONS.SET_EXPIRE_TIME,
                payload: {
                    expireTime: localExpireTime
                }
            });
        }
    }, [state.user]);

    useEffect(() => {
        if (state.accessToken) {
            updateUserInformation(state.accessToken);
        }
    }, [state.accessToken]);

    function updateUserInformation(accessToken) {
        getUser(accessToken)
            .then(user => {
                dispatch({
                    type: AUTH_ACTIONS.UPDATE_USER_INFORMATION,
                    payload: { user }
                });
            })
            .catch(() => dispatch({
                type: AUTH_ACTIONS.LOGOUT_USER
            }));
    }

    return (
        <AuthContext.Provider value={{ state, dispatch, updateUserInformation }}>
            {children}
        </AuthContext.Provider>
    );
};

export const AuthContext = createContext();

export default AuthProvider;