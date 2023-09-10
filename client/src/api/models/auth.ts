export interface AuthResponse {
  token: string;
  expiration: Date;
}

export interface StatusResponse {
  status: string;
  message: string;
}
