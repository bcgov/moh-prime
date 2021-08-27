import { Pipe, PipeTransform } from '@angular/core';
import { KeycloakInitService } from '@core/modules/keycloak/keycloak-init.service';

@Pipe({
  name: 'contains'
})
export class ContainsPipe implements PipeTransform {

  transform(value: string, line: string, position: string = ''): boolean {
    if (line != null && value != null){
      switch (position) {
        case '': {
          return line.includes(value);
        }
        case "start": {
          return line.startsWith(value);
        }
        case "end": {
          return line.endsWith(value);
        }
      };
    }
    return false;
  }

}
