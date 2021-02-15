using Dapper;
using EmployeeManagementSystem.Data.Shared.Interfaces.Creation;
using Microsoft.Data.Sqlite;
using System;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Data.SQLiteLayer.Creation
{
    public class TableBuilder : IDBTableBuilder
    {
        static readonly string CreateScript =
@"
CREATE TABLE IF NOT EXISTS Employee (
    Id INTEGER,
    Username VARCHAR (256),
    TemporaryPassword VARCHAR (256),
    Title VARCHAR (256),
    FirstName VARCHAR (256),
    LastName VARCHAR (256),
    Email VARCHAR (256),
    Phone VARCHAR (32),
    Gender VARCHAR (32),
    Deleted DATETIME NULL,
    LastChange DATETIME NULL,
    LastExport DATETIME NULL,
    PRIMARY KEY(
        Id ASC
    )
);

CREATE TABLE IF NOT EXISTS SecurityGroup (
    Id INTEGER,
    Name NVARCHAR(256),
    Description NVARCHAR(256),
    Deleted DATETIME NULL,
    LastChange DATETIME NULL,
    LastExport DATETIME NULL,
    PRIMARY KEY(
        Id ASC
    )
);

CREATE TABLE IF NOT EXISTS SecurityGroupMembership(
   Id INTEGER,
   EmployeeId INTEGER,
   GroupId INTEGER,
   Deleted DATETIME NULL,
   LastExport DATETIME NULL,
   PRIMARY KEY(
        Id ASC
    )
);
";
        private readonly string _connectionString;

        public TableBuilder(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentException($"'{nameof(connectionString)}' cannot be null or empty", nameof(connectionString));
            _connectionString = connectionString;
        }

        public async Task CreateTables()
        {
            using var connection = new SqliteConnection(_connectionString);
            await connection.ExecuteAsync(CreateScript);
        }
    }
}