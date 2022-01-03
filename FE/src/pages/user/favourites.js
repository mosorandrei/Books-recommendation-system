import React, { useState, useEffect, useContext } from "react";
import { Link } from "react-router-dom";

import BookCard from "../../components/book-card/BookCard";
import "../pages.scss";
import { getFavourites, addToReading } from "../../services/fetch-functions";
import { AuthContext } from "../../hooks/auth-context";
import Button from "../../components/button/button";

function Favourites() {
  const {
    state: { accessToken, user },
    updateUserInformation,
  } = useContext(AuthContext);
  const [books, setBooks] = useState([]);

  useEffect(() => {
    getFavourites(accessToken)
      .then((result) => {
        setBooks(result);
      })
      .catch((error) => {
        console.log(error);
      });
  }, [accessToken, user]);

  function handleAddToReading(bookId) {
    addToReading(bookId, accessToken)
      .then(() => {
        updateUserInformation(accessToken);
      })
      .catch((error) => {
        console.log(error);
      });
  }

  return (
    <div className="books">
      <h2>Favourites</h2>
      <div className="row">
        {books.map((book) => (
          <div key={book.id} className="bookLink">
            <Link to={`/user/book/${book.id}`}>
              <BookCard {...book}>
                <div className="single-button">
                  <Button
                    style="contained"
                    color="purple"
                    size="XXL"
                    onClick={(event) => {
                      event.stopPropagation();
                      event.preventDefault();
                      handleAddToReading(book.id);
                    }}
                  >
                    Add to reading
                  </Button>
                </div>
              </BookCard>
            </Link>
          </div>
        ))}
      </div>
    </div>
  );
}

export default Favourites;
