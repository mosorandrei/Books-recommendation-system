export const USER_TYPES = {
  USER: 'user',
  ADMIN: 'admin'
};

export const AUTH_ACTIONS = {
  LOGIN_USER: "LOGIN_USER",
  LOGOUT_USER: "LOGOUT_USER",
  SET_ACCESS_TOKEN: "SET_ACCESS_TOKEN",
  SET_EXPIRE_TIME: "SET_EXPIRE_TIME",
  UPDATE_USER_INFORMATION: "UPDATE_USER_INFORMATION",
  UPDATE_USER_TYPE: "UPDATE_USER_TYPE"
};

export const API_HOST = "http://localhost:7187/api/v2/Token";

export const EXPIRE_TOKEN_TIME = 3600;
