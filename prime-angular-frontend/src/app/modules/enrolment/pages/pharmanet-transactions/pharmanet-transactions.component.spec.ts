import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PharmanetTransactionsComponent } from './pharmanet-transactions.component';

describe('PharmanetTransactionsComponent', () => {
  let component: PharmanetTransactionsComponent;
  let fixture: ComponentFixture<PharmanetTransactionsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PharmanetTransactionsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PharmanetTransactionsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
