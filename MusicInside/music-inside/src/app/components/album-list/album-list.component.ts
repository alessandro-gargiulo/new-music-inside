import { Component, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { AlbumTile } from 'src/app/shared/album-tile.model';
import { MusicPlayerService } from 'src/app/modules/music-player/services/music-player.service';
import { MatSnackBar, PageEvent } from '@angular/material';
import { AlbumListService } from 'src/app/services/album-list.service';

@Component({
  selector: 'app-album-list',
  templateUrl: './album-list.component.html',
  styleUrls: ['./album-list.component.scss']
})
export class AlbumListComponent implements OnInit {

  public albums: AlbumTile[];

  public length: number;
  public pageSizeOptions: number[] = [5, 10, 25, 100];
  public pageSize: number = 10;

  public searchParameter: string;

  private _albums$: Subscription;

  constructor(
    private _plrSrv: MusicPlayerService,
    private _albLstSrv: AlbumListService,
    private _snackBar: MatSnackBar
  ) {
    this.searchParameter = '';
  }

  ngOnInit() {
    this.getAlbums(1, this.pageSize);
  }

  ngOnDestroy(): void {
    this._albums$.unsubscribe();
  }

  public search(): void {
    this.getAlbums(1, this.pageSize, this.searchParameter);
  }

  public onPageEvent(e: PageEvent) {
    this.pageSize = e.pageSize;
    this.getAlbums(e.pageIndex + 1, e.pageSize, this.searchParameter);
  }

  private getAlbums(page: number, size: number, title?: string): void {
    this._albums$ = this._albLstSrv.get(page, size, title).subscribe(res => {
      this.albums = res.albums;
      this.length = res.overallCount;
    });
  }

}
