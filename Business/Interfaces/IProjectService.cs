using Data.Entities;

namespace Business.Interfaces;

public interface IProjectService
{
    Task<IEnumerable<ProjectEntity>> GetAllProjectsAsync();
    Task<ProjectEntity?> GetProjectByIdAsync(int id);
    Task AddProjectAsync(ProjectEntity project);
    Task UpdateProjectAsync(ProjectEntity project);
    Task DeleteProjectAsync(int id);
}
