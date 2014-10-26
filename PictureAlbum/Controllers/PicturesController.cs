using ImageOrganizer.DataStore;
using ImageOrganizer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace ImageOrganizer.Controllers
{
    public class PicturesController : ApiController
    {
        private IPictureAlbumsRepository pictureAlbumsRepository;
        public PicturesController(IPictureAlbumsRepository dataStore)
        {
            this.pictureAlbumsRepository = dataStore;
        }

        [ResponseType(typeof(IQueryable<Picture>))]
        [Route("api/pictureAlbums/{pictureAlbumId}/pictures", Name = "Pictures")]
        public IHttpActionResult GetPictures(Guid pictureAlbumId)
        {
            var pictures = this.pictureAlbumsRepository.GetPictures(pictureAlbumId);
            if (pictures == null)
            {
                return BadRequest("The picture album identifier is invalid.");
            }

            return Ok(pictures);
        }

        [ResponseType(typeof(Picture))]
        [Route("api/pictureAlbums/{pictureAlbumId}/pictures/{pictureId}", Name="Picture")]
        public IHttpActionResult GetPicture(Guid pictureAlbumId, Guid pictureId)
        {
            var picture = this.pictureAlbumsRepository.GetPicture(pictureAlbumId, pictureId);
            if (picture == null)
            {
                return BadRequest("Either the picture album identifier or the picture identifier is invalid.");
            }

            return Ok(picture);
        }

        [ResponseType(typeof(void))]
        [Route("api/pictureAlbums/{pictureAlbumId}/pictures")]
        public IHttpActionResult PostPicture(Guid pictureAlbumId, Picture picture)
        {
            var pictureId = this.pictureAlbumsRepository.AddPicture(pictureAlbumId, picture);
            return CreatedAtRoute("Picture", new { pictureAlbumId = pictureAlbumId, pictureId = pictureId }, picture);
        }

        [ResponseType(typeof(void))]
        [Route("api/pictureAlbums/{pictureAlbumId}/pictures/{pictureId}")]
        public IHttpActionResult PutPicture(Guid pictureAlbumId, Guid pictureId, Picture picture)
        {
            if (ModelState.IsValid && picture != null)
            {
                picture.Id = pictureId;
                this.pictureAlbumsRepository.UpdatePicture(pictureAlbumId, picture);
            }
            
            return StatusCode(HttpStatusCode.NoContent);
        }

        [ResponseType(typeof(void))]
        [Route("api/pictureAlbums/{pictureAlbumId}/pictures/{pictureId}")]
        public IHttpActionResult PatchPicture(Guid pictureAlbumId, Guid pictureId, Picture picture)
        {
            if (ModelState.IsValid && picture != null)
            {
                var toUpdate = this.pictureAlbumsRepository.GetPicture(pictureAlbumId, pictureId);
                if (toUpdate != null)
                {
                    toUpdate.Height = picture.Height > 0 ? picture.Height : toUpdate.Height;
                    toUpdate.Width = picture.Width > 0 ? picture.Width : toUpdate.Width;
                    toUpdate.Url = string.IsNullOrWhiteSpace(picture.Url) ? toUpdate.Url : picture.Url;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        [ResponseType(typeof(void))]
        [Route("api/pictureAlbums/{pictureAlbumId}/pictures/{pictureId}")]
        public IHttpActionResult DeletePicture(Guid pictureAlbumId, Guid pictureId)
        {
            this.pictureAlbumsRepository.DeletePicture(pictureAlbumId, pictureId);
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
