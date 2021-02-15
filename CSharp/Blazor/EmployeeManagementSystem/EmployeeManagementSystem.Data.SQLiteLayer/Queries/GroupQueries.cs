using Dapper;
using EmployeeManagementSystem.Data.Shared.Entities;
using EmployeeManagementSystem.Data.Shared.Interfaces.Queries;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Data.SQLiteLayer.Queries
{
    public class GroupQueries : IGroupQueries
    {
        private readonly string _connectionString;
        static readonly string SelectGroupSql = "SELECT * FROM SecurityGroup WHERE Id = @Id;";
        static readonly string SelectGroupsSql = "SELECT * FROM SecurityGroup;";

        public GroupQueries(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentException($"'{nameof(connectionString)}' cannot be null or empty", nameof(connectionString));
            _connectionString = connectionString;
        }

        public async Task<Group> SelectGroup(Group entity)
        {
            using var connection = new SqliteConnection(_connectionString);
            return await connection.QueryFirstOrDefaultAsync<Group>(SelectGroupSql, entity);
        }

        
        public async Task<IEnumerable<Group>> SelectAllGroups()
        {
            using var connection = new SqliteConnection(_connectionString);
            return await connection.QueryAsync<Group>(SelectGroupsSql);
        }
    }
}
