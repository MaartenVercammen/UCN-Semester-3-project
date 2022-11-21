import axios from 'axios';

export const instance = axios.create({
  baseURL: "https://localhost:7088",
  headers: { "Content-Type": "application/problem+json" },
});
