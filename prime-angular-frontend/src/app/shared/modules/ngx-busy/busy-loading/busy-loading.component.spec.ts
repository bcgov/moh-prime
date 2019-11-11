import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BusyLoadingComponent } from '@shared/modules/busy/busy-loading/busy-loading.component';

describe('BusyLoadingComponent', () => {
  let component: BusyLoadingComponent;
  let fixture: ComponentFixture<BusyLoadingComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BusyLoadingComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BusyLoadingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
