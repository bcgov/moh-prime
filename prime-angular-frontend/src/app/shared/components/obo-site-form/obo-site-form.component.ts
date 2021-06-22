import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { CareSettingEnum } from '@shared/enums/care-setting.enum';
import { Config } from '@config/config.model';

@Component({
  selector: 'app-obo-site-form',
  templateUrl: './obo-site-form.component.html',
  styleUrls: ['./obo-site-form.component.scss']
})
export class OboSiteFormComponent implements OnInit {
  @Input() public form: FormGroup;
  @Input() public site: FormGroup;
  @Input() public index: number;
  @Input() public total: number;
  @Input() public last: boolean;
  @Input() public careSettingCode: number;
  @Input() public healthAuthorityName?: string;
  @Input() public jobNames: Config<number>[];
  @Input() public allowDefaultOption: boolean;
  @Input() public defaultOptionLabel: string;
  @Output() public remove: EventEmitter<number>;

  public formControlNames: string[];
  public allowRemoveNone: boolean;
  public CareSettingEnum = CareSettingEnum;

  constructor() {
    this.remove = new EventEmitter<number>();
    this.formControlNames = [
      'street',
      'city',
      'provinceCode',
      'postal'
    ];
    this.allowRemoveNone = true;
  }

  public removeOboSite(index: number) {
    this.remove.emit(index);
  }

  public ngOnInit(): void { }

  public removeNone(input: HTMLFormElement) {
    if (this.allowRemoveNone && input.value === this.defaultOptionLabel) {
      input.value = '';
      this.allowRemoveNone = false;
    }
  }
}
