using SL.Sigesoft.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SL.Sigesoft.Data.Contracts
{
   public interface IQuotationRepository :IGenericRepository<Quotation>
    {        
        Task<QuotationModel> GetQuotationAsync(int id);
        Task<IEnumerable<QuotationFilterModel>> GetFilterAsync(ParamsQuotationFilterDto parameters);
        Task<Quotation> NewVersion(Quotation entity);
        Task<IEnumerable<QuotationVersionModel>> GetVersions(string code);
        Task<bool> UpdateIsProccess(string code, int quotationId);
        Task<bool> MigrateQuotationToProtocols(int quotationId);
        Task<List<ListTrackingChartModel>> Trackingchart(ParamsTrackingChartModel trackingchartdto);
        Task<bool> MigrateoProtocolToSIGESoftWin(int quotationId,int systemUserId);
    }
}
