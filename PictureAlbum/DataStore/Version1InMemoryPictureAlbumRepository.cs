using ImageOrganizer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ImageOrganizer.DataStore
{
    public class Version1InMemoryPictureAlbumRepository : InMemoryPictureAlbumRepository
    {
        public override Guid AddPicture(Guid pictureAlbumId, Picture picture)
        {
            return base.AddPicture(pictureAlbumId, picture);
        }

        public override Guid AddPictureAlbum(dynamic pictureAlbum)
        {
            return base.AddPictureAlbum(pictureAlbum as PictureAlbum);
        }

        public override void DeletePicture(Guid pictureAlbumId, Guid pictureId)
        {
            base.DeletePicture(pictureAlbumId, pictureId);
        }

        public override void DeletePictureAlbum(Guid pictureAlbumId)
        {
            base.DeletePictureAlbum(pictureAlbumId);
        }

        public override Picture GetPicture(Guid pictureAlbumId, Guid pictureId)
        {
            return base.GetPicture(pictureAlbumId, pictureId);
        }

        public override dynamic GetPictureAlbum(Guid pictureAlbumId)
        {
            var album = base.GetPictureAlbum(pictureAlbumId) as PictureAlbum;
            return new PictureAlbumV1 { Id = album.Id, Name = album.Name, Description = album.Description };
        }

        public override IQueryable<dynamic> GetPictureAlbums(string name = "", ICollection<string> tags = null)
        {
            var albums = base.GetPictureAlbums() as IQueryable<PictureAlbum>;
            return albums.Select(a => new PictureAlbumV1 {Id = a.Id, Name = a.Name, Description = a.Description});
        }

        public override IQueryable<Picture> GetPictures(Guid pictureAlbumId)
        {
            return base.GetPictures(pictureAlbumId);
        }

        public override Guid UpdatePicture(Guid pictureAlbumId, Picture picture)
        {
            return base.UpdatePicture(pictureAlbumId, picture);
        }

        public override Guid UpdatePictureAlbum(dynamic pictureAlbum)
        {
            var standardizedAlbum = new PictureAlbum();
            standardizedAlbum.Id = pictureAlbum.Id;
            standardizedAlbum.Name = pictureAlbum.Name;
            standardizedAlbum.Description = pictureAlbum.Description;
            return base.UpdatePictureAlbum(standardizedAlbum);
        }
    }
}