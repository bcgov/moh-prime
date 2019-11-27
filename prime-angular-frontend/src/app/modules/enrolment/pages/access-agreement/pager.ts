import {
  Input, ViewChildren, QueryList, TemplateRef, EventEmitter,
  OnChanges, SimpleChanges, AfterViewInit, Output, ChangeDetectorRef
} from '@angular/core';

import { PageRefDirective } from './page-ref.directive';

export abstract class Pager implements OnChanges, AfterViewInit {
  @Input() public currentPage: number;
  @Output() public changed: EventEmitter<{ atEnd: boolean }>;

  @ViewChildren(PageRefDirective) public pages: QueryList<PageRefDirective>;
  public currentTemplate: TemplateRef<any>;

  constructor(
    protected changeDetectorRef: ChangeDetectorRef
  ) {
    this.currentPage = null;
    this.currentTemplate = null;

    this.changed = new EventEmitter<{ atEnd: boolean }>();
  }

  public get isLastPage(): boolean {
    return (this.currentPage - 1 >= this.pages.length - 1);
  }

  public ngOnChanges(changes: SimpleChanges): void {
    const skipChange = changes.currentPage.firstChange;

    // Subsequent changes after view has been initiated
    this.changePage(skipChange);
  }

  public ngAfterViewInit() {
    // Initial change after view has been initiated
    this.changePage();
  }

  public changePage(skipChange: boolean = false) {
    // Only alway changes to the template after the view
    // has been initiated
    if (!skipChange && this.pages) {
      const template = this.pages.toArray()[this.currentPage - 1];

      if (template) {
        this.currentTemplate = this.pages.toArray()[this.currentPage - 1].templateRef;
      }

      this.changed.emit({ atEnd: this.isLastPage });
      this.changeDetectorRef.detectChanges();
    }
  }
}
