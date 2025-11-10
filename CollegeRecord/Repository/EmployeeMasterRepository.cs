using CollegeRecord.Models;
using EF_EmployeeRecordMgt.IRepository;
using Microsoft.Data.SqlClient;
using System.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace EF_EmployeeRecordMgt.Repository
{
    public class EmployeeMasterRepository : IEmployeeMaster
    {
        private readonly string _connectionString;

        public EmployeeMasterRepository(IConfiguration configuration)
        {
            var connection = configuration.GetConnectionString("DefaultConnection");
            if (string.IsNullOrEmpty(connection))
                throw new InvalidOperationException(" Connection string is not Found");
            _connectionString = connection;
        }


        public IEnumerable<EmployeeMaster> GetAllEmployeeMaster()
        {
            var employees = new List<EmployeeMaster>();
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("usp_GetAllEmployees", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var employee = new EmployeeMaster
                        {
                            EmpId = Convert.ToInt32(reader["EmpId"]),
                            FirstName = reader["FirstName"]?.ToString() ?? "",
                            MiddleName = reader["MiddleName"]?.ToString() ?? "",
                            LastName = reader["LastName"]?.ToString() ?? "",
                            MobileNo = reader["MobileNo"]?.ToString() ?? "",
                            EmailId = reader["EmailId"]?.ToString() ?? "",
                            BirthDate = Convert.ToDateTime(reader["BirthDate"]),
                            Gender = reader["Gender"]?.ToString() ?? "",
                            salary = Convert.ToDecimal(reader["salary"]),
                            status = Convert.ToBoolean(reader["status"]),
                            DeptId = Convert.ToInt32(reader["DeptId"]),
                            DesignId = Convert.ToInt32(reader["DesignId"]),
                            departmentMaster = new DepartmentMaster
                            {
                                DeptId = Convert.ToInt32(reader["DeptId"]),
                                DeptName = reader["DeptName"]?.ToString() ?? ""
                            },
                            designationMaster = new DesignationMaster
                            {
                                DesignId = Convert.ToInt32(reader["DesignId"]),
                                DesignName = reader["DesignName"]?.ToString() ?? ""
                            }
                        };

                        employees.Add(employee);
                    }
                }
            }
            return employees;
        }

        public EmployeeMaster GetEmployeeById(int id)
        {
            EmployeeMaster employee = null;
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("usp_GetEmployeeById", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@EmpId", id);

                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        employee = new EmployeeMaster
                        {
                            EmpId = Convert.ToInt32(reader["EmpId"]),
                            FirstName = reader["FirstName"]?.ToString() ?? "",
                            MiddleName = reader["MiddleName"]?.ToString() ?? "",
                            LastName = reader["LastName"]?.ToString() ?? "",
                            MobileNo = reader["MobileNo"]?.ToString() ?? "",
                            EmailId = reader["EmailId"]?.ToString() ?? "",
                            BirthDate = Convert.ToDateTime(reader["BirthDate"]),
                            Gender = reader["Gender"]?.ToString() ?? "",
                            salary = Convert.ToDecimal(reader["salary"]),
                            status = Convert.ToBoolean(reader["status"]),
                            DeptId = Convert.ToInt32(reader["DeptId"]),
                            DesignId = Convert.ToInt32(reader["DesignId"]),
                            departmentMaster = new DepartmentMaster
                            {
                                DeptId = Convert.ToInt32(reader["DeptId"]),
                                DeptName = reader["DeptName"]?.ToString() ?? ""
                            },
                            designationMaster = new DesignationMaster
                            {
                                DesignId = Convert.ToInt32(reader["DesignId"]),
                                DesignName = reader["DesignName"]?.ToString() ?? ""
                            }
                        };
                    }
                }
            }
            return employee;
        }


        public void AddEmployee(EmployeeMaster employeeMaster)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("usp_AddEmployee", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@FirstName", employeeMaster.FirstName);
                command.Parameters.AddWithValue("@MiddleName", employeeMaster.MiddleName);
                command.Parameters.AddWithValue("@LastName", employeeMaster.LastName);
                command.Parameters.AddWithValue("@MobileNo", employeeMaster.MobileNo);
                command.Parameters.AddWithValue("@EmailId", employeeMaster.EmailId);
                command.Parameters.AddWithValue("@BirthDate", employeeMaster.BirthDate);
                command.Parameters.AddWithValue("@Gender", employeeMaster.Gender);
                command.Parameters.AddWithValue("@salary", employeeMaster.salary);
                command.Parameters.AddWithValue("@status", employeeMaster.status);
                command.Parameters.AddWithValue("@DeptId", employeeMaster.DeptId);
                command.Parameters.AddWithValue("@DesignId", employeeMaster.DesignId);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void EditEmployee(EmployeeMaster employeeMaster)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("usp_UpdateEmployee", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@EmpId", employeeMaster.EmpId);
                command.Parameters.AddWithValue("@FirstName", employeeMaster.FirstName);
                command.Parameters.AddWithValue("@MiddleName", employeeMaster.MiddleName);
                command.Parameters.AddWithValue("@LastName", employeeMaster.LastName);
                command.Parameters.AddWithValue("@MobileNo", employeeMaster.MobileNo);
                command.Parameters.AddWithValue("@EmailId", employeeMaster.EmailId);
                command.Parameters.AddWithValue("@BirthDate", employeeMaster.BirthDate);
                command.Parameters.AddWithValue("@Gender", employeeMaster.Gender);
                command.Parameters.AddWithValue("@salary", employeeMaster.salary);
                command.Parameters.AddWithValue("@status", employeeMaster.status);
                command.Parameters.AddWithValue("@DeptId", employeeMaster.DeptId);
                command.Parameters.AddWithValue("@DesignId", employeeMaster.DesignId);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void DeleteEmployee(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("usp_DeleteEmployee", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@EmpId", id);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        //public IEnumerable<EmployeeMaster> GetAllEmployeeMaster()
        //{
        //    var employees = new List<EmployeeMaster>();

        //    using (var connection = new SqlConnection(_connectionString))
        //    using (var command = new SqlCommand("usp_GetAllEmployees", connection))
        //    {
        //        command.CommandType = CommandType.StoredProcedure;
        //        connection.Open();

        //        using (var reader = command.ExecuteReader())
        //        {
        //            while (reader.Read())
        //            {
        //                employees.Add(new EmployeeMaster()
        //                {
        //                    EmpId = Convert.ToInt32(reader["EmpId"]),
        //                    FirstName = reader["FirstName"].ToString(),
        //                    MiddleName = reader["MiddleName"].ToString(),
        //                    LastName = reader["LastName"].ToString(),
        //                    MobileNo = reader["MobileNo"].ToString(),
        //                    EmailId = reader["EmailId"].ToString(),
        //                    BirthDate = Convert.ToDateTime(reader["BirthDate"]),
        //                    Gender = reader["Gender"].ToString(),
        //                    salary = Convert.ToDecimal(reader["salary"]),
        //                    status = Convert.ToBoolean(reader["status"]),
        //                    DeptId = Convert.ToInt32(reader["DeptId"]),
        //                    DesignId = Convert.ToInt32(reader["DesignId"])
        //                });
        //            }
        //        }
        //    }
        //    return employees;
        //}
        //public void AddEmployee(EmployeeMaster employeeMaster)
        //{
        //    using (var connection = new SqlConnection(_connectionString))
        //    using (var command = new SqlCommand("usp_AddEmployee", connection))
        //    {
        //        command.CommandType = CommandType.StoredProcedure;
        //        command.Parameters.AddWithValue("@FirstName", employeeMaster.FirstName);
        //        command.Parameters.AddWithValue("@MiddleName", employeeMaster.MiddleName);
        //        command.Parameters.AddWithValue("@LastName", employeeMaster.LastName);
        //        command.Parameters.AddWithValue("@MobileNo", employeeMaster.MobileNo);
        //        command.Parameters.AddWithValue("@EmailId", employeeMaster.EmailId);
        //        command.Parameters.AddWithValue("@BirthDate", employeeMaster.BirthDate);
        //        command.Parameters.AddWithValue("@Gender", employeeMaster.Gender);
        //        command.Parameters.AddWithValue("@salary", employeeMaster.salary);
        //        command.Parameters.AddWithValue("@status", employeeMaster.status);
        //        command.Parameters.AddWithValue("@DeptId", employeeMaster.DeptId);
        //        command.Parameters.AddWithValue("@DesignId", employeeMaster.DesignId);

        //        connection.Open();
        //        command.ExecuteNonQuery();
        //    }
        //}
        //public void EditEmployee(EmployeeMaster employeeMaster)
        //{
        //    using (var connection = new SqlConnection(_connectionString))
        //    using (var command = new SqlCommand("usp_UpdateEmployee", connection))
        //    {
        //        command.CommandType = CommandType.StoredProcedure;
        //        command.Parameters.AddWithValue("@EmpId", employeeMaster.EmpId);
        //        command.Parameters.AddWithValue("@FirstName", employeeMaster.FirstName);
        //        command.Parameters.AddWithValue("@MiddleName", employeeMaster.MiddleName);
        //        command.Parameters.AddWithValue("@LastName", employeeMaster.LastName);
        //        command.Parameters.AddWithValue("@MobileNo", employeeMaster.MobileNo);
        //        command.Parameters.AddWithValue("@EmailId", employeeMaster.EmailId);
        //        command.Parameters.AddWithValue("@BirthDate", employeeMaster.BirthDate);
        //        command.Parameters.AddWithValue("@Gender", employeeMaster.Gender);
        //        command.Parameters.AddWithValue("@salary", employeeMaster.salary);
        //        command.Parameters.AddWithValue("@status", employeeMaster.status);
        //        command.Parameters.AddWithValue("@DeptId", employeeMaster.DeptId);
        //        command.Parameters.AddWithValue("@DesignId", employeeMaster.DesignId);

        //        connection.Open();
        //        command.ExecuteNonQuery();
        //    }
        //}



        //public EmployeeMaster GetEmployeeById(int id)
        //{
        //    EmployeeMaster employee = null;

        //    using (var connection = new SqlConnection(_connectionString))
        //    using (var command = new SqlCommand("usp_GetEmployeeById", connection))
        //    {
        //        command.CommandType = CommandType.StoredProcedure;
        //        command.Parameters.AddWithValue("@EmpId", id);

        //        connection.Open();
        //        using (var reader = command.ExecuteReader())
        //        {
        //            if (reader.Read())
        //            {
        //                employee = new EmployeeMaster()
        //                {
        //                    EmpId = Convert.ToInt32(reader["EmpId"]),
        //                    FirstName = reader["FirstName"].ToString(),
        //                    MiddleName = reader["MiddleName"].ToString(),
        //                    LastName = reader["LastName"].ToString(),
        //                    MobileNo = reader["MobileNo"].ToString(),
        //                    EmailId = reader["EmailId"].ToString(),
        //                    BirthDate = Convert.ToDateTime(reader["BirthDate"]),
        //                    Gender = reader["Gender"].ToString(),
        //                    salary = Convert.ToDecimal(reader["salary"]),
        //                    status = Convert.ToBoolean(reader["status"]),
        //                    DeptId = Convert.ToInt32(reader["DeptId"]),
        //                    DesignId = Convert.ToInt32(reader["DesignId"])
        //                };
        //            }
        //        }
        //    }
        //    return employee;
        //}


        //public void DeleteEmployee(int id)
        //{
        //    using (var connection = new SqlConnection(_connectionString))
        //    using (var command = new SqlCommand("usp_DeleteEmployee", connection))
        //    {
        //        command.CommandType = CommandType.StoredProcedure;
        //        command.Parameters.AddWithValue("@EmpId", id);
        //        connection.Open();
        //        command.ExecuteNonQuery();
        //    }
        //}

    }
}
