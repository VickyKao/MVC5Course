using System;
using System.Linq;
using System.Collections.Generic;
	
namespace MVC5Course.Models
{   
	public  class ProductRepository : EFRepository<Product>, IProductRepository
	{
        public override IQueryable<Product> All() {
            return base.All().Where(p=>p.Is刪除 == false);  //繼承自EFRepository
        }

        public IQueryable<Product> Get所有資料_依據ProductId排序(int takeSize) {
            return this.All().OrderByDescending(p => p.ProductId).Take(takeSize);  //從base.All()來取資料
        }

        public Product Find(int id) {
            return this.All().FirstOrDefault(p => p.ProductId == id);
        }


	}

	public  interface IProductRepository : IRepository<Product>
	{

	}
}