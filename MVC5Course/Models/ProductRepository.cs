using System;
using System.Linq;
using System.Collections.Generic;
	
namespace MVC5Course.Models
{   
	public  class ProductRepository : EFRepository<Product>, IProductRepository
	{
        public override IQueryable<Product> All() {
            return base.All().Where(p=>p.Is�R�� == false);  //�~�Ӧ�EFRepository
        }

        public IQueryable<Product> Get�Ҧ����_�̾�ProductId�Ƨ�(int takeSize) {
            return this.All().OrderByDescending(p => p.ProductId).Take(takeSize);  //�qbase.All()�Ө����
        }

        public Product Find(int id) {
            return this.All().FirstOrDefault(p => p.ProductId == id);
        }


	}

	public  interface IProductRepository : IRepository<Product>
	{

	}
}