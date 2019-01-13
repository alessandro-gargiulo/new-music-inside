import { Component, OnInit, OnDestroy } from '@angular/core';
import { SongTile } from 'src/app/shared/song-tile.model';
import { MusicPlayerService } from 'src/app/modules/music-player/services/music-player.service';
import { MatSnackBar, PageEvent } from '@angular/material';
import { Subscription } from 'rxjs';
import { SongListService } from 'src/app/services/song-list.service';

@Component({
  selector: 'app-song-list',
  templateUrl: './song-list.component.html',
  styleUrls: ['./song-list.component.scss']
})
export class SongListComponent implements OnInit, OnDestroy {

  public songs: SongTile[];

  public length: number;
  public pageSizeOptions: number[] = [5, 10, 25, 100];
  public pageSize: number = 10;

  public searchParameter: string;

  private _songs$: Subscription;

  constructor
    (
    private _plrSrv: MusicPlayerService,
    private _sngLstSrv: SongListService,
    private _snackBar: MatSnackBar
  ) {
    this.searchParameter = '';
  }

  ngOnInit() {
    this.getSongs(1, this.pageSize);
  }

  ngOnDestroy(): void {
    this._songs$.unsubscribe();
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

  public search(): void {
    this.getSongs(1, this.pageSize, this.searchParameter);
  }

  public onPageEvent(e: PageEvent) {
    this.pageSize = e.pageSize;
    this.getSongs(e.pageIndex + 1, e.pageSize, this.searchParameter);
  }

  private getSongs(page: number, size: number, title?: string): void {
    this._songs$ = this._sngLstSrv.get(page, size, title).subscribe(res => {
      this.songs = res.songs;
      this.length = res.overallCount;
    });
  }
}
