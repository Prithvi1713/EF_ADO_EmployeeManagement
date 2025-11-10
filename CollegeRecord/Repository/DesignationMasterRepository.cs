using CollegeRecord.Models;
using EF_EmployeeRecordMgt.IRepository;
using Microsoft.Data.SqlClient;

namespace EF_EmployeeRecordMgt.Repository
{
    public class DesignationMasterRepository : IDesignationMaster
    {
        private readonly string _connectionString;

        public DesignationMasterRepository(IConfiguration configuration)
        {
            var connection = configuration.GetConnectionString("DefaultConnection");
            if (string.IsNullOrEmpty(connection))
                throw new InvalidOperationException(" Connection string is not Found");
            _connectionString = connection;
        }

        public IEnumerable<DesignationMaster> GetAllDesignations()
        {
            var designation = new List<DesignationMaster>();

            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand("SELECT * FROM DesignationMasters", conn);
                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        DesignationMaster designationMaster = new DesignationMaster()
                        {
                            DesignId = Convert.ToInt32(reader["DesignId"]),
                            DesignName = reader["DesignName"]?.ToString() ?? string.Empty
                        };

                        designation.Add(designationMaster);
                    }
                }
            }

            return designation;
        }

        public DesignationMaster GetDesignationById(int id)
        {
            DesignationMaster designation = null;
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = "select * from DesignationMasters where DesignId=@DesignId";
                var cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@DesignId", id);
                connection.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        designation = new DesignationMaster()
                        {
                            DesignId = (int)reader["DesignId"],
                            DesignName = reader["DesignName"]?.ToString() ?? string.Empty
                        };
                    }
                }
            }
            return designation;
        }
        public void AddDesignation(DesignationMaster designationMaster)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = " insert into DesignationMasters(DesignName) values(@DesignName)";

                var cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@DesignName", designationMaster.DesignName);
                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void EditDesignation(DesignationMaster designationMaster)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = "update DesignationMasters set DesignName=@DesignName where DesignId=@DesignId";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@DesignName", designationMaster.DesignName);
                command.Parameters.AddWithValue("@DesignId", designationMaster.DesignId);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void DeleteDesignation(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = "Delete DesignationMasters where DesignId = @DesignId";

                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@DesignId", id);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

    }
}
