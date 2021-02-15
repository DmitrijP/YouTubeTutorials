using Dapper;
using EmployeeManagementSystem.Data.Shared.Entities;
using EmployeeManagementSystem.Data.Shared.Interfaces.Queries;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Data.SQLiteLayer.Queries
{
    public class GroupMembershipQueries : IGroupMembershipQueries
    {
        private readonly string _connectionString;
        static readonly string SelectGroupMemberSql 
            = "SELECT * FROM Employee as e LEFT JOIN SecurityGroupMembership as a ON e.Id = a.EmployeeId WHERE a.GroupId = @Id;";
        static readonly string SelectGroupsOfEmployeeSql
            = "SELECT * FROM SecurityGroup as e LEFT JOIN SecurityGroupMembership as a ON e.Id = a.GroupId WHERE a.EmployeeId = @Id;";
        static readonly string SelectSecurityGroupMembershipByEmployeeAndGroupSql
            = "SELECT * FROM SecurityGroupMembership WHERE EmployeeId = @EmployeeId AND GroupId = @GroupId;";
        static readonly string SelectSecurityGroupMembershipSql
            = "SELECT * FROM SecurityGroupMembership WHERE Id = @Id;";

        public GroupMembershipQueries(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentException($"'{nameof(connectionString)}' cannot be null or empty", nameof(connectionString));
            _connectionString = connectionString;
        }
        public async Task<IEnumerable<Employee>> SelectGroupMembers(Group entity)
        {
            using var connection = new SqliteConnection(_connectionString);
            return await connection.QueryAsync<Employee>(SelectGroupMemberSql, entity);
        }

        public async Task<IEnumerable<Group>> SelectGroupsOfEmployee(Employee entity)
        {
            using var connection = new SqliteConnection(_connectionString);
            return await connection.QueryAsync<Group>(SelectGroupsOfEmployeeSql, entity);
        }

        public async Task<GroupMembership> SelectSecurityGroupMembershipByEmployeeAndGroup(GroupMembership entity)
        {
            using var connection = new SqliteConnection(_connectionString);
            return await connection.QueryFirstOrDefaultAsync<GroupMembership>(SelectSecurityGroupMembershipByEmployeeAndGroupSql, entity);
        }

        public async Task<GroupMembership> SelectSecurityGroupMembership(GroupMembership entity)
        {
            using var connection = new SqliteConnection(_connectionString);
            return await connection.QueryFirstOrDefaultAsync<GroupMembership>(SelectSecurityGroupMembershipSql, entity);
        }

    }
}
