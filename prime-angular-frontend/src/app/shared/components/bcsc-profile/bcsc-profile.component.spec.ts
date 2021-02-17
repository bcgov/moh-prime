import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BcscProfileComponent } from './bcsc-profile.component';

describe('BcscProfileComponent', () => {
  let component: BcscProfileComponent;
  let fixture: ComponentFixture<BcscProfileComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [BcscProfileComponent]
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(BcscProfileComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
