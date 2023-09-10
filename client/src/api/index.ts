import axios from "axios";
import { type UserInfo } from "../features/auth/auth";
import { type StatusResponse, type AuthResponse } from "./models/auth";
import {
  type AddCategoryRequest,
  type AddDefaultCategoryRequest,
  type CategoryResponse,
  type DeleteCategoryRequest,
  type UpdateCategoryRequest,
} from "./models/category";
import {
  type AddDefaultTransactionTypeRequest,
  type AddTransactionRequest,
  type AddTransactionTypeRequest,
  type DeleteDefaultTransactionTypeRequest,
  type DeleteTransactionRequest,
  type DeleteTransactionTypeRequest,
  type TransactionResponse,
  type TransactionTypeResponse,
  type UpdateTransactionRequest,
  type UpdateTransactionTypeRequest,
} from "./models/transaction";

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

export const getCategories = async () =>
  await API.get<CategoryResponse>("/Category");
export const addCategories = async (request: AddCategoryRequest) =>
  await API.post<boolean>("/Category", request);
export const updateCategories = async (request: UpdateCategoryRequest) =>
  await API.put<boolean>("/Category", request);
export const deleteCategories = async (request: DeleteCategoryRequest) =>
  await API.delete<boolean>("/Category", { data: request });
export const addDefaultCategories = async (
  request: AddDefaultCategoryRequest,
) => await API.post<boolean>("/Category/default", request);
export const deleteDefaultCategories = async (request: DeleteCategoryRequest) =>
  await API.delete<boolean>("/Category/default", { data: request });

export const getTransactionTypes = async () =>
  await API.get<TransactionTypeResponse>("/Transaction/transactionTypes");
export const addTransactionTypes = async (request: AddTransactionTypeRequest) =>
  await API.post<boolean>("/Transaction/transactionTypes", request);
export const updateTransactionTypes = async (
  request: UpdateTransactionTypeRequest,
) => await API.put<boolean>("/Transaction/transactionTypes", request);
export const deleteTransactionTypes = async (
  request: DeleteTransactionTypeRequest,
) =>
  await API.delete<boolean>("/Transaction/transactionTypes", { data: request });
export const getTransactions = async (startDate: string, endDate: string) =>
  await API.get<TransactionResponse>("/Transaction", {
    params: {
      startDate,
      endDate,
    },
  });
export const addTransactions = async (request: AddTransactionRequest) =>
  await API.post<boolean>("/Transaction", request);
export const updateTransactions = async (request: UpdateTransactionRequest) =>
  await API.put<boolean>("/Transaction", request);
export const deleteTransactions = async (request: DeleteTransactionRequest) =>
  await API.delete<boolean>("/Transaction", { data: request });
export const addDefaultTransactionTypes = async (
  request: AddDefaultTransactionTypeRequest,
) => await API.post<boolean>("/Transaction/defaultTransactionTypes", request);
export const deleteDefaultTransactionTypes = async (
  request: DeleteDefaultTransactionTypeRequest,
) =>
  await API.delete<boolean>("/Transaction/defaultTransactionTypes", {
    data: request,
  });

export const checkAdmin = async () =>
  await API.get<boolean>("/User/checkAdmin");
export const registerUser = async (request: UserInfo) =>
  await API.post<StatusResponse>("/User/register", request);
export const registerAdmin = async (request: UserInfo) =>
  await API.post<StatusResponse>("/User/registerAdmin", request);
