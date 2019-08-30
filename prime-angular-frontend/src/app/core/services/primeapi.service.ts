import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";

@Injectable({
  providedIn: "root"
})
export class PrimeAPIService {
  constructor(private http: HttpClient) {}  
  url = "http://api.optimizeprime.live/api/v1";
  if (window.location.hostname.indexOf('localhost') > 0) {
    url = "http://localhost:5000/api/v1";      
  }
  getApplications() {
    this.createApplication();
    return this.http.get(`${this.url}/application`);
  }

  createApplication() {
    console.log("POST!");
    this.http
      .post(`${this.url}/application`, {
        Content: "test_content",
        ApplicantName: "test_name",
        ApplicantId: "token",
        pharmacistRegistrationNumber: "9999"
      })
      .subscribe(res => console.log(JSON.stringify(res)));
  }
}
