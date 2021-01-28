using Dapper;
using EmployeeManagementSystem.Data.Shared.Entities;
using EmployeeManagementSystem.Data.Shared.Interfaces;
using Microsoft.Data.Sqlite;
using System;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Data.SQLiteLayer
{
    public class EmployeeCommands : IEmployeeCommands
    {
        private readonly string _connectionString;

        static readonly string InsertNameSql =
@"
INSERT INTO Name (Title, First, Last)
VALUES (@Title, @First, @Last); 
SELECT * FROM Name WHERE rowid in (SELECT last_insert_rowid());
";

        static readonly string InsertLoginSql =
@"
INSERT INTO Login (Uuid, Username, Password, Salt, SHA256) 
VALUES (@Uuid, @Username, @Password, @Salt, @SHA256); 
SELECT * FROM LOGIN WHERE rowid in (SELECT last_insert_rowid());";

        static readonly string InsertStreetSql =
@"
INSERT INTO Street (Name, Number) 
VALUES (@Name, @Number); 
SELECT * FROM Street WHERE rowid in (SELECT last_insert_rowid());";

        static readonly string InsertLocationSql =
@"
INSERT INTO Location (StreetId, Country, State, Postcode, City) 
VALUES (@StreetId, @Country, @State, @Postcode, @City); 
SELECT * FROM Location WHERE rowid in (SELECT last_insert_rowid());
";
        static readonly string InsertPictureSql =
@"
INSERT INTO Picture (Large, Medium, Thumbnail) 
VALUES (@Large, @Medium, @Thumbnail); 
SELECT * FROM Picture WHERE rowid in (SELECT last_insert_rowid());
";

        static readonly string InsertEmployeeSql =
@"
INSERT INTO Employee (NameId, PictureId, LoginUuid, LocationId, Email, Phone, Cell, Gender, Nat) 
VALUES (@NameId, @PictureId, @LoginUuid, @LocationId, @Email, @Phone, @Cell, @Gender, @Nat); 
SELECT * FROM Employee WHERE rowid in (SELECT last_insert_rowid());
";
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

        public async Task<Login> InsertLogin(Login entity)
        {
            using var connection = new SqliteConnection(_connectionString);
            return await connection.QueryFirstOrDefaultAsync<Login>(InsertLoginSql, entity);
        }

        public async Task<Picture> InsertPicture(Picture entity)
        {
            using var connection = new SqliteConnection(_connectionString);
            return await connection.QueryFirstOrDefaultAsync<Picture>(InsertPictureSql, entity);
        }

        public async Task<Name> InsertName(Name entity)
        {
            using var connection = new SqliteConnection(_connectionString);
            return await connection.QueryFirstOrDefaultAsync<Name>(InsertNameSql, entity);
        }

        public async Task<Location> InsertLocation(Location entity)
        {
            using var connection = new SqliteConnection(_connectionString);
            return await connection.QueryFirstOrDefaultAsync<Location>(InsertLocationSql, entity);
        }

        public async Task<Street> InsertStreet(Street entity)
        {
            using var connection = new SqliteConnection(_connectionString);
            return await connection.QueryFirstOrDefaultAsync<Street>(InsertStreetSql, entity);
        }
    }
}
