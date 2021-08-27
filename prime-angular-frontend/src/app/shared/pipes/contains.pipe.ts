import { Pipe, PipeTransform } from '@angular/core';
import { KeycloakInitService } from '@core/modules/keycloak/keycloak-init.service';

@Pipe({
  name: 'contains'
})
export class ContainsPipe implements PipeTransform {

  transform(word: string, line: string, position: string = ''): boolean {
    if (line != null && word != null){
      switch (position) {
        case "start": {
          return line.startsWith(word);
        }
        case "end": {
          return line.endsWith(word);
        }
        default: {
          return line.includes(word);
        }
      };
    }
    return false;
  }
}
