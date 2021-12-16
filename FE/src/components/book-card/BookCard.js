import React from "react";
import PropTypes from "prop-types";
import placeholderBookCover from "../../assets/placeholderBookCover.png";

import "./book-card.scss";

function BookCard({
  title,
  rating,
  description,
  imageUri,
  downloadUri,
  children,
}) {
  return (
    <div className="book">
      <img className="bookCover" src={imageUri || placeholderBookCover} />
      <div className="info">
        <div className="bookTitle">{title}</div>
        <div className="author">{"Author"}</div>
        <div className="bookDescription">{description}</div>
      </div>

      {children}
    </div>
  );
}

BookCard.propTypes = {};

export default BookCard;
