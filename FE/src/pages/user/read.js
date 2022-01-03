import React, { useState, useEffect, useContext } from "react";

import BookCard from "../../components/book-card/BookCard";
import "../pages.scss";
import { getReadBooks } from "../../services/fetch-functions";
import { AuthContext } from "../../hooks/auth-context";
import { Rating } from "react-simple-star-rating";

function Read() {
  const {
    state: { accessToken, user },
    updateUserInformation,
  } = useContext(AuthContext);
  const [books, setBooks] = useState([]);
  const [rating, setRating] = useState(0);

  useEffect(() => {
    getReadBooks(accessToken)
      .then((result) => {
        setBooks(result);
      })
      .catch((error) => {
        console.log(error);
      });
  }, [accessToken, user]);

  const handleRating = (rate) => {
    setRating(rate);
  };

  return (
    <div className="books">
      <h2>Read books</h2>
      <div className="row">
        {books.map((book) => (
          <div key={book.id} className="bookLink">
            <BookCard {...book}>
              <div className="options">
                <Rating
                  className="rating"
                  onClick={handleRating}
                  ratingValue={rating}
                />
              </div>
            </BookCard>
          </div>
        ))}
      </div>
    </div>
  );
}

export default Read;
