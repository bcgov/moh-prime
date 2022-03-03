import { ViewContainerRef, Component, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { TestBed, ComponentFixture, waitForAsync } from '@angular/core/testing';

import { DialogContentDirective } from './dialog-content.directive';

@Component({
  template: `<ng-template appDialogContent></ng-template>`
})
class TestDialogContentComponent {
  constructor(
    public viewContainerRef: ViewContainerRef
  ) { }
}

describe('DialogContentDirective', () => {
  let component: TestDialogContentComponent;
  let fixture: ComponentFixture<TestDialogContentComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule(
      {
        declarations: [
          TestDialogContentComponent
        ],
        schemas: [CUSTOM_ELEMENTS_SCHEMA]
      }
    ).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TestDialogContentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create an instance', () => {
    const viewContainerRef = fixture.componentInstance.viewContainerRef;
    const directive = new DialogContentDirective(viewContainerRef);
    expect(directive).toBeTruthy();
  });
});
