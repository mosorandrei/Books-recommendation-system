import React, { useState, useEffect, useContext, useRef } from "react";

import BookForm from "../../components/book-form/book-form";
import BookCard from "../../components/book-card/BookCard";
import "../pages.scss";
import { getAllBooks, deleteBook } from "../../services/fetch-functions";
import { AuthContext } from "../../hooks/auth-context";
import Button from "../../components/button/button";
import Modal from "../../components/modal/modal";

function Library() {
  const {
    state: { accessToken, user },
    updateUserInformation,
  } = useContext(AuthContext);
  const addBookModalRef = useRef();
  const editBookModalRef = useRef();
  const [books, setBooks] = useState([]);
  const [editBook, setEditBook] = useState({});

  useEffect(() => {
    getAllBooks(accessToken)
      .then((result) => {
        setBooks(result);
      })
      .catch((error) => {
        console.log(error);
      });
  }, [accessToken, user]);

  const handleDeleteBook = (bookId) => {
    deleteBook(bookId, accessToken)
      .then(() => {
        updateUserInformation(accessToken);
      })
      .catch((error) => {
        console.log(error);
      });
  };

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

  function openAddBookModal() {
    addBookModalRef.current.openModal();
  }

  function openEditBookModal(bookId) {
    var tempBook = books.find((book) => book.bookId === bookId);
    var authors = displayAuthors(tempBook.authors);
    var bookWithAuthors = { ...tempBook, authors: authors };
    setEditBook(bookWithAuthors);
    editBookModalRef.current.openModal();
  }

  return (
    <>
      <div className="books">
        <div className="top">
          <h2>All books</h2>
          <Button
            style="contained"
            color="purple"
            size="XL"
            onClick={openAddBookModal}
          >
            Add new
          </Button>
        </div>
        <div className="row">
          {books.map((book) => (
            <div key={book.bookId}>
              <BookCard {...book}>
                <div className="options">
                  <Button
                    style="outlined"
                    color="purple"
                    size="M"
                    onClick={() => openEditBookModal(book.bookId)}
                  >
                    Edit
                  </Button>
                  <Button
                    style="contained"
                    color="purple"
                    size="M"
                    onClick={() => handleDeleteBook(book.bookId)}
                  >
                    Delete
                  </Button>
                </div>
              </BookCard>
            </div>
          ))}
        </div>
      </div>

      <Modal title="Add book" ref={addBookModalRef}>
        <BookForm
          isNewBook
          onFormClose={() => addBookModalRef.current.closeModal()}
        />
      </Modal>

      <Modal title="Edit book" ref={editBookModalRef}>
        <BookForm
          {...editBook}
          onFormClose={() => editBookModalRef.current.closeModal()}
        />
      </Modal>
    </>
  );
}

export default Library;
