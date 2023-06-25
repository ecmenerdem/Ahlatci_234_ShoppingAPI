using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shopping.DAL.Abstract;
using Shopping.DAL.Concrete.EntityFramework.DataManagement;
using ShoppingAPI.Entity.Poco;

namespace Shopping.DAL.Concrete.EntityFramework
{
    public class EfCategoryRepository:EfRepository<Category>,ICategoryRepository
    {
        public EfCategoryRepository(DbContext context) : base(context)
        {
           
        }
    }
}
