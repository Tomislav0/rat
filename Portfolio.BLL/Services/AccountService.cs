using Portfolio.DAL.Models.Protfolio;
using Portfolio.DAL;
using Portfolio.DAL.DTO;
using Microsoft.EntityFrameworkCore;
using Portfolio.DAL.BM;

namespace Portfolio.BLL.Services
{
    public interface IGalleryService
    {
        public Task<IEnumerable<PictureGroup>> GetAll();
        public Task Import(Picture picture);
        public Task<IEnumerable<PictureGroupDTO>> GetGroups();

        public Task CreateGroup(PictureGroupBM group);
        public Task QuickImport(QuickImportBM model);
    }
    public class GalleryService : IGalleryService
    {
        private readonly PortfolioDB context;
        public GalleryService(PortfolioDB context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<PictureGroup>> GetAll()
        {
            return context.PictureGroup.Where(t => t.Name != null);
        }

        public async Task Import(Picture picture)
        {
            try
            {
                picture.CreatedAt = DateTime.Now;
                await context.Picture.AddAsync(picture);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task<IEnumerable<PictureGroupDTO>> GetGroups()
        {
            return await context.PictureGroup.Select(x => new PictureGroupDTO
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
            }).ToListAsync();
        }

        public async Task CreateGroup(PictureGroupBM group)
        {
            var model = new PictureGroup { Name = group.Name, Description = group.Description };
            await context.PictureGroup.AddAsync(model);
            await context.SaveChangesAsync();
        }

        public async Task QuickImport(QuickImportBM model)
        {
            List<Picture> pictures = new List<Picture>();
            foreach (var picture in model.pictures)
            {
                pictures.Add(new Picture
                {
                    PictureUrl = picture.PictureUrl,
                    Name = picture.Name,
                    Description = picture.Description,
                    PrictureGroupId = model.PrictureGroupId,
                    TakenAt = picture.TakenAt,
                    CreatedAt = DateTime.Now
                });
            }

            await context.AddRangeAsync(pictures);
            await context.SaveChangesAsync();
        }
    }
}
