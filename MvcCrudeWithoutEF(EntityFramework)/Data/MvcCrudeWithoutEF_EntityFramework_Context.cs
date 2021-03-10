using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MvcCrudeWithoutEF_EntityFramework_.Models;

namespace MvcCrudeWithoutEF_EntityFramework_.Data
{
    public class MvcCrudeWithoutEF_EntityFramework_Context : DbContext
    {
        public MvcCrudeWithoutEF_EntityFramework_Context (DbContextOptions<MvcCrudeWithoutEF_EntityFramework_Context> options)
            : base(options)
        {
        }

        public DbSet<MvcCrudeWithoutEF_EntityFramework_.Models.BookViewModel> BookViewModel { get; set; }
    }
}
