import { DialogContentDirective } from './dialog-content.directive';
import { inject } from '@angular/core/testing';
import { ViewContainerRef } from '@angular/core';

describe('DialogContentDirective', () => {
  it('should create an instance', inject([ViewContainerRef], (viewContainerRef: ViewContainerRef) => {
    const directive = new DialogContentDirective(viewContainerRef);
    expect(directive).toBeTruthy();
  }));
});
