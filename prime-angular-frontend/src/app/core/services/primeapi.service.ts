import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class PrimeAPIService {
  constructor(private http: HttpClient) { }
  url = (location.hostname.indexOf('localhost') > -1)
    ? 'http://localhost:5000/api/v1'
    : `${location.origin}/api/v1`;

  getApplications() {
    return this.http.get(`${this.url}/application`);
  }

  createApplication(application) {
    return this.http.post(`${this.url}/application`, application);
  }

  updateApplication(application) {
    return this.http.put(
      `${this.url}/application/${application.id}`,
      application
    );
  }
}
