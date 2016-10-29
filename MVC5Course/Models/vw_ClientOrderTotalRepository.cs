using System;
using System.Linq;
using System.Collections.Generic;
	
namespace MVC5Course.Models
{   
	public  class vw_ClientOrderTotalRepository : EFRepository<vw_ClientOrderTotal>, Ivw_ClientOrderTotalRepository
	{

	}

	public  interface Ivw_ClientOrderTotalRepository : IRepository<vw_ClientOrderTotal>
	{

	}
}