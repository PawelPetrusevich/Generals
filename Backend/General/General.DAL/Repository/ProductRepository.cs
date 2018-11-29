using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

using Dapper;

using General.DAL.Common.Enums;
using General.DAL.Common.Interface.Repository;
using General.DAL.Common.Interface.UnitOfWork;
using General.DAL.Common.Models;

namespace General.DAL.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public override Dictionary<StoredProcedure, string> ProcedureDictionary { get; set; }
        
        public ProductRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}