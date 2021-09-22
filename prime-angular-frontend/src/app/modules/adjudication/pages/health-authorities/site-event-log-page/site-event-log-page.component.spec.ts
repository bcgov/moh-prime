import { HttpClientTestingModule } from '@angular/common/http/testing';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { ActivatedRoute } from '@angular/router';
import { RouterTestingModule } from '@angular/router/testing';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';

import { SiteEventLogPageComponent } from './site-event-log-page.component';

describe('SiteEventLogPageComponent', () => {
  let component: SiteEventLogPageComponent;
  let fixture: ComponentFixture<SiteEventLogPageComponent>;
  const mockActivatedRoute = {
    snapshot: {
      data: {
        title: 'Site Event Log Page'
      },
      params: {
        sid: 1
      }
    }
  };

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [SiteEventLogPageComponent],
      imports: [
        RouterTestingModule,
        HttpClientTestingModule,
        MatSnackBarModule,
        ReactiveFormsModule,
        FormsModule
      ],
      providers:
        [
          {
            provide: ActivatedRoute,
            useValue: mockActivatedRoute
          },
          {
            provide: APP_CONFIG,
            useValue: APP_DI_CONFIG
          }
        ]
    })
      .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SiteEventLogPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
