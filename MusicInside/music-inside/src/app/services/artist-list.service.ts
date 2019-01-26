import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { PagedArtistTile } from '../shared/artist.model';

@Injectable({
  providedIn: 'root'
})
export class ArtistListService {

  constructor(private _http: HttpClient) { }

  get(page?: number, size?: number, name?: string): Observable<PagedArtistTile> {
    let srchName = name ? name : '';
    if (page && size) {
      return this._http.get<PagedArtistTile>(`${environment.api.api_base_path}/${environment.api.artistList.base}?${environment.api.artistList.paramName}=${srchName}&${environment.api.artistList}=${size}&${environment.api.artistList.paramPage}=${page}`);
    } else {
      return this._http.get<PagedArtistTile>(`${environment.api.api_base_path}/${environment.api.artistList.base}?${environment.api.artistList.paramName}=${srchName}`);
    }
  }
}
