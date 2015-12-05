using System.Collections.Generic;

namespace RedCloudWork.Domian
{
    public class Salesman:BaseEntity
    {
         public string Name { get; set; }
         
         public virtual IList<Bills> Billses { get; set; } 
    }
}