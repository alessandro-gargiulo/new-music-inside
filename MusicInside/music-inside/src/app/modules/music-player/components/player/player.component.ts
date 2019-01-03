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
  public currentCoverUrl: string;
  public currentTime: string;
  public currentTotalTime: string;

  public isPlayng: boolean;
  public isAudio: boolean;
  public isPlaylist: boolean;

  public disableBackward: boolean;
  public disableForward: boolean;
  public disablePlay: boolean;

  public cursor: number;
  public barMarginLeft: number;

  constructor(private _plrSrv: MusicPlayerService) {
    this.isPlaylist = false;
    this.isPlayng = false;
    this.isAudio = true;

    this.disableBackward = false;
    this.disableForward = false;
    this.disablePlay = false;

    this.cursor = 0;
    this.barMarginLeft = -320;

    this.playlist = new Array<PlaylistTrack>();
    this.currentTitle = '';
    this.currentArtist = '';
    this.currentCoverUrl = 'assets//music_placeholder.png';
    this.currentTime = '0:00';
    this.currentTotalTime = '-:--';
  }

  ngOnInit() {
    this._plrSrv.playlist.subscribe(data => {
      this.playlist = data;
      // Check if data is not null and contains at least one element
      if (this.playlist != null && this.playlist != undefined && this.playlist.length != 0) {
        // Data contains at least one element
        if (this.cursor >= this.playlist.length) {
          // If the cursor point to a removed element, point to the last element
          this.cursor = this.playlist.length - 1;
        }
        // Set the current song
        this.currentTitle = this.playlist[this.cursor].title;
        this.currentArtist = this.playlist[this.cursor].artist;
        this.currentCoverUrl = this.playlist[this.cursor].coverUrl;
        this.setAudio(this.playlist[this.cursor].songUrl, this.playlist[this.cursor].fileType);

        // Check if the cursor points to the last element of the list
        if (this.cursor == this.playlist.length - 1) {
          // The cursor points to last element, disable forward
          this.disableForward = true;
        } else {
          this.disableForward = false;
        }
        // The list contains more than one element: activate controls
        this.disableBackward = false;
        this.disablePlay = false;
      } else {
        // Data is empty: disable all controls
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
    if (this.cursor == 0 && direction == -1) return;
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
    this.currentCoverUrl = this.playlist[this.cursor].coverUrl;
    this.setAudio(this.playlist[this.cursor].songUrl, this.playlist[this.cursor].fileType);
    if (this.cursor == this.playlist.length - 1) this.disableForward = true;
    else this.disableForward = false;
  }

  public removeFromPlaylist(index: number) {
    this._plrSrv.removeTrack(index);
  }

  public updatePosition(): void {
    let totalSeconds = this.audioPlayer.nativeElement.currentTime;
    let minutes = Math.floor(totalSeconds / 60);
    var seconds = Math.floor(totalSeconds - minutes * 60);
    this.currentTime = minutes.toString().padStart(2, '0') + ':' + seconds.toString().padStart(2, '0');

    try {
      let perc = Math.floor((totalSeconds * 100) / this.audioPlayer.nativeElement.seekable.end(0));
      let toDiff = -3.2 * perc;
      this.barMarginLeft = -320 - toDiff;
      console.log(`[perc=${perc}][toDiff=${toDiff}][margin=${this.barMarginLeft}]`);
    }
    catch (e) {
      console.log(e);
    }
  }

  public updateDuration(): void {
    let totalSeconds = this.audioPlayer.nativeElement.seekable.end(0);
    let minutes = Math.floor(totalSeconds / 60);
    var seconds = Math.floor(totalSeconds - minutes * 60);
    this.currentTotalTime = minutes.toString().padStart(2, '0') + ':' + seconds.toString().padStart(2, '0');
  }

  private setAudio(src: string, type: string) {
    this.audioPlayer.nativeElement.src = src;
    this.currentTime = '0:00';
    // TODO: Set media type
  }
}
