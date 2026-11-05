using Microsoft.EntityFrameworkCore;
using TheOffice.Application.Interfaces.Repositories;
using TheOffice.Domain.Entities;
using TheOffice.Web.Data;

namespace TheOffice.Web.Repositories
{
    public class ProjectRepository(ApplicationDbContext context) : IProjectRepository
    {
        public async Task<IEnumerable<Project>> GetAllAsync()
        {
            return await context.Projects.ToListAsync();
        }

        public async Task<Project?> GetByIdAsync(int id)
        {
            return await context.Projects
                .FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task<Project> AddAsync(Project project)
        {
            var entityEntry = await context.Projects.AddAsync(project);
            await context.SaveChangesAsync();
            return entityEntry.Entity;
        }
        public async Task UpdateAsync(Project project)
        {
            context.Projects.Update(project);
            await context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var project = await context.Projects.FirstOrDefaultAsync(p => p.Id == id);
            if (project is null) return;

            context.Projects.Remove(project);
            await context.SaveChangesAsync();
        }
    }
}
