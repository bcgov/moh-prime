import { HttpHeaders } from '@angular/common/http';

export class ApiHttpResponse<T> {
  readonly status: number;
  readonly headers: HttpHeaders;
  readonly result: T;
  readonly message?: string;

  constructor(status: number, result: T, message?: string) {
    this.status = status;
    this.result = result;
    this.message = message;
  }
}
