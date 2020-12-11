import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { CareSettingEnum } from '@shared/enums/care-setting.enum';

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
  @Output() public remove: EventEmitter<number>;

  public formControlNames: string[];

  public CareSettingEnum = CareSettingEnum;

  constructor() {
    this.remove = new EventEmitter<number>();
    this.formControlNames = [
      'street',
      'city',
      'provinceCode',
      'postal'
    ];
  }

  public removeOboSite(index: number) {
    this.remove.emit(index);
  }

  public ngOnInit(): void { }
}
