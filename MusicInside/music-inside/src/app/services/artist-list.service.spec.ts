import { TestBed, inject } from '@angular/core/testing';

import { ArtistListService } from './artist-list.service';

describe('ArtistListService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ArtistListService]
    });
  });

  it('should be created', inject([ArtistListService], (service: ArtistListService) => {
    expect(service).toBeTruthy();
  }));
});
