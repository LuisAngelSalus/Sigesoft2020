﻿using SL.Sigesoft.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SL.Sigesoft.Data.Contracts
{
   public interface IQuotationRepository :IGenericRepository<Quotation>
    {        
        Task<QuotationModel> GetQuotationAsync(int id);
    }
}
