import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { PrimeAPIService } from "src/app//core/services/primeapi.service";

@Component({
  selector: "app-enrollment",
  templateUrl: "./enrollment.component.html",
  styleUrls: ["./enrollment.component.scss"]
})
export class EnrollmentComponent implements OnInit {
  constructor(
    private router: Router,
    private primeAPIService: PrimeAPIService
  ) {}
  pharmacist_prn: string;

  submitApplication() {
    this.primeAPIService
      .createApplication({
        Content: "",
        ApplicantName: "coolest name",
        ApplicantId: "token",
        PharmacistRegistrationNumber: this.pharmacist_prn
      })
      .subscribe(res => {
        if (this.pharmacist_prn) {
          this.router.navigate(["dashboard", "applicant", "complete"]);
        } else {
          this.router.navigate(["dashboard", "applicant", "inprogress"]);
        }
      });
  }

  ngOnInit() {}
}
