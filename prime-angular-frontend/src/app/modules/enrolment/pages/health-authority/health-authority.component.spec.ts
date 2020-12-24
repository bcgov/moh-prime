import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { HealthAuthorityComponent } from './health-authority.component';

describe('HealthAuthorityComponent', () => {
  let component: HealthAuthorityComponent;
  let fixture: ComponentFixture<HealthAuthorityComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ HealthAuthorityComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HealthAuthorityComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
