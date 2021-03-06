﻿using SL.Sigesoft.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SL.Sigesoft.Data.Contracts
{
    public interface ICompanyRepository:IGenericRepository<Company>
    {
        Task<Company> GetCompanyWithHeadquarter(int companyId);
        Task<Company> GetCompanyByRuc( string ruc);
        Task<List<Company>> AutocompleteByName(string value);
        Task<IEnumerable<Company>> GetAllFilterAsync(ParamsCompanyFilterModel paramsCompany);
        Task<bool> ValidateCompanyIsMine(int responsibleSystemUserId, string ruc);
    }
}
