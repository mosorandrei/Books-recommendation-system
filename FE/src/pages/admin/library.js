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
        console.log(result);
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

  function openAddBookModal() {
    addBookModalRef.current.openModal();
  }

  function openEditBookModal(bookId) {
    setEditBook(books.find((book) => book.id === bookId));
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
            <div key={book.id}>
              <BookCard {...book}>
                <div className="options">
                  <Button
                    style="outlined"
                    color="purple"
                    size="M"
                    onClick={() => openEditBookModal(book.id)}
                  >
                    Edit
                  </Button>
                  <Button
                    style="contained"
                    color="purple"
                    size="M"
                    onClick={() => handleDeleteBook(book.id)}
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
