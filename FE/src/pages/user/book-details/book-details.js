import React, { useContext, useEffect, useState } from "react";
import { useParams, useHistory } from "react-router-dom";

import Button from "../../../components/button/button";
import "./book-details.scss";
import {
  getBook,
  addToReading,
  addToFavourites,
  removeFromFavourites,
  getAllBooksForUser,
  rateBook,
  finishReading,
} from "../../../services/fetch-functions";
import { AuthContext } from "../../../hooks/auth-context";
import placeholderBookCover from "../../../assets/placeholderBookCover.png";
import closeIcon from "../../../assets/close.svg";
import heart from "../../../assets/heart.png";
import remove from "../../../assets/remove-icon.png";
import downloadIcon from "../../../assets/download-button.svg";
import checkIcon from "../../../assets/check-download.svg";
import { Rating } from "react-simple-star-rating";

function BookDetails() {
  const {
    state: { accessToken },
    updateUserInformation,
  } = useContext(AuthContext);
  const { id } = useParams();
  const [book, setBook] = useState({});
  const history = useHistory();
  const [statusForBook, setStatusForBook] = useState();
  const [rating, setRating] = useState();
  const [isFavourite, setIsFavourite] = useState();
  const [download, setDownload] = useState(false);
  const [isLoading, setIsLoading] = useState(true);

  setTimeout(() => {
    setIsLoading(false);
  }, 500);
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

  useEffect(() => {
    id &&
      accessToken &&
      getAllBooksForUser(accessToken)
        .then((result) => {
          result.map((object) => {
            if (object.book.bookId === id) {
              setStatusForBook(object.status);
              setRating(object.userScore);
              setIsFavourite(object.isFavourited);
            }
          });
        })
        .catch((error) => {
          console.log(error);
        });
  }, [accessToken, id]);

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

  function handleRemoveFromFavourites(bookId) {
    removeFromFavourites(bookId, accessToken)
      .then(() => {
        updateUserInformation(accessToken);
      })
      .catch((error) => {
        console.log(error);
      });
  }

  const handleRating = (rate, id) => {
    setRating(rate);
    rateBook(rate / 20, id, accessToken);
    updateUserInformation(accessToken);
  };

  function handleFinishReading(bookId) {
    finishReading(bookId, accessToken)
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

  const optionsForAlreadyReadBook = () => {
    return (
      <div className="bottom">
        <Rating
          className="rating"
          onClick={(event) => {
            handleRating(event, id);
            history.goBack();
          }}
          ratingValue={rating * 20}
        />
      </div>
    );
  };

  const optionsForNewBook = () => {
    return (
      <div className="bottom">
        <Button
          style="contained"
          color="purple"
          size="XXXL"
          onClick={() => {
            handleAddToReading(id);
            history.goBack();
          }}
        >
          Add to reading
        </Button>
        <img
          src={isFavourite ? remove : heart}
          className={isFavourite ? "remove" : "heart"}
          onClick={() => {
            isFavourite
              ? handleRemoveFromFavourites(id)
              : handleAddToFavourites(id);
            history.goBack();
          }}
        />
      </div>
    );
  };

  const optionsForReadingBook = (downloadLink) => {
    return (
      <div
        className="bottom"
        onClick={(event) => {
          event.stopPropagation();
        }}
      >
        <Button
          style="contained"
          color="purple"
          size="XXXL"
          onClick={() => {
            handleFinishReading(id);
            history.goBack();
          }}
        >
          Finish reading
        </Button>
        <a
          href={downloadLink}
          className={download ? "download-link" : ""}
          target="_blank"
          onClick={() => {
            setDownload(true);
            history.goBack();
          }}
        >
          <img
            className="download-button"
            src={!download ? downloadIcon : checkIcon}
          />
        </a>
      </div>
    );
  };

  return (
    <div className="page">
      <div className={isLoading && "loader"}>
        {!isLoading && (
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
                <div>
                  {statusForBook !== 0 ? (
                    <div className="rating">
                      <Rating ratingValue={book.rating * 20} readonly />
                    </div>
                  ) : (
                    ""
                  )}
                </div>
                {/* <div onClick={() => history.goBack()}> */}
                {statusForBook !== undefined &&
                  (statusForBook === 0
                    ? optionsForAlreadyReadBook()
                    : statusForBook === 1
                    ? optionsForNewBook()
                    : optionsForReadingBook(book.downloadUri))}
              </div>
            </div>
          </div>
          // </div>
        )}
      </div>
    </div>
  );
}

export default BookDetails;
