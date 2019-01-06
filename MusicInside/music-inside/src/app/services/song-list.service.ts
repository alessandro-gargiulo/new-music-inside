import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { PagedSongTile } from 'src/app/shared/song-tile.model';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class SongListService {

  constructor(private _http: HttpClient) { }

  get(page?: number, size?: number, title?: string): Observable<PagedSongTile> {
    let srchTtl = title ? title : '';
    if (page && size) {
      return this._http.get<PagedSongTile>(`${environment.api.api_base_path}/${environment.api.songList.base}?${environment.api.songList.paramTitle}=${srchTtl}&${environment.api.songList.paramSize}=${size}&${environment.api.songList.paramPage}=${page}`);
    } else {
      return this._http.get<PagedSongTile>(`${environment.api.api_base_path}/${environment.api.songList.base}?${environment.api.songList.paramTitle}=${srchTtl}`);
    }
  }
}
