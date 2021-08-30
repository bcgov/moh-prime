export class ApiHttpErrorResponse {
  readonly status: number;
  readonly errors: any | null;
  readonly message?: string;

  constructor(status: number, errors: any | null, message?: string) {
    this.status = status;
    this.errors = errors;
    this.message = message;
  }
}
