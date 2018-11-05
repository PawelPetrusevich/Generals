using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using General.DAL.Common.Enums;
using General.DAL.Common.Models;

namespace General.DAL.Common.Interface
{
    public interface IRepository<TModel> where TModel : Entity
    {
        Dictionary<StoredProcedure, string> ProcedureDictionary { get; set; }

        Task<TModel> Add(TModel obj);
    }
}