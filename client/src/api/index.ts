import axios from "axios";
import { type UserInfo } from "../features/auth/auth";
import { type AuthResponse } from "./models/auth";

const URL = "http://localhost:8073/api/";
const API = axios.create({ baseURL: URL });

API.interceptors.request.use((req) => {
  const jwtToken = localStorage.getItem("profile");
  if (jwtToken != null) {
    req.headers.Authorization = `Bearer ${jwtToken}`;
  }

  return req;
});

export const signIn = async (formData: UserInfo) =>
  await API.post<AuthResponse>("/auth/auth", formData);
export const signUp = async (formData: UserInfo) =>
  await API.post<boolean>("/user/register", formData);
