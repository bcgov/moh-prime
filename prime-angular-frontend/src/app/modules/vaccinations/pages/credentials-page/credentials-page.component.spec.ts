import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CredentialsPageComponent } from './credentials-page.component';

describe('CredentialsPageComponent', () => {
  let component: CredentialsPageComponent;
  let fixture: ComponentFixture<CredentialsPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CredentialsPageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CredentialsPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
