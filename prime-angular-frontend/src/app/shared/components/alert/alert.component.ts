import {
  Component,
  OnInit,
  Input,
  ContentChildren,
  QueryList,
  AfterContentInit,
  TemplateRef
} from '@angular/core';

@Component({
  selector: 'app-alert',
  templateUrl: './alert.component.html',
  styleUrls: ['./alert.component.scss']
})
export class AlertComponent implements OnInit, AfterContentInit {
  @Input() type: string;

  @ContentChildren('alertIcon', { descendants: true })
  public alertIconChildren: QueryList<TemplateRef<any>>;
  @ContentChildren('alertTitle', { descendants: true })
  public alertTitleChildren: QueryList<TemplateRef<any>>;
  @ContentChildren('alertContent', { descendants: true })
  public alertContentChildren: QueryList<TemplateRef<any>>;

  constructor() { }

  ngOnInit() { }

  ngAfterContentInit() { }
}
