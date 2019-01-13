import { TestBed, inject } from '@angular/core/testing';
import { SlideListService } from 'src/app/services/slide-list.service';

describe('SlideListService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [SlideListService]
    });
  });

  it('should be created', inject([SlideListService], (service: SlideListService) => {
    expect(service).toBeTruthy();
  }));
});
