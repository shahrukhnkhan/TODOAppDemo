// API Response custom object
export interface ApiResponse<T> {
  data: T;
  status: number;
}