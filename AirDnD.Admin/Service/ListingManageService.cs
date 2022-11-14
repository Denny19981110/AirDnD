using Airdnd.Admin.Helpers;
using Airdnd.Admin.Models;
using Airdnd.Core.Entities;
using Airdnd.Core.Interfaces;
using Coravel.Cache.Interfaces;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Airdnd.Admin.Service
{
    public class ListingManageService
    {
        private readonly IDbConnection _dbConn;
        private readonly IDistributedCache _cache;

        public ListingManageService( IDbConnection dbConnection, IDistributedCache cache )
        {
            _dbConn = dbConnection;
            _cache = cache;
        }
        public IEnumerable<ListingDto> GetAllListings()
        {
            var result = new List<ListingDto>();
            using var dbConn = _dbConn;

            string sql = @" SELECT 
                                ListingId
                                ,ListingName
                                ,(SELECT u.Name FROM UserAccount AS u WHERE u.UserAccountId = l.UserAcountId) as Host
	                            , SUBSTRING(Address,0,CHARINDEX('-',Address)) +'-'+
                                SUBSTRING(Address,
                                    CHARINDEX('-',Address)+1,
                                    CHARINDEX('-',Address,
                                                CHARINDEX('-',Address, CHARINDEX('-',Address)+1)) -
                                                CHARINDEX('-',Address, CHARINDEX('-',Address))-1 
                                ) AS Region
                                ,BedRoom AS BedRooms
                                ,Bed AS Beds
                                ,Bathroom AS BathRooms
                                ,FORMAT(CreateTime, 'yyyy-MM-dd') AS CreateTime
                                ,(SELECT p.PropertyName FROM PropertyType AS p WHERE p.PropertyID = l.PropertyID) AS PropertyName
                                ,Status
                                ,FORMAT(DefaultPrice,'#,###') AS Price
                            FROM Listing AS l;";

            result = dbConn.Query<ListingDto>(sql).ToList();


            return result;
        }
        public async Task<int> switchListingStatus( int listingId, int status )
        {
            var listings = new List<ListingDto>();
            using var dbConn = _dbConn;
            dbConn.Open();
            int changeStatus = status == 1 ? 2 : 1;
            try
            {
                string takeDownListingSql = @"UPDATE Listing 
                                            SET Status = @Status 
                                            WHERE ListingId = @ListingId";

                int modifyFiles = await dbConn.ExecuteAsync(takeDownListingSql, new { ListingId = listingId, Status = changeStatus });

                //刪除Cache頁面               
                string countSql = @"SELECT count(*)
                                    FROM Listing
                                    WHERE ListingId <= @ListingId";
                string allPagesSql = "SELECT count(*) FROM Listing";

                int current = await dbConn.QueryFirstAsync<int>(countSql, new { ListingId = listingId });
                int all = await dbConn.QueryFirstAsync<int>(allPagesSql, new { ListingId = listingId });

                int page = (current / 16) + 1;
                int allPages = (all / 16) + 1;
                for( int i = page;i <= allPages;i++ )
                {
                    _cache.Remove($"listings-page{i}");
                }
                return modifyFiles;
            }
            catch
            {
                return -1;
            }

        }

    }
}
