using System.ComponentModel.DataAnnotations;

namespace server_csharp_sqlite.Models
{
  public class Post
  {
    [Key]
    public int Id { get; set; }
    
    [Required]
    [MaxLength(150)]
    public string Title { get; set; }
    
    [Required]
    public string Content { get; set; }
  }
}