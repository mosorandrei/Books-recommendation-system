import React, { useState, useEffect, useContext } from "react";
import { Link } from "react-router-dom";

import BookCard from "../../components/book-card/BookCard";
import "../pages.scss";
import {
  getAllBooks,
  addToFavourites,
  addToReading,
} from "../../services/fetch-functions";
import { AuthContext } from "../../hooks/auth-context";
import Button from "../../components/button/button";
import heart from "../../assets/heart.png";

function Discover() {
  const {
    state: { accessToken, user },
    updateUserInformation,
  } = useContext(AuthContext);
  const [books, setBooks] = useState([]);

  function handleAddToReading(bookId) {
    addToReading(bookId, accessToken)
      .then(() => {
        updateUserInformation(accessToken);
      })
      .catch((error) => {
        console.log(error);
      });
  }

  function handleAddToFavourites(bookId) {
    addToFavourites(bookId, accessToken)
      .then(() => {
        updateUserInformation(accessToken);
      })
      .catch((error) => {
        console.log(error);
      });
  }

  useEffect(() => {
    getAllBooks(accessToken)
      .then((result) => {
        console.log(result);
        setBooks(result);
      })
      .catch((error) => {
        console.log(error);
      });
  }, [accessToken, user]);

  return (
    <div className="books">
      <h2>All books</h2>
      <div className="row">
        {books.map((book) => (
          <div key={book.bookId} className="bookLink">
            <Link to={`/user/book/${book.bookId}`}>
              <BookCard {...book}>
                <div className="options">
                  <Button
                    style="contained"
                    color="purple"
                    size="XL"
                    onClick={(event) => {
                      event.stopPropagation();
                      event.preventDefault();
                      handleAddToReading(book.bookId);
                    }}
                  >
                    Add to reading
                  </Button>
                  <img
                    src={heart}
                    className="heart"
                    onClick={(event) => {
                      event.stopPropagation();
                      event.preventDefault();
                      handleAddToFavourites(book.bookId);
                    }}
                  />
                </div>
              </BookCard>
            </Link>
          </div>
        ))}
      </div>
    </div>
  );
}

export default Discover;
