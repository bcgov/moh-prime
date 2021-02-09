import { ComponentFixture, TestBed } from '@angular/core/testing';
import { NgxMaterialModule } from '@lib/modules/ngx-material/ngx-material.module';

import { ClaimNoteComponent } from './claim-note.component';

describe('ClaimNoteComponent', () => {
  let component: ClaimNoteComponent;
  let fixture: ComponentFixture<ClaimNoteComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        NgxMaterialModule
      ],
      declarations: [ClaimNoteComponent]
    })
      .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ClaimNoteComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
