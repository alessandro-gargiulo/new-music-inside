export class ArtistTile {
  public id: number;
  public artname: string;
  public numSongs: number;
}

export class PagedArtistTile {
  public overallCount: number;
  public page: number;
  public pageSize: number;
  public artists: ArtistTile[];
}
