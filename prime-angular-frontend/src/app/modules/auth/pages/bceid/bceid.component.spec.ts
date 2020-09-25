import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BceidComponent } from './bceid.component';

describe('BceidComponent', () => {
  let component: BceidComponent;
  let fixture: ComponentFixture<BceidComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [BceidComponent]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BceidComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
