﻿using ImageOrganizer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ImageOrganizer.DataStore
{
    public class InMemoryPicturealbumRepository : IPictureAlbumsRepository
    {
        public IQueryable<Picture> GetPictures(Guid pictureAlbumId)
        {
            var album = InMemoryDataStore.PictureAlbums.FirstOrDefault(a => a.Id == pictureAlbumId);
            if (album != null)
            {
                return album.Pictures.AsQueryable();
            }
            return null;
        }

        public Picture GetPicture(Guid pictureAlbumId, Guid pictureId)
        {
            var album = InMemoryDataStore.PictureAlbums.FirstOrDefault(a => a.Id == pictureAlbumId);
            if (album != null)
            {
                return album.Pictures.FirstOrDefault(p => p.Id == pictureId);
            }
            return null;
        }

        public Guid AddPicture(Guid pictureAlbumId, Picture picture)
        {
            picture.Id = Guid.NewGuid();
            picture.ParentAlbumId = pictureAlbumId;
            var album = InMemoryDataStore.PictureAlbums.FirstOrDefault(a => a.Id == pictureAlbumId);
            if (album != null)
            {
                album.Pictures.Add(picture);
                return picture.Id;
            }
            return Guid.Empty;
        }

        public Guid UpdatePicture(Guid pictureAlbumId, Picture picture)
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

        public void DeletePicture(Guid pictureAlbumId, Guid pictureId)
        {
            var album = InMemoryDataStore.PictureAlbums.FirstOrDefault(a => a.Id == pictureAlbumId);
            if (album != null)
            {
                var toRemove = album.Pictures.FirstOrDefault(p => p.Id == pictureId);
                if (toRemove != null)
                {
                    album.Pictures.Remove(toRemove);
                }
            }
        }

        public IQueryable<PictureAlbum> GetPictureAlbums(string name = "", ICollection<string> tags = null)
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

        public PictureAlbum GetPictureAlbum(Guid pictureAlbumId)
        {
            return InMemoryDataStore.PictureAlbums.FirstOrDefault(a => a.Id == pictureAlbumId);
        }

        public Guid AddPictureAlbum(PictureAlbum pictureAlbum)
        {
            pictureAlbum.Id = Guid.NewGuid();
            InMemoryDataStore.PictureAlbums.Add(pictureAlbum);
            return pictureAlbum.Id;
        }

        public Guid UpdatePictureAlbum(PictureAlbum pictureAlbum)
        {
            var toUpdate = InMemoryDataStore.PictureAlbums.FirstOrDefault(a => a.Id == pictureAlbum.Id);
            if (toUpdate != null)
            {
                toUpdate.Name = pictureAlbum.Name;
                toUpdate.Description = pictureAlbum.Description;
                toUpdate.Tags = pictureAlbum.Tags;
                toUpdate.Pictures = pictureAlbum.Pictures;
            }
            return pictureAlbum.Id;
        }

        public void DeletePictureAlbum(Guid pictureAlbumId)
        {
            var toDelete = InMemoryDataStore.PictureAlbums.FirstOrDefault(a => a.Id == pictureAlbumId);
            InMemoryDataStore.PictureAlbums.Remove(toDelete);
        }
    }
}