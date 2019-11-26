import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ClipboardIconComponent } from './clipboard-icon.component';
import { ClipboardModule } from 'ngx-clipboard';
import { NgxMaterialModule } from '@shared/modules/ngx-material/ngx-material.module';

describe('ClipboardIconComponent', () => {
  let component: ClipboardIconComponent;
  let fixture: ComponentFixture<ClipboardIconComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
        ClipboardModule,
        NgxMaterialModule
      ],
      declarations: [
        ClipboardIconComponent,
      ]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ClipboardIconComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
