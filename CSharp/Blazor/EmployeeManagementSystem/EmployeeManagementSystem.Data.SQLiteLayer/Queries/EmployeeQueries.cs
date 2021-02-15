using Dapper;
using EmployeeManagementSystem.Data.Shared.Entities;
using EmployeeManagementSystem.Data.Shared.Interfaces.Queries;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Data.SQLiteLayer.Queries
{
    public class EmployeeQueries : IEmployeeQueries
    {
        private readonly string _connectionString;

        static readonly string SelectEmployeeSql = "SELECT * FROM Employee WHERE Id = @Id;";
        static readonly string SelectAllEmployeeSql = "SELECT * FROM Employee;";

        public EmployeeQueries(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentException($"'{nameof(connectionString)}' cannot be null or empty", nameof(connectionString));
            _connectionString = connectionString;
        }

        public async Task<Employee> SelectEmployee(Employee entity)
        {
            using var connection = new SqliteConnection(_connectionString);
            return await connection.QueryFirstOrDefaultAsync<Employee>(SelectEmployeeSql, entity);
        }

        public async Task<IEnumerable<Employee>> SelectAllEmployee()
        {
            using var connection = new SqliteConnection(_connectionString);
            return await connection.QueryAsync<Employee>(SelectAllEmployeeSql);
        }
    }
}
