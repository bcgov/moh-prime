import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CloseSiteComponent } from './close-site.component';

describe('CloseSiteComponent', () => {
  let component: CloseSiteComponent;
  let fixture: ComponentFixture<CloseSiteComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CloseSiteComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CloseSiteComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
