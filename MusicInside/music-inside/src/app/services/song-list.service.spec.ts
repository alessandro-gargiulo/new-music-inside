import { TestBed, inject } from '@angular/core/testing';
import { SongListService } from 'src/app/services/song-list.service';

describe('SongListService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [SongListService]
    });
  });

  it('should be created', inject([SongListService], (service: SongListService) => {
    expect(service).toBeTruthy();
  }));
});
