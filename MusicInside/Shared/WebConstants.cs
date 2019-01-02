namespace MusicInside.Shared
{
    public class WebConstants
    {
        public struct ROUTES
        {
            #region Album Controller Routes
            public const string ALBUM_ROUTE = "api/albums";
            public const string ALBUM_SUB_LIST_ROUTE = "list"; // www.site.domain/api/albums/list
            public const string ALBUM_SUB_DETAIL_ROUTE = "list/{id}"; // www.site.domain/api/albums/list/4
            public const string ALBUM_SUB_COVER_ROUTE = "cover/{id}"; // www.site.domain/api/albums/cover/4
            #endregion

            #region Artist Controller Routes
            public const string ARTIST_ROUTE = "api/artist";
            #endregion

            #region Song Controller Routes
            public const string SONG_ROUTE = "api/songs";
            public const string SONG_SUB_LIST_ROUTE = "list"; // www.site.domain/api/songs/list
            public const string SONG_SUB_DETAIL_ROUTE = "list/{id}"; // www.site.domain/api/songs/list/4
            #endregion
        }

        public struct VALUES
        {
            public const int DEFAULT_ID = -1; // Default id assigned to parameters
        }
    }
}
