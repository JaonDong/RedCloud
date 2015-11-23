using System.Data.Entity;


namespace RedCloudWork.Domian
{
    public class MyDbcontext:DbContext
    {
        public virtual IDbSet<Products> Products { get; set; }
        public virtual IDbSet<Salesman> Saleswoman { get; set; }
        public virtual IDbSet<Merchants> Merchants { get; set; }
        public virtual IDbSet<Bills> Bills { get; set; }


        public MyDbcontext()
            :base("DefaultConnection")
        {
        }
        public MyDbcontext(string name) : base(name)
        { }

      
    }
}