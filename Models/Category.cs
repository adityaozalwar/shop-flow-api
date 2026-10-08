using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopFlowAPI.Models;

[Table("category")]
public class Category
{
    [Key]
    [Column("category_id")]
    public int CategoryId { get; set; }

    [Column("category_name")] 
    public required string CategoryName { get; set; }
    
    [Column("created_at")]
    public DateTime CreatedAt { get; set; }
    
    [Column("modified_at")]
    public DateTime ModifiedAt { get; set; }
}