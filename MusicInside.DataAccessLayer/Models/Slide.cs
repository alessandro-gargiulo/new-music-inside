using System;

namespace MusicInside.DataAccessLayer.Models
{
    public class Slide
    {
        #region Properties
        public int Id { get; set; }
        public string Source { get; set; }
        public DateTime? ValidityFrom { get; set; }
        public DateTime? ValidityTo { get; set; }
        public string Section { get; set; }
        public string Header { get; set; }
        public string Text { get; set; }
        public string AltText { get; set; }
        public int? Order { get; set; }
        #endregion
    }
}
