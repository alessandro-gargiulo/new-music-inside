import { OverlayModule } from "@angular/cdk/overlay";
import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MatButtonModule, MatInputModule, MatPaginatorModule } from '@angular/material';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatSnackBar, MatSnackBarContainer, MatSnackBarModule } from '@angular/material/snack-bar';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgbCarouselModule, NgbModalModule } from "@ng-bootstrap/ng-bootstrap";
import { AppRoutingModule } from 'src/app/app-routing.module';
import { AppComponent } from 'src/app/app.component';
import { AboutMeComponent } from 'src/app/components/about-me/about-me.component';
import { AlbumListComponent } from 'src/app/components/album-list/album-list.component';
import { ArtistListComponent } from 'src/app/components/artist-list/artist-list.component';
import { HomeComponent } from 'src/app/components/home/home.component';
import { PageNotFoundComponent } from 'src/app/components/page-not-found/page-not-found.component';
import { SidebarComponent } from 'src/app/components/sidebar/sidebar.component';
import { SongListComponent } from 'src/app/components/song-list/song-list.component';
import { MusicPlayerModule } from 'src/app/modules/music-player/music-player.module';
import { AlbumListService } from 'src/app/services/album-list.service';
import { ArtistListService } from 'src/app/services/artist-list.service';
import { SongListService } from 'src/app/services/song-list.service';
import { OptionsBarComponent } from './components/options-bar/options-bar.component';
import { SongsModalComponent } from './components/songs-modal/songs-modal.component';
import { SlideListService } from './services/slide-list.service';

@NgModule({
  declarations: [
    AppComponent,
    SidebarComponent,
    HomeComponent,
    PageNotFoundComponent,
    SongListComponent,
    AlbumListComponent,
    AboutMeComponent,
    ArtistListComponent,
    OptionsBarComponent,
    SongsModalComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    MusicPlayerModule.forRoot(),
    MatGridListModule,
    MatSnackBarModule,
    MatPaginatorModule,
    MatInputModule,
    MatButtonModule,
    MatFormFieldModule,
    NgbCarouselModule,
    OverlayModule,
    FormsModule,
    NgbModalModule
  ],
  providers: [
    SongListService,
    SlideListService,
    AlbumListService,
    ArtistListService,
    MatSnackBar
  ],
  entryComponents: [
    MatSnackBarContainer,
    SongsModalComponent
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
