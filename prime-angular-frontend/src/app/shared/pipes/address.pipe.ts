import { Pipe, PipeTransform } from '@angular/core';
import { Address } from '@shared/models/address.model';
import { StringUtils } from '@lib/utils/string-utils.class';

@Pipe({
  name: 'address'
})
export class AddressPipe implements PipeTransform {
  // TODO include country if/when needed, and use second param to exclude
  public transform(model: Address): string {
    return (model?.street && model?.city && model?.provinceCode && model?.postal)
      ? `${model.street}, ${model.city} ${model.provinceCode}. ${StringUtils.splice(model.postal.toUpperCase(), 3, ' ')}`
      : '';
  }
}
