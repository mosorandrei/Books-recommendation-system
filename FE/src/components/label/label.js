import React from "react";
import "./label.scss";

function Label({ color, fontWeight, children }) {
  return <div className={`label ${color} ${fontWeight}`}>{children}</div>;
}
export default Label;
