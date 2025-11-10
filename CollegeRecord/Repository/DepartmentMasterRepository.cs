using CollegeRecord.Models;
using Microsoft.Data.SqlClient;
using EF_EmployeeRecordMgt.IRepository;

namespace EF_EmployeeRecordMgt.Repository
{
    public class DepartmentMasterRepository : IDepartmentMaster
    {
        private readonly string _connectionString;

        public DepartmentMasterRepository(IConfiguration configuration)
        {
            var connection = configuration.GetConnectionString("DefaultConnection");
            if (string.IsNullOrEmpty(connection))
                throw new InvalidOperationException(" Connection string is not Found");
            _connectionString = connection;
        }

        public IEnumerable<DepartmentMaster> GetAllDepartment()
        {
            var department = new List<DepartmentMaster>();

            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand("SELECT * FROM DepartmentMasters", conn);
                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        DepartmentMaster departmentMaster = new DepartmentMaster()
                        {
                            DeptId = Convert.ToInt32(reader["DeptId"]),
                            DeptName = reader["DeptName"]?.ToString() ?? string.Empty
                        };

                        department.Add(departmentMaster);
                    }
                }
            }

            return department;
        }

        public DepartmentMaster GetDepartmentById(int id)
        {
            DepartmentMaster department = null;
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = "select * from departmentMasters where DeptId=@DeptId";
                var cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@DeptId", id);
                connection.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        department = new DepartmentMaster()
                        {
                            DeptId = (int)reader["DeptId"],
                            DeptName = reader["DeptName"]?.ToString() ?? string.Empty
                        };
                    }
                }
            }
            return department;
        }
        public void AddDepartment(DepartmentMaster departmentMaster)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = " insert into departmentMasters(DeptName) values(@DeptName)";

                var cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@DeptName", departmentMaster.DeptName);
                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void EditDepartment(DepartmentMaster departmentMaster)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = "update departmentMasters set DeptName=@DeptName where DeptId=@DeptId";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@DeptName", departmentMaster.DeptName);
                command.Parameters.AddWithValue("@DeptId", departmentMaster.DeptId);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void DeleteDepartment(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = "Delete departmentMasters where DeptId = @DeptId";

                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@DeptId", id);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }


        //public List<DepartmentMaster> GetALlDepartmentByDataAdpter()
        //{
        //    List<DepartmentMaster> departmentMasters = new List<DepartmentMaster>();
        //    using (var connection = new SqlConnection(_connectionString))
        //    {
        //        var query = " Select * form departmentMaster";
        //        //var command = new SqlCommand(query, connection);
        //        SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query,connection);
        //        DataSet ds = new DataSet();
        //        sqlDataAdapter.Fill(ds, "departmentMasters");

        //        var table = ds.Tables["departmentMasters"];
        //        if(table != null)
        //        {
        //            foreach (DataRow dr in table.Rows)
        //            {
        //                departmentMasters.Add(new DepartmentMaster
        //                {
        //                    DeptId = Convert.ToInt32(dr["DeptId"]),
        //                    DeptName = dr["DeptName"]?.ToString() ?? string.Empty
        //                });
        //            }
        //        }
        //    }
        //}

    }
}
