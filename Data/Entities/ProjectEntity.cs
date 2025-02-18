using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;

public class ProjectEntity
{
    [Key]
    public int Id { get; set; }

    public string ProjectNumber { get; private set; } = null!;

    public string ProjectName { get; set; } = null!;

    [Column(TypeName = "date")]
    public DateTime StartDate { get; set; }

    [Column(TypeName = "date")]
    public DateTime EndDate { get; set; }

    public string ProjectManager { get; set; } = null!;

    public int CustomerId { get; set; }
    public CustomerEntity Customer { get; set; } = null!;

    public int HoursWorked { get; set; }

    public decimal HourlyRate { get; set; } = 1000;

    public decimal TotalPrice { get; set; }

   
    public int Status { get; set; }

    public ProjectEntity()
    {
        ProjectNumber = $"P-{Guid.NewGuid().ToString().Substring(0, 3)}";
    }
}
