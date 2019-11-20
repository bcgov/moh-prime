import { configure } from '@storybook/angular';

configure(require.context('../src', true, /\.stories\.[tj]s$/), module);
