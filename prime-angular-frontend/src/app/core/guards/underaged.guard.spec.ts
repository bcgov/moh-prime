import { provideHttpClientTesting } from '@angular/common/http/testing';
import { TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { AuthService } from '@auth/shared/services/auth.service';
import { AppRoutes } from 'app/app.routes';
import { KeycloakService } from 'keycloak-angular';
import { MockAuthService } from 'test/mocks/mock-auth.service';


import { UnderagedGuard } from './underaged.guard';
import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';
import { NotEligibleComponent } from '@lib/modules/root-routes/components/not-eligible/not-eligible.component';


describe('UnderagedGuard', () => {
    let guard: UnderagedGuard;

    beforeEach(() => {
        TestBed.configureTestingModule({
            imports: [RouterTestingModule.withRoutes([
                {
                    path: AppRoutes.UNDERAGED,
                    component: NotEligibleComponent
                }
            ])],
            providers: [
                {
                    provide: AuthService,
                    useClass: MockAuthService
                },
                KeycloakService,
                provideHttpClient(withInterceptorsFromDi()),
                provideHttpClientTesting()
            ]
        });
        guard = TestBed.inject(UnderagedGuard);
    });

    it('should be created', () => {
        expect(guard).toBeTruthy();
    });
});
