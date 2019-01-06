import { TestBed, inject } from '@angular/core/testing';

import { Services\songListService } from './services\song-list.service';

describe('Services\songListService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [Services\songListService]
    });
  });

  it('should be created', inject([Services\songListService], (service: Services\songListService) => {
    expect(service).toBeTruthy();
  }));
});
