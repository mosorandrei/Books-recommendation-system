import React from "react";

import "../../pages/pages.scss";

function AlreadyReadBook({ handleRating, getRateForBook }) {
  return (
    <div
      className="single-button"
      onClick={(e) => {
        e.stopPropagation();
        e.preventDefault();
      }}
    >
      <Rating
        className="rating"
        onClick={() => {
          handleRating();
        }}
        ratingValue={getRateForBook()}
      />
    </div>
  );
}

export default AlreadyReadBook;
