using Business.Interfaces;
using Data.Entities;
using Data.Interfaces;

namespace Business.Services;

public class ProjectService : IProjectService
{
    private readonly IProjectRepository _projectRepository;

    public ProjectService(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<IEnumerable<ProjectEntity>> GetAllProjectsAsync()
    {
        return await _projectRepository.GetAllProjectsAsync();
    }

    public async Task<ProjectEntity?> GetProjectByIdAsync(int id)
    {
        return await _projectRepository.GetProjectByIdAsync(id);
    }

    public async Task AddProjectAsync(ProjectEntity project)
    {
        project.TotalPrice = project.HoursWorked * project.HourlyRate;
        await _projectRepository.AddProjectAsync(project);
    }

    public async Task UpdateProjectAsync(ProjectEntity project)
    {
        project.TotalPrice = project.HoursWorked * project.HourlyRate;
        await _projectRepository.UpdateProjectAsync(project);
    }

    public async Task DeleteProjectAsync(int id)
    {
        await _projectRepository.DeleteProjectAsync(id);
    }
}
