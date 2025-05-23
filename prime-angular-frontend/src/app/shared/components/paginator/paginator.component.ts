import { AfterContentInit, Component, ContentChild, EventEmitter, Input, OnChanges, Output, SimpleChanges } from '@angular/core';
import { AbstractControl, UntypedFormBuilder, UntypedFormControl, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { UtilsService } from '@core/services/utils.service';

import { FormControlValidators } from '@lib/validators/form-control.validators';

@Component({
  selector: 'app-paginator',
  templateUrl: './paginator.component.html',
  styleUrls: ['./paginator.component.scss']
})
export class PaginatorComponent implements AfterContentInit, OnChanges {
  /**
   * @description
   * Hide the paginator from view.
   *
   * NOTE:
   * Does not remove it from the DOM which would prevent it
   * being found as the table datasource is initialized.
   */
  @Input() public hidePaginator: boolean;
  @Input() public page: number;
  @Output() public changed: EventEmitter<{ pageIndex: number }>;
  @ContentChild(MatPaginator, { static: true }) public paginator: MatPaginator;

  public form: UntypedFormControl;

  constructor(
    private utilService: UtilsService,
    private fb: UntypedFormBuilder
  ) { }

  public get disabled(): boolean {
    if (!this.paginator) {
      return true;
    }

    const value = +this.form.value;
    return value < 1 || value > this.paginator.getNumberOfPages();
  }

  public onChange(event): void {
    if (!this.paginator) {
      return;
    }

    event.preventDefault();
    // Zero index the form value for comparison
    const value = +this.form.value - 1;
    if (value !== this.paginator.pageIndex && value <= this.paginator.getNumberOfPages()) {
      // Update the page index and invoke next to update the
      // datasource to keep the table and paginator in sync
      this.paginator.pageIndex = value;
      this.paginator.page.next({
        pageIndex: value,
        previousPageIndex: this.paginator.pageIndex,
        pageSize: this.paginator.pageSize,
        length: this.paginator.length
      });
    }

  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes.page) {
      this.form?.patchValue(changes.page.currentValue);
    }
  }

  public ngAfterContentInit(): void {
    if (!this.paginator) {
      return;
    }

    const currentPage = this.paginator.pageIndex + 1;
    this.form = this.fb.control(currentPage, [
      Validators.required,
      FormControlValidators.numeric,
      Validators.min(1),
      this.maxPage()
    ]);

    this.paginator.page
      .subscribe((event: PageEvent) => {
        this.form.patchValue(event.pageIndex + 1);
        this.utilService.scrollTop();
      });
  }

  /**
   * @description
   * Max value form validator.
   *
   * NOTE:
   * Component specific validator to allow for use of closures
   * to check the current number of pages, which will be zero
   * during initialization.
   */
  private maxPage(): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
      if (!this.paginator.getNumberOfPages()) { return null; }
      const valid = +control.value <= this.paginator.getNumberOfPages();
      return valid ? null : { max: true };
    };
  }
}
