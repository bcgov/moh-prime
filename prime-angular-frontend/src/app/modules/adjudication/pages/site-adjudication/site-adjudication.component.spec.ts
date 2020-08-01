import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { MatSnackBarModule } from '@angular/material/snack-bar';

import { SiteAdjudicationComponent } from './site-adjudication.component';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';

describe('SiteAdjudicationComponent', () => {
  let component: SiteAdjudicationComponent;
  let fixture: ComponentFixture<SiteAdjudicationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [SiteAdjudicationComponent],
      imports: [
        RouterTestingModule,
        ReactiveFormsModule,
        HttpClientTestingModule,
        MatSnackBarModule
      ],
      providers: [
        {
          provide: APP_CONFIG,
          useValue: APP_DI_CONFIG
        }
      ]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SiteAdjudicationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
