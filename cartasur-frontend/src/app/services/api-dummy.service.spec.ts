import { TestBed } from '@angular/core/testing';

import { ApiDummyService } from './api-dummy.service';

describe('ApiDummyService', () => {
  let service: ApiDummyService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ApiDummyService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
