import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HealthAuthorityTableComponent } from './health-authority-table.component';

describe('HealthAuthorityTableComponent', () => {
  let component: HealthAuthorityTableComponent;
  let fixture: ComponentFixture<HealthAuthorityTableComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ HealthAuthorityTableComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(HealthAuthorityTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
