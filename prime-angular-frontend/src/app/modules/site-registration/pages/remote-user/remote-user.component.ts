import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';

import { Subscription } from 'rxjs';

import { FormArrayValidators } from '@lib/validators/form-array.validators';

import { SiteRoutes } from '@registration/site-registration.routes';
import { RemoteUser } from '@registration/shared/models/remote-user.model';
import { RouteUtils } from '@registration/shared/classes/route-utils.class';

@Component({
  selector: 'app-remote-user',
  templateUrl: './remote-user.component.html',
  styleUrls: ['./remote-user.component.scss']
})
export class RemoteUserComponent implements OnInit {
  public busy: Subscription;
  public form: FormGroup;
  public routeUtils: RouteUtils;
  public isCompleted: boolean;
  public remoteUser: RemoteUser;
  public SiteRoutes = SiteRoutes;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private fb: FormBuilder
  ) { }

  public onSubmit() {
    // TODO check validity of the local form
    // TODO update the global form with changes
    // TODO want to save from here not in remote users, but only once not again in remote users
    // TODO should we be saving remote users individually or as wholesale PUT?
    // TODO DO NOT update state service form directly when editing a remote user
    // TODO need a way to be able to create a different instance of the formgroup when editing
  }

  public onBack() {
    // TODO remove new index from remote users array with id: 0
  }

  public ngOnInit(): void {
    // TODO setup in site state service and maintain list
    // TODO view the updates from the state service in remote users
    this.createFormInstance();
    this.initForm();
  }

  public createFormInstance() {
    this.form = this.fb.group({
      firstName: [
        '',
        [Validators.required]
      ],
      lastName: [
        '',
        [Validators.required]
      ],
      remoteUserLocations: this.fb.array(
        [],
        [FormArrayValidators.atLeast(1)]
      )
    });
  }

  public initForm() {
    const remoteUserId = this.route.snapshot.data.id;
    // TODO populate with data if the id is not zero, otherwise add an index to the remote users array
  }
}
