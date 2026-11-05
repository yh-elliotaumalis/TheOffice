using System;
using System.Collections.Generic;
using System.Text;
using TheOffice.Domain.Entities;

namespace TheOffice.Application.Interfaces.Services
{
    public interface IProjectService
    {
        Task<IEnumerable<Project>> GetAllAsync();
        Task<Project> GetByIdAsync(int id);
        Task<Project> AddAsync(Project project);
        Task UpdateAsync(Project project);
        Task DeleteAsync(int id);
    }
}
