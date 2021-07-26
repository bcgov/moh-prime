import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UploadOverviewComponent } from './upload-overview.component';

describe('UploadOverviewComponent', () => {
  let component: UploadOverviewComponent;
  let fixture: ComponentFixture<UploadOverviewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UploadOverviewComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(UploadOverviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
