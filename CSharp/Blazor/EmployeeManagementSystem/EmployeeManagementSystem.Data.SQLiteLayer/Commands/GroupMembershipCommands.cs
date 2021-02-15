using Dapper;
using EmployeeManagementSystem.Data.Shared.Entities;
using EmployeeManagementSystem.Data.Shared.Interfaces.Commands;
using Microsoft.Data.Sqlite;
using System;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Data.SQLiteLayer.Commands
{
    public class GroupMembershipCommands : IGroupMembershipCommands
    {
        private readonly string _connectionString;

        static readonly string SetExportedDateSql =
            "UPDATE SecurityGroupMembership SET LastExport = @LastExport WHERE Id = @Id;";
        static readonly string InsertMembershipSql =
            "INSERT INTO SecurityGroupMembership (EmployeeId, GroupId) VALUES (@EmployeeId, @GroupId);";
        static readonly string MarkMembershipForDeletionSql =
            "UPDATE SecurityGroupMembership SET Deleted = @Deleted WHERE EmployeeId = @EmployeeId AND GroupId = @GroupId;";
        static readonly string DeleteMembershipSql =
            "DELETE FROM SecurityGroupMembership WHERE EmployeeId = @EmployeeId AND GroupId = @GroupId;";

        public GroupMembershipCommands(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentException($"'{nameof(connectionString)}' cannot be null or empty", nameof(connectionString));
            _connectionString = connectionString;
        }

       
        public async Task InsertMembership(GroupMembership membership)
        {
            using var connection = new SqliteConnection(_connectionString);
            await connection.ExecuteAsync(InsertMembershipSql, membership);
        }

        public async Task MarkMembershipForDeletion(GroupMembership membership)
        {
            membership.Deleted = DateTime.Now;
            using var connection = new SqliteConnection(_connectionString);
            await connection.ExecuteAsync(MarkMembershipForDeletionSql, membership);
        }

        public async Task DeleteMembership(GroupMembership membership)
        {
            using var connection = new SqliteConnection(_connectionString);
            await connection.ExecuteAsync(DeleteMembershipSql, membership);
        }

        public async Task SetExportedDate(GroupMembership entity)
        {
            using var connection = new SqliteConnection(_connectionString);
            await connection.ExecuteAsync(SetExportedDateSql, entity);
        }
    }
}
