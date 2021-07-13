import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdministratorOverviewComponent } from './administrator-overview.component';

describe('AdministratorOverviewComponent', () => {
  let component: AdministratorOverviewComponent;
  let fixture: ComponentFixture<AdministratorOverviewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AdministratorOverviewComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AdministratorOverviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
