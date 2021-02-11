import { HttpClientTestingModule } from '@angular/common/http/testing';
import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { RouteUtils } from '@lib/utils/route-utils.class';
import { PhsaEformsModule } from '@phsa/phsa-eforms.module';

import { PhsaEformsProgressIndicatorComponent } from './phsa-eforms-progress-indicator.component';

describe('PhsaEformsProgressIndicatorComponent', () => {
  let component: PhsaEformsProgressIndicatorComponent;
  let fixture: ComponentFixture<PhsaEformsProgressIndicatorComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      imports: [
        HttpClientTestingModule,
        RouterTestingModule,
        PhsaEformsModule
      ],
      declarations: []
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PhsaEformsProgressIndicatorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
