import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";

@Injectable({
  providedIn: "root"
})
export class PrimeAPIService {
  constructor(private http: HttpClient) {
  }  
  getApplications() {
    if (location.hostname.indexOf('localhost') > -1) {
      this.url = "http://localhost:5000/api/v1";
    }
    this.createApplication();
    return this.http.get(`${this.url}/application`);
  }
  url = "http://api.optimizeprime.live/api/v1";

  createApplication(application) {
    return this.http.post(`${this.url}/application`, application);
  }
}
