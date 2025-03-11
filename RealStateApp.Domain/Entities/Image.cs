using RealEstateApp.Domain.Entities;
using System.ComponentModel.DataAnnotations;

public class Image
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public Guid PropertyId { get; set; }
    public virtual Property Property { get; set; }

    [Required]
    [StringLength(500)]
    public string Url { get; set; }
}
