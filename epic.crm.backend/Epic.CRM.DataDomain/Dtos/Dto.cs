using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epic.CRM.DataDomain.Dtos
{
    public abstract class Dto<TEntity>
    {
        public abstract TEntity Map();
        public abstract Dto<TEntity> Map(TEntity entity);
    }
}
