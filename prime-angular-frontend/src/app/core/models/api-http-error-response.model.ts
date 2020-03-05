export class ApiHttpErrorResponse {
  readonly status: number;
  readonly errors: any;
  readonly message?: string;

  constructor(status: number, errors: any, message?: string) {
    this.status = status;
    this.errors = errors;
    this.message = message;
  }
}
