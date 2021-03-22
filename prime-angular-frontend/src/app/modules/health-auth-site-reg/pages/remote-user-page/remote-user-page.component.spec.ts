import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RemoteUserPageComponent } from './remote-user-page.component';

describe('RemoteUserPageComponent', () => {
  let component: RemoteUserPageComponent;
  let fixture: ComponentFixture<RemoteUserPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RemoteUserPageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RemoteUserPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
