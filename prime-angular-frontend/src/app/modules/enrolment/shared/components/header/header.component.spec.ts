import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NgxProgressModule } from '@lib/modules/ngx-progress/ngx-progress.module';
import { NgxMaterialModule } from '@lib/modules/ngx-material/ngx-material.module';

import { HeaderComponent } from './header.component';

describe('HeaderComponent', () => {
  let component: HeaderComponent;
  let fixture: ComponentFixture<HeaderComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule(
      {
        imports: [
          NgxMaterialModule,
          NgxProgressModule
        ],
        declarations: [
          HeaderComponent
        ]
      }
    ).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HeaderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
