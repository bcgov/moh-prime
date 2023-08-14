export interface DialogOptions {
  icon?: string;
  imageSrc?: string; // Alternative to an icon
  title?: string;
  boldMessage?: string;
  message?: string;
  message2?: string;
  actionType?: 'primary' | 'accent' | 'warn';
  actionText?: string;
  actionHide?: boolean;
  actionLink?: {
    href: string;
    target: '_self' | '_blank';
    text: string;
  };
  cancelText?: string;
  cancelHide?: boolean;
  component?: any;
  data?: { [key: string]: any };
}
