import { API_HOST, API_HOST_WITHOUT_TOKEN } from "../utils/constants";

function validateResponse(response) {
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
  console.log(signInInfo);
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
  return fetch(`${API_HOST}/GetAllMembers`, requestOptions("GET")).then((r) =>
    validateResponse(r)
  );
}

export function getAllBooks(accessToken) {
  return fetch(
    `${API_HOST_WITHOUT_TOKEN}/Books`,
    requestOptionsToken("GET", undefined, accessToken)
  ).then((r) => validateResponse(r));
}

export function deleteBook(bookId, accessToken) {
  return fetch(
    `${API_HOST_WITHOUT_TOKEN}/Books?id=${bookId}`,
    requestOptionsToken("DELETE", undefined, accessToken)
  ).then((r) => validateResponse(r));
}

export function addBook(bookFields, accessToken) {
  return fetch(
    `${API_HOST_WITHOUT_TOKEN}/Books`,
    requestOptionsToken(
      "POST",
      {
        title: bookFields.title,
        rating: bookFields.rating,
        downloadUri: bookFields.downloadLink,
        description: bookFields.description,
      },
      accessToken
    )
  ).then((r) => validateResponse(r));
}

export function editBook(bookFields, bookId, accessToken) {
  return fetch(
    `${API_HOST_WITHOUT_TOKEN}/Books?id=${bookId}`,
    requestOptionsToken(
      "PUT",
      { id: bookId, title: bookFields.title },
      accessToken
    )
  ).then((r) => validateResponse(r));
}
