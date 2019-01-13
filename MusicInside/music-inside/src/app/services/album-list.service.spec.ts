import { TestBed, inject } from '@angular/core/testing';
import { AlbumListService } from 'src/app/services/album-list.service';


describe('AlbumListService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [AlbumListService]
    });
  });

  it('should be created', inject([AlbumListService], (service: AlbumListService) => {
    expect(service).toBeTruthy();
  }));
});
