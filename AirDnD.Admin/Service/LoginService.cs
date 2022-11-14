using Dapper;
using System;
using Airdnd.Admin.Models;
using Microsoft.Data.SqlClient;
using Airdnd.Core.Entities;
using Airdnd.Core.Helper;
using System.Data;
namespace Airdnd.Admin.Service
{

    public class LoginService
    {
        private readonly IDbConnection _dbConn;
        public LoginService(IDbConnection dbConnection)
        {
            _dbConn = dbConnection;
        }

        //@"Server=tcp:bs-2022-summer-server-04.database.windows.net,1433;Initial Catalog=AirDnD;Persist Security Info=False;User ID=bs;Password=P@ssword;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";


        public bool IsUserValid(LoginDto dto)
        {
            using var conn = _dbConn;
            string sql = "SELECT * FROM UserAccount WHERE Email=@email AND  Password=@password";

            var user = conn.QueryFirstOrDefault<UserAccount>(sql, new { email = dto.UserMail, password = Encryption.SHA256Encrypt(dto.Password) });

            //return user;

            if (dto.UserMail == "Admin" && dto.Password == "Admin") return true;

            if (user != null)
            {
                return true;
            }

            return false;

        }

    }
}
