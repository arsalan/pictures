using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ImageOrganizer.Models
{
    [JsonObject(Title = "pictureAlbums")]
    public class PictureAlbum
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IList<string> Tags { get; set; }
        public int PictureCount { get; set; }
        public virtual ICollection<Picture> Pictures { get; set; }
    }
}