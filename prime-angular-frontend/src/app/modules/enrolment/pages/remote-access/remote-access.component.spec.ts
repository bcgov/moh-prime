import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';

import { NgxMaterialModule } from '@lib/modules/ngx-material/ngx-material.module';

import { RemoteAccessComponent } from './remote-access.component';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { ReactiveFormsModule } from '@angular/forms';


describe('RemoteAccessComponent', () => {
  let component: RemoteAccessComponent;
  let fixture: ComponentFixture<RemoteAccessComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
        RouterTestingModule,
        NgxMaterialModule,
        HttpClientTestingModule,
        ReactiveFormsModule,
      ],
      providers: [
        {
          provide: APP_CONFIG,
          useValue: APP_DI_CONFIG
        }
      ],
      declarations: [RemoteAccessComponent]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RemoteAccessComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
