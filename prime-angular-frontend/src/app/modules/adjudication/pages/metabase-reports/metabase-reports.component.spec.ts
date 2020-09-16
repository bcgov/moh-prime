import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MetabaseReportsComponent } from './metabase-reports.component';

describe('MetabaseReportsComponent', () => {
  let component: MetabaseReportsComponent;
  let fixture: ComponentFixture<MetabaseReportsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MetabaseReportsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MetabaseReportsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
