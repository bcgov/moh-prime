import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PlrInfoComponent } from './plr-info.component';
import { DefaultPipe } from '@shared/pipes/default.pipe';
import { FormatDatePipe } from '@shared/pipes/format-date.pipe';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';

describe('PlrInfoComponent', () => {
  let component: PlrInfoComponent;
  let fixture: ComponentFixture<PlrInfoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [
        PlrInfoComponent,
        DefaultPipe,
        FormatDatePipe
      ],
      schemas: [CUSTOM_ELEMENTS_SCHEMA]
    })
      .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PlrInfoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
