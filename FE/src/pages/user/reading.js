import React, { useState, useEffect, useContext } from "react";
import { Link } from "react-router-dom";

import BookCard from "../../components/book-card/BookCard";
import "../pages.scss";
import { getReadingBooks, finishReading } from "../../services/fetch-functions";
import { AuthContext } from "../../hooks/auth-context";
import Button from "../../components/button/button";
import downloadIcon from "../../assets/download-button.svg";
import checkIcon from "../../assets/check-download.svg";

function Reading() {
  const {
    state: { accessToken, user },
    updateUserInformation,
  } = useContext(AuthContext);
  const [books, setBooks] = useState([]);
  const [download, setDownload] = useState([]);

  function handleFinishReading(bookId) {
    finishReading(bookId, accessToken)
      .then(() => {
        updateUserInformation(accessToken);
      })
      .catch((error) => {
        console.log(error);
      });
  }

  useEffect(() => {
    getReadingBooks(accessToken)
      .then((result) => {
        setBooks(result);
      })
      .catch((error) => {
        console.log(error);
      });
    let tempDownloadStatus = [];
    books.map((book) => {
      tempDownloadStatus.push({ id: book.bookId, download: false });
    });
    setDownload(tempDownloadStatus);
  }, [accessToken, user]);

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

  return (
    <div className="books">
      <h2>Reading books</h2>
      <div className="row">
        {books.map((book) => (
          <div key={book.bookId} className="bookLink">
            <Link to={`/user/book/${book.bookId}`}>
              <BookCard {...book}>
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
                      handleFinishReading(book.bookId);
                    }}
                  >
                    Finish reading
                  </Button>
                  <a
                    href={book.downloadUri}
                    className={
                      getDownloadStatusForBook(book.bookId)
                        ? "download-link"
                        : ""
                    }
                    target="_blank"
                    onClick={() => {
                      setDownloadStatusForBook(book.bookId);
                    }}
                  >
                    <img
                      className="download-button"
                      src={
                        !getDownloadStatusForBook(book.bookId)
                          ? downloadIcon
                          : checkIcon
                      }
                    />
                  </a>
                </div>
              </BookCard>
            </Link>
          </div>
        ))}
      </div>
    </div>
  );
}

export default Reading;
