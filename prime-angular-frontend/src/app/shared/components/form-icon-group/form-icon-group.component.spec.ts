import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FormIconGroupComponent } from './form-icon-group.component';
import { NgxContextualHelpModule } from '@shared/modules/ngx-contextual-help/ngx-contextual-help.module';
import { NgxMaterialModule } from '@shared/modules/ngx-material/ngx-material.module';

describe('FormIconGroupComponent', () => {
  let component: FormIconGroupComponent;
  let fixture: ComponentFixture<FormIconGroupComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule(
      {
        imports: [
          NgxContextualHelpModule,
          NgxMaterialModule
        ],
        declarations: [
          FormIconGroupComponent
        ]
      }
    ).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FormIconGroupComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
