using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace prjMVCDemo.Models
{
    public class CCustomerFactory
    {
        public void Delete(int fId)
        {
            List<SqlParameter> paras = new List<SqlParameter>();
            string sql = "DELETE FROM tCustomer WHERE fId = @K_FID";
            paras.Add(new SqlParameter("K_FID", (object)fId));
            executeCmd(sql, paras);
        }

        public CCustomer queryById(int fId)
        {
            string sql = "SELECT * FROM tCustomer WHERE fId = @K_FID";
            List<SqlParameter> paras = new List<SqlParameter>();
            paras.Add(new SqlParameter("K_FID", (object)fId));
            var list = queryBySql(sql, paras);
            if (list.Count == 0)
                return null;
            return list[0];
        }

        internal List<CCustomer> queryByKeyword(string keyword)
        {
            string sql = "SELECT * FROM tCustomer WHERE fName LIKE @K_KEYWORD";
            sql += " OR fPhone LIKE @K_KEYWORD";
            sql += " OR fEmail LIKE @K_KEYWORD";
            sql += " OR fAddress LIKE @K_KEYWORD";

            List<SqlParameter> paras = new List<SqlParameter>();
            paras.Add(new SqlParameter("K_KEYWORD", "%" + (object)keyword + "%"));
            return queryBySql(sql, paras); ;
        }

        public List<CCustomer> queryAll()
        {
            return queryBySql("SELECT * FROM tCustomer", null);
        }

        private List<CCustomer> queryBySql(string sql, List<SqlParameter> paras)
        {
            List<CCustomer> list = new List<CCustomer>();
            SqlConnection con = new SqlConnection();
            con.ConnectionString = @"Data Source=.;Initial Catalog=dbDemo;Integrated Security=True";
            con.Open();

            SqlCommand cmd = new SqlCommand(sql, con);
            if (paras != null)
            {
                foreach (SqlParameter p in paras)
                {
                    cmd.Parameters.Add(p);
                }
            }
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new CCustomer()
                {
                    fId = (int)reader["fId"],
                    fName = reader["fName"].ToString(),
                    fPhone = reader["fPhone"].ToString(),
                    fEmail = reader["fEmail"].ToString(),
                    fAddress = reader["fAddress"].ToString(),
                    fPassword = reader["fPassword"].ToString(),
                });
            }

            con.Close();
            return list;            
        }
        public void update(CCustomer p)
        {
            List<SqlParameter> paras = new List<SqlParameter>();
            string sql = "UPDATE tCustomer SET";
            if (!string.IsNullOrEmpty(p.fName))
            {
                sql += " fName=@K_FNAME, ";
                paras.Add(new SqlParameter("K_FNAME", (object)p.fName));
            }
            if (!string.IsNullOrEmpty(p.fPhone))
            {
                sql += " fPhone=@K_FPHONE, ";
                paras.Add(new SqlParameter("K_FPHONE", (object)p.fPhone));
            }
            if (!string.IsNullOrEmpty(p.fEmail))
            {
                sql += " fEmail=@K_FEMAIL, ";
                paras.Add(new SqlParameter("K_FEMAIL", (object)p.fEmail));
            }
            if (!string.IsNullOrEmpty(p.fAddress))
            {
                sql += " fAddress=@K_FADDRESS, ";
                paras.Add(new SqlParameter("K_FADDRESS", (object)p.fAddress));
            }
            if (!string.IsNullOrEmpty(p.fPassword))
            {
                sql += " fPassword=@K_FPASSWORD, ";
                paras.Add(new SqlParameter("K_FPASSWORD", (object)p.fPassword));
            }
            sql = sql.Trim();
            if (sql.Substring(sql.Length - 1, 1) == ",")
                sql = sql.Substring(0, sql.Length - 1);
            sql += " WHERE fId=@FID";
            paras.Add(new SqlParameter("FID", (object)p.fId));
            executeCmd(sql, paras);
        }

        public void insert(CCustomer p)
        {
            List<SqlParameter> paras = new List<SqlParameter>();
            string sql = "INSERT INTO tCustomer(";
            if (!string.IsNullOrEmpty(p.fName))
                sql += " fName, ";
            if (!string.IsNullOrEmpty(p.fPhone))
                sql += " fPhone, ";
            if (!string.IsNullOrEmpty(p.fEmail))
                sql += " fEmail, ";
            if (!string.IsNullOrEmpty(p.fAddress))
                sql += " fAddress, ";
            if (!string.IsNullOrEmpty(p.fPassword))
                sql += " fPassword, ";
            sql = sql.Trim();
            if (sql.Substring(sql.Length - 1, 1) == ",")
                sql = sql.Substring(0, sql.Length - 1);
            sql += " )VALUES( ";
            if (!string.IsNullOrEmpty(p.fName))
            {
                sql += " @K_FNAME, ";
                paras.Add(new SqlParameter("K_FNAME", (object)p.fName));
            }
            if (!string.IsNullOrEmpty(p.fPhone))
            {
                sql += " @K_FPHONE, ";
                paras.Add(new SqlParameter("K_FPHONE", (object)p.fPhone));
            }
            if (!string.IsNullOrEmpty(p.fEmail))
            {
                sql += " @K_FEMAIL, ";
                paras.Add(new SqlParameter("K_FEMAIL", (object)p.fEmail));
            }
            if (!string.IsNullOrEmpty(p.fAddress))
            {
                sql += " @K_FADDRESS, ";
                paras.Add(new SqlParameter("K_FADDRESS", (object)p.fAddress));
            }
            if (!string.IsNullOrEmpty(p.fPassword))
            {
                sql += " @K_FPASSWORD, ";
                paras.Add(new SqlParameter("K_FPASSWORD", (object)p.fPassword));
            }
            sql = sql.Trim();
            if (sql.Substring(sql.Length - 1, 1) == ",")
                sql = sql.Substring(0, sql.Length - 1);
            sql += ")";
            executeCmd(sql, paras);
        }

        private void executeCmd(string sql, List<SqlParameter> paras)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = @"Data Source=.;Initial Catalog=dbDemo;Integrated Security=True";
            con.Open();

            SqlCommand cmd = new SqlCommand(sql, con);
            if (paras != null)
            {
                foreach(SqlParameter p in paras)
                {
                    cmd.Parameters.Add(p);
                }
            }
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}