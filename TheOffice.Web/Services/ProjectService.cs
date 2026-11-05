using TheOffice.Application.Interfaces.Repositories;
using TheOffice.Application.Interfaces.Services;
using TheOffice.Domain.Entities;

namespace TheOffice.Web.Services
{
    public class ProjectService(IProjectRepository projectRepository) : IProjectService
    {
        public Task<Project> AddAsync(Project project)
        {
            return projectRepository.AddAsync(project);
        }

        public Task DeleteAsync(int id)
        {
            return projectRepository.DeleteAsync(id);
        }

        public Task<IEnumerable<Project>> GetAllAsync()
        {
            return projectRepository.GetAllAsync();
        }

        public Task<Project> GetByIdAsync(int id)
        {
            return projectRepository.GetByIdAsync(id);
        }

        public Task UpdateAsync(Project project)
        {
            return projectRepository.UpdateAsync(project);
        }
    }
}
