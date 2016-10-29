using System;
using System.Linq;
using System.Collections.Generic;
	
namespace MVC5Course.Models
{   
	public  class ProductRepository : EFRepository<Product>, IProductRepository
	{
        public Product Find(int id)
        {
            return this.All().FirstOrDefault(p => p.ProductId == id);
        }

        //override+ªÅ¥Õ_tab
        public override void Add(Product entity)
        {
            base.Add(entity);
        }
    }

	public  interface IProductRepository : IRepository<Product>
	{

	}
}