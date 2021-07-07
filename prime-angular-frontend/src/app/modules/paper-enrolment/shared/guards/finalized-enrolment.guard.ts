import { Injectable, Inject } from '@angular/core';
import {
  Router,
  Params,
  CanActivateChild,
  ActivatedRouteSnapshot,
  RouterStateSnapshot,
  UrlTree,
  UrlSegment
} from '@angular/router';
import { PaperEnrolmentResource } from '@paper-enrolment/services/paper-enrolment-resource.service';
import { HttpEnrollee } from '@shared/models/enrolment.model';
import { PaperEnrolmentRoutes } from '@paper-enrolment/paper-enrolment.routes';
import { Observable } from 'rxjs';
import { AppConfig, APP_CONFIG } from 'app/app-config.module';
import { map } from 'rxjs/operators';


@Injectable({
  providedIn: 'root'
})
export class FinalizedEnrolmentGuard implements CanActivateChild {
  constructor(
    private router: Router,
    @Inject(APP_CONFIG) private config: AppConfig,
    private paperEnrolmentResource: PaperEnrolmentResource
  ) { }

  canActivateChild(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    const url = this.getUrl(state);
    return this.checkAccess(url, next.params);
  }

  protected checkAccess(routePath: string = null, params: Params): Observable<boolean> | Promise<boolean> {
    const enrolleeId = +params.eid;
    if (enrolleeId) {
      return this.paperEnrolmentResource.getEnrolleeById(enrolleeId)
        .pipe(
          map((enrollee: HttpEnrollee) => {
            return this.routeDestination(routePath, enrollee, params);
          })
        );
    } else {
      return new Promise(async (resolve, reject) => resolve(true));
    }
  }

  private routeDestination(routePath: string, httpEnrollee: HttpEnrollee, params) {
    if (!httpEnrollee?.approvedDate) {
      return true;
    }
    if (routePath.includes('demographic')) {
      routePath = routePath.replace('demographic', '');
    }
    this.navigate(PaperEnrolmentRoutes.NEXT_STEPS, params);
    return false;
  }

  private getUrl(routeParam: UrlSegment[] | RouterStateSnapshot): string {
    return (Array.isArray(routeParam))
      ? routeParam.reduce((path, segment) => `${path}/${segment.path}`, '')
      : routeParam.url;
  }

  private navigate(routePath: string, params): boolean {
    const modulePath = this.config.routes.paperEnrolment;
    const comparePath = `/${modulePath}/${params}/${routePath}`;

    if (routePath === comparePath) {
      return true;
    } else {
      this.router.navigate([comparePath]);
      return false;
    }
  }

}
