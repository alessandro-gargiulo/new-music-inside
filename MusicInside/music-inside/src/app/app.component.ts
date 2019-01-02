import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  public title = 'Music Inside';
  public playerOpen: boolean;

  constructor() {
    this.playerOpen = false;
  }

  public togglePlayer(): void {
    this.playerOpen = !this.playerOpen;
  }

}
