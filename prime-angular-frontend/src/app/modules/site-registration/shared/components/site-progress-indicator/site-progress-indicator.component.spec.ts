import { waitForAsync, ComponentFixture, TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';

import { MockOrganizationService } from 'test/mocks/mock-organization.service';

import { SiteProgressIndicatorComponent } from './site-progress-indicator.component';
import { SharedModule } from '@shared/shared.module';
import { OrganizationService } from '@registration/shared/services/organization.service';

describe('SiteProgressIndicatorComponent', () => {
  let component: SiteProgressIndicatorComponent;
  let fixture: ComponentFixture<SiteProgressIndicatorComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      imports: [
        SharedModule,
        RouterTestingModule
      ],
      providers: [
        {
          provide: OrganizationService,
          useClass: MockOrganizationService
        }
      ],
      declarations: [SiteProgressIndicatorComponent]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SiteProgressIndicatorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
