import React, { useContext, useEffect, useState } from "react";

import {
  getUser,
  resetPassword,
  signInUser,
} from "../../services/fetch-functions";
import TextInput from "../text-input/text-input";
import Button from "../button/button";
import useForm from "../../hooks/use-form";
import "./sign-in-form.scss";
import { AuthContext } from "../../hooks/auth-context";
import { AUTH_ACTIONS } from "../../utils/constants";
import checkIcon from "../../assets/send-email-v2.png";
import closeIcon from "../../assets/back-button.svg";

import { useHistory } from "react-router-dom";

function SignInForm({ onSwitchButton }) {
  const { dispatch } = useContext(AuthContext);
  const [forgotPassword, setForgotPassword] = useState(false);
  const [inputValue, setInputValue] = useState("");
  const [codeValue, setCodeValue] = useState("");
  const [newPasswordValue, setNewPasswordValue] = useState("");
  const [confirmNewPasswordValue, setConfirmNewPasswordValue] = useState("");
  const [displayResetInputs, setDisplayResetInputs] = useState(false);
  const [email, setEmail] = useState("");
  const history = useHistory();

  useEffect(() => {
    if (!forgotPassword && !displayResetInputs)
      document.getElementById("title-form").innerHTML = "Sign In";
    else document.getElementById("title-form").innerHTML = "Forgot Password";
  });
  function openForgotPassword() {
    setForgotPassword(true);
  }
  function sendEmailWithCode(email) {
    //call api
    console.log(email);
    setEmail(email);
    setDisplayResetInputs(true);
  }
  const onChangeHandlerInput = (event) => {
    setInputValue(event.target.value);
  };
  const onChangeHandlerCode = (event) => {
    setCodeValue(event.target.value);
  };
  const onChangeHandlerNewPassword = (event) => {
    setNewPasswordValue(event.target.value);
  };
  const onChangeHandlerConfirmNewPassword = (event) => {
    setConfirmNewPasswordValue(event.target.value);
  };
  const onHandleResetPassword = () => {
    //call api to reset
    console.log(codeValue, newPasswordValue, confirmNewPasswordValue, email);
  };
  const { fields, errors, setErrors, handleInputChange, handleSubmit } =
    useForm(
      {
        email: "",
        password: "",
      },
      {
        email: {
          required: true,
          pattern: {
            value: /^[^\s@]+@[^\s@]+\.[^\s@]+$/,
            message: "Email is not valid",
          },
        },
        password: {
          required: true,
        },
      },
      (fields) => {
        signInUser(fields)
          .then((result) => {
            getUser(result.token)
              .then((userInformation) =>
                dispatch({
                  type: AUTH_ACTIONS.LOGIN_USER,
                  payload: {
                    user: userInformation,
                    accessToken: result.token,
                    expireTime: result.expireTime,
                  },
                })
              )
              .catch(() =>
                dispatch({
                  type: AUTH_ACTIONS.LOGOUT_USER,
                })
              );
          })
          .catch((error) => {
            console.log(error);
            if (error === `User is blocked`) {
              setErrors((prevErrors) => ({
                ...prevErrors,
                form: "This account is blocked!",
              }));
            } else {
              setErrors((prevErrors) => ({
                ...prevErrors,
                form: "The credentials provided are incorrect!",
              }));
            }
          });
      }
    );

  return (
    <>
      <form
        className={
          !forgotPassword && !displayResetInputs
            ? "sign-in-form"
            : "sign-in-form forgot-password"
        }
        onSubmit={handleSubmit}
      >
        {!forgotPassword && !displayResetInputs && (
          <>
            <TextInput
              inputName="email"
              inputLabel="Email"
              onChange={handleInputChange}
              value={fields.email}
              error={errors.email}
            />
            <TextInput
              inputName="password"
              inputLabel="Password"
              type="password"
              onChange={handleInputChange}
              value={fields.password}
              error={errors.password}
            />
          </>
        )}
        {errors.form && <p className="error-message">{errors.form}</p>}
        <div className={!forgotPassword ? "form-bottom" : "form-bottom-reset"}>
          {!forgotPassword && (
            <p>
              No account?
              <a onClick={onSwitchButton}> Register here</a>
            </p>
          )}
          {!forgotPassword && !displayResetInputs && (
            <p>
              <a onClick={() => openForgotPassword()}>Forgot password?</a>
            </p>
          )}
          {!forgotPassword && (
            <Button type="submit" style="contained" color="purple" size="XXXL">
              Sign In
            </Button>
          )}
        </div>
      </form>

      {forgotPassword && !displayResetInputs ? (
        <div className="send-email-form">
          <p>
            &nbsp;Please enter the email address that you used to register, and
            we will send you an email with a verification code in order to reset
            your password.{" "}
          </p>
          <div className="send-email">
            <div className="input-reset">
              <input
                className="field"
                type="text"
                name="name"
                onChange={onChangeHandlerInput}
                value={inputValue}
              />
            </div>
            <img
              className="download-button"
              src={checkIcon}
              onClick={() => sendEmailWithCode(inputValue)}
            />
          </div>
          <img
            className="close-button-reset"
            src={closeIcon}
            onClick={() => history.goBack()}
          />
        </div>
      ) : (
        forgotPassword &&
        displayResetInputs && (
          <div className="reset-password-form">
            <div className="input-reset-password">
              <input
                className="field"
                type="text"
                name="name"
                placeholder="Verification Code"
                onChange={onChangeHandlerCode}
                value={codeValue}
              />
            </div>
            <div className="input-reset-password">
              <input
                className="field"
                type="text"
                name="name"
                placeholder="New Password"
                onChange={onChangeHandlerNewPassword}
                value={newPasswordValue}
              />
            </div>
            <div className="input-reset-password">
              <input
                className="field"
                type="text"
                name="name"
                placeholder="Confirm New Password"
                onChange={onChangeHandlerConfirmNewPassword}
                value={confirmNewPasswordValue}
              />
            </div>
            <div className="double-button-reset">
              <Button
                style="contained"
                color="purple"
                size="XL"
                onClick={() => history.goBack()}
              >
                Cancel
              </Button>
              <Button
                style="contained"
                color="purple"
                size="XL"
                onClick={() => onHandleResetPassword()}
              >
                Reset Password
              </Button>
            </div>
          </div>
        )
      )}
    </>
  );
}

export default SignInForm;
