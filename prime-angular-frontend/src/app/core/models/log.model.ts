import { LogType } from './log-type.enum';

export interface Log {
  message: string;
  data: string;
  logType: LogType;
}
