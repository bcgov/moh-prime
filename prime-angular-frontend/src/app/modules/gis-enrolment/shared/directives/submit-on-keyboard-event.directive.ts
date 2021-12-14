// import { Directive, HostListener, Input, OnInit } from '@angular/core';
// import { MatDialog } from '@angular/material/dialog';
// import { ActivatedRoute, Router } from '@angular/router';
// import { FormUtilsService } from '@core/services/form-utils.service';
// import { GisEnrolmentRoutes } from '@gis/gis-enrolment.routes';
// import { LdapUserPageFormState } from '@gis/pages/ldap-user-page/ldap-user-page-form-state.class';
// import { AbstractEnrolmentPage } from '@lib/classes/abstract-enrolment-page.class';
// import { RouteUtils } from '@lib/utils/route-utils.class';
// import { Observable, of } from 'rxjs';
// import { GisEnrolmentFormStateService } from '../services/gis-enrolment-form-state.service';
// import { GisEnrolmentService } from '../services/gis-enrolment.service';

// @Directive({
//   selector: '[appSubmitonkeyboardevent]'
// })
// export class SubmitOnKeyboardEventDirective extends AbstractEnrolmentPage implements OnInit {

//   public formState: LdapUserPageFormState;
//   private routeUtils: RouteUtils;
//   @Input('appSubmitonkeyboardevent') myFunction: Function;

//   constructor(
//     protected dialog: MatDialog,
//     protected formUtilsService: FormUtilsService,
//     private formStateService: GisEnrolmentFormStateService,
//     private gisEnrolmentService: GisEnrolmentService,
//     route: ActivatedRoute,
//     router: Router,
//   ) {
//     super(dialog, formUtilsService);


//     this.routeUtils = new RouteUtils(route, router, GisEnrolmentRoutes.routePath(GisEnrolmentRoutes.MODULE_PATH));
//    }
//   @HostListener('window:keyup', ['$event'])
//   keyEvent(event: KeyboardEvent) {
//     if (event.code === 'Enter') {
//       console.log('Enter');
//       this.myFunction.call(this);
//     }
//   }
//   public ngOnInit(): void {
//     this.createFormInstance();
//     this.patchForm();
//   }

//   protected createFormInstance(): void {
//     this.formState = this.formStateService.ldapUserPageFormState;
//   }

//   protected patchForm(): void {
//     this.formStateService.setForm(this.gisEnrolmentService.enrolment);
//   }

//   protected initForm(): void { } // NOOP

//   protected performSubmission(): Observable<null> {
//     return of(null);
//   }

// }
