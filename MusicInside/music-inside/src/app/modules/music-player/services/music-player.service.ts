import { Injectable } from '@angular/core';
import { PlaylistTrack } from 'src/app/modules/music-player/components/player/player';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class MusicPlayerService {

  private _playlist: BehaviorSubject<PlaylistTrack[]>;

  private _datastore: {
    tracks: PlaylistTrack[]
  };

  constructor() {
    this._datastore = { tracks : [] }
    this._playlist = new BehaviorSubject([]);
  }

  get playlist() : Observable<PlaylistTrack[]>{
    return this._playlist.asObservable();
  }

  public pushTrack(track: PlaylistTrack): void {
    this._datastore.tracks.push(track);
    this._playlist.next(Object.assign({}, this._datastore).tracks);
  }

  public removeTrack(index: number) {
    this._datastore.tracks.splice(index, 1);
    this._playlist.next(Object.assign({}, this._datastore).tracks);
  }
}
