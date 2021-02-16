import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PreferredNameFormComponent } from './preferred-name-form.component';

describe('PreferredNameFormComponent', () => {
  let component: PreferredNameFormComponent;
  let fixture: ComponentFixture<PreferredNameFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PreferredNameFormComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PreferredNameFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
