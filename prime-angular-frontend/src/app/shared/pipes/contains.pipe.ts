import { Pipe, PipeTransform } from '@angular/core';
import { KeycloakInitService } from '@core/modules/keycloak/keycloak-init.service';

@Pipe({
  name: 'contains'
})
export class ContainsPipe implements PipeTransform {

  transform(
    word: string,
    line: string,
    position: 'startsWith' | 'endsWith' | 'withIn' = 'withIn'): boolean {
    if (!line && !word && typeof line === 'string' && typeof word === 'string'){
      switch (position) {
        case 'withIn':  {
          return line.includes(word);
        }
        case 'startsWith': {
          return line.startsWith(word);
        }
        case 'endsWith': {
          return line.endsWith(word);
        }
        default: {
          throw new Error(`Invalid "position" argument specified: ${position}`);
        }
      };
    }
    return false;
  }
}
