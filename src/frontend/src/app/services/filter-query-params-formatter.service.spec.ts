import { TestBed } from '@angular/core/testing';

import { FilterQueryParamsFormatterService } from './filter-query-params-formatter.service';

describe('FilterQueryParamsFormatterService', () => {
  let service: FilterQueryParamsFormatterService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(FilterQueryParamsFormatterService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
