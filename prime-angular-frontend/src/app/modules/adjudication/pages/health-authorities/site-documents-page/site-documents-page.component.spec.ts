import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SiteDocumentsPageComponent } from './site-documents-page.component';

describe('SiteDocumentsPageComponent', () => {
  let component: SiteDocumentsPageComponent;
  let fixture: ComponentFixture<SiteDocumentsPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SiteDocumentsPageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SiteDocumentsPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
