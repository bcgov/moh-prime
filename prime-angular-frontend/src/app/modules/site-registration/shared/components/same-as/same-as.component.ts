import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';

import { SiteService } from '@registration/shared/services/site.service';
import { Contact } from '@registration/shared/models/contact.model';
import { Person } from '@registration/shared/models/person.model';

@Component({
  selector: 'app-same-as',
  templateUrl: './same-as.component.html',
  styleUrls: ['./same-as.component.scss']
})
export class SameAsComponent implements OnInit {
  // When not provided selections are not displayed
  @Input() public selectFor: string;
  @Output() public selected: EventEmitter<Contact>;
  public contacts: { key: string, display: string, data: Contact }[];

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
        data: this.removeContactIds(site?.provisioner)
      },
      {
        key: 'administratorPharmaNet',
        display: 'Administrator of PharmaNet Onboarding',
        data: this.removeContactIds(site?.administratorPharmaNet)
      },
      {
        key: 'privacyOfficer',
        display: 'Privacy Officer',
        data: this.removeContactIds(site?.privacyOfficer)
      },
      {
        key: 'technicalSupport',
        display: 'Technical Support',
        data: this.removeContactIds(site?.technicalSupport)
      }
    ];

    const index = this.contacts.findIndex(p => p.key === this.selectFor);
    this.contacts = this.contacts.slice(0, index);
  }

  private removeContactIds(person: Person): Person {
    if (!person) {
      return null;
    }

    person.id = 0;
    if (person.physicalAddress) {
      person.physicalAddressId = 0;
      person.physicalAddress.id = 0;
    }
    if (person.mailingAddress) {
      person.mailingAddressId = 0;
      person.mailingAddress.id = 0;
    }

    return person;
  }
}
