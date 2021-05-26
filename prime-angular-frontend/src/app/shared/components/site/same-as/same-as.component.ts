import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';

import { Party } from '@lib/models/party.model';
import { Person } from '@lib/models/person.model';
import { Contact } from '@lib/models/contact.model';
import { Address, AddressType } from '@shared/models/address.model';
import { SiteService } from '@registration/shared/services/site.service';

@Component({
  selector: 'app-same-as',
  templateUrl: './same-as.component.html',
  styleUrls: ['./same-as.component.scss']
})
export class SameAsComponent implements OnInit {
  // When not provided selections are not displayed
  @Input() public selectFor: string;
  @Output() public selected: EventEmitter<Contact>;
  public contacts: { key: string, display: string, data: Contact; }[];

  constructor(
    private siteService: SiteService
  ) {
    this.selected = new EventEmitter<Contact>();
  }

  public onSelect(contact: Contact) {
    this.selected.emit(contact);
  }

  public ngOnInit(): void {
    const site = this.siteService.site;
    this.contacts = [
      {
        key: 'signingAuthority',
        display: 'Signing Authority',
        data: this.fromParty({ ...site?.provisioner })
      },
      {
        key: 'administratorPharmaNet',
        display: 'Administrator of PharmaNet Onboarding',
        data: this.removeUniqueIds({ ...site?.administratorPharmaNet })
      },
      {
        key: 'privacyOfficer',
        display: 'Privacy Officer',
        data: this.removeUniqueIds({ ...site?.privacyOfficer })
      },
      {
        key: 'technicalSupport',
        display: 'Technical Support',
        data: this.removeUniqueIds({ ...site?.technicalSupport })
      }
    ];

    const index = this.contacts.findIndex(p => p.key === this.selectFor);
    this.contacts = this.contacts.slice(0, index);
  }

  private fromParty(party: Party): Contact {
    if (!party) {
      return null;
    }

    // Use verifiedAddress by default and fallback to physicalAddress
    if (Address.isNotEmpty(party.verifiedAddress)) {
      party.physicalAddress = { ...party.verifiedAddress };
    }

    return this.removeUniqueIds(party);
  }

  private removeUniqueIds(person: Person): Contact {
    if (!person) {
      return null;
    }

    person.id = 0;
    ['physicalAddress', 'mailingAddress']
      .forEach((addressType: AddressType) => {
        if (person[addressType]) {
          person[`${ addressType }Id`] = 0;
          person[addressType].id = 0;
        }
      });

    return person;
  }
}
