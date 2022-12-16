import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SelfDeclarationTermComponent } from './self-declaration-term.component';

describe('SelfDeclarationTermComponent', () => {
  let component: SelfDeclarationTermComponent;
  let fixture: ComponentFixture<SelfDeclarationTermComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SelfDeclarationTermComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SelfDeclarationTermComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
