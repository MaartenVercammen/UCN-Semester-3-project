import axios from 'axios';

export const instanceNoJWT = axios.create({
  baseURL: "https://localhost:7088",
  headers: { "Content-Type": "application/problem+json" },
});

export const instance = axios.create({
  baseURL: "https://localhost:7088",
  headers: { "Content-Type": "application/problem+json", Authorization: "Bearer " + sessionStorage.getItem('token') },
});
