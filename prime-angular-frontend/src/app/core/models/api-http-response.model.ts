export class ApiHttpResponse<T> {
  readonly status: number;
  readonly result: T;
  readonly message?: string;

  constructor(status: number, result: T, message?: string) {
    this.status = status;
    this.result = result;
    this.message = message;
  }
}
