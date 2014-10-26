//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.Entity;
//using System.Data.Entity.Infrastructure;
//using System.Linq;
//using System.Net;
//using System.Net.Http;
//using System.Threading.Tasks;
//using System.Web.Http;
//using System.Web.Http.Description;
//using ImageOrganizer.Models;

//namespace ImageOrganizer.Controllers
//{
//    public class ImagesController : ApiController
//    {
//        private ImageOrganizerContext db = new ImageOrganizerContext();

//        // GET: api/Pictures
//        [Route("api/pictureAlbums/{pictureAlbumId}/pictures")]
//        public IQueryable<Picture> GetPictures(int pictureAlbumId)
//        {
//            return db.PictureAlbums.SingleOrDefault(a => a.Id == pictureAlbumId).Pictures as IQueryable<Picture>;
//        }

//        // GET: api/Pictures/5

//        [Route("api/pictureAlbums/{pictureAlbumId}/pictures/{pictureId}")]
//        [ResponseType(typeof(Picture))]
//        public async Task<IHttpActionResult> GetPicture(int pictureAlbumId, int pictureId)
//        {
//            var album = await db.PictureAlbums.SingleOrDefaultAsync(a => a.Id == pictureAlbumId);
//            if (album == null)
//            {
//                return NotFound();
//            }
//            Picture picture = album.Pictures.SingleOrDefault(p => p.Id == pictureId);
//            if (picture == null)
//            {
//                return NotFound();
//            }

//            return Ok(picture);
//        }

//        // PUT: api/Pictures/5
//        [ResponseType(typeof(void))]
//        [Route("api/pictureAlbums/{pictureAlbumId}/pictures/{pictureId}")]
//        public async Task<IHttpActionResult> PutPicture(int pictureAlbumId, int pictureId, Picture picture)
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }

//            if (pictureId != picture.Id)
//            {
//                return BadRequest();
//            }

//            db.Entry(picture).State = EntityState.Modified;

//            try
//            {
//                await db.SaveChangesAsync();
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                if (!PictureExists(pictureId))
//                {
//                    return NotFound();
//                }
//                else
//                {
//                    throw;
//                }
//            }

//            return StatusCode(HttpStatusCode.NoContent);
//        }

//        // POST: api/Pictures
//        [ResponseType(typeof(Picture))]
//        [Route("api/pictureAlbums/{pictureAlbumId}/pictures")]
//        public async Task<IHttpActionResult> PostPicture(int pictureAlbumId, Picture picture)
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }
//            var album = await db.PictureAlbums.FindAsync(pictureAlbumId);
//            if (album.Pictures == null)
//            {
//                album.Pictures = new List<Picture>();
//            }
//            album.Pictures.Add(picture);
//            await db.SaveChangesAsync();

//            IDictionary<string, object> routeValues;
//            routeValues = this.ControllerContext.RouteData.Values;
//            var dCR = CreatedAtRoute("Picture", new { pictureId = picture.Id }, picture);

//            var urlHelper = new System.Web.Http.Routing.UrlHelper();
//            var link = urlHelper.Link("Picture", routeValues);
//            var result = new System.Web.Http.Results.CreatedAtRouteNegotiatedContentResult<Picture>("Picture", routeValues, picture, this);
//            return result;
//            //return CreatedAtRoute("Picture", new { pictureId = picture.Id }, picture);
//        }

//        // DELETE: api/Pictures/5
//        [ResponseType(typeof(Picture))]
//        public async Task<IHttpActionResult> DeletePicture(int id)
//        {
//            Picture picture = await db.Pictures.FindAsync(id);
//            if (picture == null)
//            {
//                return NotFound();
//            }

//            db.Pictures.Remove(picture);
//            await db.SaveChangesAsync();

//            return Ok(picture);
//        }

//        protected override void Dispose(bool disposing)
//        {
//            if (disposing)
//            {
//                db.Dispose();
//            }
//            base.Dispose(disposing);
//        }

//        private bool PictureExists(int id)
//        {
//            return db.Pictures.Count(e => e.Id == id) > 0;
//        }
//    }
//}