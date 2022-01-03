import React, { useState, useEffect, useContext } from "react";

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
  const [download, setDownload] = useState(false);

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
  }, [accessToken, user]);

  return (
    <div className="books">
      <h2>Reading books</h2>
      <div className="row">
        {books.map((book) => (
          <div key={book.id} className="bookLink">
            <BookCard {...book}>
              <div className="options">
                <Button
                  style="contained"
                  color="purple"
                  size="XL"
                  onClick={(event) => {
                    event.stopPropagation();
                    event.preventDefault();
                    handleFinishReading(book.id);
                  }}
                >
                  Finish reading
                </Button>
                <a
                  href={book.downloadUri}
                  className={download ? "download-link" : ""}
                  target="_blank"
                  onClick={() => {
                    setDownload(true);
                  }}
                >
                  <img
                    className="download-button"
                    src={!download ? downloadIcon : checkIcon}
                  />
                </a>
              </div>
            </BookCard>
          </div>
        ))}
      </div>
    </div>
  );
}

export default Reading;
