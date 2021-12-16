import React, { useState, useEffect, useContext } from "react";
import { Link } from "react-router-dom";

import BookCard from "../../components/book-card/BookCard";
import "../pages.scss";
import { getAllBooks } from "../../services/fetch-functions";
import { AuthContext } from "../../hooks/auth-context";
import Button from "../../components/button/button";
import heart from "../../assets/heart.png";

function Discover() {
  const {
    state: { accessToken, user },
  } = useContext(AuthContext);
  const [books, setBooks] = useState([]);

  const addToReading = () => {};
  const addToFavourites = () => {};

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
          <div key={book.id} className="bookLink">
            <Link to={`/user/book/${book.id}`}>
              <BookCard {...book}>
                <div className="options">
                  <Button
                    style="contained"
                    color="purple"
                    size="XL"
                    onClick={() => addToReading()}
                  >
                    Add to reading
                  </Button>
                  <img
                    src={heart}
                    className="heart"
                    onClick={() => addToFavourites()}
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
