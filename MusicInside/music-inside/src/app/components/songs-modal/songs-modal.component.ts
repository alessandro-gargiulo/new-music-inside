import { Component, Input } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { SongModal } from 'src/app/shared/song-modal.model';

@Component({
  selector: 'app-songs-modal',
  templateUrl: './songs-modal.component.html',
  styleUrls: ['./songs-modal.component.scss']
})
export class SongsModalComponent {

  @Input() title: string;
  @Input() closeLabel: string;
  @Input() songs: SongModal[];

  constructor(public activeModal: NgbActiveModal) { }

}
