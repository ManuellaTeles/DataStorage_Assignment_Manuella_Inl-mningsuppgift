using Data.Entities;

namespace Data.Interfaces;

public interface IProjectRepository
{
    Task<IEnumerable<ProjectEntity>> GetAllProjectsAsync();
    Task<ProjectEntity?> GetProjectByIdAsync(int id);
    Task AddProjectAsync(ProjectEntity project);
    Task UpdateProjectAsync(ProjectEntity project);
    Task DeleteProjectAsync(int id);
}
