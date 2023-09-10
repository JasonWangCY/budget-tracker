export interface CategoryResponse {
  categoryId: string;
  categoryName: string;
  description: string;
  isDefaultCategory: boolean;
}

export interface AddCategoryRequest {
  categoryName: string;
  description: string;
}

export interface UpdateCategoryRequest {
  categoryId: string;
  categoryName: string;
  description: string;
}

export interface DeleteCategoryRequest {
  categoryId: string;
}

export interface AddDefaultCategoryRequest {
  categoryName: string;
  description: string;
}
