using Airdnd.Admin.Models;
using Airdnd.Core.Entities;
using Airdnd.Core.Interfaces;
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;


namespace Airdnd.Admin.Service
{
    public class MemberService
    {
        private readonly IDbConnection _dbConn;

        public MemberService(IDbConnection dbConnection)
        {

            _dbConn = dbConnection;
        }
        static string connString = @"Server=tcp:bs-2022-summer-server-04.database.windows.net,1433;Initial Catalog=AirDnD;Persist Security Info=False;User ID=bs;Password=P@ssword;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        //private readonly IRepository<UserAccount> _accountRepo;
        //public MemberService(IRepository<UserAccount> accountRepo)
        //{
        //    _accountRepo = accountRepo;
        //}

        public List<MemberDto> GetAllMember()
        {
            List<MemberDto> member = null;
            using (var conn = _dbConn)
            {
                string sql = @"
                            Select 
                            UserAccountID,
                            Gender,
                            [Name],
							case when UserAccountID in(select UserAccountID
                            from UserAccount
                            where UserAccountID not in(select DISTINCT UserAcountID from Listing)) then 1 ELSE 0 END As Host,
                            COALESCE(NULLIF(AvatarUrl,''), 'https://kamiiliu.github.io/exPerson/fake.png') As AvatarUrl,
                            COALESCE(NULLIF(Email,''), null) As Email,
                            COALESCE(NULLIF(Phone,''), null) As Phone,
                            COALESCE(NULLIF(AboutMe,''), N'此用戶未填寫') As AboutMe,
                            COALESCE(NULLIF([Address],''), N'此用戶未填寫') As [Address],
                            ISNULL(CONVERT(VARCHAR,CONVERT(datetime,Birthday),110),null) As Birthday,
                            CONVERT(VARCHAR,CONVERT(datetime,CreateDate),110) As CreateDate
                            from UserAccount
                            ";
                member = conn.Query<MemberDto>(sql).ToList();
            }
            //(select UserAccountID
            //from UserAccount
            //where UserAccountID not in(select DISTINCT UserAcountID from Listing))
            return member;
        }
        /// <summary>
        /// 輸入生日,算出年紀
        /// </summary>
        /// <param name="birthday"></param>
        /// <returns></returns>
        public int GetAge(DateTime? birthday)
        {
            if (birthday.HasValue)
            {
                var now = DateTime.UtcNow;
                if (now.Month <= birthday.Value.Month)
                {
                    return now.Year - birthday.Value.Year - 1;
                }
                else
                {
                    return now.Year - birthday.Value.Year;
                }
            }
            else
            {
                return 0;
            }
        }
    }
}
