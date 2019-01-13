export class CarouselSlide {
  id: number;
  source: string;
  alt: string;
  header: string;
  text: string;
}

export class PagedCarouselSlide {
  public overallCount: number;
  public page: number;
  public pageSize: number;
  public songs: CarouselSlide[];
}
