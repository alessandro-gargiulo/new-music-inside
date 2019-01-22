import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material';
import { IOptionsBar, IOptionsEvent } from 'src/app/components/options-bar/options-bar.component.types';
import { MusicPlayerService } from 'src/app/modules/music-player/services/music-player.service';

@Component({
  selector: 'app-artist-list',
  templateUrl: './artist-list.component.html',
  styleUrls: ['./artist-list.component.scss']
})
export class ArtistListComponent implements OnInit {

  public length: number;
  public options: IOptionsBar;

  public searchParameter: string;

  constructor(
    private _plrSrv: MusicPlayerService,
    private _snackBar: MatSnackBar
  ) {
    this.options = {
      pageSize: 10,
      pageSizeOptions: [5, 10, 25, 100]
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

  private getArtists(page: number, size: number, title?: string): void {

  }
}
