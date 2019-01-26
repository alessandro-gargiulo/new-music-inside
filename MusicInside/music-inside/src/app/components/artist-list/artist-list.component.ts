import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material';
import { IOptionsBar, IOptionsEvent } from 'src/app/components/options-bar/options-bar.component.types';
import { MusicPlayerService } from 'src/app/modules/music-player/services/music-player.service';
import { ArtistListService } from 'src/app/services/artist-list.service';
import { ArtistTile } from 'src/app/shared/artist.model';

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

  constructor(
    private _artSrv: ArtistListService,
    private _plrSrv: MusicPlayerService,
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
