import { Injectable } from '@angular/core';
import {
  Router,
  Params,
  CanActivate,
  ActivatedRouteSnapshot,
  RouterStateSnapshot,
  UrlTree,
  UrlSegment
} from '@angular/router';
import { PaperEnrolmentResource } from '@paper-enrolment/services/paper-enrolment-resource.service';
import { HttpEnrollee } from '@shared/models/enrolment.model';
import { PaperEnrolmentRoutes } from '@paper-enrolment/paper-enrolment.routes';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';


@Injectable({
  providedIn: 'root'
})
export class FinalizedEnrolmentGuard implements CanActivate {
  constructor(
    private router: Router,
    private paperEnrolmentResource: PaperEnrolmentResource
  ) { }

  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    const url = this.getUrl(state);
    return this.checkAccess(url, next.params);
  }

  protected checkAccess(routePath: string = null, params: Params): Observable<boolean> | Promise<boolean> {
    const enrolleeId = +params.eid;
    if (!enrolleeId) {
      return this.paperEnrolmentResource.getEnrolleeById(enrolleeId)
        .pipe(
          map((enrollee: HttpEnrollee) => {
            return this.routeDestination(routePath, enrollee);
          })
        );
    } else {
      return new Promise(async (resolve, reject) => resolve(true));
    }
  }

  private routeDestination(routePath: string, httpEnrollee: HttpEnrollee) {
    if (!httpEnrollee?.approvedDate) {
      return true;
    }
    this.router.navigate([PaperEnrolmentRoutes.NEXT_STEPS]);
    return false;
  }

  private getUrl(routeParam: UrlSegment[] | RouterStateSnapshot): string {
    return (Array.isArray(routeParam))
      ? routeParam.reduce((path, segment) => `${path}/${segment.path}`, '')
      : routeParam.url;
  }

}
