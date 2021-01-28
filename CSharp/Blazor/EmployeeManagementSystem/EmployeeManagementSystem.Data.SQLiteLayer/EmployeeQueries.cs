using Dapper;
using EmployeeManagementSystem.Data.Shared.Entities;
using EmployeeManagementSystem.Data.Shared.Interfaces;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Data.SQLiteLayer
{
    public class EmployeeQueries : IEmployeeQueries
    {
        private readonly string _connectionString;

        static readonly string SelectNameSql = "SELECT * FROM Name WHERE Id = @Id;";
        static readonly string SelectLoginSql = "SELECT * FROM Login WHERE Uuid = @Uuid;";
        static readonly string SelectStreetSql = "SELECT * FROM Street WHERE Id = @Id;";
        static readonly string SelectLocationSql = "SELECT * FROM Location WHERE Id = @Id;";
        static readonly string SelectPictureSql = "SELECT * FROM Picture WHERE Id = @Id;";
        static readonly string SelectEmployeeSql = "SELECT * FROM Employee WHERE Id = @Id;";

        static readonly string SelectAllLoginSql = "SELECT * FROM Login;";
        static readonly string SelectAllEmployeeSql = "SELECT * FROM Employee;";
        static readonly string SelectAllNameSql = "SELECT * FROM Name;";


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

        public async Task<Login> SelectLogin(Login entity)
        {
            using var connection = new SqliteConnection(_connectionString);
            return await connection.QueryFirstOrDefaultAsync<Login>(SelectLoginSql, entity);
        }

        public async Task<Picture> SelectPicture(Picture entity)
        {
            using var connection = new SqliteConnection(_connectionString);
            return await connection.QueryFirstOrDefaultAsync<Picture>(SelectPictureSql, entity);
        }

        public async Task<Name> SelectName(Name entity)
        {
            using var connection = new SqliteConnection(_connectionString);
            return await connection.QueryFirstOrDefaultAsync<Name>(SelectNameSql, entity);
        }

        public async Task<Location> SelectLocation(Location entity)
        {
            using var connection = new SqliteConnection(_connectionString);
            return await connection.QueryFirstOrDefaultAsync<Location>(SelectLocationSql, entity);
        }

        public async Task<Street> SelectStreet(Street entity)
        {
            using var connection = new SqliteConnection(_connectionString);
            return await connection.QueryFirstOrDefaultAsync<Street>(SelectStreetSql, entity);
        }

        public async Task<IEnumerable<Employee>> SelectAllEmployee()
        {
            using var connection = new SqliteConnection(_connectionString);
            return await connection.QueryAsync<Employee>(SelectAllEmployeeSql);
        }

        public async Task<IEnumerable<Login>> SelectAllLogin()
        {
            using var connection = new SqliteConnection(_connectionString);
            return await connection.QueryAsync<Login>(SelectAllLoginSql);
        }

        public async Task<IEnumerable<Name>> SelectAllName()
        {
            using var connection = new SqliteConnection(_connectionString);
            return await connection.QueryAsync<Name>(SelectAllNameSql);
        }
    }
}
