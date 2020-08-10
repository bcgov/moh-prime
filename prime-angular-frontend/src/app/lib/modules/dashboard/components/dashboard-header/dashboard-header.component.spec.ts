import { async, ComponentFixture, TestBed } from '@angular/core/testing';


import { DashboardHeaderComponent } from './dashboard-header.component';
import { NgxMaterialModule } from '@lib/modules/ngx-material/ngx-material.module';
import { NgxProgressModule } from '@lib/modules/ngx-progress/ngx-progress.module';

describe('DashboardHeaderComponent', () => {
  let component: DashboardHeaderComponent;
  let fixture: ComponentFixture<DashboardHeaderComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule(
      {
        imports: [
          NgxMaterialModule,
          NgxProgressModule
        ],
        declarations: [
          DashboardHeaderComponent
        ]
      }
    ).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DashboardHeaderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
