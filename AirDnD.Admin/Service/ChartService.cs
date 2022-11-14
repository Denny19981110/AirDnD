using Airdnd.Core.Entities;
using Airdnd.Core.Interfaces;
using System;
using System.Linq;
using Dapper;
using Microsoft.Data.SqlClient;
using static Airdnd.Admin.Models.DtoModels.ChartDto;
using Airdnd.Admin.Models.DtoModels;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;


namespace Airdnd.Admin.Service
{
    public class ChartService
    {
        public int thisMonth = DateTime.Now.Month;
        public int thisYear = DateTime.Now.Year;
        public string connectionString;
        public ChartService(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("Airdnd");
        }

        public int GetAllMembers()
        {
            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = "select * from UserAccount";
                var result = conn.Query(sql).Count();
                return result;
            }
        }
        public int GetThisMonthOrders()
        {
            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = "select * from [dbo].[Order] where DATEPART(year, CreateDate) = @thisYear AND DATEPART(month, CreateDate) = @thisMonth";
                var result = conn.Query(sql, new { thisYear = DateTime.Now.Year, thisMonth = DateTime.Now.Month }).Count();
                return result;
            }
        }
        public int GetThisYearOrders()
        {
            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = $"select * from [dbo].[Order] where DATEPART(year, CreateDate) = @thisYear";
                var result = conn.Query(sql, new { thisYear = DateTime.Now.Year}).Count();
                return result;
            }
        }
        public dynamic GetThisMonthRevenue()
        {
            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = $"select sum(UnitPrice) from [dbo].[Order] where DATEPART(month, CreateDate) = @thisMonth";
                var result = conn.Query(sql, new {thisMonth = DateTime.Now.Month});
                return result;
            }
        }
        public dynamic GetThisYearRevenue()
        {
            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = $"select sum(UnitPrice) from [dbo].[Order] where DATEPART(year, CreateDate) = @thisYear";
                var result = conn.Query(sql, new {thisYear = DateTime.Now.Year});
                return result;
            }
        }
        public dynamic[] GetMemberGender()
        {
            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                string female = "select count(*) from UserAccount where Gender = 0";
                string male = "select count(*) from UserAccount where Gender = 1";
                string other = "select count(*) from UserAccount where Gender = 2";
                return new dynamic[]
                {
                    conn.Query(female),
                    conn.Query(male),
                    conn.Query(other)
                };
            }
        }
        public dynamic[] GetEveryMonthOrders()
        {
            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                dynamic[] result = new dynamic[12];
                for (int i = 1; i <= 12; i++)
                {
                    string sql = $"select count(*) from [dbo].[Order] where DATEPART(year, CreateDate) = @thisYear AND DATEPART(month, CreateDate) = @i";
                    var monthInfo = conn.Query(sql, new {thisYear = DateTime.Now.Year, i = i});
                    result[i-1] = monthInfo;
                }
                return result;
            }
        }
        public dynamic[] GetEvertMonthRevenue()
        {
            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                dynamic[] result = new dynamic[12];
                for(int i = 1; i <= 12; i++)
                {
                    string sql = $"select sum(UnitPrice) from [dbo].[Order] where DATEPART(year, CreateDate) = @thisYear AND DATEPART(month, CreateDate) = @i";
                    dynamic monthInfo = conn.Query(sql, new {thisYear = DateTime.Now.Year, i = i});
                    result[i-1] = monthInfo;
                }
                return result;
            }
        }
        public dynamic[] GetEverySeasonMember()
        {
            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                dynamic[] result = new dynamic[4];
                int start = 1;
                int end = 3;
                for(int i = 0; i < 4; i++)
                {
                    string sql = $"select count(*) from UserAccount where DATEPART(year, CreateDate) = @thisYear and DATEPART(month, CreateDate) >= @Start and DATEPART(month, CreateDate) <= @End";
                    dynamic seasonInfo = conn.Query(sql, new { thisYear = DateTime.Now.Year, Start = start, End = end });
                    result[i] = seasonInfo;
                    start += 3;
                    end += 3;
                }
                return result;
            }
        }
        public dynamic[] GetEverySeasonRoomSource()
        {
            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                dynamic[] result = new dynamic[4];
                int start = 1;
                int end = 3;
                for (int i = 0; i < 4; i++)
                {
                    string sql = $"select count(*) from Listing where DATEPART(year, CreateTime) = @thisYear and DATEPART(month, CreateTime) >= @start and DATEPART(month, CreateTime) <= @end";
                    dynamic seasonInfo = conn.Query(sql, new {thisYear = DateTime.Now.Year, start = start, end = end});
                    result[i] = seasonInfo;
                    start += 3;
                    end += 3;
                }
                return result;
            }
        }
    }
}
