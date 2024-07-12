using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cutech.Express.DataAccess;
using Cutech.Express.Inspection.Masters;
using DevExpress.Web;

namespace cuteQM19.Cutech.Express.Inspection.Masters
{
    public partial class EmployeeList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {            
            if (!IsPostBack)
            {
                ASPxRadioButtonList RadioButtonListGender = FindControl("RadioButtonListGender") as ASPxRadioButtonList;
                if (RadioButtonListGender != null)
                {
                    RadioButtonListGender.Items.Add("Male");
                    RadioButtonListGender.Items.Add("Female");
                }
            }
            if (!IsPostBack)
            {
                ASPxCheckBox CheckBoxIndia = FindControl("CheckBoxIndia") as ASPxCheckBox;
                ASPxCheckBox CheckBoxSingapore = FindControl("CheckBoxSingapore") as ASPxCheckBox;
                ASPxCheckBox CheckBoxChina = FindControl("CheckBoxChina") as ASPxCheckBox;
                if (CheckBoxIndia != null && CheckBoxSingapore != null && CheckBoxChina != null)
                {
                    CheckBoxIndia.Text = "India";
                    CheckBoxSingapore.Text = "Singapore";
                    CheckBoxChina.Text = "China";
                }
            }

            if (!IsPostBack)
            {
                EmployeeGrid.CustomCallback += EmployeeGrid_CustomCallback;
            }
        }

        protected void ButtonAdd_Click(object sender, EventArgs e)
        {
            EmployeeGrid.AddNewRow();
        }

        protected void EmployeeObjectDataSource_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
        {
            try
            {
                if (e.Exception != null)
                {
                    e.ExceptionHandled = true;
                }
                else
                {
                    int Result = Convert.ToInt32(e.ReturnValue);
                    string Message = "Record Not Inserted";

                    if (Result > 0)
                    {
                        Message = "Record Inserted Successfully";
                        EmployeeGrid.JSProperties["cpIsUpdated"] = Message + ";Success";
                    }
                    else if (Result == 0)
                    {
                        Message = "Record Not Inserted";
                        EmployeeGrid.JSProperties["cpIsUpdated"] = Message + ";Error";
                    }
                }
            }               
                catch (Exception )
                {
                    throw;
                }
        }

        protected void EmployeeObjectDataSource_Updated(object sender, ObjectDataSourceStatusEventArgs e)
        {
            if (e.Exception != null)
            {

                e.ExceptionHandled = true;
            }
            else
            {

            }
                try
                {
                    int Result = Convert.ToInt32(e.ReturnValue);
                    string Message = String.Empty;

                    if (Result > 0)
                    {
                        Message = "Record Updated Successfully";
                        EmployeeGrid.JSProperties["cpIsUpdated"] = Message + ";Success";
                    ClientScript.RegisterStartupScript(GetType(), "insertSuccess", "alert('Record Updated Successfully');", true);
                }
                    else if (Result == 0)
                    {
                        Message = "Record Not Updated";
                        EmployeeGrid.JSProperties["cpIsUpdated"] = Message + ";Error";
                    ClientScript.RegisterStartupScript(GetType(), "insertSuccess", "alert('Record Not Updated');", true);
                }
                    else
                    {
                        Message = "Record Not Updated";
                        EmployeeGrid.JSProperties["cpIsUpdated"] = Message + ";Error";
                    ClientScript.RegisterStartupScript(GetType(), "insertSuccess", "alert('Record Not Updated');", true);
                }
                }
                catch (Exception)
                {
                    throw;
                }
        }

        protected void EmployeeObjectDataSource_Deleted(object sender, ObjectDataSourceStatusEventArgs e)
        {
            if (e.Exception != null)
            {
                e.ExceptionHandled = true;
            }
            else
            {

            }
            try
            {
                int Result = Convert.ToInt32(e.ReturnValue);
                string Message = String.Empty;

                if (Result > 0)
                {
                    Message = "Record Deleted Successfully";
                    EmployeeGrid.JSProperties["cpIsUpdated"] = Message + ";Success";
                }
                else if (Result == 0)
                {
                    Message = "Record Not Deleted";
                    EmployeeGrid.JSProperties["cpIsUpdated"] = Message + ";Error";
                }
                else
                {
                    Message = "Record Not Deleted";
                    EmployeeGrid.JSProperties["cpIsUpdated"] = Message + ";Error";
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void ButtonExport_Click(object sender, EventArgs e)
        {
            GridExporter.FileName = "EmployeeList_" + DateTime.Now.ToString("dd-MM-yyyy");
            if (((ASPxButton)sender as ASPxButton).ToolTip == Resources.Resource.EXPORTTOPDF)
            {
                GridExporter.WritePdfToResponse();
            }
            else
            {
                GridExporter.WriteXlsxToResponse();
            }
        }

        protected void ButtonFilter_Click(object sender, EventArgs e)
        {
            EmployeeGrid.Settings.ShowFilterRow = !EmployeeGrid.Settings.ShowFilterRow;
        }

        public void ShowMessage(string MsgType, ASPxGridView gridView)
        {
            try
            {
                if (HttpContext.Current.Session[MsgType] != null)
                {
                    string Message = HttpContext.Current.Session[MsgType].ToString();

                    if (Message.Contains("successfully"))
                    {
                        gridView.JSProperties["cpIsUpdated"] = Message + ";Success";
                    }
                    else
                    {
                        gridView.JSProperties["cpIsUpdated"] = Message + ";Error";
                    }
                    HttpContext.Current.Session.Remove(MsgType);
                }
            }
            catch (Exception ex)
            {
                string str = ex.Message.ToString();
                LogError(ex.Message, "Master Message Failed... ");
            }
        }
        private void LogError(string message, string v)
        {
            throw new NotImplementedException();
        }
        public List<string> GetExistingEmployeeNamesFromDB()
        {
            List<string> existingNames = new List<string>();
            try
            {
                DbCommand command = db.GetStoredProcCommand("Ins_EmployeeInsert");
                using (IDataReader reader = ExecuteReader(command))
                {
                    while (reader.Read())
                    {
                        existingNames.Add(reader["Name"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error fetching existing employee names: " + ex.Message);
                }
            return existingNames;
        }
        private IDataReader ExecuteReader(DbCommand command)
        {
            DataSet dataSet = db.ExecuteDataSet(command);
            if (dataSet.Tables.Count > 0)
            {
                return dataSet.Tables[0].CreateDataReader();
            }
            else
            {
                return null;
            }
        }
        public bool EmployeeNameExistsInDB(string Name)
        {
            Name = Name.Trim();
            List<string> existingNames = GetExistingEmployeeNamesFromDB();
            return existingNames.Exists(existingName => string.Equals(existingName, Name, StringComparison.OrdinalIgnoreCase));
        }

        protected void EmployeeGrid_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            string ValidInsert;
            try
            {
                if (e.NewValues[0].ToString() != string.Empty)
                {
                    string Message = InspectionDataAccess.CheckNameInputUnique(e.NewValues[0].ToString());
                    if (Message == "Name already exists")
                    {
                        ValidInsert = "No";
                        EmployeeObjectDataSource.InsertParameters[0].DefaultValue = ValidInsert;
                    }
                    else
                    {
                        ValidInsert = "Yes";
                        EmployeeObjectDataSource.InsertParameters[0].DefaultValue = ValidInsert;
                    }
                }
            }
            catch (Exception ex)
            {
                string str = ex.Message.ToString();
            }
            try
            {
                ASPxRadioButton RadioMale = EmployeeGrid.FindEditFormTemplateControl("RadioMale") as ASPxRadioButton;
                ASPxRadioButton RadioFemale = EmployeeGrid.FindEditFormTemplateControl("RadioFemale") as ASPxRadioButton;
                if (RadioMale != null && RadioFemale != null && e.NewValues[0].ToString() != string.Empty)
                {
                    if (RadioMale.Checked)
                    {
                        e.NewValues["Gender"] = "Male";
                    }
                    else if (RadioFemale.Checked)
                    {
                        e.NewValues["Gender"] = "Female";
                    }
                }
            }
            catch (Exception ex)
            {
                string str = ex.Message.ToString();
            }
            try
            {
                ASPxCheckBox CheckBoxIndia = EmployeeGrid.FindEditFormTemplateControl("CheckBoxIndia") as ASPxCheckBox;
                ASPxCheckBox CheckBoxSingapore = EmployeeGrid.FindEditFormTemplateControl("CheckBoxSingapore") as ASPxCheckBox;
                ASPxCheckBox CheckBoxChina = EmployeeGrid.FindEditFormTemplateControl("CheckBoxChina") as ASPxCheckBox;
                List<string> Location = new List<string>();
                string DesiredLocation = string.Join(",", Location);
                if (CheckBoxIndia.Checked)
                    DesiredLocation += "India,";
                if (CheckBoxSingapore.Checked)
                    DesiredLocation += "Singapore,";
                if (CheckBoxChina.Checked)
                    DesiredLocation += "China,";
                e.NewValues["DesiredLocation"] = DesiredLocation;
            }
            catch (Exception ex)
            {
                string str = ex.Message.ToString();
            }
        }

        protected void EmployeeGrid_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            try
            {
                string ValidUpdate;
                if (e.NewValues[0].ToString() != null)
                {
                    string Message = InspectionDataAccess.CheckNameInputUnique(e.NewValues[0].ToString());
                    if (Message == "Name already exists")
                    {
                        ValidUpdate = "No";
                        EmployeeObjectDataSource.UpdateParameters[0].DefaultValue = ValidUpdate;
                    }
                    else
                    {
                        ValidUpdate = "Yes";
                        EmployeeObjectDataSource.UpdateParameters[0].DefaultValue = ValidUpdate;
                        }
                }
            }
            catch (Exception ex)
            {
                string str = ex.Message.ToString();
            }
            try
            {
                ASPxRadioButton RadioMale = EmployeeGrid.FindEditFormTemplateControl("RadioMale") as ASPxRadioButton;
                ASPxRadioButton RadioFemale = EmployeeGrid.FindEditFormTemplateControl("RadioFemale") as ASPxRadioButton;
                if (RadioMale != null && RadioFemale != null && e.NewValues[0].ToString() != string.Empty)
                {
                    if (RadioMale.Checked)
                    {
                        e.NewValues["Gender"] = "Male";
                    }
                    else if (RadioFemale.Checked)
                    {
                        e.NewValues["Gender"] = "Female";
                    }
                }
            }
            catch (Exception ex)
            {
                string str = ex.Message.ToString();
            }
            try
            {
                ASPxCheckBox CheckBoxIndia = EmployeeGrid.FindEditFormTemplateControl("CheckBoxIndia") as ASPxCheckBox;
                ASPxCheckBox CheckBoxSingapore = EmployeeGrid.FindEditFormTemplateControl("CheckBoxSingapore") as ASPxCheckBox;
                ASPxCheckBox CheckBoxChina = EmployeeGrid.FindEditFormTemplateControl("CheckBoxChina") as ASPxCheckBox;
                List<string> Location = new List<string>();
                string DesiredLocation = string.Join(",", Location);
                if (CheckBoxIndia.Checked)
                    DesiredLocation += "India,";
                if (CheckBoxSingapore.Checked)
                    DesiredLocation += "Singapore,";
                if (CheckBoxChina.Checked)
                    DesiredLocation += "China,";
                e.NewValues["DesiredLocation"] = DesiredLocation;
            }
            catch (Exception ex)
            {
                string str = ex.Message.ToString();
            }
        }

        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            EmployeeGrid.UpdateEdit();
        }

        protected void EmployeeGrid_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            int editedRowIndex = int.Parse(e.Parameters);
            string genderValue = EmployeeGrid.GetRowValues(editedRowIndex, "Gender").ToString();
            ASPxRadioButton RadioMale = EmployeeGrid.FindEditFormTemplateControl("RadioMale") as ASPxRadioButton;
            ASPxRadioButton RadioFemale = EmployeeGrid.FindEditFormTemplateControl("RadioFemale") as ASPxRadioButton;
            if (genderValue == "Male")
            {
                RadioMale.Checked = true;
            }
            else if (genderValue == "Female")
            {
                RadioFemale.Checked = true;
            }
            string DesiredLocationValue = EmployeeGrid.GetRowValues(editedRowIndex, "DesiredLocation").ToString();
            ASPxCheckBox CheckBoxIndia = EmployeeGrid.FindEditFormTemplateControl("CheckBoxIndia") as ASPxCheckBox;
            ASPxCheckBox CheckBoxSingapore = EmployeeGrid.FindEditFormTemplateControl("CheckBoxSingapore") as ASPxCheckBox;
            ASPxCheckBox CheckBoxChina = EmployeeGrid.FindEditFormTemplateControl("CheckBoxChina") as ASPxCheckBox;
            if (DesiredLocationValue == "India")
            {
                CheckBoxIndia.Checked = true;
            }
            else if (DesiredLocationValue == "Singapore")
            {
                CheckBoxSingapore.Checked = true;
            }
            else if (DesiredLocationValue == "China")
            {
                CheckBoxChina.Checked = true;
            }
        }
    }
    }


