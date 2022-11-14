using Airdnd.Admin.Models;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Airdnd.Admin.Service
{
    public class TransactionService
    {
        private readonly IDbConnection _dbConn;

        public TransactionService(IDbConnection dbConn)
        {
            _dbConn = dbConn;
        }

        public List<TransactionDto> GetTrans()
        {
            List<TransactionDto> trans = null;
            using (var conn = _dbConn)
            {
                string sql = @"
                            SELECT
                            OrderId,
                            ListingId,
                            CheckInDate AS StartDate,
                            FinishDate AS EndDate,
                            FORMAT(UnitPrice,'#,###') AS Price,
                            Status,
                            TranStatus,
                            CreateDate,
                            (SELECT l.UserAcountId From Listing AS l WHERE l.ListingId = o.ListingId) AS UserAccountId,
                            (SELECT l.ListingName From Listing AS l WHERE l.ListingId = o.ListingId) AS ListingName
                            FROM [dbo].[Order] AS o
                            ";
                trans = conn.Query<TransactionDto>(sql).ToList();
            }

            return trans;
        }
        public async Task<int> ChangeTranStatus(int orderId)
        {
            var orders = new List<TransactionDto>();
            
            using (var dbconn = _dbConn)
            {
                dbconn.Open();
                int result;
                using (var transaction = dbconn.BeginTransaction())
                {
                    
                    string grant = @"UPDATE [dbo].[Order]
                                    SET TranStatus = 3
                                    WHERE OrderId = @OrderId
                                    ";
                    result = await dbconn.ExecuteAsync(grant , new {OrderId = orderId},transaction);
                    transaction.Commit();
                }
                return result;
            }
        }
    }
}
