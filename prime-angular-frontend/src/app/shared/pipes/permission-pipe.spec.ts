import { inject, TestBed } from '@angular/core/testing';
import { PermissionService } from '@auth/shared/services/permission.service';
import { MockPermissionService } from 'test/mocks/mock-permission-service';
import { PermissionPipe } from './permission-pipe';

describe('PermissionPipe', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [
        PermissionPipe,
        {
          provide: PermissionService,
          useClass: MockPermissionService
        }
      ]
    });
  });


  it('should create', inject([PermissionPipe], (pipe: PermissionPipe) => {
    expect(pipe).toBeTruthy();
  }));

});
