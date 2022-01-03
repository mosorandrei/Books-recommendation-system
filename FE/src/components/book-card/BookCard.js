import React from "react";
import PropTypes from "prop-types";
import placeholderBookCover from "../../assets/placeholderBookCover.png";

import "./book-card.scss";

function BookCard({ title, description, imageUri, authors, children }) {
  function displayAuthors(authors) {
    let authorsLabel = "";
    authors.map((author) => {
      authorsLabel = authorsLabel.concat(
        author.firstName + " " + author.lastName + ", "
      );
    });
    authorsLabel = authorsLabel.slice(0, -2);

    return authorsLabel;
  }
  return (
    <div className="book">
      <img className="bookCover" src={imageUri || placeholderBookCover} />
      <div className="info">
        <div className="bookTitle">{title}</div>
        <div className="author">{authors && displayAuthors(authors)}</div>
        <div className="bookDescription">{description}</div>
      </div>

      {children}
    </div>
  );
}

BookCard.propTypes = {};

export default BookCard;
