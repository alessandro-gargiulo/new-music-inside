import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { SlideListService } from 'src/app/services/slide-list.service';
import { CarouselSlide } from 'src/app/shared/carousel-slide.model';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit, OnDestroy {

  public images: CarouselSlide[];

  private _slides$ : Subscription;

  constructor(private _sldSrv : SlideListService) {
  }

  ngOnInit() {
    this._slides$ = this._sldSrv.getActive().subscribe(res => {
      this.images = res;
    });
  }

  ngOnDestroy(): void {
    this._slides$.unsubscribe();
  }

}
