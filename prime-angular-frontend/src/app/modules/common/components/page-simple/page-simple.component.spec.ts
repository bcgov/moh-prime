import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PageSimpleComponent } from './page-simple.component';
import { CommonModule } from '@common/common.module';

describe('PageSimpleComponent', () => {
  let component: PageSimpleComponent;
  let fixture: ComponentFixture<PageSimpleComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule(
      {
        imports: [
          CommonModule
        ],
        declarations: []
      }
    ).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PageSimpleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
