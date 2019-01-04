import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatSnackBarModule, MatSnackBar, MatSnackBarContainer } from '@angular/material/snack-bar';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { OverlayModule } from "@angular/cdk/overlay";
import { NgbCarouselModule } from "@ng-bootstrap/ng-bootstrap"

import { AppComponent } from 'src/app/app.component';
import { MusicPlayerModule } from 'src/app/modules/music-player/music-player.module';
import { SidebarComponent } from 'src/app/components/sidebar/sidebar.component';
import { HomeComponent } from 'src/app/components/home/home.component';
import { PageNotFoundComponent } from 'src/app/components/page-not-found/page-not-found.component';
import { AppRoutingModule } from 'src/app/app-routing.module';
import { SongListComponent } from 'src/app/components/song-list/song-list.component';
import { AlbumListComponent } from 'src/app/components/album-list/album-list.component';
import { ArtistListComponent } from 'src/app/components/artist-list/artist-list.component';
import { AboutMeComponent } from 'src/app/components/about-me/about-me.component';

@NgModule({
  declarations: [
    AppComponent,
    SidebarComponent,
    HomeComponent,
    PageNotFoundComponent,
    SongListComponent,
    AlbumListComponent,
    AboutMeComponent,
    ArtistListComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MusicPlayerModule.forRoot(),
    MatGridListModule,
    MatSnackBarModule,
    NgbCarouselModule,
    OverlayModule
  ],
  providers: [
    MatSnackBar
  ],
  entryComponents: [
    MatSnackBarContainer
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
