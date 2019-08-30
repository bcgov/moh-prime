import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { Router } from '@angular/router';
import { PrimeAPIService } from 'src/app/core/services/primeapi.service';
import { AuthTokenService } from 'src/app/core/services/auth-token.service';

@Component({
  selector: 'app-enrollment',
  templateUrl: './enrollment.component.html',
  styleUrls: ['./enrollment.component.scss']
})
export class EnrollmentComponent implements OnInit {
  @ViewChild('prn', { static: true }) prnInput: ElementRef<HTMLInputElement>;
  @ViewChild('pharmacist', { static: true }) pharmacistInput: ElementRef<HTMLInputElement>;
  public pharmacistPrn: string;

  constructor(
    private router: Router,
    private primeAPIService: PrimeAPIService,
    private authTokenService: AuthTokenService
  ) { }

  updateInput(value: string) {
    this.pharmacistPrn = (value.length < 5) ? value : this.pharmacistPrn;

    if (this.pharmacistPrn.length < 5) {
      this.prnInput.nativeElement.value = this.pharmacistPrn;
    }
  }

  submitApplication() {
    const decodedToken = this.authTokenService.decodeToken();

    this.primeAPIService
      .createApplication({
        Content: '',
        ApplicantName: decodedToken.name,
        ApplicantId: decodedToken.at_hash,
        PharmacistRegistrationNumber: this.pharmacistPrn
      })
      .subscribe(res => {
        if (this.pharmacistPrn) {
          this.router.navigate(['dashboard', 'applicant', 'complete']);
        } else {
          this.router.navigate(['dashboard', 'applicant', 'inprogress']);
        }
      });
  }

  ngOnInit() {
    const user = this.authTokenService.decodeToken() as any;
    this.pharmacistInput.nativeElement.value = `${user.given_name} ${user.family_name}`;
  }
}
