using Data.Contexts;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class ProjectRepository : IProjectRepository
{
    private readonly DataContext _context;

    public ProjectRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ProjectEntity>> GetAllProjectsAsync()
    {
        return await _context.Projects.Include(p => p.Customer).ToListAsync();
    }

    public async Task<ProjectEntity?> GetProjectByIdAsync(int id)
    {
        return await _context.Projects.Include(p => p.Customer).FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task AddProjectAsync(ProjectEntity project)
    {
        _context.Projects.Add(project);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateProjectAsync(ProjectEntity project)
    {
        _context.Projects.Update(project);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteProjectAsync(int id)
    {
        var project = await _context.Projects.FindAsync(id);
        if (project != null)
        {
            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
        }
    }
}
