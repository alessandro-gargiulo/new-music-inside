// This file can be replaced during build by using the `fileReplacements` array.
// `ng build ---prod` replaces `environment.ts` with `environment.prod.ts`.
// The list of file replacements can be found in `angular.json`.

export const environment = {
  production: false,
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

/*
 * In development mode, to ignore zone related error stack frames such as
 * `zone.run`, `zoneDelegate.invokeTask` for easier debugging, you can
 * import the following file, but please comment it out in production mode
 * because it will have performance impact when throw error
 */
// import 'zone.js/dist/zone-error';  // Included with Angular CLI.
