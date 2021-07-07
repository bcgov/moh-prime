import { HttpClientTestingModule } from '@angular/common/http/testing';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ActivatedRoute } from '@angular/router';
import { RouterTestingModule } from '@angular/router/testing';
import { NgxMaterialModule } from '@lib/modules/ngx-material/ngx-material.module';
import { DefaultPipe } from '@shared/pipes/default.pipe';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';

import { NextStepsPageComponent } from './next-steps-page.component';

describe('NextStepsComponent', () => {
  let component: NextStepsPageComponent;
  let fixture: ComponentFixture<NextStepsPageComponent>;
  const mockActivatedRoute = {
    snapshot: { params: { eid: 1 } }
  };

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [
        NextStepsPageComponent,
        DefaultPipe
      ],
      imports: [
        NgxMaterialModule,
        ReactiveFormsModule,
        HttpClientTestingModule,
        RouterTestingModule,
        BrowserAnimationsModule
      ],
      providers: [
        {
          provide: APP_CONFIG,
          useValue: APP_DI_CONFIG
        },
        {
          provide: ActivatedRoute,
          useValue: mockActivatedRoute
        }
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
