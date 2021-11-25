import { ComponentFixture, TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { DefaultPipe } from '@shared/pipes/default.pipe';

import { FullnamePipe } from '@shared/pipes/fullname.pipe';
import { AdministratorOverviewComponent } from './administrator-overview.component';

describe('AdministratorOverviewComponent', () => {
  let component: AdministratorOverviewComponent;
  let fixture: ComponentFixture<AdministratorOverviewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        RouterTestingModule
      ],
      declarations: [
        AdministratorOverviewComponent,
        FullnamePipe,
        DefaultPipe
      ]
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AdministratorOverviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
