import { Component, OnInit } from '@angular/core';
import { CarouselSlide } from 'src/app/shared/carousel-slide';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  public images: CarouselSlide[];

  constructor() {
    this.images = [];
    this.images.push({
      src: `https://picsum.photos/900/500?random&t=${Math.random()}`,
      alt: 'example',
      header: 'First slide label',
      text: 'Nulla vitae elit libero, a pharetra augue mollis interdum.'
    });
    this.images.push({
      src: `https://picsum.photos/900/500?random&t=${Math.random()}`,
      alt: 'example',
      header: 'Second slide label',
      text: 'Lorem ipsum dolor sit amet, consectetur adipiscing elit.'
    });
    this.images.push({
      src: `https://picsum.photos/900/500?random&t=${Math.random()}`,
      alt: 'example',
      header: 'Third slide label',
      text: 'Praesent commodo cursus magna, vel scelerisque nisl consectetur.'
    });
  }

  ngOnInit() {
  }

}
