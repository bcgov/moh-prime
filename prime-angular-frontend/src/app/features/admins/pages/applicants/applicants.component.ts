import { Component, OnInit } from "@angular/core";
import { MatDialog, MAT_DIALOG_DATA } from "@angular/material";
import { PrimeAPIService } from "src/app//core/services/primeapi.service";

@Component({
  selector: "app-applicants",
  templateUrl: "./applicants.component.html",
  styleUrls: ["./applicants.component.scss"]
})
export class ApplicantsComponent implements OnInit {
  applications;

  constructor(
    public dialog: MatDialog,
    private primeAPIService: PrimeAPIService
  ) {}

  authenticate() {}

  ngOnInit() {
    this.primeAPIService.getApplications().subscribe(data => {
      this.applications = data;
    });
  }
}
