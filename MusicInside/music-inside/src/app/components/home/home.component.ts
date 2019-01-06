import { Component, OnInit, OnDestroy } from '@angular/core';
import { CarouselSlide } from 'src/app/shared/carousel-slide';
import { Subscription } from 'rxjs';
import { SlideListService } from 'src/app/services/slide-list.service';

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
