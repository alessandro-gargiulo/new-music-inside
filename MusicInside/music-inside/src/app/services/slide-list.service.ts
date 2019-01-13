import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { PagedCarouselSlide, CarouselSlide } from '../shared/carousel-slide.model';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class SlideListService {

  constructor(private _http: HttpClient) { }

  get(page?: number, size?: number): Observable<PagedCarouselSlide> {
    if (page && size) {
      return this._http.get<PagedCarouselSlide>(`${environment.api.api_base_path}/${environment.api.slideList.base}?${environment.api.slideList.paramSize}=${size}&${environment.api.slideList.paramPage}=${page}`);
    } else {
      return this._http.get<PagedCarouselSlide>(`${environment.api.api_base_path}/${environment.api.slideList.base}`);
    }
  }

  getActive(): Observable<CarouselSlide[]> {
    return this._http.get<CarouselSlide[]>(`${environment.api.api_base_path}/${environment.api.slideList.base}/${environment.api.slideList.active}`);
  }
}
