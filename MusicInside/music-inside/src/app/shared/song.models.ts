export class SongTile {
  public id: number;
  public title: string;
  public artist: string;
  public album: string;
  public genre: string;
  public coverUrl: string;
  public fileUrl: string;
  public fileType: string;
  public statCount: number;
  public statWhen: string;
}

export class PagedSongTile {
  public overallCount: number;
  public page: number;
  public pageSize: number;
  public songs: SongTile[];
}

export class PlayableSong {
  public id: number;
  public title: string;
  public artist: string;
  public coverUrl: string;
  public fileUrl: string;
  public fileType: string;
}
