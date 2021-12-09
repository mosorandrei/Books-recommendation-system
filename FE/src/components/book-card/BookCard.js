import React from "react";
import PropTypes from "prop-types";

import "./book-card.scss";

function BookCard({
  title,
  rating,
  description,
  imageUri,
  downloadUri,
  children,
}) {
  console.log(title);
  return (
    <div className={"card"}>
      <div>{title}</div>
      <div>{rating}</div>
      <div>{description}</div>
      <div>{imageUri}</div>
      <div>{downloadUri}</div>

      {children}
    </div>
  );
}

BookCard.propTypes = {};

export default BookCard;
