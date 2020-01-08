import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LicenceClassClauseComponent } from './licence-class-clause.component';
import { PageSubheaderComponent } from '@shared/components/page-subheader/page-subheader.component';

describe('LicenceClassClauseComponent', () => {
  let component: LicenceClassClauseComponent;
  let fixture: ComponentFixture<LicenceClassClauseComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [
        LicenceClassClauseComponent,
        PageSubheaderComponent
      ]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LicenceClassClauseComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
