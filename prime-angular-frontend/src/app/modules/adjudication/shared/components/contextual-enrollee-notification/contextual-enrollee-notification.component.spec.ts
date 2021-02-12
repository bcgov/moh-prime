import { HttpClientTestingModule } from '@angular/common/http/testing';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { NgxMaterialModule } from '@lib/modules/ngx-material/ngx-material.module';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';

import { ContextualEnrolleeNotificationComponent } from './contextual-enrollee-notification.component';

describe('ContextualEnrolleeNotificationComponent', () => {
  let component: ContextualEnrolleeNotificationComponent;
  let fixture: ComponentFixture<ContextualEnrolleeNotificationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        HttpClientTestingModule,
        NgxMaterialModule
      ],
      declarations: [ContextualEnrolleeNotificationComponent],
      providers: [
        {
          provide: APP_CONFIG,
          useValue: APP_DI_CONFIG
        },
      ]
    })
      .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ContextualEnrolleeNotificationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
