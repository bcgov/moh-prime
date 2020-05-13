import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { ReactiveFormsModule } from '@angular/forms';
import { SameAsComponent } from './same-as.component';
import { RouterTestingModule } from '@angular/router/testing';
import { SiteRegistrationModule } from '@registration/site-registration.module';

describe('SameAsComponent', () => {
  let component: SameAsComponent;
  let fixture: ComponentFixture<SameAsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
        ReactiveFormsModule,
        RouterTestingModule,
        SiteRegistrationModule
      ],
      declarations: [SameAsComponent]

    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SameAsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
