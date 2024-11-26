using Dapper;
using DataLibrary.Models;
using SqlDataManager.Internal.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;


namespace SqlDataManager.DataAccess
{
    public static class UserData
    {
        public static async Task<IEnumerable<UserModel>> getPeople(string lastname)
        {
            using(IDbConnection connection = new System.Data.SqlClient.SqlConnection(SqlDataAccess.getconnectionString("DefaultConnection")))
            {
                // Asynchronously execute the query
                var list = await connection.QueryAsync<UserModel>($"Select * from UserTable WHERE LastName = '{lastname}'");
                return list.ToList();
            }
        }
    }
}
