import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ProgressIndicator2Component } from './progress-indicator2.component';
import { NgxMaterialModule } from '@shared/modules/ngx-material/ngx-material.module';

describe('ProgressIndicator2Component', () => {
  let component: ProgressIndicator2Component;
  let fixture: ComponentFixture<ProgressIndicator2Component>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
        NgxMaterialModule
      ],
      declarations: [
        ProgressIndicator2Component
      ]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProgressIndicator2Component);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
