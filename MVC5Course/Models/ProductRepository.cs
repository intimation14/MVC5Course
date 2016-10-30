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


        public override IQueryable<Product> All()
        {
            return base.All().Where(p => p.IsDeleted == false);
        }

        //override+�ť�_tab
        public override void Add(Product entity)
        {
            base.Add(entity);
        }

 
        public IQueryable<Product> Get�Ҧ����_�̾�productid�Ƨ�(int takeSize)
        {
            return this.All().OrderByDescending(p => p.ProductId).Take(takeSize);
        }

        public override void Delete(Product product)
        {
            product.IsDeleted = true;
        }
    }


    public  interface IProductRepository : IRepository<Product>
	{

	}
}