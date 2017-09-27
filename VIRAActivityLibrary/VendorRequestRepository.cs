using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;

namespace VIRAActivityLibrary
{
   
        // Repository of vendor request data
        public static class VendorRequestRepository
        {
            // Save a vendorRequest
            public static void Save(VendorRequestInfo vendorRequestInfo)
            {
                string connectionString = ConfigurationManager.ConnectionStrings["VIRA"].ConnectionString;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = connection.CreateCommand();
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "SaveVendorRequest";

                    command.Parameters.Add(new SqlParameter("@id", vendorRequestInfo.Id));
                    command.Parameters.Add(new SqlParameter("@requesterId", vendorRequestInfo.RequesterId));
                    command.Parameters.Add(new SqlParameter("@creationDate", vendorRequestInfo.CreationDate));
                    command.Parameters.Add(new SqlParameter("@facilityId", vendorRequestInfo.FacilityId));
                    command.Parameters.Add(new SqlParameter("@approvers", vendorRequestInfo.Approvers));
                    command.Parameters.Add(new SqlParameter("@workflowInstanceId", vendorRequestInfo.WorkflowInstanceId));
                    command.Parameters.Add(new SqlParameter("@isCompleted", vendorRequestInfo.IsCompleted ? 1 : 0));
                    command.Parameters.Add(new SqlParameter("@isSuccess", vendorRequestInfo.IsSuccess ? 1 : 0));
                    command.Parameters.Add(new SqlParameter("@isCancelled", vendorRequestInfo.IsCancelled ? 1 : 0));

                    command.ExecuteNonQuery();
                }
            }

            // Loads the data of a VendorRequest from the database
            public static VendorRequestInfo Load(string VendorRequestId)
            {
                string connectionString = ConfigurationManager.ConnectionStrings["VIRA"].ConnectionString;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = connection.CreateCommand();
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "SelectVendorRequest";
                    command.Parameters.Add(new SqlParameter("@id", VendorRequestId));

                    using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.Read())
                        {
                            return new VendorRequestInfo
                            {
                                Id = new Guid(reader["Id"].ToString()),
                                RequesterId = reader["RequesterId"].ToString(),
                                FacilityId = reader["FacilityId"].ToString(),
                                Approvers = reader["Approvers"].ToString(),
                                IsSuccess = (bool)reader["IsSuccess"],
                                IsCompleted = (bool)reader["IsCompleted"],
                                IsCancelled = (bool)reader["IsCancelled"],
                                WorkflowInstanceId = reader["WorkflowInstanceId"] is DBNull ? Guid.Empty : new Guid(reader["WorkflowInstanceId"].ToString())
                            };
                        }
                        else
                        {
                            throw new ArgumentException(string.Format("Invalid argument. No data available for Vendor Request {0}", VendorRequestId));
                        }
                    }
                }
            }       
        }    
    }

