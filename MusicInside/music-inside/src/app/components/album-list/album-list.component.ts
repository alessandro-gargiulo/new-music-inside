import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Subscription } from 'rxjs';
import { IOptionsBar, IOptionsEvent } from 'src/app/components/options-bar/options-bar.component.types';
import { MusicPlayerService } from 'src/app/modules/music-player/services/music-player.service';
import { AlbumListService } from 'src/app/services/album-list.service';
import { SongListService } from 'src/app/services/song-list.service';
import { AlbumTile } from 'src/app/shared/album.models';
import { PlayableSong } from 'src/app/shared/song.models';
import { SongsModalComponent } from '../songs-modal/songs-modal.component';

@Component({
  selector: 'app-album-list',
  templateUrl: './album-list.component.html',
  styleUrls: ['./album-list.component.scss']
})
export class AlbumListComponent implements OnInit {

  public albums: AlbumTile[];

  public length: number;
  public options: IOptionsBar;

  public searchParameter: string;

  private _albums$: Subscription;
  private _lastSongs: PlayableSong[];

  constructor(
    private _plrSrv: MusicPlayerService,
    private _albLstSrv: AlbumListService,
    private _sngLstSrv: SongListService,
    private _snackBar: MatSnackBar,
    private _modalSrv: NgbModal
  ) {
    this.options = {
      pageSize: 10,
      pageSizeOptions: [5, 10, 25, 100]
    }
  }

  ngOnInit() {
    this.getAlbums(1, this.options.pageSize);
  }

  ngOnDestroy(): void {
    this._albums$.unsubscribe();
  }

  public openModal(albumId: number): void {
    if (albumId < 0) console.log('Album identifier cannot be less than 0');
    this._sngLstSrv.getFromAlbum(albumId).subscribe(songs => {
      this._lastSongs = songs;
      const modalRef = this._modalSrv.open(SongsModalComponent);
      modalRef.componentInstance.title = 'Choose a song...';
      modalRef.componentInstance.closeLabel = 'Close';
      modalRef.componentInstance.songs = songs;
      modalRef.componentInstance.addSong.subscribe((songId) => {
        let selectedSong = this._lastSongs.find(x => x.id === songId);
        if (selectedSong !== null && selectedSong !== undefined) {
          this._plrSrv.pushTrack({
            id: selectedSong.id,
            artist: selectedSong.artist,
            coverUrl: selectedSong.coverUrl,
            fileType: selectedSong.fileType,
            songUrl: selectedSong.fileUrl,
            title: selectedSong.title
          });
          this._snackBar.open(`'${selectedSong.title}' added to playlist.`, 'Dismiss', { duration: 5000 });
        }
      });
    });
  }

  public search(e: IOptionsEvent): void {
    this.getAlbums(1, e.pageSize, e.searchParameter);
  }

  public onPageEvent(e: IOptionsEvent) {
    this.getAlbums(e.pageIndex, e.pageSize, e.searchParameter);
  }

  private getAlbums(page: number, size: number, title?: string): void {
    this._albums$ = this._albLstSrv.get(page, size, title).subscribe(res => {
      this.albums = res.albums;
      this.length = res.overallCount;
    });
  }

}
