//Data Access has coded under different class, namespace.
public static DataSet Ins_EmployeeSelect()
        {
            try
            {
                DbCommand cmd = db.GetStoredProcCommand("Ins_EmployeeSelect");
                DataSet ds = db.ExecuteDataSet(cmd);
                if (ds != null)
                {
                    return ds;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                String str = ex.Message.ToString();
                return null;
            }
        }
        public static Int32 Ins_EmployeeInsert(string Name, int Salary, int Experience, String Gender, String DesiredLocation)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(Name) || Salary <= 0 || Experience <= 0)
                {
                    return -1;
                }
                string sql = "Ins_EmployeeInsert";
                DbCommand cmd = db.GetStoredProcCommand(sql);
                db.AddInParameter(cmd, "Name", DbType.String, Name);
                db.AddInParameter(cmd, "Salary", DbType.Int32, Salary);
                db.AddInParameter(cmd, "Experience", DbType.Int32, Experience);
                db.AddInParameter(cmd, "Gender", DbType.String, Gender);
                db.AddInParameter(cmd, "DesiredLocation", DbType.String, DesiredLocation);
                db.ExecuteNonQuery(cmd);
                object x = db.GetParameterValue(cmd, "EmployeeID");
                if (x != System.DBNull.Value)
                    return Convert.ToInt32(x);
                else
                    return -1;
            }
            catch (Exception ex)
            {
                string strError = ex.Message.ToString();
                return -1;
            }
        }
        public static Int32 Ins_EmployeeUpdate(int EmployeeID, string Name, int Salary, int Experience, string Gender, string DesiredLocation)
        {
            try
            {
                string UpdatedBy = HttpContext.Current.User.Identity.Name.ToString();
                string sql = "Ins_EmployeeUpdate";
                DbCommand cmd = db.GetStoredProcCommand(sql);
                db.AddInParameter(cmd, "EmployeeID", DbType.Int32, EmployeeID);
                db.AddInParameter(cmd, "Name", DbType.String, Name);
                db.AddInParameter(cmd, "Salary", DbType.Int32, Salary);
                db.AddInParameter(cmd, "Experience", DbType.Int32, Experience);
                db.AddInParameter(cmd, "Gender", DbType.String, Gender);
                db.AddInParameter(cmd, "DesiredLocation", DbType.String, DesiredLocation);
                db.ExecuteNonQuery(cmd);
                return 1;
            }
            catch (Exception ex)
            {
                string strError = ex.Message.ToString();
                return -1;
            }
        }
        public static Int32 Ins_EmployeeDelete(int EmployeeID)
        {
            try
            {
                string sql = "Ins_EmployeeDelete";
                DbCommand cmd = db.GetStoredProcCommand(sql);
                db.AddInParameter(cmd, "EmployeeID", DbType.Int32, EmployeeID);
                db.ExecuteNonQuery(cmd);

                return 1;
            }
            catch (Exception ex)
            {
                string strError = ex.Message.ToString();

                return -1;
            }
        }
