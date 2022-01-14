import React from "react";
import PropTypes from "prop-types";

import "./text-input.scss";

function TextInput({
  inputName,
  inputLabel,
  type = "text",
  error,
  value,
  onChange,
}) {
  return (
    <div className="input">
      <input
        className={`input-field ${error && "error"}`}
        name={inputName}
        type={type}
        value={value || ""}
        onChange={onChange}
        placeholder=" "
      />
      <label className="input-label">{inputLabel}</label>
      {error && <p className="error-message">{error}</p>}
    </div>
  );
}

TextInput.propTypes = {
  inputName: PropTypes.string,
  inputLabel: PropTypes.string,
  type: PropTypes.oneOf(["text", "password", "number"]),
  error: PropTypes.string,
  value: PropTypes.string,
  onChange: PropTypes.func,
};

export default TextInput;
