import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SendBulkEmailComponent } from './send-bulk-email.component';

describe('SendBulkEmailComponent', () => {
  let component: SendBulkEmailComponent;
  let fixture: ComponentFixture<SendBulkEmailComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SendBulkEmailComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SendBulkEmailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
