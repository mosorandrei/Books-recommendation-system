import React, { useState, useEffect, useContext } from "react";
import { Link } from "react-router-dom";

import BookCard from "../../components/book-card/BookCard";
import "../pages.scss";
import { getReadBooks, rateBook } from "../../services/fetch-functions";
import { AuthContext } from "../../hooks/auth-context";
import { Rating } from "react-simple-star-rating";

function Read() {
  const {
    state: { accessToken, user },
  } = useContext(AuthContext);
  const [books, setBooks] = useState([]);
  const [rating, setRating] = useState([]);

  useEffect(() => {
    accessToken &&
      getReadBooks(accessToken)
        .then((result) => {
          setBooks(result);
        })
        .catch((error) => {
          console.log(error);
        });
  }, [accessToken, user]);

  useEffect(() => {
    if (books) {
      let tempRating = [];
      books.map((book) => {
        tempRating.push({
          id: book.bookId,
          rate: book.userAssignedScore * 20,
        });
      });
      setRating(tempRating);
    }
  }, [books]);

  const handleRating = (rate, id) => {
    let tempRating = rating;
    tempRating = tempRating.map((bookRating) => {
      if (bookRating.id == id) {
        bookRating.rate = rate;
      }
      return bookRating;
    });
    setRating(tempRating);
    rateBook(rate / 20, id, accessToken);
  };

  const getRateForBook = (id) => {
    let bookRating = 0;
    rating &&
      rating.map((rating) => {
        if (rating.id === id) {
          bookRating = rating.rate;
        }
      });
    return bookRating;
  };

  return (
    <div className="books">
      <h2>Read books</h2>
      <div className="row">
        {books.map((book) => (
          <div key={book.bookId} className="bookLink">
            <Link to={`/user/book/${book.bookId}`}>
              <BookCard {...book}>
                <div
                  className="options"
                  onClick={(event) => {
                    event.stopPropagation();
                    event.preventDefault();
                  }}
                >
                  <Rating
                    className="rating"
                    onClick={(rate) => handleRating(rate, book.bookId)}
                    ratingValue={getRateForBook(book.bookId)}
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

export default Read;
