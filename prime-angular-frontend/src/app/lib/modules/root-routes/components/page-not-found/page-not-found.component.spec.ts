import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RootRoutesModule } from '../../root-routes.module';
import { PageNotFoundComponent } from './page-not-found.component';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';

describe('PageNotFoundComponent', () => {
  let component: PageNotFoundComponent;
  let fixture: ComponentFixture<PageNotFoundComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule(
      {
        imports: [
          RootRoutesModule
        ],
        declarations: [],
        providers: [
          {
            provide: APP_CONFIG,
            useValue: APP_DI_CONFIG
          }
        ]
      }
    ).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PageNotFoundComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
