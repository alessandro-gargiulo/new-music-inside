export class AlbumTile {
  public id: number;
  public title: string;
  public artist: string;
  public numSongs: number;
  public coverUrl: string;
}

export class PagedAlbumTile {
  public overallCount: number;
  public page: number;
  public pageSize: number;
  public albums: AlbumTile[];
}
