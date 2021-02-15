using Dapper;
using EmployeeManagementSystem.Data.Shared.Entities;
using EmployeeManagementSystem.Data.Shared.Interfaces.Commands;
using Microsoft.Data.Sqlite;
using System;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Data.SQLiteLayer.Commands
{
    public class GroupCommands : IGroupCommands
    {
        private readonly string _connectionString;

        static readonly string InsertGroupSql =
@"
INSERT INTO SecurityGroup (Name, Description) 
VALUES (@Name, @Description); 
SELECT * FROM SecurityGroup WHERE rowid in (SELECT last_insert_rowid());
";
        static readonly string UpdateADGroupSql =
            "UPDATE SecurityGroup SET Name = @Name, Description = @Description, LastChange = @LastChange WHERE Id = @Id;";
        static readonly string SetExportedDateSql =
            "UPDATE SecurityGroup SET LastExport = @LastExport WHERE Id = @Id;";
        static readonly string MarkADGroupForDeletionSql =
            "UPDATE SecurityGroup SET Deleted = @Deleted WHERE Id = @Id;";
        static readonly string DeleteADGroupSql =
            "DELETE FROM SecurityGroup WHERE Id = @Id;";

        public GroupCommands(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentException($"'{nameof(connectionString)}' cannot be null or empty", nameof(connectionString));
            _connectionString = connectionString;
        }

        public async Task<Group> InsertGroup(Group entity)
        {
            using var connection = new SqliteConnection(_connectionString);
            return await connection.QueryFirstOrDefaultAsync<Group>(InsertGroupSql, entity);
        }

        public async Task UpdateGroup(Group entity)
        {
            entity.LastChange = DateTime.Now;
            using var connection = new SqliteConnection(_connectionString);
             await connection.ExecuteAsync(UpdateADGroupSql, entity);
        }

        public async Task SetExportedDate(Group entity)
        {
            using var connection = new SqliteConnection(_connectionString);
             await connection.ExecuteAsync(SetExportedDateSql, entity);
        }
        public async Task SetDeletedDate(Group entity)
        {
            entity.Deleted = DateTime.Now;
            using var connection = new SqliteConnection(_connectionString);
             await connection.ExecuteAsync(MarkADGroupForDeletionSql, entity);
        }
        public async Task Delete(Group entity)
        {
            using var connection = new SqliteConnection(_connectionString);
             await connection.ExecuteAsync(DeleteADGroupSql, entity);
        }
    }
}
