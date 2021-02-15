using Dapper;
using EmployeeManagementSystem.Data.Shared.Entities;
using EmployeeManagementSystem.Data.Shared.Interfaces.Commands;
using Microsoft.Data.Sqlite;
using System;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Data.SQLiteLayer.Commands
{
    public class EmployeeCommands : IEmployeeCommands
    {
        private readonly string _connectionString;

        static readonly string InsertEmployeeSql =
@"
INSERT INTO Employee (Email, Phone, Title, FirstName, LastName, Username, TemporaryPassword) 
VALUES (@Email, @Phone, @Title, @FirstName, @LastName, @Username, @TemporaryPassword); 
SELECT * FROM Employee WHERE rowid in (SELECT last_insert_rowid());
";
        static readonly string UpdateEmployeeSql =
@"
UPDATE Employee SET Email = @Email, Phone = @Phone, Title = @Title, FirstName = @FirstName, 
LastName = @LastName, Username = @Username, TemporaryPassword = @TemporaryPassword, LastChange = @LastChange
WHERE Id = @Id;
";
        static readonly string SetExportedDateSql = 
            "UPDATE Employee SET LastExport = @LastExport WHERE Id = @Id;";
        static readonly string SetDeletedDateSql = 
            "UPDATE Employee SET Deleted = @Deleted WHERE Id = @Id;";
        static readonly string DeleteSql  = 
            "DELETE FROM Employee WHERE Id = @Id";

        public EmployeeCommands(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentException($"'{nameof(connectionString)}' cannot be null or empty", nameof(connectionString));
            _connectionString = connectionString;
        }

        public async Task<Employee> InsertEmployee(Employee entity)
        {
            using var connection = new SqliteConnection(_connectionString);
            return await connection.QueryFirstOrDefaultAsync<Employee>(InsertEmployeeSql, entity);
        }

        public async Task UpdateEmployee(Employee entity)
        {
            entity.LastChange = DateTime.Now;
            using var connection = new SqliteConnection(_connectionString);
            await connection.ExecuteAsync(UpdateEmployeeSql, entity);
        }

        public async Task SetExportedDate(Employee entity)
        {
            using var connection = new SqliteConnection(_connectionString);
            await connection.ExecuteAsync(SetExportedDateSql, entity);
        }

        public async Task SetDeletedDate(Employee entity)
        {
            entity.Deleted = DateTime.Now;
            using var connection = new SqliteConnection(_connectionString);
            await connection.ExecuteAsync(SetDeletedDateSql, entity);
        }
        public async Task Delete(Employee entity)
        {
            using var connection = new SqliteConnection(_connectionString);
            await connection.ExecuteAsync(DeleteSql, entity);
        }
    }
}
