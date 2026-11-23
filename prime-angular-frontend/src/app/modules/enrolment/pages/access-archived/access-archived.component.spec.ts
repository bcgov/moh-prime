import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AccessArchivedComponent } from './access-archived.component';

describe('AccessArchivedComponent', () => {
  let component: AccessArchivedComponent;
  let fixture: ComponentFixture<AccessArchivedComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AccessArchivedComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AccessArchivedComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
