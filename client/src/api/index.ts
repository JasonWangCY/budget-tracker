import axios from "axios";
import { type UserInfo } from "../features/auth/auth";

const URL = "http://localhost:7073/api/";
const API = axios.create({ baseURL: URL });

export const signIn = async (formData: UserInfo) =>
  await API.post("/auth/auth", formData);
export const signUp = async (formData: UserInfo) =>
  await API.post("/user/register", formData);
