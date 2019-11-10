import { ViewContainerRef, Component } from '@angular/core';
import { inject, TestBed, async, ComponentFixture } from '@angular/core/testing';

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

  beforeEach(async(() => {
    TestBed.configureTestingModule(
      {
        declarations: [
          TestDialogContentComponent
        ]
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
