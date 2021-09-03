import { Pipe, PipeTransform } from '@angular/core';
import { KeycloakInitService } from '@core/modules/keycloak/keycloak-init.service';

@Pipe({
  name: 'contains'
})
export class ContainsPipe implements PipeTransform {
  public transform(
    value: string,
    search: string,
    position: 'startsWith' | 'endsWith' | 'withIn' = 'withIn'
  ): boolean {
    if(!value || !search || typeof value !== 'string' || typeof search !== 'string') {
      return false;
    }

    switch (position) {
      case 'withIn':  {
        return value.includes(search);
      }
      case 'startsWith': {
        return value.startsWith(search);
      }
      case 'endsWith': {
        return value.endsWith(search);
      }
      default: {
        throw new Error(`Invalid "position" argument specified: ${position}`);
      }
    }
  }
}
