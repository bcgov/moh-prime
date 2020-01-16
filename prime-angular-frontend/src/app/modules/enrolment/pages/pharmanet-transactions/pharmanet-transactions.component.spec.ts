import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PharmanetTransactionsComponent } from './pharmanet-transactions.component';
import { NgxBusyModule } from '@shared/modules/ngx-busy/ngx-busy.module';
import { PageComponent } from '@shared/components/page/page.component';
import { PageHeaderComponent } from '@shared/components/page-header/page-header.component';

describe('PharmanetTransactionsComponent', () => {
  let component: PharmanetTransactionsComponent;
  let fixture: ComponentFixture<PharmanetTransactionsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule(
      {
        imports: [
          NgxBusyModule
        ],
        declarations: [
          PharmanetTransactionsComponent,
          PageComponent,
          PageHeaderComponent
        ]
      }
    ).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PharmanetTransactionsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
