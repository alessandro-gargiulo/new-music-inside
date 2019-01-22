import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material';
import { Subscription } from 'rxjs';
import { IOptionsBar, IOptionsEvent } from 'src/app/components/options-bar/options-bar.component.types';
import { MusicPlayerService } from 'src/app/modules/music-player/services/music-player.service';
import { AlbumListService } from 'src/app/services/album-list.service';
import { AlbumTile } from 'src/app/shared/album-tile.model';

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

  constructor(
    private _plrSrv: MusicPlayerService,
    private _albLstSrv: AlbumListService,
    private _snackBar: MatSnackBar
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
