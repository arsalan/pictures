using ImageOrganizer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ImageOrganizer.DataStore
{
    public class InMemoryDataStore
    {
        public static ICollection<PictureAlbum> PictureAlbums = new List<PictureAlbum>();
        private static ICollection<Picture> Pictures = new List<Picture>();
        static string RandomImageUrl = "http://randomimage.setgetgo.com/get.php";

        static InMemoryDataStore()
        {
            for (var i = 1; i < 6; i++)
            {
                var newAlbum = new PictureAlbum 
                    {
                        Id = Guid.NewGuid(),
                        Name = "Album " + i,
                        Description = "This is Album number " + i,
                        Tags = new List<string> { "album", i.ToString() },
                        Pictures = new List<Picture>()
                    };
                var numberOfPictures = new Random(25).Next(15) + 1;
                for (var j = 1; j < numberOfPictures; j++)
                {
                    newAlbum.Pictures.Add(new Picture 
                        {
                            Id = Guid.NewGuid(),
                            Height = 100 * j,
                            Width = 150 * j,
                            Url = RandomImageUrl,
                            ParentAlbumId = newAlbum.Id
                        }
                    );
                }
                PictureAlbums.Add(newAlbum);
            }

        }
    }
}