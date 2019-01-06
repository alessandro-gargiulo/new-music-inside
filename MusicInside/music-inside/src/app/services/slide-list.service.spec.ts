import { TestBed, inject } from '@angular/core/testing';

import { Services\slideListService } from './services\slide-list.service';

describe('Services\slideListService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [Services\slideListService]
    });
  });

  it('should be created', inject([Services\slideListService], (service: Services\slideListService) => {
    expect(service).toBeTruthy();
  }));
});
