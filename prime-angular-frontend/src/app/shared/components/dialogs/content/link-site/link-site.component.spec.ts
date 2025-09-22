import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LinkSiteComponent } from './link-site.component';

describe('LinkSiteComponent', () => {
  let component: LinkSiteComponent;
  let fixture: ComponentFixture<LinkSiteComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [LinkSiteComponent]
    });
    fixture = TestBed.createComponent(LinkSiteComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
