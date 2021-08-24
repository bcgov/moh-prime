import { Component, Input, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, FormControl } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

import { Subscription, BehaviorSubject, pipe, Observable, UnaryFunction } from 'rxjs';
import { map } from 'rxjs/operators';

import { SiteResource } from '@core/resources/site-resource.service';
import { BaseAdjudicatorNote } from '@shared/models/adjudicator-note.model';

import { PermissionService } from '@auth/shared/services/permission.service';

import { NoteType } from '@adjudication/shared/enums/note-type.enum';
import { Role } from '@auth/shared/enum/role.enum';
import { AdjudicationResource } from '@adjudication/shared/services/adjudication-resource.service';
import { DateContent } from '@adjudication/shared/components/dated-content-table/dated-content-table.component';
import { RouteUtils } from '@lib/utils/route-utils.class';
import { AdjudicationRoutes } from '@adjudication/adjudication.routes';

@Component({
  selector: 'app-adjudicator-notes',
  templateUrl: './adjudicator-notes.component.html',
  styleUrls: ['./adjudicator-notes.component.scss']
})
export class AdjudicatorNotesComponent implements OnInit {
  @Input() public noteType: NoteType;

  public busy: Subscription;
  public form: FormGroup;
  public columns: string[];
  public adjudicatorNotes$: BehaviorSubject<DateContent[]>;
  public hasActions: boolean;

  public AdjudicationRoutes = AdjudicationRoutes;
  public Role = Role;

  private routeUtils: RouteUtils;

  constructor(
    private route: ActivatedRoute,
    protected router: Router,
    private fb: FormBuilder,
    private adjudicationResource: AdjudicationResource,
    private siteResource: SiteResource,
    private permissionService: PermissionService
  ) {
    // Arbitrary base route path, since all routing is relative in this page.
    this.routeUtils = new RouteUtils(route, router, AdjudicationRoutes.routePath(AdjudicationRoutes.ENROLLEES));
    this.hasActions = false;
    this.adjudicatorNotes$ = new BehaviorSubject<DateContent[]>(null);
  }

  public get note(): FormControl {
    return this.form.get('note') as FormControl;
  }

  public onSubmit(): void {
    if (this.form.valid) {
      switch (this.noteType) {
        case NoteType.EnrolleeAdjudicationNote:
          this.createEnrolleeNote(+this.route.snapshot.params.id, this.note.value);
          break;
        case NoteType.SiteRegistrationNote:
          this.createSiteRegistrationNote(+this.route.snapshot.params.sid, this.note.value);
          break;
        default:
          break;
      }
    }
  }

  public onRoute(routePath: string | (string | number)[]): void {
    this.routeUtils.routeRelativeTo(routePath);
  }

  public ngOnInit(): void {
    this.createFormInstance();
    switch (this.noteType) {
      case NoteType.EnrolleeAdjudicationNote:
        this.getAdjudicatorNotes(+this.route.snapshot.params.id);
        this.route.params.subscribe(params => this.getAdjudicatorNotes(+params.id));
        break;
      case NoteType.SiteRegistrationNote:
        this.getSiteRegistrationNotes(+this.route.snapshot.params.sid);
        this.route.params.subscribe(params => this.getSiteRegistrationNotes(+params.sid));
        break;
      default:
        break;
    }
  }

  protected createFormInstance(): void {
    this.form = this.fb.group({
      note: [
        {
          value: '',
          disabled: !this.permissionService.hasRoles(Role.MANAGE_ENROLLEE)
        },
        []
      ]
    });
  }

  private getAdjudicatorNotes(enrolleeId: number): void {
    this.busy = this.adjudicationResource.getAdjudicatorNotes(enrolleeId)
      .pipe(this.toDateContentPipe())
      .subscribe((datedContent: DateContent[]) =>
        this.adjudicatorNotes$.next(datedContent)
      );
  }

  private createEnrolleeNote(enrolleeId: number, note: string): void {
    this.adjudicationResource
      .createAdjudicatorNote(enrolleeId, note)
      .pipe(this.toDateContentPipe())
      .subscribe((adjudicatorNote: DateContent) => {
        const notes = [adjudicatorNote, ...this.adjudicatorNotes$.value];
        this.adjudicatorNotes$.next(notes);
        this.note.reset();
      });
  }

  private getSiteRegistrationNotes(siteId: number): void {
    this.busy = this.siteResource.getSiteRegistrationNotes(siteId)
      .pipe(this.toDateContentPipe())
      .subscribe((datedContent: DateContent[]) =>
        this.adjudicatorNotes$.next(datedContent)
      );
  }

  private createSiteRegistrationNote(site: number, note: string): void {
    this.siteResource
      .createSiteRegistrationNote(site, note)
      .pipe(this.toDateContentPipe())
      .subscribe((adjudicatorNote: DateContent) => {
        const notes = [adjudicatorNote, ...this.adjudicatorNotes$.value];
        this.adjudicatorNotes$.next(notes);
        this.note.reset();
      });
  }

  private toDateContentPipe(): UnaryFunction<Observable<BaseAdjudicatorNote | BaseAdjudicatorNote[]>, Observable<DateContent | unknown[]>> {
    return pipe(
      map((adjudicationNotes: BaseAdjudicatorNote | BaseAdjudicatorNote[]) =>
        (Array.isArray(adjudicationNotes))
          ? adjudicationNotes.map(this.toDateContent.bind(this))
          : this.toDateContent(adjudicationNotes)
      )
    );
  }

  private toDateContent(adjudicationNote: BaseAdjudicatorNote): DateContent {
    return {
      date: adjudicationNote.noteDate,
      name: adjudicationNote.adjudicator.idir,
      content: adjudicationNote.note
    };
  }
}
