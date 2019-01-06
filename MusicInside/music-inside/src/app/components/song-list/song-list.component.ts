import { Component, OnInit } from '@angular/core';
import { SongTile } from 'src/app/shared/song-tile.model';
import { MusicPlayerService } from 'src/app/modules/music-player/services/music-player.service';
import { MatSnackBar } from '@angular/material';

@Component({
  selector: 'app-song-list',
  templateUrl: './song-list.component.html',
  styleUrls: ['./song-list.component.scss']
})
export class SongListComponent implements OnInit {

  public songs: Array<SongTile>;

  constructor
    (
    private _plrSrv: MusicPlayerService,
    private _snackBar: MatSnackBar
  ) {
    this.songs = new Array<SongTile>();
    this.songs.push({
      id: 0,
      title: 'Behind those eyes',
      artist: '3 Doors Down',
      album: 'Titolo Album',
      genre: 'Pop',
      coverUrl: 'http://localhost:80//2_1.png',
      fileUrl: 'http://localhost:80//A.mp3',
      fileType: 'audio/mpeg',
      statCount: 4,
      statWhen: '25 Dec 2018'
    });
    this.songs.push({
      id: 1,
      title: 'Be Like That',
      artist: '3 Doors Down',
      album: 'Titolo Album',
      genre: 'Pop',
      coverUrl: 'http://localhost:80//4_2.png',
      fileUrl: 'http://localhost:80//B.mp3',
      fileType: 'audio/mpeg',
      statCount: 4,
      statWhen: '25 Dec 2018'
    });
    this.songs.push({
      id: 2,
      title: 'Around The World',
      artist: 'Daft Punk',
      album: 'Titolo Album',
      genre: 'Pop',
      coverUrl: 'http://localhost:80//6_3.png',
      fileUrl: 'http://localhost:80//C.mp3',
      fileType: 'audio/mpeg',
      statCount: 4,
      statWhen: '25 Dec 2018'
    });
  }

  ngOnInit() {
  }

  public addToPlaylist(index: number): void {
    this._plrSrv.pushTrack({
      title: this.songs[index].title,
      artist: this.songs[index].artist,
      coverUrl: this.songs[index].coverUrl,
      songUrl: this.songs[index].fileUrl,
      fileType: this.songs[index].fileType
    });
    this._snackBar.open(`${this.songs[index].title} added to playlist.`, 'Dismiss', { duration: 5000 });
  }

  public getCssBackgroundRuleUrl(url: string): string {
    return `url(${url})`;
  }
}
