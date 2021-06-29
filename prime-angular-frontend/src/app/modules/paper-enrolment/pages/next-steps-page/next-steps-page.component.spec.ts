import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ReactiveFormsModule } from '@angular/forms';
import { NgxMaterialModule } from '@lib/modules/ngx-material/ngx-material.module';

import { NextStepsPageComponent } from './next-steps-page.component';

describe('NextStepsComponent', () => {
  let component: NextStepsPageComponent;
  let fixture: ComponentFixture<NextStepsPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [NextStepsPageComponent],
      imports: [
        NgxMaterialModule,
        ReactiveFormsModule
      ]
    })
      .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(NextStepsPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
