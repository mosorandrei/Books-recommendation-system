import React, { useContext, useEffect, useState } from "react";
import { useParams, Link, useHistory } from "react-router-dom";

import Button from "../../../components/button/button";
import "./book-details.scss";
import {
  getBook,
  addToReading,
  addToFavourites,
} from "../../../services/fetch-functions";
import { AuthContext } from "../../../hooks/auth-context";
import placeholderBookCover from "../../../assets/placeholderBookCover.png";
import closeIcon from "../../../assets/close.svg";
import heart from "../../../assets/heart.png";
import { Rating } from "react-simple-star-rating";

function BookDetails() {
  const {
    state: { accessToken },
    updateUserInformation,
  } = useContext(AuthContext);
  const { id } = useParams();
  const [book, setBook] = useState({});
  const history = useHistory();

  useEffect(() => {
    if (id) {
      getBook(id, accessToken)
        .then((result) => {
          setBook(result);
        })
        .catch((error) => {
          console.log(error);
        });
    }
  }, [id, accessToken]);

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

  function displayGenres(genres) {
    let genresLabel = "";
    genres.map((genre) => {
      genresLabel = genresLabel.concat(genre.name + ", ");
    });
    genresLabel = genresLabel.slice(0, -2);

    return genresLabel;
  }

  return (
    <div className="product">
      <div className="left-info">
        <img
          className="bookImage"
          src={book.imageUri || placeholderBookCover}
        />
      </div>
      <div className="right-info">
        <div className="close" onClick={() => history.goBack()}>
          <img src={closeIcon} />
        </div>
        <div className="details">
          <div className="name">{book.title}</div>
          <div className="authors">
            {book.authors && displayAuthors(book.authors)}
          </div>
          <div className="specification">{book.description}</div>
          <div className="genres">
            {book.genres && displayGenres(book.genres)}
          </div>
          <Rating
            className="rating"
            ratingValue={(book.rating * 100) / 5}
            readonly
          />
          <Link to="/user/book">
            <div className="bottom">
              <Button
                style="contained"
                color="purple"
                size="XXXL"
                onClick={() => {
                  handleAddToReading(id);
                  history.push("/user/book");
                }}
              >
                Add to reading
              </Button>
              <img
                src={heart}
                className="heart"
                onClick={() => {
                  handleAddToFavourites(id);
                  history.push("/user/book");
                }}
              />
            </div>
          </Link>
        </div>
      </div>
    </div>
  );
}

export default BookDetails;
