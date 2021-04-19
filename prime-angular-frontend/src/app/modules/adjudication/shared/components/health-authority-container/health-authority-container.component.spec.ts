import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HealthAuthorityContainerComponent } from './health-authority-container.component';

describe('HealthAuthorityContainerComponent', () => {
  let component: HealthAuthorityContainerComponent;
  let fixture: ComponentFixture<HealthAuthorityContainerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ HealthAuthorityContainerComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(HealthAuthorityContainerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
