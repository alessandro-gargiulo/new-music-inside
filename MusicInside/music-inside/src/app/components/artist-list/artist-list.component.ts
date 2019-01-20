import { Component, OnInit } from '@angular/core';
import { MusicPlayerService } from 'src/app/modules/music-player/services/music-player.service';

@Component({
  selector: 'app-artist-list',
  templateUrl: './artist-list.component.html',
  styleUrls: ['./artist-list.component.scss']
})
export class ArtistListComponent implements OnInit {

  public length: number;
  public pageSizeOptions: number[] = [5, 10, 25, 100];
  public pageSize: number = 10;

  public searchParameter: string;

  constructor(
    private _plrSrv: MusicPlayerService
  ) { }

  ngOnInit() {
  }


}
