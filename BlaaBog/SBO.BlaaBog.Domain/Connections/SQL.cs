﻿using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace SBO.BlaaBog.Domain.Connections
{
    public class SQL
    {
        private IConfigurationRoot _configuration;
        private string _connectionString;

        public SqlCommand Execute(string storedProcedure)
        {
            SqlCommand sqlCommand = new SqlCommand(storedProcedure, new SqlConnection(_connectionString));
            sqlCommand.CommandType = CommandType.StoredProcedure;
            return sqlCommand;
        }

        public SQL()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: false);
            _configuration = builder.Build();
            _connectionString = _configuration.GetConnectionString("Default");
        }
    }
}
