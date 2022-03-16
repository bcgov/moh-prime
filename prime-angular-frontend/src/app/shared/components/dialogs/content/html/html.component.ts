import { ChangeDetectionStrategy, Component, Input } from '@angular/core';

import { IDialogContent } from '../../dialog-content.model';

// Copied from moh-pidp\workspace\libs\shared\ui\src\lib\components\dialogs\content\html\html.component.ts
@Component({
  selector: 'ui-html',
  template: `<p [innerHtml]="data.content | safe: 'html'"></p>`,
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class HtmlComponent implements IDialogContent {
  @Input() public data!: { content: string };
}
