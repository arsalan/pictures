using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ImageOrganizer.Models
{
    public class Picture
    {
        [Key]
        public Guid Id { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string Url { get; set; }
        public virtual Guid ParentAlbumId { get; set; }
    }
}
