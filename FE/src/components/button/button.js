import React from "react";
import PropTypes from "prop-types";

import "./button.scss";

function Button({
  type = "submit",
  style,
  color,
  size,
  onClick,
  children,
  disabled,
}) {
  return (
    <button
      type={type}
      className={`${style} ${disabled ? "disabled" : color} ${size}`}
      onClick={onClick}
      disabled={disabled}
    >
      {children}
    </button>
  );
}

Button.propTypes = {
  type: PropTypes.oneOf(["button", "submit", "reset"]),
  style: PropTypes.oneOf(["outlined", "contained"]),
  color: PropTypes.oneOf(["purple", "green"]),
  size: PropTypes.oneOf([
    "XXS",
    "XS",
    "S",
    "M",
    "L",
    "XL",
    "XXL",
    "XXXL",
    "special",
  ]),
  onClick: PropTypes.func,
  children: PropTypes.node,
};

export default Button;
