import { Component, OnInit, OnDestroy } from '@angular/core';
import { SongTile } from 'src/app/shared/song-tile.model';
import { MusicPlayerService } from 'src/app/modules/music-player/services/music-player.service';
import { MatSnackBar, PageEvent } from '@angular/material';
import { Subscription } from 'rxjs';
import { SongListService } from 'src/app/services/song-list.service';
import { IOptionsEvent, IOptionsBar } from 'src/app/components/options-bar/options-bar.component.types';

@Component({
  selector: 'app-song-list',
  templateUrl: './song-list.component.html',
  styleUrls: ['./song-list.component.scss']
})
export class SongListComponent implements OnInit, OnDestroy {

  public songs: SongTile[];

  public length: number;
  public options: IOptionsBar;

  private _songs$: Subscription;

  constructor
    (
    private _plrSrv: MusicPlayerService,
    private _sngLstSrv: SongListService,
    private _snackBar: MatSnackBar
  ) {
    this.options = {
      pageSize: 10,
      pageSizeOptions: [5, 10, 25, 100]
    }
  }

  ngOnInit() {
    this.getSongs(1, this.options.pageSize);
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

  public search(e: IOptionsEvent): void {
    this.getSongs(1, e.pageSize, e.searchParameter);
  }

  public onPageEvent(e: IOptionsEvent) {
    this.getSongs(e.pageIndex, e.pageSize, e.searchParameter);
  }

  private getSongs(page: number, size: number, title?: string): void {
    this._songs$ = this._sngLstSrv.get(page, size, title).subscribe(res => {
      this.songs = res.songs;
      this.length = res.overallCount;
    });
  }
}
