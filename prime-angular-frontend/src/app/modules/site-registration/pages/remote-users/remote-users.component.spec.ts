import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { ReactiveFormsModule } from '@angular/forms';

import { RemoteUsersComponent } from './remote-users.component';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';

describe('RemoteUsersComponent', () => {
  let component: RemoteUsersComponent;
  let fixture: ComponentFixture<RemoteUsersComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [RemoteUsersComponent],
      imports: [
        RouterTestingModule,
        HttpClientTestingModule,
        MatSnackBarModule,
        ReactiveFormsModule
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
    fixture = TestBed.createComponent(RemoteUsersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
