using Microsoft.AspNetCore.Http;
using MySql.Data.MySqlClient;
using ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class DataAccessService : IDataAccessService
    {
        private readonly string _connectionString;

        public DataAccessService(string connectionString) 
        {
            _connectionString = connectionString;
        }

        public void SaveFileToDB(IFormFile file, int id)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                var sanitizedFilename = MySqlHelper.EscapeString(file.FileName);

                // Prepare the SQL command with parameter placeholders
                var sql = "INSERT INTO FileRecords (Filename, FileData, ReportId) VALUES (@Filename, @FileData, @ReportId)";

                using (var command = new MySqlCommand(sql, connection))
                {
                    // Set the parameter values
                    command.Parameters.AddWithValue("@Filename", sanitizedFilename);
                    command.Parameters.AddWithValue("@FileData", ReadFileData(file));
                    command.Parameters.AddWithValue("@ReportId", id);

                    // Execute the SQL command
                    command.ExecuteNonQuery();
                }
            }
        }
        private byte[] ReadFileData(IFormFile file)
        {
            using (var memoryStream = new MemoryStream())
            {
                file.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }
}
