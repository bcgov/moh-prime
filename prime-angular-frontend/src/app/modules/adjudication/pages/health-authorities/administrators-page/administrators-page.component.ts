import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';

import { Observable } from 'rxjs';
import { exhaustMap, map, tap } from 'rxjs/operators';

import { Contact } from '@lib/models/contact.model';
import { RouteUtils } from '@lib/utils/route-utils.class';
import { ContactFormState } from '@lib/classes/contact-form-state.class';
import { AbstractEnrolmentPage } from '@lib/classes/abstract-enrolment-page.class';
import { FormUtilsService } from '@core/services/form-utils.service';
import { HealthAuthorityResource } from '@core/resources/health-authority-resource.service';
import { HealthAuthority } from '@shared/models/health-authority.model';
import { CardListItem } from '@shared/components/card-list/card-list.component';

import { AdjudicationRoutes } from '@adjudication/adjudication.routes';
import {
  AdministratorPageFormState
} from '@adjudication/pages/health-authorities/administrators-page/administrators-page-form-state.class';

@Component({
  selector: 'app-administrators-page',
  templateUrl: './administrators-page.component.html',
  styleUrls: ['./administrators-page.component.scss']
})
export class AdministratorsPageComponent extends AbstractEnrolmentPage implements OnInit {
  public title: string;
  public formState: AdministratorPageFormState;
  public isInitialEntry: boolean;
  public isEditing: boolean;
  public contacts: Contact[];
  public contactCardListItems: CardListItem[];

  private routeUtils: RouteUtils;

  constructor(
    protected dialog: MatDialog,
    protected formUtilsService: FormUtilsService,
    private fb: FormBuilder,
    private healthAuthResource: HealthAuthorityResource,
    private route: ActivatedRoute,
    router: Router
  ) {
    super(dialog, formUtilsService);

    this.title = route.snapshot.data.title;
    this.isInitialEntry = !!this.route.snapshot.queryParams.initial;
    this.routeUtils = new RouteUtils(route, router, [
      AdjudicationRoutes.routePath(AdjudicationRoutes.SITE_REGISTRATIONS),
      AdjudicationRoutes.SITE_REGISTRATIONS,
      AdjudicationRoutes.HEALTH_AUTHORITIES,
      this.route.snapshot.params.haid
    ]);
    // Don't load either view until HTTP response
    this.contacts = null;
  }

  public onAdd(): void {
    this.isEditing = true;
    this.formState.form.reset();
  }

  public onEdit(index: number): void {
    this.isEditing = true;
    console.log('EDIT', index, this.contacts);
    // this.form.patchValue(this.contacts[index]);
  }

  public onRemove(index: number): void {
    // TODO what happens if we're allowed to remove (archive) an administrator?
    this.contacts.splice(index, 1);
    // TODO update to be behaviour subject to stream changes
    this.updateCardList(this.contacts);
    // TODO remove the administrator needs an API endpoint
  }

  // TODO feels like there's a gotcha when removing admins
  public onBack(): void {
    // Previous view when showing the list of cards or no contacts
    // exist, otherwise toggle the view from form to list
    if (!this.isEditing || !this.contacts.length) {
      this.routeTo(AdjudicationRoutes.HEALTH_AUTH_TECHNICAL_SUPPORTS);
    } else {
      this.isEditing = false;
    }
  }

  /**
   * @description
   * Continue to next view when at least one contact exists.
   */
  public onContinue(): void {
    // Protection from routing, but not necessary as the view
    // toggles when the list of contacts is empty
    if (this.contacts.length) {
      this.routeTo();
    }
  }

  public ngOnInit(): void {
    this.createFormInstance();
    this.busy = this.getContacts().subscribe();
  }

  protected createFormInstance(): void {
    this.formState = new ContactFormState(this.fb, this.formUtilsService);
  }

  /**
   * @description
   * Patch the form with a contact.
   *
   * NOTE: Usage differs from other views since this view toggles
   * between list and form so not invoked on initialization.
   */
  protected patchForm(contact: Contact): void {
    this.formState.patchValue(contact);
  }

  protected performSubmission(): Observable<void> {
    const contact = this.formState.json;
    // TODO alternative update single administrator at a time
    const payload = (!contact.id)
      ? this.contacts.concat([contact])
      : this.contacts.map(administrator =>
        (administrator.id === contact.id)
          ? contact
          : administrator
      );
    return this.healthAuthResource
      .updatePharmanetAdministrators(this.route.snapshot.params.haid, payload)
      .pipe(
        // Refresh the list of contacts for updates
        exhaustMap(() => this.getContacts()),
        map((contacts: Contact[]) => {
          // Populate the list of card items
          this.updateCardList(contacts);
        }),
        // Display the card list view with the updates
        tap(() => this.isEditing = false)
      );
  }

  private getContacts(): Observable<Contact[]> {
    return this.healthAuthResource.getHealthAuthorityById(this.route.snapshot.params.haid)
      // TODO pipe to increase reuse
      .pipe(
        map(({ pharmanetAdministrators }: HealthAuthority) => {
          this.isEditing = !pharmanetAdministrators?.length;
          if (!this.isEditing) {
            this.updateCardList(pharmanetAdministrators);
          }

          // Store contacts or an empty list to replace default, otherwise
          // card list or form views won't be displayed
          return this.contacts = pharmanetAdministrators;
        })
      );
  }

  private updateCardList(contacts: Contact[]): void {
    this.contactCardListItems = this.getContactsCardList(contacts);

    if (!contacts.length) {
      this.isEditing = true;
    }
  }

  private getContactsCardList(contacts: Contact[]): CardListItem[] {
    return contacts.map(this.getContactListItem);
  }

  private getContactListItem(contact: Contact): CardListItem {
    return {
      icon: 'account_circle',
      title: `PharmaNet Admin: ${contact.firstName} ${contact.lastName}`,
      properties: [
        { key: 'Job Title', value: contact.jobRoleTitle }
      ],
      action: {
        title: 'Update Information'
      }
    };
  }

  private routeTo(routeSegment?: string): void {
    const routePath = (this.isInitialEntry && routeSegment)
      ? routeSegment
      : AdjudicationRoutes.ORGANIZATION_INFORMATION;
    this.routeUtils.routeRelativeTo(routePath, { queryParamsHandling: 'preserve' });
  }
}
