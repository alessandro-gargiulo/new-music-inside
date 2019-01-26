import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { IOptionsBar, IOptionsEvent } from 'src/app/components/options-bar/options-bar.component.types';
import { MusicPlayerService } from 'src/app/modules/music-player/services/music-player.service';
import { ArtistListService } from 'src/app/services/artist-list.service';
import { SongListService } from 'src/app/services/song-list.service';
import { ArtistTile } from 'src/app/shared/artist.models';
import { PlayableSong } from 'src/app/shared/song.models';
import { SongsModalComponent } from '../songs-modal/songs-modal.component';

@Component({
  selector: 'app-artist-list',
  templateUrl: './artist-list.component.html',
  styleUrls: ['./artist-list.component.scss']
})
export class ArtistListComponent implements OnInit {

  public length: number;
  public options: IOptionsBar;

  public searchParameter: string;

  public artists: ArtistTile[];

  private _lastSongs: PlayableSong[];

  constructor(
    private _artSrv: ArtistListService,
    private _sngLstSrv: SongListService,
    private _plrSrv: MusicPlayerService,
    private _modalSrv: NgbModal,
    private _snackBar: MatSnackBar
  ) {
    this.options = {
      pageSize: 25,
      pageSizeOptions: [10, 25, 100, 200]
    }
  }

  ngOnInit() {
    this.getArtists(1, this.options.pageSize);
  }

  public openModal(artistId: number): void {
    if (artistId < 0) console.log('Album identifier cannot be less than 0');
    this._sngLstSrv.getFromArtist(artistId).subscribe(songs => {
      this._lastSongs = songs;
      const modalRef = this._modalSrv.open(SongsModalComponent);
      modalRef.componentInstance.title = 'Choose a song...';
      modalRef.componentInstance.closeLabel = 'Close';
      modalRef.componentInstance.songs = songs;
      modalRef.componentInstance.addSong.subscribe((songId) => {
        let selectedSong = this._lastSongs.find(x => x.id === songId);
        if (selectedSong !== null && selectedSong !== undefined) {
          this._plrSrv.pushTrack({
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

  public getInitials(name: string): string {
    if (name === null || name === undefined) return 'N.A.';
    let splits: string[] = name.split(/[ ]+/);
    let initials: string = '';
    splits.map((element, $i, array) => {
      initials = initials + element.charAt(0);
    });
    return initials;
  }

  public search(e: IOptionsEvent): void {
    this.getArtists(1, e.pageSize, e.searchParameter);
  }

  public onPageEvent(e: IOptionsEvent) {
    this.getArtists(e.pageIndex, e.pageSize, e.searchParameter);
  }

  private getArtists(page: number, size: number, name?: string): void {
    this._artSrv.get(page, size, name).subscribe((res) => {
      this.artists = res.artists;
      this.length = res.overallCount;
    });
  }
}
