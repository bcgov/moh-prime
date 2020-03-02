export class ApiHttpResponse<T> {
  readonly result: T;
  readonly statusCode: number;

  constructor(result: T, statusCode: number) {
    this.result = result;
    this.statusCode = statusCode;
  }
}
