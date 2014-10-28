using ImageOrganizer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ImageOrganizer.DataStore
{
    public class VersionedInMemoryPictureAlbumRepository : IPictureAlbumsRepository
    {
        private InMemoryPictureAlbumRepository decoratedInMemoyRepository;
        private int version;

        public VersionedInMemoryPictureAlbumRepository(int version)
        {
            switch(version)
            {
                case 1:
                    this.decoratedInMemoyRepository = new Version1InMemoryPictureAlbumRepository();
                    break;
                default:
                    this.decoratedInMemoyRepository = new InMemoryPictureAlbumRepository();
                    break;
            }
            
            this.version = version;
        }

        public IQueryable<Picture> GetPictures(Guid pictureAlbumId)
        {
            return this.decoratedInMemoyRepository.GetPictures(pictureAlbumId);
        }

        public Picture GetPicture(Guid pictureAlbumId, Guid pictureId)
        {
            return this.decoratedInMemoyRepository.GetPicture(pictureAlbumId, pictureId);
        }

        public Guid AddPicture(Guid pictureAlbumId, Picture picture)
        {
            return this.decoratedInMemoyRepository.AddPicture(pictureAlbumId, picture);
        }

        public Guid UpdatePicture(Guid pictureAlbumId, Picture picture)
        {
            return this.decoratedInMemoyRepository.UpdatePicture(pictureAlbumId, picture);
        }

        public void DeletePicture(Guid pictureAlbumId, Guid pictureId)
        {
            this.decoratedInMemoyRepository.DeletePicture(pictureAlbumId, pictureId);
        }

        public IQueryable<dynamic> GetPictureAlbums(string name = "", ICollection<string> tags = null)
        {
            return this.decoratedInMemoyRepository.GetPictureAlbums(name, tags);
        }

        public dynamic GetPictureAlbum(Guid pictureAlbumId)
        {
            return this.decoratedInMemoyRepository.GetPictureAlbum(pictureAlbumId);
        }

        public Guid AddPictureAlbum(PictureAlbum pictureAlbum)
        {
            return this.decoratedInMemoyRepository.AddPictureAlbum(pictureAlbum);
        }

        public Guid UpdatePictureAlbum(PictureAlbum pictureAlbum)
        {
            return this.decoratedInMemoyRepository.UpdatePictureAlbum(pictureAlbum);
        }

        public void DeletePictureAlbum(Guid pictureAlbumId)
        {
            this.decoratedInMemoyRepository.DeletePictureAlbum(pictureAlbumId);
        }
    }
}