import React, { useState, useEffect, useContext } from "react";
import { Link } from "react-router-dom";

import BookCard from "../../components/book-card/BookCard";
import "../pages.scss";
import { getAllGenres, getBooksByGenre } from "../../services/fetch-functions";
import { AuthContext } from "../../hooks/auth-context";

function Categories() {
  const {
    state: { accessToken, user },
    updateUserInformation,
  } = useContext(AuthContext);
  const [genres, setGenres] = useState([]);
  const [books, setBooks] = useState([]);
  var booksByGenre = [];
  const [selected, setSelected] = useState("");
  const [displayedBooks, setDisplayedBooks] = useState([]);

  useEffect(() => {
    setSelected(localStorage.getItem("selected"));
    setBooks(JSON.parse(localStorage.getItem("books")));
  }, [window.location]);

  useEffect(() => {
    localStorage.getItem("accessToken") &&
      getAllGenres(accessToken)
        .then((result) => {
          setGenres(result);
        })
        .catch((error) => {
          console.log(error);
        });
  }, [accessToken, user]);

  useEffect(() => {
    genres &&
      genres.map((genre) => {
        getBooksByGenre(genre.id, accessToken)
          .then((result) => {
            booksByGenre = booksByGenre.concat([
              { id: genre.id, result: result },
            ]);
            setBooks(booksByGenre);
            localStorage.setItem("books", JSON.stringify(booksByGenre));
          })
          .catch((error) => {
            console.log(error);
          });
      });
  }, [accessToken, user, genres]);

  useEffect(() => {
    if (selected && books) {
      books.map((genre) => {
        if (genre.id === selected) {
          setDisplayedBooks(genre.result);
        }
      });
    }
  }, [accessToken, user, selected]);

  return (
    <div className="categories-page">
      <div className="categories">
        {genres.map((genre) => (
          <div
            key={genre.id}
            className={selected === genre.id ? "genre active" : "genre"}
            onClick={() => {
              setSelected(genre.id);
              localStorage.setItem("selected", genre.id);
            }}
          >
            {genre.name}
          </div>
        ))}
      </div>
      <div className="books">
        <div className="row">
          {displayedBooks.map((book) => (
            <div key={book.bookId} className="bookLink">
              <Link to={`/user/book/${book.bookId}`}>
                <BookCard {...book}>
                  <div className="options"></div>
                </BookCard>
              </Link>
            </div>
          ))}
        </div>
      </div>
    </div>
  );
}

export default Categories;
