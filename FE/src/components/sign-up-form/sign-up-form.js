import React from "react";
import PropTypes from "prop-types";

import { signUpUser } from "../../services/fetch-functions";
import TextInput from "../text-input/text-input";
import Button from "../button/button";
import useForm from "../../hooks/use-form";
import "./sign-up-form.scss";

function SignUpForm({ onSuccesfulRegister, onSwitchButton }) {
  const { fields, errors, setErrors, handleInputChange, handleSubmit } =
    useForm(
      {
        firstName: "",
        lastName: "",
        username: "",
        email: "",
        password: "",
        confirm: "",
      },
      {
        firstName: {
          require: true,
        },
        lastName: {
          require: true,
        },
        username: {
          require: true,
        },
        email: {
          required: true,
          pattern: {
            value: /^[^\s@]+@[^\s@]+\.[^\s@]+$/,
            message: "Email is not valid",
          },
        },
        password: {
          required: true,
          minLength: {
            value: 8,
            message: "Password must be at least 8 characters long",
          },
          maxLength: {
            value: 64,
            message: "Password can not contain more than 64 characters",
          },
          pattern: {
            value:
              /^(?=.*[A-Za-z])(?=.*\d)(?=.*[!#$%&*?@])[\d!#$%&*?@A-Za-z]{8,}$/,
            message:
              "Password must contain one letter, digit and special character",
          },
        },
        confirm: {
          required: true,
          minLength: {
            value: 8,
            message: "Password must be at least 8 characters long",
          },
          maxLength: {
            value: 64,
            message: "Password can not contain more than 64 characters",
          },
          pattern: {
            value:
              /^(?=.*[A-Za-z])(?=.*\d)(?=.*[!#$%&*?@])[\d!#$%&*?@A-Za-z]{8,}$/,
            message:
              "Password must contain one letter, digit and special character",
          },
          isEqualTo: {
            field: "password",
            message: "Password must match!",
          },
        },
      },
      (fields) => {
        signUpUser(fields)
          .then((result) => {
            onSuccesfulRegister();
          })
          .catch(() => {
            setErrors((prevErrors) => ({
              ...prevErrors,
              form: "The credentials provided are incorrect!",
            }));
          });
      }
    );

  return (
    <form className="sign-up-form" onSubmit={handleSubmit}>
      <TextInput
        inputName="firstName"
        inputLabel="First Name"
        type="text"
        onChange={handleInputChange}
        value={fields.firstName}
        error={errors.firstName}
      />
      <TextInput
        inputName="lastName"
        inputLabel="Last Name"
        type="text"
        onChange={handleInputChange}
        value={fields.lastName}
        error={errors.lastName}
      />
      <TextInput
        inputName="username"
        inputLabel="Username"
        onChange={handleInputChange}
        value={fields.username}
        error={errors.username}
      />
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
      <TextInput
        inputName="confirm"
        inputLabel="Confirm Password"
        type="password"
        onChange={handleInputChange}
        value={fields.confirm}
        error={errors.confirm}
      />

      {errors.form && <p className="error-message">{errors.form}</p>}
      <div className="form-bottom">
        <p>
          Already have an account?
          <a onClick={onSwitchButton}> Sign in</a>
        </p>
        <Button type="submit" style="contained" color="purple" size="XXXL">
          Sign Up
        </Button>
      </div>
    </form>
  );
}

SignUpForm.propTypes = {
  onSuccesfulRegister: PropTypes.func,
  onSwitchButton: PropTypes.func,
};

export default SignUpForm;
