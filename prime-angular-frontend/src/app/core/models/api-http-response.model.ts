import { HttpHeaders } from '@angular/common/http';

export class ApiHttpResponse<T> {
  readonly status: number;
  readonly headers: HttpHeaders;
  readonly result: T;
  readonly message?: string;

  constructor(status: number, headers: HttpHeaders, result: T, message?: string) {
    this.status = status;
    this.headers = headers;
    this.result = result;
    this.message = message;
  }
}
