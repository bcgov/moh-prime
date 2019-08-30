import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { PrimeAPIService } from 'src/app/core/services/primeapi.service';
import { AuthTokenService } from 'src/app/core/services/auth-token.service';

@Component({
  selector: 'app-enrollment',
  templateUrl: './enrollment.component.html',
  styleUrls: ['./enrollment.component.scss']
})
export class EnrollmentComponent implements OnInit {
  public pharmacist_prn: string;

  constructor(
    private router: Router,
    private primeAPIService: PrimeAPIService,
    private authTokenService: AuthTokenService
  ) { }

  submitApplication() {
    const decodedToken = this.authTokenService.decodeToken();

    this.primeAPIService
      .createApplication({
        Content: '',
        ApplicantName: decodedToken.name,
        ApplicantId: decodedToken.at_hash,
        PharmacistRegistrationNumber: this.pharmacist_prn
      })
      .subscribe(res => {
        if (this.pharmacist_prn) {
          this.router.navigate(['dashboard', 'applicant', 'complete']);
        } else {
          this.router.navigate(['dashboard', 'applicant', 'inprogress']);
        }
      });
  }

  ngOnInit() { }
}
