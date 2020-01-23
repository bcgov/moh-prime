import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EnrolleePropertyErrorComponent } from './enrollee-property-error.component';
import { NgxContextualHelpModule } from '@shared/modules/ngx-contextual-help/ngx-contextual-help.module';
import { NgxMaterialModule } from '@shared/modules/ngx-material/ngx-material.module';

describe('EnrolleePropertyErrorComponent', () => {
  let component: EnrolleePropertyErrorComponent;
  let fixture: ComponentFixture<EnrolleePropertyErrorComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
        NgxContextualHelpModule,
        NgxMaterialModule
      ],
      declarations: [EnrolleePropertyErrorComponent]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EnrolleePropertyErrorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
