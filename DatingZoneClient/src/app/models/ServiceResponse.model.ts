export interface ServiceResponse<T> {
  Data?: T;
  Message: string;
  Status: number;
  Success: boolean;
}
