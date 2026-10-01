import { Component, EventEmitter, Input, OnChanges, OnInit, Output, SimpleChanges } from '@angular/core';
import { UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { LicenseConfig, StudentTypeConfig } from '@config/config.model';
import { ConfigService } from '@config/config.service';
import { FormUtilsService } from '@core/services/form-utils.service';
import { RegulatoryFormState } from '@enrolment/pages/regulatory/regulatory-form-state';
import { CollegeLicenceClassEnum } from '@shared/enums/college-licence-class.enum';

@Component({
  selector: 'app-unlicensed-student-nurse-form',
  templateUrl: './unlicensed-student-nurse-form.component.html',
  styleUrls: ['./unlicensed-student-nurse-form.component.scss'],
  standalone: false
})
export class UnlicensedStudentNurseFormComponent implements OnInit, OnChanges {
  @Input() public form: UntypedFormGroup;
  @Input() public formState: RegulatoryFormState;
  @Input() public index: number;
  @Input() public total: number;
  @Input() public validate: boolean;
  @Input() public formControlNames: string[];
  @Output() public remove: EventEmitter<number>;

  public studentTypes: StudentTypeConfig[];

  public CollegeLicenceClassEnum = CollegeLicenceClassEnum;

  public filteredLicensesClass: LicenseConfig[] = [];

  constructor(
    private formUtilsService: FormUtilsService,
    private configService: ConfigService,
  ) {
    this.studentTypes = this.configService.studentTypes;

    this.remove = new EventEmitter<number>();
    this.validate = false;
  }

  public get studentTypeCode(): UntypedFormControl {
    return this.form.get('studentTypeCode') as UntypedFormControl;
  }

  public removeUnlicensedStudent(): void {
    this.remove.emit(this.index);
  }

  public ngOnChanges(changes: SimpleChanges): void {
    if (this.form) {
      if (this.validate) {
        this.setUnlicensedStudentValidators()
      } else {
        this.removeValidations();
      }
    }
  }

  public ngOnInit(): void {
  }

  private setUnlicensedStudentValidators(): void {
    this.formUtilsService.setValidators(this.studentTypeCode, [Validators.required]);
  }

  private removeValidations(): void {
    this.form.markAsPristine();
    this.formUtilsService.setValidators(this.studentTypeCode, []);
  }
}
