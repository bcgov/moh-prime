import 'hammerjs';
import { storiesOf, moduleMetadata } from '@storybook/angular';

import { AlertComponent } from './alert.component';
import { MatIconModule } from '@angular/material';
import { NgxMaterialModule } from '@shared/modules/ngx-material/ngx-material.module';
storiesOf('Alert Component', module)
  .addDecorator(
    moduleMetadata({
      imports: [NgxMaterialModule,
      ],
      // providers: [MyExampleService]
    }),
  )
  .add('Chris', () => ({
    component: AlertComponent,
    props: {
      text: 'Chris',
      alertType: 'alert alert-success',
    },
  }))
  .add('Jane', () => ({
    component: AlertComponent,
    props: {
      text: 'Jane',
    },
  }))
  .add('Joe', () => ({
    component: AlertComponent,
    props: {
      text: 'Joe',
    },
  }));

