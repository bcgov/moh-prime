export interface DialogDefaultOptions {
  icon?: string;
  title?: string;
  message?: string;
  actionType?: 'primary' | 'accent' | 'warn';
  actionText?: string;
  cancelText?: string;
  cancelHide?: boolean;
  data?: { [key: string]: any };
}
