using ImageOrganizer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ImageOrganizer.DataStore
{
    public interface IPictureAlbumsRepository
    {
        IQueryable<Picture> GetPictures(Guid pictureAlbumId);
        Picture GetPicture(Guid pictureAlbumId, Guid pictureId);
        Guid AddPicture(Guid pictureAlbumId, Picture picture);
        Guid UpdatePicture(Guid pictureAlbumId, Picture picture);
        void DeletePicture(Guid pictureAlbumId, Guid pictureId);

        IQueryable<dynamic> GetPictureAlbums(string name = "", ICollection<string> tags = null);
        dynamic GetPictureAlbum(Guid pictureAlbumId);
        Guid AddPictureAlbum(dynamic pictureAlbum);
        Guid UpdatePictureAlbum(dynamic pictureAlbum);
        void DeletePictureAlbum(Guid pictureAlbumId);
    }
}