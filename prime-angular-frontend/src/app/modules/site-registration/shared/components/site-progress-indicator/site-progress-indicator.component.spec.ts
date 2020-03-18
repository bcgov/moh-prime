import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SiteProgressIndicatorComponent } from './site-progress-indicator.component';
import { SharedModule } from '@shared/shared.module';

describe('SiteProgressIndicatorComponent', () => {
  let component: SiteProgressIndicatorComponent;
  let fixture: ComponentFixture<SiteProgressIndicatorComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
        SharedModule
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
