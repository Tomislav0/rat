using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portfolio.BLL.Services;
using Portfolio.DAL.BM;
using Portfolio.DAL.DTO;
using Portfolio.DAL.Models.Protfolio;

namespace Portfolio.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GalleryController : ControllerBase
    {
        private IHttpContextAccessor Context { get; set; }
        private IGalleryService galleryService;
        public GalleryController(IHttpContextAccessor context, IGalleryService galleryService)
        {
            Context = context;
            this.galleryService = galleryService;
        }

        [HttpGet]
        [Route("all")]
        public async Task<IEnumerable<PictureGroup>> GetAll()
        {
            return await galleryService.GetAll();
        }

        [HttpPost]
        [Authorize]
        [Route("import")]
        public async Task Import(Picture picture)
        {
           await galleryService.Import(picture);
        }

        [HttpGet]
        [Authorize]
        [Route("groups")]
        public async Task<IEnumerable<PictureGroupDTO>> GetGroups()
        {
            return await galleryService.GetGroups();
        }

        [HttpPost]
        [Authorize]
        [Route("create-group")]
        public async Task CreateGroup(PictureGroupBM group)
        {
            await galleryService.CreateGroup(group);
        }

        [HttpPost]
        [Authorize]
        [Route("quick-import")]
        public async Task QuickImport(QuickImportBM quickImport)
        {
            await galleryService.QuickImport(quickImport);
        }

    }
}
