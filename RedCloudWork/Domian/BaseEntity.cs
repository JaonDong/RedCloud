using System.ComponentModel.DataAnnotations;

namespace RedCloudWork.Domian
{
    public class BaseEntity:IEntity
    {
        [Key]
         public int Id { get; set; }
    }
}