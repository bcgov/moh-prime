import { LicenseNumberLabelPipe } from './license-number-label.pipe';

/**
 * Based on src\app\config\config-code.pipe.spec.ts
 */
describe('LicenseNumberLabelPipe', () => {
  let pipe: LicenseNumberLabelPipe;

  beforeEach(() => pipe = new LicenseNumberLabelPipe());

  it('create an instance', () => expect(pipe).toBeTruthy());
});
