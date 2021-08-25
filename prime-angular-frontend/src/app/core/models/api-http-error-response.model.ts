import { HttpHeaders } from '@angular/common/http';

export class ApiHttpErrorResponse {
  readonly status: number;
  readonly errors: any;
  readonly headers: HttpHeaders;
  readonly message?: string;

  constructor(status: number, errors: any, headers: HttpHeaders, message?: string) {
    this.status = status;
    this.errors = errors;
    this.headers = headers;
    this.message = message;
  }
}
