using ImageOrganizer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ImageOrganizer.DataStore
{
    public class InMemoryPictureAlbumRepository : IPictureAlbumsRepository
    {
        public virtual IQueryable<Picture> GetPictures(Guid pictureAlbumId)
        {
            var album = InMemoryDataStore.PictureAlbums.FirstOrDefault(a => a.Id == pictureAlbumId);
            if (album != null)
            {
                return album.Pictures.AsQueryable();
            }
            return null;
        }

        public virtual Picture GetPicture(Guid pictureAlbumId, Guid pictureId)
        {
            var album = InMemoryDataStore.PictureAlbums.FirstOrDefault(a => a.Id == pictureAlbumId);
            if (album != null)
            {
                return album.Pictures.FirstOrDefault(p => p.Id == pictureId);
            }
            return null;
        }

        public virtual Guid AddPicture(Guid pictureAlbumId, Picture picture)
        {
            picture.Id = Guid.NewGuid();
            picture.ParentAlbumId = pictureAlbumId;
            var album = InMemoryDataStore.PictureAlbums.FirstOrDefault(a => a.Id == pictureAlbumId);
            if (album != null)
            {
                album.Pictures.Add(picture);
                album.PictureCount = album.Pictures.Count;
                return picture.Id;
            }
            return Guid.Empty;
        }

        public virtual Guid UpdatePicture(Guid pictureAlbumId, Picture picture)
        {
            var album = InMemoryDataStore.PictureAlbums.FirstOrDefault(a => a.Id == pictureAlbumId);
            var toUpdate = album.Pictures.FirstOrDefault(p => p.Id == picture.Id);
            if (toUpdate != null)
            {
                toUpdate.Height = picture.Height;
                toUpdate.Width = picture.Width;
                toUpdate.Url = picture.Url;
                toUpdate.ParentAlbumId = pictureAlbumId;
                return picture.Id;
            }
            return Guid.Empty;
        }

        public virtual void DeletePicture(Guid pictureAlbumId, Guid pictureId)
        {
            var album = InMemoryDataStore.PictureAlbums.FirstOrDefault(a => a.Id == pictureAlbumId);
            if (album != null)
            {
                var toRemove = album.Pictures.FirstOrDefault(p => p.Id == pictureId);
                if (toRemove != null)
                {
                    album.Pictures.Remove(toRemove);
                    album.PictureCount = album.Pictures.Count;
                }
            }
        }

        public virtual IQueryable<dynamic> GetPictureAlbums(string name = "", ICollection<string> tags = null)
        {
            if (string.IsNullOrWhiteSpace(name) && tags == null)
            {
                return InMemoryDataStore.PictureAlbums.AsQueryable();
            }
            if (tags == null)
            {
                var album = InMemoryDataStore.PictureAlbums.Where(a => a.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
                if (album != null)
                {
                    return album.AsQueryable();
                }
            }
            if (string.IsNullOrWhiteSpace(name))
            {
                IEnumerable<PictureAlbum> found = new List<PictureAlbum>();
                foreach (var tag in tags)
                {
                    found = found.Intersect(InMemoryDataStore.PictureAlbums.Where(a => a.Tags.Contains(tag)));
                }

                return found.AsQueryable();
            }
            IEnumerable<PictureAlbum> foundWithNamesAndTags = new List<PictureAlbum>();
            foreach (var tag in tags)
            {
                foundWithNamesAndTags = foundWithNamesAndTags.Intersect(InMemoryDataStore.PictureAlbums.Where(a => a.Tags.Contains(tag)));
            }

            var albumsFound = foundWithNamesAndTags.Where(a => a.Name.Contains(name));
            if (albumsFound != null)
            {
                return albumsFound.AsQueryable();
            }

            return new List<PictureAlbum>().AsQueryable();
        }

        public virtual dynamic GetPictureAlbum(Guid pictureAlbumId)
        {
            return InMemoryDataStore.PictureAlbums.FirstOrDefault(a => a.Id == pictureAlbumId);
        }

        public virtual Guid AddPictureAlbum(dynamic pictureAlbum)
        {
            pictureAlbum.Id = Guid.NewGuid();
            InMemoryDataStore.PictureAlbums.Add(pictureAlbum);
            return pictureAlbum.Id;
        }

        public virtual Guid UpdatePictureAlbum(dynamic pictureAlbum)
        {
            var toUpdate = InMemoryDataStore.PictureAlbums.FirstOrDefault(a => a.Id == pictureAlbum.Id);
            if (toUpdate != null)
            {
                toUpdate.Name = pictureAlbum.Name;
                toUpdate.Description = pictureAlbum.Description;
                toUpdate.Tags = pictureAlbum.Tags;
                toUpdate.Pictures = pictureAlbum.Pictures;
                toUpdate.PictureCount = pictureAlbum.Pictures != null ? pictureAlbum.Pictures.Count : 0;
            }
            return pictureAlbum.Id;
        }

        public virtual void DeletePictureAlbum(Guid pictureAlbumId)
        {
            var toDelete = InMemoryDataStore.PictureAlbums.FirstOrDefault(a => a.Id == pictureAlbumId);
            InMemoryDataStore.PictureAlbums.Remove(toDelete);
        }
    }
}