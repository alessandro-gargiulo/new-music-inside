import { Component, EventEmitter, Input, Output } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { PlayableSong } from 'src/app/shared/song.models';

@Component({
  selector: 'app-songs-modal',
  templateUrl: './songs-modal.component.html',
  styleUrls: ['./songs-modal.component.scss']
})
export class SongsModalComponent {

  @Input() title: string;
  @Input() closeLabel: string;
  @Input() songs: PlayableSong[];

  @Output() addSong: EventEmitter<number> = new EventEmitter();

  constructor(public activeModal: NgbActiveModal) { }

  public addToPlaylist(index: number): void {
    this.addSong.emit(this.songs[index].id);
  }

}
