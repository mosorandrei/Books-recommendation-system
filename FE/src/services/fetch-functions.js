import { API_HOST } from "../utils/constants";

function validateResponse(response) {
  if (response.status === 403) {
    throw `User is blocked`;
  }
  if (!response.ok) {
    throw new Error(`[${response.status}] There was an error fetching data.`);
  }
  return response.json();
}

function requestOptions(method, body) {
  return {
    method,
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(body),
  };
}

function requestOptionsToken(method, body, accessToken) {
  return {
    method,
    headers: {
      "Content-Type": "application/json",
      Authorization: `Bearer ${accessToken}`,
    },
    body: JSON.stringify(body),
  };
}

export function signInUser(signInInfo) {
  return fetch(
    `${API_HOST}/Authenticate`,
    requestOptions("POST", { ...signInInfo })
  ).then((r) => validateResponse(r));
}

export function signUpUser(signUpInfo) {
  return fetch(
    `${API_HOST}/RegisterMember`,
    requestOptions("POST", { ...signUpInfo })
  ).then((r) => validateResponse(r));
}

export function getUser(accessToken) {
  return fetch(
    `${API_HOST}/GetUser`,
    requestOptionsToken("GET", undefined, accessToken)
  ).then((r) => validateResponse(r));
}

export function getNewAccessToken(accessToken) {
  return fetch(
    `${API_HOST}/RefreshToken`,
    requestOptionsToken("GET", undefined, accessToken)
  ).then((r) => validateResponse(r));
}

export function getAllMembers(accessToken) {
  return fetch(
    `${API_HOST}/GetAllMembers`,
    requestOptionsToken("GET", undefined, accessToken)
  ).then((r) => validateResponse(r));
}

export function getAllBooks(accessToken) {
  return fetch(
    `${API_HOST}/Books`,
    requestOptionsToken("GET", undefined, accessToken)
  ).then((r) => validateResponse(r));
}

export function getAllBooksForUser(accessToken) {
  return fetch(
    `${API_HOST}/GetMyStatuses`,
    requestOptionsToken("GET", undefined, accessToken)
  ).then((r) => validateResponse(r));
}

export function getAllBooksRecommendationForUser(accessToken) {
  return fetch(
    `${API_HOST}/GetRecommendations`,
    requestOptionsToken("GET", undefined, accessToken)
  ).then((r) => validateResponse(r));
}

export function deleteBook(bookId, accessToken) {
  return fetch(
    `${API_HOST}/Books?id=${bookId}`,
    requestOptionsToken("DELETE", undefined, accessToken)
  ).then((r) => validateResponse(r));
}

export function addBook(bookFields, accessToken) {
  return fetch(
    `${API_HOST}/Books`,
    requestOptionsToken(
      "POST",
      {
        title: bookFields.title,
        rating: bookFields.rating,
        downloadUri: bookFields.downloadLink,
        description: bookFields.description,
        imageUri: bookFields.image,
      },
      accessToken
    )
  ).then((r) => validateResponse(r));
}

export function editBook(bookFields, bookId, accessToken) {
  console.log(bookFields);
  return fetch(
    `${API_HOST}/Books?id=${bookId}`,
    requestOptionsToken(
      "PUT",
      {
        id: bookId,
        title: bookFields.title,
        imageUri: bookFields.image,
        downloadUri: bookFields.downloadLink,
        description: bookFields.description,
        rating: bookFields.rating,
      },
      accessToken
    )
  ).then((r) => validateResponse(r));
}

export function getBook(bookId, accessToken) {
  return fetch(
    `${API_HOST}/GetBookById?BookId=${bookId}`,
    requestOptionsToken("GET", undefined, accessToken)
  ).then((r) => validateResponse(r));
}

export function addToReading(bookId, accessToken) {
  return fetch(
    `${API_HOST}/StartReading?BookId=${bookId}`,
    requestOptionsToken("PUT", undefined, accessToken)
  ).then((r) => validateResponse(r));
}

export function addToFavourites(bookId, accessToken) {
  return fetch(
    `${API_HOST}/AddToMyFavourites?BookId=${bookId}`,
    requestOptionsToken("POST", undefined, accessToken)
  ).then((r) => validateResponse(r));
}

export function getReadingBooks(accessToken) {
  return fetch(
    `${API_HOST}/GetMyReadings`,
    requestOptionsToken("GET", undefined, accessToken)
  ).then((r) => validateResponse(r));
}

export function finishReading(bookId, accessToken) {
  return fetch(
    `${API_HOST}/FinishReading?BookId=${bookId}`,
    requestOptionsToken("PUT", undefined, accessToken)
  ).then((r) => validateResponse(r));
}

export function getReadBooks(accessToken) {
  return fetch(
    `${API_HOST}/GetMyReads`,
    requestOptionsToken("GET", undefined, accessToken)
  ).then((r) => validateResponse(r));
}

export function getFavourites(accessToken) {
  return fetch(
    `${API_HOST}/GetMyFavourites`,
    requestOptionsToken("GET", undefined, accessToken)
  ).then((r) => validateResponse(r));
}

export function getAllGenres(accessToken) {
  return fetch(
    `${API_HOST}/Genres`,
    requestOptionsToken("GET", undefined, accessToken)
  ).then((r) => validateResponse(r));
}

export function getBooksByGenre(genreId, accessToken) {
  return fetch(
    `${API_HOST}/GetBooksByGenreId?GenreId=${genreId}`,
    requestOptionsToken("GET", undefined, accessToken)
  ).then((r) => validateResponse(r));
}

export function rateBook(rating, bookId, accessToken) {
  return fetch(
    `${API_HOST}/RateBook?BookId=${bookId}&Score=${rating}`,
    requestOptionsToken("PUT", undefined, accessToken)
  ).then((r) => validateResponse(r));
}

export function removeFromFavourites(bookId, accessToken) {
  return fetch(
    `${API_HOST}/DeleteFromMyFavourites?bookId=${bookId}`,
    requestOptionsToken("DELETE", undefined, accessToken)
  ).then((r) => validateResponse(r));
}

export function blockUser(userId, accessToken) {
  return fetch(
    `${API_HOST}/BlockUser?UserId=${userId}`,
    requestOptionsToken("PUT", undefined, accessToken)
  ).then((r) => validateResponse(r));
}

export function unblockUser(userId, accessToken) {
  return fetch(
    `${API_HOST}/UnblockUser?UserId=${userId}`,
    requestOptionsToken("PUT", undefined, accessToken)
  ).then((r) => validateResponse(r));
}

export function addAuthor(firstName, lastName, accessToken) {
  return fetch(
    `${API_HOST}/Authors`,
    requestOptionsToken(
      "POST",
      { firstName: firstName, lastName: lastName },
      accessToken
    )
  ).then((r) => validateResponse(r));
}

export function addAuthorToBook(bookId, authorId, accessToken) {
  return fetch(
    `${API_HOST}/AddAuthorToBook?BookId=${bookId}&AuthorId=${authorId}`,
    requestOptionsToken("POST", undefined, accessToken)
  ).then((r) => validateResponse(r));
}

export function sendEmail(email) {
  return fetch(
    `${API_HOST}/SendResetEmail?Email=${email}`,
    requestOptions("POST", undefined)
  ).then((r) => validateResponse(r));
}

export function resetPassword(email, code, password, confirm) {
  return fetch(
    `${API_HOST}/ResetPassword?Email=${email}&Token=${code}&Password=${password}&ConfirmPassword=${confirm}`,
    requestOptions("POST", undefined)
  ).then((r) => validateResponse(r));
}
