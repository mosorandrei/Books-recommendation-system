import React from "react";
import PropTypes from "prop-types";

import "./button.scss";

function Button({
  type = "submit",
  style,
  color,
  size,
  onClick,
  children
}) {
  return <button type={type} className={`${style} ${color} ${size} `} onClick={onClick}>{children}</button>;
}

Button.propTypes = {
  type: PropTypes.oneOf(['button', 'submit', 'reset']),
  style: PropTypes.oneOf(['outlined', 'contained']),
  color: PropTypes.oneOf(['purple', 'green']),
  size: PropTypes.oneOf(['XXS', 'XS', 'S', 'M', 'L', 'XL', 'XXL', 'XXXL']),
  onClick: PropTypes.func,
  children: PropTypes.node
};

export default Button;
