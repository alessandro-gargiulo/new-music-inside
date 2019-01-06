namespace MusicInside.Shared
{
    public class WebConstants
    {
        public struct ROUTES
        {
            #region Album Controller Routes
            public const string ALBUM_LIST_ROUTE = "api/album-list";
            public const string ALBUM_ROUTE = "api/albums";
            #endregion

            #region Artist Controller Routes
            public const string ARTIST_LIST_ROUTE = "api/artist-list";
            public const string ARTIST_ROUTE = "api/artists";
            #endregion

            #region Song Controller Routes
            public const string SONG_LIST_ROUTE = "api/song-list";
            public const string SONG_ROUTE = "api/songs";
            #endregion
        }

        public struct VALUES
        {
            public const int DEFAULT_ID = -1; // Default id assigned to parameters
        }
    }
}
