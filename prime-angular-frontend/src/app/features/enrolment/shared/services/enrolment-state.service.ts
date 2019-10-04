import { Injectable } from '@angular/core';
import { Enrolment } from '../models/enrolment.model';

@Injectable({
  providedIn: 'root'
})
export class EnrolmentStateService {
  private enrolment: Enrolment;

  constructor() { }


}
