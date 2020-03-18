import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DatedContentTableComponent } from './dated-content-table.component';
import { AdjudicationModule } from '@adjudication/adjudication.module';

describe('DatedContentTableComponent', () => {
  let component: DatedContentTableComponent;
  let fixture: ComponentFixture<DatedContentTableComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
        AdjudicationModule
      ],
      declarations: []
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DatedContentTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
