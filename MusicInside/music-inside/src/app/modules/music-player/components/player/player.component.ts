import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { PlaylistTrack } from 'src/app/modules/music-player/components/player/player';
import { MusicPlayerService } from 'src/app/modules/music-player/services/music-player.service';


@Component({
  selector: 'app-music-player',
  templateUrl: './player.component.html',
  styleUrls: ['./player.component.scss']
})
export class PlayerComponent implements OnInit {

  @ViewChild('playerRef') audioPlayer: ElementRef;

  public playlist: Array<PlaylistTrack>;

  public current: PlaylistTrack;

  public isPlayng: boolean;
  public isAudio: boolean;
  public isPlaylist: boolean;
  public disableForward: boolean;

  public cursor: number;

  constructor(private _plrSrv: MusicPlayerService) {
    this.isPlaylist = false;
    this.isPlayng = false;
    this.isAudio = true;
    this.disableForward = false;

    this.cursor = 0;

    this.playlist = new Array<PlaylistTrack>();
    this.current = new PlaylistTrack();
    this.current.songUrl = 'http://localhost:80//C.mp3';
    this.current.fileType = 'audio/mpeg';
  }

  ngOnInit() {
    this._plrSrv.playlist.subscribe(data => {
      this.playlist = data;
      if (this.playlist != null && this.playlist != undefined && this.playlist.length != 0) {
        if (this.cursor >= this.playlist.length) this.cursor = this.playlist.length - 1;
        this.current = this.playlist[this.cursor];
      }
    }, error => {
      console.log("An error occurred when retrieve playlist from service.");
    })
  }

  public togglePlaylist(): void {
    this.isPlaylist = !this.isPlaylist;
  }

  public togglePlay(): void {
    this.isPlayng = !this.isPlayng;
    if (this.isPlayng) this.audioPlayer.nativeElement.play();
    else this.audioPlayer.nativeElement.pause();
  }

  public toggleAudio(): void {
    this.isAudio = !this.isAudio;
    this.audioPlayer.nativeElement.muted = !this.isAudio;
  }

  public skip(direction: number) {
    if (direction != -1 && direction != 1) return;
    this.cursor += direction;
    this.current = this.playlist[this.cursor];
    if (this.cursor == this.playlist.length) this.disableForward = true;
  }
}
