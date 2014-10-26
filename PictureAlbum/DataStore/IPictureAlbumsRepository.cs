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
        Picture GetPicture(Guid pictureAlbumId, Guid pictreId);
        Guid AddPicture(Guid pictureAlbumId, Picture picture);
        Guid UpdatePicture(Guid pictureAlbumId, Picture picture);
        void DeletePicture(Guid pictureAlbumId, Guid pictureId);

        IQueryable<PictureAlbum> GetPictureAlbums(string name = "", ICollection<string> tags = null);
        PictureAlbum GetPictureAlbum(Guid pictureAlbumId);
        Guid AddPictureAlbum(PictureAlbum pictureAlbum);
        Guid UpdatePictureAlbum(PictureAlbum pictureAlbum);
        void DeletePictureAlbum(Guid pictureAlbumId);
    }
}