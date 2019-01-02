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

  public currentTitle: string;
  public currentArtist: string;
  public currentTime: string;
  public currentTotalTime: string;

  public isPlayng: boolean;
  public isAudio: boolean;
  public isPlaylist: boolean;

  public disableBackward: boolean;
  public disableForward: boolean;
  public disablePlay: boolean;

  public cursor: number;

  constructor(private _plrSrv: MusicPlayerService) {
    this.isPlaylist = false;
    this.isPlayng = false;
    this.isAudio = true;

    this.disableBackward = false;
    this.disableForward = false;
    this.disablePlay = false;

    this.cursor = 0;

    this.playlist = new Array<PlaylistTrack>();
    this.currentTitle = '';
    this.currentArtist = '';
    this.currentTime = '0:00';
    this.currentTotalTime = '-:--';
  }

  ngOnInit() {
    this._plrSrv.playlist.subscribe(data => {
      this.playlist = data;
      if (this.playlist != null && this.playlist != undefined && this.playlist.length != 0) {
        if (this.cursor >= this.playlist.length) this.cursor = this.playlist.length - 1;
        this.currentTitle = this.playlist[this.cursor].title;
        this.currentArtist = this.playlist[this.cursor].artist;
        this.setAudio(this.playlist[this.cursor].songUrl, this.playlist[this.cursor].fileType);
        if (this.cursor < this.playlist.length - 1) this.disableForward = false;
        this.disableBackward = false;
        this.disablePlay = false;
      } else {
        this.disableBackward = true;
        this.disableForward = true;
        this.disablePlay = true;
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
    if (direction == 1 && this.disableForward) return;

    if (this.isPlayng) {
      switch (direction) {
        case -1:
          this.audioPlayer.nativeElement.currentTime = 0;
          return;
        case 1:
          this.togglePlay();
          break;
        default:
          break;
      }
    }

    this.cursor += direction;
    this.currentTitle = this.playlist[this.cursor].title;
    this.currentArtist = this.playlist[this.cursor].artist;
    this.setAudio(this.playlist[this.cursor].songUrl, this.playlist[this.cursor].fileType);
    if (this.cursor == this.playlist.length - 1) this.disableForward = true;
    else this.disableForward = false;
  }

  public removeFromPlaylist(index: number) {
    this._plrSrv.removeTrack(index);
  }

  private setAudio(src: string, type: string) {
    this.audioPlayer.nativeElement.src = src;
    // TODO: Set media type
  }
}
