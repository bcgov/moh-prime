import { Component, OnInit } from "@angular/core";
import { PrimeAPIService } from "./primeapi.service";

@Component({
  selector: "app-root",
  templateUrl: "./app.component.html",
  styleUrls: ["./app.component.scss"]
})
export class AppComponent implements OnInit {
  title = "angular-frontend";
  values;

  constructor(private primeAPIService: PrimeAPIService) {}
  ngOnInit() {
    this.primeAPIService.getValues().subscribe(data => {
      console.log(data);
      this.values = data;
    });
  }
}
