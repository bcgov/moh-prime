import { Component, OnInit, Inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
import { Router, ActivatedRoute, NavigationExtras } from '@angular/router';

import { Subscription } from 'rxjs';
import { map } from 'rxjs/operators';

import { APP_CONFIG, AppConfig } from 'app/app-config.module';
import { LoggerService } from '@core/services/logger.service';
import { AuthResource } from '@auth/shared/services/auth-resource.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  public form: FormGroup;
  public busy: Subscription;
  public submitted = false;
  private redirectUrl: string;

  constructor(
    @Inject(APP_CONFIG) private config: AppConfig,
    private route: ActivatedRoute,
    private router: Router,
    private fb: FormBuilder,
    private authResource: AuthResource,
    private logger: LoggerService
  ) { }

  /**
   * Handle form submission.
   *
   * @param {Event} event
   * @memberof LoginComponent
   */
  public onSubmit(event: Event): void {
    this.router.navigate(['enrolment/profile']);

    this.submitted = true;

    if (this.form.valid) {
      this.logger.info('VALID', this.form.value);
      // const payload = { ...this.form.value };
      // this.busy = this.authResource.login(payload)
      //   .subscribe(
      //     () => this.onLoginComplete(),
      //     (error: any) => this.onLoginError(error)
      //   );
    } else {
      this.logger.info('INVALID', this.form.value);
      // TODO: can't use instanceOf FormGroup FormControl, FormArray
      Object.keys(this.form.controls).forEach((controlName) => {
        const control = this.form.get(controlName) as FormGroup;
        control.markAllAsTouched();
        control.updateValueAndValidity();
      });
    }
  }

  /**
   * OnInit lifecycle hook.
   *
   * @memberof LoginComponent
   */
  public ngOnInit() {
    this.createFormInstance();
    this.getRedirectUrlParam();
  }

  /**
   * Create the login form instance.
   *
   * @private
   * @memberof LoginComponent
   */
  private createFormInstance() {
    this.form = this.fb.group({
      login: []
    });
  }

  /**
   * Handle login success.
   *
   * @private
   * @memberof LoginComponent
   */
  private onLoginComplete() {
    const { routeLinkParams, queryParams } = this.getRouteParams(this.redirectUrl);
    if (queryParams) {
      this.router.navigate(routeLinkParams, queryParams);
    } else {
      this.router.navigate(routeLinkParams);
    }
  }

  /**
   * Handle login errors.
   *
   * @private
   * @param {*} { error }
   * @memberof LoginComponent
   */
  // TODO: how will errors be provided via API
  // TODO: provided errors by Laravel error bag
  // private onLoginError({ error }: ApiHttpErrorResponse) {
  private onLoginError({ error }: any) {
    const message = (error.errors)
      ? this.getSingleError(error.errors)
      : error.message;

    // TODO: notify user of login failure
  }

  /**
   * Get a single error from a response error bag.
   *
   * @private
   * @param {string[]} errorBag
   * @returns {string}
   * @memberof LoginComponent
   */
  // TODO: how will errors be provided via API
  private getSingleError(errorBag: string[]): string {
    return errorBag[Object.keys(errorBag)[0]].shift();
  }

  /**
   * Gets the URL the member was previously on prior to their token expiring, which
   * will be used to redirect them back where they originated if re-authenticated.
   *
   * @private
   * @memberof LoginComponent
   */
  private getRedirectUrlParam() {
    this.route.queryParams
      .pipe(
        map(params => params.redirectUrl || this.config.routes.dashboard)
      )
      .subscribe((redirectUrl: string) => this.redirectUrl = redirectUrl);
  }

  /**
   * Get the route parameters from the URL.
   *
   * @private
   * @param {string} url
   * @returns {{ routeLinkParams: any, queryParams: any }}
   * @memberof LoginComponent
   */
  // TODO: separate out into own service
  private getRouteParams(url: string): { routeLinkParams: any, queryParams: any } {
    // Separate the path and any optional route params from the query string
    const [pathWithRouteParams, queryString] = url.split('?');
    // Separate the path from any optional route params
    const [path, ...routeParams] = pathWithRouteParams.split(';');
    // Construct navigation extras from the query string
    const queryParams = this.getQueryStringParams(queryString);
    // Construct route link params
    const routeLinkParams = this.getRouteLinkParams(path, routeParams);
    return { routeLinkParams, queryParams };
  }

  /**
   * Constructs the route link parameters, which consists of a routing path and
   * a route parameters (optional and required).
   *
   * @private
   * @param {string} path
   * @param {Array<string>} params
   * @returns {Array<string>}
   * @memberof LoginComponent
   */
  // TODO: separate out into own service
  private getRouteLinkParams(path: string, params: Array<string>): Array<string> {
    // Always apply the path, and default to having no route params
    const routeLinkParams = [path];
    let routeParams = null;
    // Construct route params if any exist
    params.forEach((param, index) => {
      if (index === 0) { routeParams = {}; }
      const [key, value] = param.split('=');
      routeParams[key] = value;
    });
    // Add route params if they exist
    if (routeParams) {
      routeLinkParams.push(routeParams);
    }
    return routeLinkParams;
  }

  /**
   * Get the query parameters from the querystring.
   *
   * @private
   * @param {string} queryString
   * @returns {NavigationExtras}
   * @memberof LoginComponent
   */
  // TODO: separate out into own service
  private getQueryStringParams(queryString: string): NavigationExtras {
    if (queryString) {
      const pairs = queryString.split('&');
      let queryParams = null;
      pairs.forEach((pair, index) => {
        if (index === 0) { queryParams = {}; }
        const [key, value] = pair.split('=');
        queryParams[key] = value;
      });
      return { queryParams };
    } else {
      return null;
    }
  }

  /**
   * Check form field is not empty.
   *
   * @param {string} name
   * @returns {boolean}
   * @memberof RegisterComponent
   */
  // TODO: extend base form component
  public required(name: string): boolean {
    return (
      this.form.get(name).hasError('required') &&
      this.form.get(name).touched
    );
  }
}
