import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";

@Injectable({
  providedIn: "root"
})
export class PrimeAPIService {
  constructor(private http: HttpClient) {}
  url = "http://localhost:5000/api";
  getValues() {
    return this.http.get(`${this.url}/values`);
  }
}
