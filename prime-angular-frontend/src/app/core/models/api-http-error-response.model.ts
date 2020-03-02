export class ApiHttpErrorResponse {
  readonly error: any;
  readonly statusCode: number;

  constructor(error: any, statusCode: number) {
    this.error = error;
    this.statusCode = statusCode;
  }
}
