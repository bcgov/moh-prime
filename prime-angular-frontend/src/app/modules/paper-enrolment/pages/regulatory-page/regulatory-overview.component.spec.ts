import { ComponentFixture, TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';

import { RegulatoryOverviewComponent } from './regulatory-overview.component';

import { DefaultPipe } from '@shared/pipes/default.pipe';

describe('RegulatoryOverviewComponent', () => {
  let component: RegulatoryOverviewComponent;
  let fixture: ComponentFixture<RegulatoryOverviewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        RouterTestingModule
      ],
      declarations: [
        RegulatoryOverviewComponent,
        DefaultPipe
      ]
    })
      .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RegulatoryOverviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
