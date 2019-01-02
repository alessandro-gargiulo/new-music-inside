import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PlayerComponent } from './components/player/player.component';
import { ModuleWithProviders } from '@angular/core';
import { MusicPlayerService } from 'src/app/modules/music-player/services/music-player.service';

@NgModule({
  imports: [
    CommonModule
  ],
  declarations: [
    PlayerComponent
  ],
  exports: [
    PlayerComponent
  ]
})
export class MusicPlayerModule {
  static forRoot(): ModuleWithProviders {
    return {
      ngModule: MusicPlayerModule,
      providers: [
        MusicPlayerService
      ]
    };
  }
}
