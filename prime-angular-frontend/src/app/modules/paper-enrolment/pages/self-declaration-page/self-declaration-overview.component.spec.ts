import { ComponentFixture, TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';

import { SelfDeclarationOverviewComponent } from './self-declaration-overview.component';

describe('SelfDeclarationOverviewComponent', () => {
  let component: SelfDeclarationOverviewComponent;
  let fixture: ComponentFixture<SelfDeclarationOverviewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        RouterTestingModule
      ],
      declarations: [
        SelfDeclarationOverviewComponent
      ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SelfDeclarationOverviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
