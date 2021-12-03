import React, { useContext } from "react";

import { getUser, signInUser } from "../../services/fetch-functions";
import TextInput from "../text-input/text-input";
import Button from "../button/button";
import useForm from "../../hooks/use-form";
import "./sign-in-form.scss";
import { AuthContext } from "../../hooks/auth-context";
import { AUTH_ACTIONS } from "../../utils/constants";


function SignInForm({ onSwitchButton }) {

    const { dispatch } = useContext(AuthContext);

    const { fields, errors, setErrors, handleInputChange, handleSubmit } = useForm(
        {
            email: '',
            password: '',
        },
        {
            email: {
                required: true,
                pattern: {
                    value: /^[^\s@]+@[^\s@]+\.[^\s@]+$/,
                    message: "Email is not valid"
                }
            },
            password: {
                required: true,
            }
        },
        fields => {
            signInUser(fields)
                .then(result => {
                    getUser(result.token)
                        .then(userInformation => (
                            dispatch({
                                type: AUTH_ACTIONS.LOGIN_USER,
                                payload: {
                                    user:  userInformation ,
                                    accessToken: result.token,
                                    expireTime: result.expireTime
                                }
                            }))
                        )
                        .catch(() => dispatch({
                            type: AUTH_ACTIONS.LOGOUT_USER
                        }));
                })
                .catch(() => {
                    setErrors(prevErrors => ({ ...prevErrors, form: 'The credentials provided are incorrect!' }
                    ));
                });
        }
    );

     return (
    <form className="sign-in-form" onSubmit={handleSubmit}>
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

      {errors.form && <p className="error-message">{errors.form}</p>}
      <div className="form-bottom">
        <p>
          No account?
          <a onClick={onSwitchButton}> Register here</a>
        </p>
        <Button type="submit" style="contained" color="purple" size="XXXL">
          Sign In
        </Button>
      </div>
    </form>
  );
}

export default SignInForm;


