import React, { useContext } from "react";
import PropTypes from "prop-types";

import TextInput from "../text-input/text-input";
import Button from "../button/button";
import useForm from "../../hooks/use-form";
import "./book-form.scss";
import {
  addBook,
  editBook,
  addAuthor,
  addAuthorToBook,
} from "../../services/fetch-functions";
import { AuthContext } from "../../hooks/auth-context";

function BookForm({
  bookId,
  title,
  authors,
  rating,
  description,
  downloadUri,
  imageUri,
  isNewBook = false,
  onFormClose,
}) {
  const { state, updateUserInformation } = useContext(AuthContext);
  const { fields, errors, setErrors, handleInputChange, handleSubmit } =
    useForm(
      {
        title: title || "",
        authors: authors || "",
        rating: rating ? rating.toString() : "",
        description: description || "",
        image: imageUri || "",
        downloadLink: downloadUri || "",
      },
      {
        title: { required: true },
        authors: { required: true },
        description: { required: true },
        downloadLink: { required: true },
        rating: {
          pattern: {
            value: /^\d+$/,
            message: "Rating can only contain digits",
          },
        },
      },
      (fields) => {
        var newBookId = "";
        var newAuthorId = "";
        let newPromise = Promise;
        let addBookPromise = Promise;
        if (isNewBook) {
          addBookPromise = addBook(fields, state.accessToken);
          addBookPromise.then((result) => {
            newBookId = result;
          });
          let author = fields.authors.split(" ");
          newPromise = addAuthor(author[0], author[1], state.accessToken).then(
            (result) => {
              newAuthorId = result;
              addAuthorToBook(newBookId, newAuthorId, state.accessToken);
            }
          );
        } else {
          newPromise = editBook(fields, bookId, state.accessToken);
        }

        newPromise
          .then(() => {
            onFormClose();
            updateUserInformation(state.accessToken);
          })
          .catch(() => {
            setErrors((prevErrors) => ({
              ...prevErrors,
              form: "There was an error submitting the form!",
            }));
          });
      }
    );

  return (
    <form className="book-form" onSubmit={handleSubmit}>
      <TextInput
        inputName="title"
        inputLabel="Title"
        onChange={handleInputChange}
        value={fields.title}
        error={errors.title}
      />
      <div className="form-input-split">
        <TextInput
          inputName="authors"
          inputLabel="Author"
          onChange={handleInputChange}
          value={fields.authors}
          error={errors.authors}
        />
        <TextInput
          inputName="rating"
          inputLabel="Rating"
          onChange={handleInputChange}
          value={fields.rating}
          error={errors.rating}
        />
      </div>
      <TextInput
        inputName="description"
        inputLabel="Description"
        onChange={handleInputChange}
        value={fields.description}
        error={errors.description}
      />
      <div className="form-input-split">
        <TextInput
          inputName="image"
          inputLabel="Image"
          onChange={handleInputChange}
          value={fields.image}
          error={errors.image}
        />
        <TextInput
          inputName="downloadLink"
          inputLabel="Download link"
          onChange={handleInputChange}
          value={fields.downloadLink}
          error={errors.downloadLink}
        />
      </div>

      <div className="form-button-split right">
        {errors.form && <p className="error-message">{errors.form}</p>}
        <Button
          type="button"
          style="outlined"
          color="purple"
          size="M"
          onClick={onFormClose}
        >
          Cancel
        </Button>
        <Button type="submit" style="contained" color="purple" size="M">
          {isNewBook ? "Add" : "Save"}
        </Button>
      </div>
    </form>
  );
}

BookForm.propTypes = {};

export default BookForm;
