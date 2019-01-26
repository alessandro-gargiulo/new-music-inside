export const environment = {
  production: true,
  api: {
    api_base_path: 'api',
    song: {
      base: 'songs',
      fromAlbum: 'fromAlbum',
      fromArtist: 'fromArtist',
      paramId: 'id'
    },
    songList: {
      base: 'song-list',
      paramSize: 'limit',
      paramPage: 'page',
      paramTitle: 'title'
    },
    artist: {
      base: 'artists'
    },
    artistList: {
      base: 'artist-list',
      paramSize: 'limit',
      paramPage: 'page',
      paramName: 'name'
    },
    album: {
      base: 'albums'
    },
    albumList: {
      base: 'album-list',
      paramSize: 'limit',
      paramPage: 'page',
      paramTitle: 'title'
    },
    slideList: {
      base: 'slides-list',
      active: 'active',
      paramSize: 'limit',
      paramPage: 'page'
    }
  }
};
