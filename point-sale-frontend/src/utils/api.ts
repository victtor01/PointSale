import axios from "axios";

const _BASE_URL = process.env.NEXT_PUBLIC_API_URL;

const api = axios.create({
  baseURL: _BASE_URL,
  withCredentials: true,
});

export { api };

