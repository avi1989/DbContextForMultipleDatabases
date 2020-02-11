using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MultiDbContext.Data.Conventions
{
    public interface IDatabaseConventionConverter
    {
        void SetConvention(ModelBuilder modelBuilder);
    }
}
