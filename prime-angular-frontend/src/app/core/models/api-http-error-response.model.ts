import { HttpHeaders } from '@angular/common/http';

export class ApiHttpErrorResponse {
  readonly status: number;
  readonly errors: any | null;
  readonly headers: HttpHeaders;
  readonly message?: string;

  constructor(status: number, errors: any | null, headers: HttpHeaders, message?: string) {
    this.status = status;
    this.errors = errors;
    this.headers = headers;
    this.message = message;
  }
}
