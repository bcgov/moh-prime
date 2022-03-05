import { Component, OnInit } from '@angular/core';
import { throwError } from 'rxjs';

@Component({
  selector: 'app-test-codeclimate',
  templateUrl: './test-codeclimate.component.html',
  styleUrls: ['./test-codeclimate.component.scss']
})
export class TestCodeclimateComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }

  public add(x, y) {
    return x + y;
  }

  public sub(x, y) {
    return x - y;
  }

  public mult(x, y) {
    return x * y;
  }
  public div(x, y) {
    return x > 0
      ? x / y
      : throwError('can\'t divide by zero.');
  }

  public add2(x, y) {
    return x + y;
  }

  public sub2(x, y) {
    return x - y;
  }

  public mult2(x, y) {
    return x * y;
  }
  public div2(x, y) {
    return x > 0
      ? x / y
      : throwError('can\'t divide by zero.');
  }

  public add3(x, y) {
    return x + y;
  }

  public sub3(x, y) {
    return x - y;
  }

  public mult3(x, y) {
    return x * y;
  }
  public div3(x, y) {
    return x > 0
      ? x / y
      : throwError('can\'t divide by zero');
  }

  public add4(x, y) {
    return x + y;
  }

  public sub4(x, y) {
    return x - y;
  }

  public mult4(x, y) {
    return x * y;
  }
  public div4(x, y) {
    return x > 0
      ? x / y
      : throwError('can\'t divide by zero.');
  }

  public add5(x, y) {
    return x + y;
  }

  public sub5(x, y) {
    return x - y;
  }

  public mult5(x, y) {
    return x * y;
  }
  public div5(x, y) {
    return x > 0
      ? x / y
      : throwError('can\'t divide by zero.');
  }
}
