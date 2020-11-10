import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { UnderagedComponent } from './underaged.component';

describe('UnderagedComponent', () => {
  let component: UnderagedComponent;
  let fixture: ComponentFixture<UnderagedComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UnderagedComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UnderagedComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
