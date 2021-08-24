import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PlrInfoComponent } from './plr-info.component';

describe('PlrInfoComponent', () => {
  let component: PlrInfoComponent;
  let fixture: ComponentFixture<PlrInfoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PlrInfoComponent ]
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
