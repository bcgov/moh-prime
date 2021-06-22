import { ActivatedRoute, Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { FormBuilder } from '@angular/forms';

import { Observable, UnaryFunction } from 'rxjs';
import { exhaustMap, map, tap } from 'rxjs/operators';

import { RouteUtils } from '@lib/utils/route-utils.class';
import { Contact } from '@lib/models/contact.model';
import { ContactFormState } from '@lib/classes/contact-form-state.class';
import { AbstractEnrolmentPage } from '@lib/classes/abstract-enrolment-page.class';
import { NoContent } from '@core/resources/abstract-resource';
import { FormUtilsService } from '@core/services/form-utils.service';
import { HealthAuthorityResource } from '@core/resources/health-authority-resource.service';
import { CardListItem } from '@shared/components/card-list/card-list.component';
import { AdjudicationRoutes } from '@adjudication/adjudication.routes';
import { HealthAuthority } from '@shared/models/health-authority.model';

// TODO step this back to something more generic, and refine to
// accommodate other list form combinations like remote users
export abstract class AbstractContactsPage extends AbstractEnrolmentPage {
  public title: string;
  public formState: ContactFormState;
  public isInitialEntry: boolean;
  public isEditing: boolean;
  public contacts: Contact[];
  public contactCardListItems: CardListItem[];

  protected cardTitlePrefix: string;
  protected backRoute: string;
  protected nextRoute: string;
  protected routeUtils: RouteUtils;

  protected constructor(
    protected route: ActivatedRoute,
    protected dialog: MatDialog,
    protected formUtilsService: FormUtilsService,
    protected fb: FormBuilder,
    protected healthAuthResource: HealthAuthorityResource,
    router: Router,
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
    this.cardTitlePrefix = '';
  }

  public onAdd(): void {
    this.isEditing = true;
    this.formState.form.reset();
  }

  public onEdit(index: number): void {
    this.isEditing = true;
    this.formState.patchValue(this.contacts[index]);
  }

  public onRemove(index: number): void {
    // TODO remove the administrator needs an API endpoint
    // TODO what happens if we're allowed to remove (archive) an administrator?
    // TODO update to be behaviour subject to stream changes
    this.contacts.splice(index, 1);
    this.updateCardList(this.contacts);
  }

  public onBack(): void {
    // Previous view when showing the list of cards or no contacts
    // exist, otherwise toggle the view from form to list
    if (!this.isEditing || !this.contacts.length) {
      this.routeTo(this.backRoute);
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
      this.routeTo(this.nextRoute);
    }
  }

  /**
   * @description
   * Perform common initialization.
   */
  protected init(): void {
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

  /**
   * @description
   * Get a list of contacts.
   */
  protected getContacts(): Observable<Contact[]> {
    return this.healthAuthResource.getHealthAuthorityById(this.route.snapshot.params.haid)
      .pipe(
        this.getContactsPipe(),
        map((contacts: Contact[]) => {
          this.isEditing = !contacts?.length;
          if (!this.isEditing) {
            this.updateCardList(contacts);
          }

          // Store administrators or an empty list to replace default, otherwise
          // card list or form views won't be displayed
          return this.contacts = contacts;
        })
      );
  }

  /**
   * @description
   * Hook to pipe the list of contacts from the response.
   */
  protected abstract getContactsPipe(): UnaryFunction<Observable<HealthAuthority>, Observable<Contact[]>>;

  /**
   * @description
   * Update the contact information.
   */
  protected performSubmission(): Observable<void> {
    const updatedContact = this.formState.json;
    const payload = (!updatedContact.id)
      ? this.contacts.concat([updatedContact])
      : this.contacts.map(contact =>
        (contact.id === updatedContact.id)
          ? updatedContact
          : contact
      );
    return this.performSubmissionRequest(payload)
      .pipe(
        // Refresh the list of administrators for updates
        exhaustMap(() => this.getContacts()),
        map((contacts: Contact[]) => {
          // Populate the list of card items
          this.updateCardList(contacts);
        }),
        // Display the card list view with the updates
        tap(() => this.isEditing = false)
      );
  }

  /**
   * @description
   * Hook to perform a contact update request.
   */
  protected abstract performSubmissionRequest(contact: Contact[]): NoContent;

  /**
   * @description
   * Update the contact card list.
   */
  protected updateCardList(contacts: Contact[]): void {
    this.contactCardListItems = this.getContactsCardList(contacts);

    if (!contacts.length) {
      this.isEditing = true;
    }
  }

  /**
   * @description
   * Map contacts to contact list items.
   */
  protected getContactsCardList(contacts: Contact[]): CardListItem[] {
    return contacts.map(this.getContactListItem());
  }

  /**
   * @description
   * Hook for contact list item properties.
   */
  protected getContactListItem(): (contact: Contact) => CardListItem {
    return (contact: Contact) => ({
      icon: 'account_circle',
      title: `${this.cardTitlePrefix}${contact.firstName} ${contact.lastName}`,
      properties: [
        { key: 'Job Title', value: contact.jobRoleTitle }
      ],
      action: {
        title: 'Update Information'
      }
    });
  }

  protected routeTo(routeSegment?: string): void {
    const routePath = (this.isInitialEntry && routeSegment)
      ? routeSegment
      : AdjudicationRoutes.ORGANIZATION_INFORMATION;
    this.routeUtils.routeRelativeTo(routePath, { queryParamsHandling: 'preserve' });
  }
}
