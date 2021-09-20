import { EmailUtils } from '@lib/utils/email-utils.class';

describe('EmailUtils', () => {
  const util = EmailUtils;

  beforeEach(() => { });

  it('should exist', () => expect(util).toBeTruthy());

  describe('EmailUtils.createMailToUrl', () => {
    const mailToUrlPrefix = 'mailto:';
    const emailAddress = 'test@example.com';
    const emailSubject = 'Attention: Unattended coffee may be consumed by gnomes';
    const emailBody = 'Unattended coffee going missing is a major issue and gnomes have been slurping up this untapped resource.';

    it('should create mailTo URL with email', () => {
      const result = util.createMailToUrl(emailAddress);
      expect(result).toEqual(`${mailToUrlPrefix}${emailAddress}`);
    });

    it('should create mailTo URL with email and subject', () => {
      const result = util.createMailToUrl(emailAddress, emailSubject);
      expect(result).toEqual(`${mailToUrlPrefix}${emailAddress}?subject=${encodeURI(emailSubject)}`);
    });

    it('should create mailTo URL with email and body', () => {
      const result = util.createMailToUrl(emailAddress, '', emailBody);
      expect(result).toEqual(`${mailToUrlPrefix}${emailAddress}?body=${encodeURI(emailBody)}`);
    });

    it('should create mailTo URL with email, subject, and body', () => {
      const result = util.createMailToUrl(emailAddress, emailSubject, emailBody);
      expect(result).toEqual(`${mailToUrlPrefix}${emailAddress}?subject=${encodeURI(emailSubject)}&body=${encodeURI(emailBody)}`);
    });

    it('should not create mailTo URL when email is missing', () => {
      const results = [
        util.createMailToUrl(''),
        util.createMailToUrl('', emailSubject),
        util.createMailToUrl('', emailSubject, ''),
        util.createMailToUrl('', emailSubject, emailBody)
      ];
      expect(results.some(url => url)).toBeFalsy();
    });
  });
});
