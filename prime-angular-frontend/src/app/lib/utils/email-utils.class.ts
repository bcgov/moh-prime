import { ArrayUtils } from '@lib/utils/array-utils.class';

export class EmailUtils {
  /**
   * @description
   * Open an email using the user's default email client.
   */
  public static openEmailClient(recipient: string, subject: string = '', body: string = ''): void {
    window.location.href = this.createMailToUrl(recipient, subject, body);
  }

  public static createMailToUrl(recipient: string, subject: string = '', body: string = '') {
    if (!recipient) {
      return;
    }

    const params = [
      ...ArrayUtils.insertIf(subject, `subject=${encodeURI(subject)}`),
      ...ArrayUtils.insertIf(body, `body=${encodeURI(body)}`)
    ];

    let mailToUrl = `mailto:${recipient}`;
    if (params.length) {
      mailToUrl += `?${params.join('&')}`;
    }

    return mailToUrl;
  }
}
