import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { PagedAlbumTile } from 'src/app/shared/album-tile.model';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AlbumListService {

  constructor(private _http: HttpClient) { }

  get(page?: number, size?: number, title?: string): Observable<PagedAlbumTile> {
    let srchTtl = title ? title : '';
    if (page && size) {
      return this._http.get<PagedAlbumTile>(`${environment.api.api_base_path}/${environment.api.albumList.base}?${environment.api.albumList.paramTitle}=${srchTtl}&${environment.api.albumList.paramSize}=${size}&${environment.api.albumList.paramPage}=${page}`);
    } else {
      return this._http.get<PagedAlbumTile>(`${environment.api.api_base_path}/${environment.api.albumList.base}?${environment.api.albumList.paramTitle}=${srchTtl}`);
    }
  }
}
