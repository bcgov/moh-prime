import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SiteProgressIndicatorComponent } from './site-progress-indicator.component';

describe('SiteProgressIndicatorComponent', () => {
  let component: SiteProgressIndicatorComponent;
  let fixture: ComponentFixture<SiteProgressIndicatorComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SiteProgressIndicatorComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SiteProgressIndicatorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
