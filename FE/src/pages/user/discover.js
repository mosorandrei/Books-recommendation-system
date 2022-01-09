import React, { useState, useEffect, useContext } from "react";
import { Link } from "react-router-dom";

import BookCard from "../../components/book-card/BookCard";
import { Rating } from "react-simple-star-rating";
import "../pages.scss";
import {
  getAllBooksForUser,
  addToFavourites,
  addToReading,
  rateBook,
  finishReading,
  removeFromFavourites,
} from "../../services/fetch-functions";
import { AuthContext } from "../../hooks/auth-context";
import Button from "../../components/button/button";
import heart from "../../assets/heart.png";
import remove from "../../assets/remove-icon.png";
import downloadIcon from "../../assets/download-button.svg";
import checkIcon from "../../assets/check-download.svg";

function Discover() {
  const {
    state: { accessToken, user },
    updateUserInformation,
  } = useContext(AuthContext);
  const [books, setBooks] = useState([]);
  const [rating, setRating] = useState([]);
  const [download, setDownload] = useState([]);

  useEffect(() => {
    accessToken &&
      getAllBooksForUser(accessToken)
        .then((result) => {
          setBooks(result);
        })
        .catch((error) => {
          console.log(error);
        });
    let tempDownloadStatus = [];
    books.map((object) => {
      tempDownloadStatus.push({ id: object.book.bookId, download: false });
    });
    setDownload(tempDownloadStatus);
  }, [accessToken, user]);

  useEffect(() => {
    if (books) {
      let tempRating = [];
      books.map((book) => {
        tempRating.push({
          id: book.book.bookId,
          rate: book.userScore * 20,
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

  function handleAddToReading(bookId) {
    addToReading(bookId, accessToken)
      .then(() => {
        updateUserInformation(accessToken);
      })
      .catch((error) => {
        console.log(error);
      });
  }

  function handleFinishReading(bookId) {
    finishReading(bookId, accessToken)
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

  const getRateForBook = (id) => {
    let bookRating = 0;
    books &&
      rating.map((rating) => {
        if (rating.id === id) {
          bookRating = rating.rate;
        }
      });
    return bookRating;
  };

  const getDownloadStatusForBook = (id) => {
    let downloadStatus = false;
    download &&
      download.map((statusForBook) => {
        if (statusForBook.id === id) {
          downloadStatus = statusForBook.download;
        }
      });
    return downloadStatus;
  };

  const setDownloadStatusForBook = (id) => {
    let tempDownloadStatus = download;

    tempDownloadStatus = tempDownloadStatus.map((statusForBook) => {
      if (statusForBook.id === id) {
        return {
          ...statusForBook,
          download: true,
        };
      }
      return statusForBook;
    });

    setDownload(tempDownloadStatus);
  };

  const optionsForAlreadyReadBook = (id) => {
    return (
      <div
        className="single-button"
        onClick={(e) => {
          e.stopPropagation();
          e.preventDefault();
        }}
      >
        <Rating
          className="rating"
          onClick={(event) => {
            handleRating(event, id);
          }}
          ratingValue={getRateForBook(id)}
        />
      </div>
    );
  };

  const optionsForReadingBook = (id, downloadLink) => {
    return (
      <div
        className="options"
        onClick={(event) => {
          event.stopPropagation();
        }}
      >
        <Button
          style="contained"
          color="purple"
          size="XL"
          onClick={() => {
            handleFinishReading(id);
          }}
        >
          Finish reading
        </Button>
        <a
          href={downloadLink}
          className={getDownloadStatusForBook(id) ? "download-link" : ""}
          target="_blank"
          onClick={() => {
            setDownloadStatusForBook(id);
          }}
        >
          <img
            className="download-button"
            src={!getDownloadStatusForBook(id) ? downloadIcon : checkIcon}
          />
        </a>
      </div>
    );
  };

  const optionsForNewBook = (id, isFavourite) => {
    return (
      <div
        className="options"
        onClick={(event) => {
          event.stopPropagation();
          event.preventDefault();
        }}
      >
        <Button
          style="contained"
          color="purple"
          size="XL"
          onClick={() => {
            handleAddToReading(id);
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
          }}
        />
      </div>
    );
  };

  return (
    <div className="books">
      <h2>All books</h2>
      <div className="row">
        {books.map((object) => (
          <div key={object.book.bookId} className="bookLink">
            <Link to={`/user/book/${object.book.bookId}`}>
              <BookCard {...object.book}>
                <div>
                  {object.status == 0
                    ? optionsForAlreadyReadBook(object.book.bookId)
                    : object.status == 2
                    ? optionsForReadingBook(
                        object.book.bookId,
                        object.book.downloadUri
                      )
                    : optionsForNewBook(
                        object.book.bookId,
                        object.isFavourited
                      )}
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
