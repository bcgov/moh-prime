import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HealthAuthoritySiteContainerComponent } from './health-authority-site-container.component';

describe('HealthAuthoritySiteContainerComponent', () => {
  let component: HealthAuthoritySiteContainerComponent;
  let fixture: ComponentFixture<HealthAuthoritySiteContainerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ HealthAuthoritySiteContainerComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(HealthAuthoritySiteContainerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
