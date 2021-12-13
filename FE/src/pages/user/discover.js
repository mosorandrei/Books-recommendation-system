import React, { useState, useEffect, useContext } from "react";

import BookCard from "../../components/book-card/BookCard";
import "../pages.scss";
import { getAllBooks } from "../../services/fetch-functions";
import { AuthContext } from "../../hooks/auth-context";

function Discover() {
  const {
    state: { accessToken, user },
  } = useContext(AuthContext);
  const [books, setBooks] = useState([]);

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
    <div className="cards">
      <h2>All books</h2>
      <div className="row">
        {books.map((book) => (
          <div key={book.id}>
            <BookCard {...book} />
          </div>
        ))}
      </div>
    </div>
  );
}

export default Discover;
