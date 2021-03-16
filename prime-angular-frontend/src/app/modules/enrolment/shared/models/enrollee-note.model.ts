//
export class EnrolleeNote {
  id: number;
  enrolleeId: number = null;
  note: string = null;

  constructor(
    enrolleeId: number = null,
    note: string = null
  ) {
    this.enrolleeId = enrolleeId;
    this.note = note;
  }
}
