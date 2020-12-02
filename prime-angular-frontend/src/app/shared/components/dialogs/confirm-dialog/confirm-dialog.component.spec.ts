import { waitForAsync, ComponentFixture, TestBed } from '@angular/core/testing';
import { NgxMaterialModule } from '@lib/modules/ngx-material/ngx-material.module';

import { ConfirmDialogComponent } from './confirm-dialog.component';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

describe('ConfirmDialogComponent', () => {
  let component: ConfirmDialogComponent;
  let fixture: ComponentFixture<ConfirmDialogComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule(
      {
        imports: [
          NgxMaterialModule
        ],
        declarations: [
          ConfirmDialogComponent
        ],
        providers: [
          {
            provide: MatDialogRef,
            useValue: {
              close: (dialogResult: any) => { }
            }
          },
          {
            provide: MAT_DIALOG_DATA,
            useValue: {}
          }
        ]
      }
    ).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ConfirmDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
