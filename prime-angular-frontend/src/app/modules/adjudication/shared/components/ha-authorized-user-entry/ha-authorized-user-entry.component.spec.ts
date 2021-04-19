import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HaAuthorizedUserEntryComponent } from './ha-authorized-user-entry.component';

describe('HaAuthorizedUserEntryComponent', () => {
  let component: HaAuthorizedUserEntryComponent;
  let fixture: ComponentFixture<HaAuthorizedUserEntryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ HaAuthorizedUserEntryComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(HaAuthorizedUserEntryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
