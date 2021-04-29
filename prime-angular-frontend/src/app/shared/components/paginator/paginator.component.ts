import { AfterContentInit, Component, ContentChild, Input } from '@angular/core';
import { FormBuilder, FormControl, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';

@Component({
  selector: 'app-paginator',
  templateUrl: './paginator.component.html',
  styleUrls: ['./paginator.component.scss']
})
export class PaginatorComponent implements AfterContentInit {
  @Input() public hidePaginator: boolean;
  @ContentChild(MatPaginator, { static: true }) public paginator: MatPaginator;
  public form: FormControl;

  constructor(
    private fb: FormBuilder
  ) { }

  public onChange(): void {
    const value = +this.form.value;
    if (value && value <= this.paginator.getNumberOfPages()) {
      this.paginator.pageIndex = +this.form.value - 1;
    }
  }

  public ngAfterContentInit(): void {
    const currentPage = this.paginator.pageIndex + 1;
    this.form = this.fb.control(currentPage, [
      Validators.required,
      Validators.min(1),
      Validators.max(this.paginator.getNumberOfPages())
    ]);
  }
}
