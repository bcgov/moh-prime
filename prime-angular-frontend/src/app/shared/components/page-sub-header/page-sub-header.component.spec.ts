import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PageSubHeaderComponent } from './page-sub-header.component';

describe('PageSubHeaderComponent', () => {
  let component: PageSubHeaderComponent;
  let fixture: ComponentFixture<PageSubHeaderComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PageSubHeaderComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PageSubHeaderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
