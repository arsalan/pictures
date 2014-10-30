using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using ImageOrganizer.Models;
using ImageOrganizer.DataStore;
using System.Web.Http.Results;

namespace ImageOrganizer.Controllers
{
    public class PictureAlbumsController : ApiController
    {
        private IPictureAlbumsRepository pictureAlbumsRepository;

        public PictureAlbumsController(IPictureAlbumsRepository pictureAlbumsRepository)
        {
            this.pictureAlbumsRepository = pictureAlbumsRepository;
        }

        public virtual IPictureAlbumsRepository PictureAlbumsRepository
        {
            get
            {
                if (this.RequestContext.RouteData.Values["version"] == null || this.RequestContext.RouteData.Values["version"].Equals(0))
                {
                    this.pictureAlbumsRepository = new InMemoryPictureAlbumRepository();
                }
                return this.pictureAlbumsRepository;
            }
        }

        // GET: api/PictureAlbums
        public IList<dynamic> GetPictureAlbums()
        {
            return this.PictureAlbumsRepository.GetPictureAlbums().ToList();
        }

        // GET: api/PictureAlbums/5
        [ResponseType(typeof(PictureAlbum))]
        public async Task<IHttpActionResult> GetPictureAlbum(Guid pictureAlbumId)
        {
            var fetchTask = new Task<dynamic>(() => this.PictureAlbumsRepository.GetPictureAlbum(pictureAlbumId));
            fetchTask.Start();
            var album = await fetchTask;
            if (album == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(album);
            }
        }

        // PUT: api/PictureAlbums/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutPictureAlbum(Guid pictureAlbumId, PictureAlbum pictureAlbum)
        {
            if (ModelState.IsValid && pictureAlbum != null)
            {
                pictureAlbum.Id = pictureAlbumId;
                var updateTask = new Task<Guid>(() => this.PictureAlbumsRepository.UpdatePictureAlbum(pictureAlbum));
                updateTask.Start();
                await updateTask;
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // PATCH: api/PictureAlbums/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PatchPictureAlbum(Guid pictureAlbumId, PictureAlbum pictureAlbum)
        {
            if (ModelState.IsValid && pictureAlbum != null)
            {
                var fetchTask = new Task<dynamic>(() => this.PictureAlbumsRepository.GetPictureAlbum(pictureAlbumId));
                fetchTask.Start();
                var album = await fetchTask;

                album.Id = pictureAlbumId;
                album.Name = string.IsNullOrWhiteSpace(pictureAlbum.Name) ? album.Name : pictureAlbum.Name;
                album.Description = string.IsNullOrWhiteSpace(pictureAlbum.Description) ? album.Description : pictureAlbum.Description;
                album.Tags = pictureAlbum.Tags == null ? album.Tags : pictureAlbum.Tags;

                var updateTask = new Task<Guid>(() => this.PictureAlbumsRepository.UpdatePictureAlbum(pictureAlbum));
                updateTask.Start();
                await updateTask;
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/PictureAlbums
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PostPictureAlbum(PictureAlbum pictureAlbum)
        {
            var addTask = new Task<Guid>(() => this.PictureAlbumsRepository.AddPictureAlbum(pictureAlbum));
            addTask.Start();
            var pictureAlbumId = await addTask;
            return CreatedAtRoute("PictureAlbum", new { id = pictureAlbumId }, pictureAlbum);
        }

        // DELETE: api/PictureAlbums/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> DeletePictureAlbum(Guid pictureAlbumId)
        {
            var deleteTask = new Task(() => this.PictureAlbumsRepository.DeletePictureAlbum(pictureAlbumId));
            deleteTask.Start();
            await deleteTask;
            return StatusCode(HttpStatusCode.NoContent);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}