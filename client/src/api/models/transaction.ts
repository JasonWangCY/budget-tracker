export interface TransactionTypeResponse {
  transactionTypeId: string;
  transactionTypeName: string;
  description: string;
  isDefaultType: boolean;
}

export interface AddTransactionTypeRequest {
  transactionTypeName: string;
  description: string;
}

export interface UpdateTransactionTypeRequest {
  transactionTypeId: string;
  transactionTypeName: string;
  description: string;
}

export interface DeleteTransactionTypeRequest {
  transactionTypeId: string;
}

export interface TransactionResponse {
  transactionId: string;
  transactionDate: Date;
  transactionAmount: number;
  description: string;
  currency: string;
  transactionTypeId: string;
  transactionTypeName: string;
  categoryId: string;
  categoryName: string;
}

export interface AddTransactionRequest {
  transactionDate: Date;
  transactionAmount: number;
  currency: string;
  description: string;
  transactionTypeId: string;
  categoryId: string;
}

export interface UpdateTransactionRequest {
  transactionId: string;
  transactionDate: Date;
  transactionAmount: number;
  currency: string;
  description: string;
  transactionTypeId: string;
  categoryId: string;
}

export interface DeleteTransactionRequest {
  transactionId: string;
}

export interface AddDefaultTransactionTypeRequest {
  transactionTypeName: string;
  description: string;
}

export interface DeleteDefaultTransactionTypeRequest {
  transactionTypeId: string;
}
