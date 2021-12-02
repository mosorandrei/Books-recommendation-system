import React from "react";
import PropType from 'prop-types';

import Label from "../label/label";
import "./level.scss";

function Level({
  alreadyRead = 0,
  allBooks = 0,
}) {
  const completedBooks = Math.floor((alreadyRead * 100) / (allBooks || 1));

  return (
    <div className="status">
      <div className="label">
        <Label color="purple" fontWeight="bold">
          {alreadyRead} / {allBooks} Books
        </Label>
      </div>

      <div className="bar">
        <div
          className="done-bar"
          style={{ width: `${completedBooks}% ` }}
        ></div>
        <div
          className="remain-bar"
          style={{ width: `${100 - completedBooks}%` }}
        ></div>
      </div>
    </div>
  );
}

Level.propTypes = {
  alreadyRead: PropType.number,
  allBooks: PropType.number,
};

export default Level;
