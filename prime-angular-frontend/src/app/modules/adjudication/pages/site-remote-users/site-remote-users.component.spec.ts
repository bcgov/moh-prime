import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SiteRemoteUsersComponent } from './site-remote-users.component';
import { ActivatedRoute } from '@angular/router';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { NgxMaterialModule } from '@lib/modules/ngx-material/ngx-material.module';

describe('SiteRemoteUsersComponent', () => {
  let component: SiteRemoteUsersComponent;
  let fixture: ComponentFixture<SiteRemoteUsersComponent>;
  const mockActivatedRoute = {
    snapshot: {
      data: {
        title: 'Remote Users'
      },
      params: {
        sid: 1
      }
    }
  };

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
        HttpClientTestingModule,
        NgxMaterialModule,
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
      ],
      declarations: [SiteRemoteUsersComponent]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SiteRemoteUsersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
