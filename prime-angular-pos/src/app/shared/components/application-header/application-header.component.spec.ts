import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ApplicationHeaderComponent } from './application-header.component';
import { NgxMaterialModule } from '@shared/modules/ngx-material/ngx-material.module';
import { NgxProgressModule } from '@shared/modules/ngx-progress/ngx-progress.module';

describe('ApplicationHeaderComponent', () => {
  let component: ApplicationHeaderComponent;
  let fixture: ComponentFixture<ApplicationHeaderComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule(
      {
        imports: [
          NgxMaterialModule,
          NgxProgressModule
        ],
        declarations: [
          ApplicationHeaderComponent
        ]
      }
    ).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ApplicationHeaderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
