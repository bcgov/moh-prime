import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ClaimSiteComponent } from './claim-site.component';

describe('ClaimSiteComponent', () => {
  let component: ClaimSiteComponent;
  let fixture: ComponentFixture<ClaimSiteComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ClaimSiteComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ClaimSiteComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
