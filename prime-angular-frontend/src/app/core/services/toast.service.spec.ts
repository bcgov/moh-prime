import { TestBed } from '@angular/core/testing';

import { ToastService } from './toast.service';
import { NgxMaterialModule } from '@shared/modules/ngx-material/ngx-material.module';

describe('ToastService', () => {
  beforeEach(() => TestBed.configureTestingModule({
    imports: [
      NgxMaterialModule
    ]
  }));

  it('should be created', () => {
    const service: ToastService = TestBed.get(ToastService);
    expect(service).toBeTruthy();
  });
});
