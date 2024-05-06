import { Pipe, PipeTransform } from '@angular/core';

import { CollegeConfig } from '@config/config.model';
import { ConfigService } from '@config/config.service';

@Pipe({
  name: 'collegeName'
})
export class CollegeNamePipe implements PipeTransform {
  constructor(
    private configService: ConfigService
  ) { }

  public transform(collegeCode: number): string {
    let college = this.configService.colleges
      .find((collegeConfig: CollegeConfig) => collegeConfig.code === collegeCode)
    return college ? college.name : "";
  }
}
