using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Security.Principal;
using System.Diagnostics;

namespace TazamaDSSClinicLinker
{
    public partial class fmMain : Form
    {
        public fmMain()
        {
            InitializeComponent();
        }

        string PatientId = "";
        string  TempUniqueCTCIDNumber = "";
        int countForTempUniqueCTCIDNumber = 20;
        string adssID = "";
        string Indiduallocation = "";
       // string MatchStatus = "";
        DataTable DssIndividuals = new DataTable();//holds adss individuals data retrieved from the healthfacility database
        DataRow AdssIndividualRow;
       // bool checkSearchIfClicked = false;
        string search_criteria = "";

        private void fmMain_Load(object sender, EventArgs e)
        {
            comHealthFacilityName.Text = "KISESA";
        }

        private void ClearPatientDetails()
        {

            foreach (Control ctrl in this.tpPatientDetails.Controls  )
            {
                if (ctrl.GetType() == typeof(System.Windows.Forms.GroupBox))
                {
                    foreach (Control gctrl in ctrl.Controls)
                    {
                        if (gctrl.GetType() == typeof(System.Windows.Forms.TextBox) | gctrl.GetType() == typeof(System.Windows.Forms.ComboBox))
                        {
                           
                                gctrl.Text = "";
                            
                           

                        }

                    }

                }

            }
            dgvVisitInformation.Rows.Clear();
            PatientId = "";

        }

        private void ClearLinkInformation()
        {

            foreach (Control ctrl in this.tpgLinkWithDSS.Controls)
            {
                if (ctrl.GetType() == typeof(System.Windows.Forms.GroupBox))
                {
                    foreach (Control gctrl in ctrl.Controls)
                    {
                        if (gctrl.GetType() == typeof(System.Windows.Forms.TextBox) | gctrl.GetType() == typeof(System.Windows.Forms.ComboBox)
                            | gctrl.GetType() == typeof(System.Windows.Forms.CheckBox))
                        {
                           
                                gctrl.Text = "";
                            
                                                    
                           

                        }

                    }

                }

            }
            ClearCheckBoxes();
            dgvDSSpersonalInformation.Rows.Clear();
            dgvMemberships.Rows.Clear();
            PatientId = "";
        }
       
        private void EnablePatientDetatils()
        {
            foreach (Control ctrl in this.tpPatientDetails.Controls)
            {
                if (ctrl.GetType() == typeof(System.Windows.Forms.GroupBox)& ctrl.Name != "gbVisitInformation" & ctrl.Name != "gpMatch_Status")
                {
                    foreach (Control gctrl in ctrl.Controls)
                    {
                        if (gctrl.GetType() == typeof(System.Windows.Forms.TextBox) | gctrl.GetType() == typeof(System.Windows.Forms.ComboBox))
                            
                        {

                            gctrl.Enabled = true;


                        }

                    }

                }

            }
        }

        private void DisablePatientDetatils()
        {
            foreach (Control ctrl in this.tpPatientDetails.Controls)
            {
                if (ctrl.GetType() == typeof(System.Windows.Forms.GroupBox) & ctrl.Name != "gbVisitInformation" & ctrl.Name != "gpMatch_Status")
                {
                    foreach (Control gctrl in ctrl.Controls)
                    {
                        if (gctrl.GetType() == typeof(System.Windows.Forms.TextBox) | gctrl.GetType() == typeof(System.Windows.Forms.ComboBox))
                        {

                            gctrl.Enabled = false;


                        }

                    }

                }

            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearPatientDetails();
            ClearLinkInformation();
            EnablePatientDetatils();
            txtUniqueCTCIDNumber.Text = "";
            txtUniqueCTCIDNumber.Enabled = true;
            //checkSearchIfClicked = false;
            this.txtUniqueCTCIDNumber.ForeColor = System.Drawing.Color.Black;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            if (comHealthFacilityName.Text != "" && (txtFileRef.TextLength > 0 || txtFirstName.TextLength > 0))
            {
                Object value;
                value = DBNull.Value;

                System.Data.SqlClient.SqlConnection sqlcnt = new System.Data.SqlClient.SqlConnection(Properties.Settings.Default.connection);
                try
                {
                    
                    // open connection
                    sqlcnt.Open();
                    // create command
                    SqlCommand sqlcmd = sqlcnt.CreateCommand();
                    // specify stored procedure to execute
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    sqlcmd.CommandText = "spAP_DA2C8B1D8B3D4DF58F346673B3FA1024";
                    //add health facility parameter
                    sqlcmd.Parameters.Add("@HealthFacilityName", SqlDbType.Char).Value = comHealthFacilityName.Text;
                    sqlcmd.Parameters.Add("@HealthFacilityDepartment", SqlDbType.Char).Value = comHealthFacilityDepartment.Text;
                    // add other parameters
                   
                    foreach (Control ctrl in this.tpPatientDetails.Controls)
                    {
                        if (ctrl.GetType() == typeof(System.Windows.Forms.GroupBox) & ctrl.Name != "gpMatch_Status" & ctrl.Name != "gbVisitInformation")
                        {

                            foreach (Control gctrl in ctrl.Controls)
                            {
                                if (gctrl.Tag != null)
                                {
                                    if (gctrl.Tag.ToString().Substring(0, 1) == "@")
                                    {
                                        if (gctrl.Text.Length == 0)
                                        {
                                            value = DBNull.Value;
                                        }
                                        else if (gctrl.Tag.ToString() == "@UniqueCTCIDNumber" && txtUniqueCTCIDNumber.Text == "  -  -  -    -")
                                        {
                                            value = "";
                                        }

                                        else
                                        {
                                            value = gctrl.Text;


                                        }
                                        SqlParameter para = new SqlParameter(gctrl.Tag.ToString(), value);
                                        sqlcmd.Parameters.Add(para);
                                    }

                                }

                            }
                        }

                    }
                    sqlcmd.ExecuteNonQuery();
                    DisablePatientDetatils();
                    Select_RecordNo();
                    PassLinkingVariables();
                    txtUniqueCTCIDNumber.Enabled = false;
                                                

                }

                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.ToString(), "Save Patient Record Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    sqlcnt.Close();
                }
            }
        }
        private void RetrievePatientDetails()
        { 
        
         
             Object value;
            value = DBNull.Value;
            using (System.Data.SqlClient.SqlConnection sqlcnt = new System.Data.SqlClient.SqlConnection(Properties.Settings.Default.connection))
                try
                {
                    // open connection
                    sqlcnt.Open();
                    // create command
                    SqlCommand sqlcmd = sqlcnt.CreateCommand();
                    // specify stored procedure to execute
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    sqlcmd.CommandText = "spAP_E132EDAA8F404CB1A69BAE17EE47DD03";
                    //add parameters
                    sqlcmd.Parameters.Add("@HealthFacilityName", SqlDbType.VarChar).Value = comHealthFacilityName.Text;
                    sqlcmd.Parameters.Add("@FileRef", SqlDbType.VarChar).Value = txtFileRef.Text;

                    // Read in the SELECT results.
                    //
                    SqlDataReader sqlrdr = sqlcmd.ExecuteReader();//sqlrdr reads from the select from procedure
                    while (sqlrdr.Read())
                    {

                        if (sqlrdr.IsDBNull(0) == false)
                        {
                            txtRegistrationDate.Text = sqlrdr.GetValue(0).ToString();
                        }
                        if (sqlrdr.IsDBNull(1) == false)
                        {
                            txtUniqueCTCIDNumber.Text = sqlrdr.GetValue(1).ToString();
                        }

                        if (sqlrdr.IsDBNull(2) == false)
                        {
                            txtTGRFormNumber.Text = sqlrdr.GetValue(2).ToString();
                        }
                        if (sqlrdr.IsDBNull(3) == false)
                        {
                            txtFirstName.Text = sqlrdr.GetValue(3).ToString();
                        }
                        if (sqlrdr.IsDBNull(4) == false)
                        {
                            txtMiddleName.Text = sqlrdr.GetValue(4).ToString();
                        }
                        if (sqlrdr.IsDBNull(5) == false)
                        {
                            txtLastName.Text = sqlrdr.GetValue(5).ToString();
                        }
                        if (sqlrdr.IsDBNull(6) == false)
                        {
                            comSex.Text = sqlrdr.GetValue(6).ToString();
                        }
                        if (sqlrdr.IsDBNull(7) == false)
                        {
                          comYearofBirth.Text = sqlrdr.GetValue(7).ToString();
                        }

                        if (sqlrdr.IsDBNull(8) == false)
                        {
                            comMonthofBirth.Text = sqlrdr.GetValue(8).ToString();
                        }

                        if (sqlrdr.IsDBNull(9) == false)
                        {
                            comDayofBirth.Text= sqlrdr.GetValue(9).ToString();
                        }

                        
                        if (sqlrdr.IsDBNull(10) == false)
                        {
                            txtVillage.Text = sqlrdr.GetValue(10).ToString();
                        }
                        if (sqlrdr.IsDBNull(11) == false)
                        {
                            txtSubvillage.Text = sqlrdr.GetValue(11).ToString();
                        }
                        if (sqlrdr.IsDBNull(12) == false)
                        {
                          comYearAtCurResident.Text = sqlrdr.GetValue(12).ToString();
                        }
                        if (sqlrdr.IsDBNull(13) == false)
                        {
                            comMonthAtCurResident.Text = sqlrdr.GetValue(13).ToString();
                        }
                        if (sqlrdr.IsDBNull(14) == false)
                        {
                            comDayAtCurResident.Text = sqlrdr.GetValue(14).ToString();
                        }
                        if (sqlrdr.IsDBNull(15) == false)
                        {
                            txtTCLFirstName.Text = sqlrdr.GetValue(15).ToString();
                        }

                        if (sqlrdr.IsDBNull(16) == false)
                        {
                            txtTCLMiddleName.Text = sqlrdr.GetValue(16).ToString();
                        }
                        
                        if (sqlrdr.IsDBNull(17) == false)
                        {
                            txtTCLLastName.Text = sqlrdr.GetValue(17).ToString();
                        }
                        if (sqlrdr.IsDBNull(18) == false)
                        {
                            txtHHMemberFirstName.Text = sqlrdr.GetValue(18).ToString();
                        }
                        if (sqlrdr.IsDBNull(19) == false)
                        {
                            txtHHMemberMiddleName.Text = sqlrdr.GetValue(19).ToString();
                        }
                        if (sqlrdr.IsDBNull(20) == false)
                        {
                           txtHHMemberLastName.Text = sqlrdr.GetValue(20).ToString();
                        }
                        if (sqlrdr.IsDBNull(21) == false)
                        {
                            txtTelNumber.Text = sqlrdr.GetValue(21).ToString();
                        }

                        if (sqlrdr.IsDBNull(22) == false)
                        {
                           comConsentStatus.Text = sqlrdr.GetValue(22).ToString();
                        }
                                                
                         if (sqlrdr.IsDBNull(23) == false)
                        {
                            PatientId = sqlrdr.GetValue(23).ToString();
                        }

                         DisablePatientDetatils();
                         PassLinkingVariables();
                         Retrieve_VisitDateInfo();
                         txtUniqueCTCIDNumber.Enabled = false;
                         this.txtUniqueCTCIDNumber.ForeColor = System.Drawing.Color.Black;

                         //if (CheckIfMatchExists() == true)
                         //{
                         //    DisplayMatchStatus();

                         //}

                    }
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.ToString(), "Read database tables Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    sqlcnt.Close();
                }
           
                
        
        }

        //private void LoadHealthFacilities()
        //{
        //    //clear health facilities
        //    //comHealthFacilityName.Items.Clear();

        //    //Load Health Facilities
        //    // query
        //    string sql = @"EXEC spAP_F9B3186072CC43B786020D6914065B95";
        //    DataTable dt = new DataTable();
        //    System.Data.SqlClient.SqlConnection sqlcnt = new System.Data.SqlClient.SqlConnection(Properties.Settings.Default.connection);
        //    try
        //    {
        //        sqlcnt.Open();
        //        // create data adapter

        //        SqlDataAdapter da = new SqlDataAdapter(sql, sqlcnt);
        //        // create dataset
        //        DataSet ds = new DataSet();
        //        // fill dataset
        //        da.Fill(ds, "tables");
        //        // get data table
        //        dt = ds.Tables["tables"];
        //    }

        //    catch (Exception Ex)
        //    {
        //        MessageBox.Show(Ex.ToString(), "Read database tables Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //    finally
        //    {
        //        sqlcnt.Close();
        //        foreach (DataRow row in dt.Rows)
        //        {
        //            foreach (DataColumn col in dt.Columns)
        //            {
        //                comHealthFacilityName.Items.Add(row[col]);
                        
        //            }
        //        }
        //    }
        //}

        //private void comHealthFacilityName_Enter(object sender, EventArgs e)
        //{
        //    //LoadHealthFacilities();
            
        //}

        private void DisplayMatchStatus()
        {


            txtDSSID.Clear();
            txtDSS_ID.Clear();
           

            Object value;
            value = DBNull.Value;
            using (System.Data.SqlClient.SqlConnection sqlcnt = new System.Data.SqlClient.SqlConnection(Properties.Settings.Default.connection))
                try
                {
                    // open connection
                    sqlcnt.Open();
                    // create command
                    SqlCommand sqlcmd = sqlcnt.CreateCommand();
                    // specify stored procedure to execute
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    sqlcmd.CommandText = "spAP_FBBACFDDDA584A618694C9A4CBE7FD17";
                    //add parameters
                    sqlcmd.Parameters.Add("@FileREf", SqlDbType.VarChar).Value = txtFileRef.Text;
                    //sqlcmd.Parameters.Add("@UniqueCTC", SqlDbType.VarChar).Value = txtUniqueCTCIDNumber.Text;
                    sqlcmd.Parameters.Add("@HealthFacilityName", SqlDbType.VarChar).Value = comHealthFacilityName.Text;
                    
                    // Read in the SELECT results.
                    //
                    SqlDataReader sqlrdr = sqlcmd.ExecuteReader();
                    while (sqlrdr.Read())
                    {

                        if (sqlrdr.IsDBNull(0) == false)
                        {
                            txtDSSID.Text = sqlrdr.GetValue(0).ToString();
                            txtDSS_ID.Text = sqlrdr.GetValue(0).ToString();
                        }
                        if (sqlrdr.IsDBNull(1) == false)
                        {
                            txtMatchStatus.Text = sqlrdr.GetValue(1).ToString();

                        }


                    }


                }

                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.ToString(), "Read database tables Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    sqlcnt.Close();
                }

        }

        private void txtFileRef_TextChanged(object sender, EventArgs e)
        {
            //ClearsPatientDetailsExceptFileRef();
            //ClearLinkInformation();
            //txtUniqueCTCIDNumber.Text = "";
            RetrievePatientDetails();
            
                       

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            EnablePatientDetatils();
            txtUniqueCTCIDNumber.Enabled = true;
        }

        private void txtHHMemberLastName_Leave(object sender, EventArgs e)
        {
            btnSave.Focus();
        }

        private void PassLinkingVariables()
         {
            txtFirstNameLink.Text= txtFirstName.Text;
            txtMiddleNameLink.Text = txtMiddleName.Text;
            txtLastNameLink.Text = txtLastName.Text;
            if (comSex.Text != "")
            {
                txtSexLink.Text = comSex.Text.Substring(0, 1);
            }
            txtYearLink.Text = comYearofBirth.Text;
            txtMonthLink.Text = comMonthofBirth.Text;
            txtDayLink.Text = comDayofBirth.Text;
            txtVillageLink.Text = txtVillage.Text;
            txtSubVillageLink.Text = txtSubvillage.Text;
            txtTCLFirstNameLink.Text = txtTCLFirstName.Text;
            txtTCLMiddleNameLink.Text = txtTCLMiddleName.Text;
            txtTCLLastNameLink.Text = txtTCLLastName.Text;
            txtHHMemberFirstNameLink.Text = txtHHMemberFirstName.Text;
            txtHHMemberMiddlenameLink.Text = txtHHMemberMiddleName.Text;
            txtHHMemberLastNameLink.Text = txtHHMemberLastName.Text;


    
    
         }

        private void btnSearchAHDSS_Click(object sender, EventArgs e)
        {/*
            if ((cbFirstName.Checked = true && txtFirstNameLink.TextLength > 0) || (cbMiddleName.Checked = true && txtMiddleNameLink.TextLength > 0) ||
               (cbLastName.Checked = true && txtLastName.TextLength > 0) || (cbSex.Checked = true && txtSexLink.TextLength > 0) ||
                 (cbYear.Checked = true && txtYearLink.TextLength > 0) || (cbMonth.Checked = true && txtMonthLink.TextLength > 0) ||
                (cbDay.Checked = true && txtDayLink.TextLength > 0) || (cbVillage.Checked = true && txtVillageLink.TextLength > 0) ||
                (cbSubVillage.Checked = true && txtSubvillage.TextLength > 0) || (cbTCLFirstName.Checked = true && txtTCLFirstNameLink.TextLength > 0) ||
                (cbTCLMiddleName.Checked = true && txtTCLFirstNameLink.TextLength > 0) || (cbTCLLastName.Checked = true && txtTCLLastNameLink.TextLength > 0)
              )
          */
            txtDSSID.Clear();
            dgvDSSpersonalInformation.Rows.Clear();
            dgvMemberships.Rows.Clear();
            search_criteria = ""; //reset search criteria
            Search4DSSindividualPotentialMatches();
           // checkSearchIfClicked = true;
        }

      
        

        private void btnSaveVisitDate_Click(object sender, EventArgs e)
        {
            FollowUPVisitDate();
        }

        private void FollowUPVisitDate()
        {
            if (txtVisitDate.Text.Length > 0)
            {
                int nrow;
                List<string> list = new List<string>();
                string[] FollowUPVisits;
                nrow = dgvVisitInformation.RowCount;
                if (nrow > 0)
                {
                    for (int i = 0; i < nrow; i++)
                    {
                        list.Add(dgvVisitInformation.Rows[i].Cells[0].Value.ToString());//cell[0]=column[0]
                    }
                }
                FollowUPVisits = list.ToArray();
                if (FollowUPVisits.Any(txtVisitDate.Text.Contains))
                {

                }
                else
                {
                    dgvVisitInformation.Rows.Add(txtVisitDate.Text);
                }

               try
                {
                    using (System.Data.SqlClient.SqlConnection sqlcnt = new System.Data.SqlClient.SqlConnection(Properties.Settings.Default.connection))
                    {
                        // open connection
                        sqlcnt.Open();
                        // create command
                        SqlCommand sqlcmd = sqlcnt.CreateCommand();
                        // specify stored procedure to execute
                        sqlcmd.CommandType = CommandType.StoredProcedure;
                        sqlcmd.CommandText = "spAP_E9E31C7395EB4322817BBC578C1C4167";
                        //add health facility parameter
                        sqlcmd.Parameters.Add("@PatientId", SqlDbType.VarChar).Value = PatientId;
                        sqlcmd.Parameters.Add("@HealthFacilityName", SqlDbType.VarChar).Value = comHealthFacilityName.Text;
                        sqlcmd.Parameters.Add("@FileRef", SqlDbType.VarChar).Value = txtFileRef.Text;
                        sqlcmd.Parameters.Add("@VisitDate", SqlDbType.VarChar).Value = txtVisitDate.Text;
                        sqlcmd.Parameters.Add("@VisitBy", SqlDbType.VarChar).Value = ComVisitBy.Text;
                        sqlcmd.ExecuteNonQuery();
                        txtVisitDate.Clear();
                        ComVisitBy.Text = "";
                        Retrieve_VisitDateInfo();
                        dgvVisitInformation.ReadOnly = true;
                        sqlcnt.Close();

                    }
                }

                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.ToString(), "Save Patient Record Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    //sqlcnt.Close();
                }
               
            }
        }
        
        private void Retrieve_VisitDateInfo()
        {
            Object value;
            value = DBNull.Value;
            SqlDataAdapter da = new SqlDataAdapter();
            DataTable dt = new DataTable();

            using (System.Data.SqlClient.SqlConnection sqlcnt = new System.Data.SqlClient.SqlConnection(Properties.Settings.Default.connection))
                try
                {
                    // open connection
                    sqlcnt.Open();
                    // create command
                    SqlCommand sqlcmd = sqlcnt.CreateCommand();
                    // specify stored procedure to execute
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    sqlcmd.CommandText = "spAP_FE2AA0B3066347B1A54823DA8785B2F5";
                    //add parameters
                    sqlcmd.Parameters.Add("@HealthFacilityName", SqlDbType.VarChar).Value = comHealthFacilityName.Text;
                    sqlcmd.Parameters.Add("@FileRef", SqlDbType.VarChar).Value = txtFileRef.Text;
                    da.SelectCommand = sqlcmd;
                    da.Fill(dt);
                    dgvVisitInformation.Rows.Clear();
                    int nrows = dt.Rows.Count;
                    int ncols = dt.Columns.Count;
                    for (int i = 0; i < nrows; i++)
                    {
                        dgvVisitInformation.Rows.Add(dt.Rows[i].ItemArray);//fill the data grid of VisitDates with data
                    }


                }

                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.ToString(), "Read database tables Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    sqlcnt.Close();
                }

        }

        private void txtUniqueCTCIDNumber_Leave(object sender, EventArgs e)
        {

            if (countForTempUniqueCTCIDNumber == 2 && txtUniqueCTCIDNumber.Text != "  -  -  -    -")
            {

                TempUniqueCTCIDNumber = txtUniqueCTCIDNumber.Text;
                countForTempUniqueCTCIDNumber = 1;
                txtUniqueCTCIDNumber.Clear();
                txtUniqueCTCIDNumber.Focus();

            }

            else if (countForTempUniqueCTCIDNumber == 1 && txtUniqueCTCIDNumber.Text != "  -  -  -    -")
            {
                if (TempUniqueCTCIDNumber == txtUniqueCTCIDNumber.Text)
                {
                    this.txtUniqueCTCIDNumber.ForeColor = System.Drawing.Color.Green;
                    txtTGRFormNumber.Focus();

                }
                else if (TempUniqueCTCIDNumber != txtUniqueCTCIDNumber.Text)
                {
                    this.txtUniqueCTCIDNumber.ForeColor = System.Drawing.Color.Red;
                    txtUniqueCTCIDNumber.Focus();

                }

                else if (txtUniqueCTCIDNumber.Mask == "  -  -  -    -")
                {
                    txtTGRFormNumber.Focus();

                }

            }
            else
            {
                txtTGRFormNumber.Focus();

            }
            txtTGRFormNumber.Focus();
       
        }

        


        private void ClearsPatientDetailsExceptFileRef()
        {

            foreach (Control ctrl in this.tpPatientDetails.Controls)
            {
                if (ctrl.GetType() == typeof(System.Windows.Forms.GroupBox))
                {
                    foreach (Control gctrl in ctrl.Controls)
                    {
                        if (gctrl.GetType() == typeof(System.Windows.Forms.TextBox) | gctrl.GetType() == typeof(System.Windows.Forms.ComboBox))
                        {
                            if (gctrl.Name != "txtFileRef")
                            {
                                gctrl.Text = "";
                            }

                        }

                    }

                }

            }
            dgvVisitInformation.Rows.Clear();
            PatientId = "";                          
                  
        }

        private void Search4DSSindividualPotentialMatches()
        {
            Object value;
            value = DBNull.Value;
            SqlDataAdapter da = new SqlDataAdapter();
            //DataTable DssIndividuals = new DataTable();

            bool UseFirstName;
            if (cbFirstName.Checked == true)
            {
                UseFirstName = true;
                search_criteria = search_criteria + "First Name";
            }
            else
                UseFirstName = false;

            bool UseMiddleName;
            if (cbMiddleName.Checked == true)
            {
                UseMiddleName = true;
                search_criteria = search_criteria + ", Middle Name";
            }
            else
                UseMiddleName = false;

            bool UseLastName;
            if (cbLastName.Checked == true)
            {
                UseLastName = true;
                search_criteria = search_criteria + ", Last Name";
            }
            else
                UseLastName = false;

            bool UseTLFirstName;
            if (cbTCLFirstName.Checked == true)
            {
                UseTLFirstName = true;
                search_criteria = search_criteria + ", TCL First Name";
            }
            else
                UseTLFirstName = false;

            bool UseTLMiddleName;
            if (cbTCLMiddleName.Checked == true)
            {
                UseTLMiddleName = true;
                search_criteria = search_criteria + ", TCL Middle Name";
            }
            else
                UseTLMiddleName = false;

            bool UseTLLastName;
            if (cbTCLLastName.Checked == true)
            {
                UseTLLastName = true;
                search_criteria = search_criteria + ", TCL Last Name";
            }
            else
                UseTLLastName = false;

            bool UseGender;
            if (cbSex.Checked == true)
            {
                UseGender = true;
                search_criteria = search_criteria + ", Sex";
            }
            else
                UseGender = false;

            bool UseBDay;
            if (cbDay.Checked == true)
            {
                UseBDay = true;
                search_criteria = search_criteria + ", Day";

            }
            else
                UseBDay = false;

            bool UseBMonth;
            if (cbMonth.Checked == true)
            {
                UseBMonth = true;
                search_criteria = search_criteria + ", Month";
            }
            else
                UseBMonth = false;

            bool UseBYear;
            if (cbYear.Checked == true)
            {
                UseBYear = true;
                search_criteria = search_criteria + ", BYear";
            }
            else
                UseBYear = false;

            bool UseVillage;
            if (cbVillage.Checked == true)
            {
                UseVillage = true;
                search_criteria = search_criteria + ", Village";
            }
            else
                UseVillage = false;

            bool UseSubVillage;
            if (cbSubVillage.Checked == true)
            {
                UseSubVillage = true;
                search_criteria = search_criteria + ", Sub Village";
            }
            else
                UseSubVillage = false;

            using (System.Data.SqlClient.SqlConnection sqlcnt = new System.Data.SqlClient.SqlConnection(Properties.Settings.Default.connection))
                try
                {


                    sqlcnt.Open();
                    // create command
                    SqlCommand sqlcmd = sqlcnt.CreateCommand();
                    // specify stored procedure to execute
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    sqlcmd.CommandTimeout = 180;
                    sqlcmd.CommandText = "spAP_FC287F11030F4901B63A6EE22FC62CE4";

                    //SQL Code to generate
                    //SELECT 'sqlcmd.Parameters.Add("@'+Variable+'", SqlDbType.VarChar).Value = txt'+Variable+'Link.Text'
                    //FROM ProbsAndWeights

                    sqlcmd.Parameters.Add("@FirstName", SqlDbType.VarChar).Value = txtFirstNameLink.Text;
                    sqlcmd.Parameters.Add("@MiddleName", SqlDbType.VarChar).Value = txtMiddleNameLink.Text;
                    sqlcmd.Parameters.Add("@LastName", SqlDbType.VarChar).Value = txtLastNameLink.Text;
                    sqlcmd.Parameters.Add("@TLFirstName", SqlDbType.VarChar).Value = txtTCLFirstNameLink.Text;
                    sqlcmd.Parameters.Add("@TLMiddleName", SqlDbType.VarChar).Value = txtTCLMiddleNameLink.Text;
                    sqlcmd.Parameters.Add("@TLLastName", SqlDbType.VarChar).Value = txtTCLLastNameLink.Text;
                    sqlcmd.Parameters.Add("@Gender", SqlDbType.VarChar).Value = txtSexLink.Text;
                    sqlcmd.Parameters.Add("@BDay", SqlDbType.VarChar).Value = txtDayLink.Text;
                    sqlcmd.Parameters.Add("@BMonth", SqlDbType.VarChar).Value = txtMonthLink.Text;
                    sqlcmd.Parameters.Add("@BYear", SqlDbType.VarChar).Value = txtYearLink.Text;
                    sqlcmd.Parameters.Add("@Village", SqlDbType.VarChar).Value = txtVillageLink.Text;
                    sqlcmd.Parameters.Add("@SubVillage", SqlDbType.VarChar).Value = txtSubVillageLink.Text;

                    sqlcmd.Parameters.Add("@UseFirstName", SqlDbType.Bit).Value = UseFirstName;
                    sqlcmd.Parameters.Add("@UseMiddleName", SqlDbType.Bit).Value = UseMiddleName;
                    sqlcmd.Parameters.Add("@UseLastName", SqlDbType.Bit).Value = UseLastName;
                    sqlcmd.Parameters.Add("@UseTLFirstName", SqlDbType.Bit).Value = UseTLFirstName;
                    sqlcmd.Parameters.Add("@UseTLMiddleName", SqlDbType.Bit).Value = UseTLMiddleName;
                    sqlcmd.Parameters.Add("@UseTLLastName", SqlDbType.Bit).Value = UseTLLastName;
                    sqlcmd.Parameters.Add("@UseGender", SqlDbType.Bit).Value = UseGender;
                    sqlcmd.Parameters.Add("@UseBDay", SqlDbType.Bit).Value = UseBDay;
                    sqlcmd.Parameters.Add("@UseBMonth", SqlDbType.Bit).Value = UseBMonth;
                    sqlcmd.Parameters.Add("@UseBYear", SqlDbType.Bit).Value = UseBYear;
                    sqlcmd.Parameters.Add("@UseVillage", SqlDbType.Bit).Value = UseVillage;
                    sqlcmd.Parameters.Add("@UseSubVillage", SqlDbType.Bit).Value = UseSubVillage;

                    da.SelectCommand = sqlcmd;
                    DssIndividuals.Clear();
                    da.Fill(DssIndividuals);
                    int nrows = DssIndividuals.Rows.Count;
                    int ncols = DssIndividuals.Columns.Count;

                    for (int i = 0; i < nrows; i++)
                    {

                        dgvDSSpersonalInformation.Rows.Add(DssIndividuals.Rows[i].ItemArray);//fill the data grid of patient history with data
                    }

                }


                catch (Exception Ex)
                {

                    MessageBox.Show(Ex.ToString(), "Read database tables Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    sqlcnt.Close();
                }

        }

        private void btnAssignMatch_Click(object sender, EventArgs e)
        {
            if (CheckIfFileRefandDSSIDMatchExists() != true)
            {
                //MatchStatus = "LINKED";
                InsertMatch();
                //DisplayMatchStatus();
            }
        }

        public bool CheckIfMatchExists()
        {

            bool result = false;
            using (System.Data.SqlClient.SqlConnection sqlcnt = new System.Data.SqlClient.SqlConnection(Properties.Settings.Default.connection))
            {

                string sql = @"SELECT dbo.fnIT_A777B73E407843E59BC129D2C097C684
                                                    ( 
                                                       @FileREf
                                                        ,UniqueCTC
                                                      ,@HealthFacilityName


                                                      
                                                    )";
                using (SqlCommand sqlcmd = new SqlCommand(sql, sqlcnt))
                {
          
                    //add parameters
                    sqlcmd.Parameters.AddWithValue("@FileREf", txtFileRef.Text);
                    sqlcmd.Parameters.AddWithValue("@UniqueCTC", txtUniqueCTCIDNumber.Text);
                    sqlcmd.Parameters.AddWithValue("@HealthFacilityName",comHealthFacilityName.Text);
                    sqlcnt.Open();
                    result = (bool)sqlcmd.ExecuteScalar();
                    sqlcnt.Close();
                }

                return result;
            }
        }
        private void InsertMatch()
        {
            if ( comHealthFacilityName.Text != "")
            {
                Object value;
                value = DBNull.Value;
                using (System.Data.SqlClient.SqlConnection sqlcnt = new System.Data.SqlClient.SqlConnection(Properties.Settings.Default.connection))
                    try
                    {
                        // open connection
                        sqlcnt.Open();
                        // create command
                        SqlCommand sqlcmd = sqlcnt.CreateCommand();
                        // specify stored procedure to execute
                        sqlcmd.CommandType = CommandType.StoredProcedure;
                        sqlcmd.CommandText = "spAP_E1C0DF3642614D7588FCA1B24CDD4272";
                        //add health facility parameter
                        sqlcmd.Parameters.Add("@RecordNo", SqlDbType.VarChar).Value = PatientId;
                        sqlcmd.Parameters.Add("@HealthFacilityName", SqlDbType.VarChar).Value = comHealthFacilityName.Text;
                        sqlcmd.Parameters.Add("@FileREf", SqlDbType.VarChar).Value = txtFileRef.Text;
                        sqlcmd.Parameters.Add("@UniqueCTC", SqlDbType.VarChar).Value = txtUniqueCTCIDNumber.Text;
                        sqlcmd.Parameters.Add("@SearchCriteria", SqlDbType.VarChar).Value = search_criteria;
                        sqlcmd.Parameters.Add("@DSS_IDLong", SqlDbType.VarChar).Value = txtDSSID.Text;
                        sqlcmd.ExecuteNonQuery();

                        if (txtDSSID.Text.Length>0)
                        {
                            MessageBox.Show("Match assigned", "Match information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }//RecordSaved = true;
                    }

                    catch (Exception Ex)
                    {
                        MessageBox.Show(Ex.ToString(), "Save matched PatientID and ADSSID Record Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        sqlcnt.Close();
                    }


            }
        }

        private void Search4ADSSMembersThatStaysWithIndividual_UsingADSSID_location()
        {

            Object value;
            value = DBNull.Value;
            SqlDataAdapter da = new SqlDataAdapter();
            DataTable dt = new DataTable();

            using (System.Data.SqlClient.SqlConnection sqlcnt = new System.Data.SqlClient.SqlConnection(Properties.Settings.Default.connection))
                try
                {
                    // open connection
                    sqlcnt.Open();
                    // create command
                    SqlCommand sqlcmd = sqlcnt.CreateCommand();
                    // specify stored procedure to execute
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    sqlcmd.CommandText = "spAP_BCFA7EB7D843466A995BF730FEA64255";
                    //add health facility parameter
                    sqlcmd.Parameters.Add("@Id", SqlDbType.VarChar).Value = adssID;
                    sqlcmd.Parameters.Add("@location", SqlDbType.VarChar).Value = Indiduallocation;
                    da.SelectCommand = sqlcmd;
                    da.Fill(dt);
                    dgvMemberships.Rows.Clear();
                    int nrows = dt.Rows.Count;
                    int ncols = dt.Columns.Count;

                    //dgvDSSpersonalInformation.Rows.Add(DssIndividuals.Rows[i].ItemArray)

                    for (int i = 0; i < nrows; i++)
                    {
                        dgvMemberships.Rows.Add(dt.Rows[i].ItemArray);//fill Members in the datagrid that stays with the searched individual

                    }


                }


                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.ToString(), "Read database tables Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    sqlcnt.Close();
                }

        } 

       

        private void dgvDSSpersonalInformation_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            try
            {


                string IndividualName;
                int rowIndex = 0;
                object value = dgvDSSpersonalInformation.Rows[e.RowIndex].Cells[0].Value;
                if (value is DBNull) { return; }
                IndividualName = value.ToString();
                rowIndex = e.RowIndex;

                //assigning rows from retrieved table
                AdssIndividualRow = DssIndividuals.Rows[rowIndex]; //AdssIndividualTable.Rows[rowIndex];

                //Fill the membership datagrid
                adssID = AdssIndividualRow.ItemArray.GetValue(12).ToString();

                txtDSSID.Clear();
                dgvMemberships.Rows.Clear();
                txtDSSID.Text = adssID;
                Indiduallocation = AdssIndividualRow.ItemArray.GetValue(13).ToString();
                Search4ADSSMembersThatStaysWithIndividual_UsingADSSID_location();

            }

            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        private void Select_RecordNo()
        {
            Object value;
            value = DBNull.Value;
            using (System.Data.SqlClient.SqlConnection sqlcnt = new System.Data.SqlClient.SqlConnection(Properties.Settings.Default.connection))
                try
                {
                    // open connection
                    sqlcnt.Open();
                    // create command
                    SqlCommand sqlcmd = sqlcnt.CreateCommand();
                    // specify stored procedure to execute
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    sqlcmd.CommandText = "spAP_CE6C38EA7BC4426796E9337016B932D3";
                    //add parameters
                    sqlcmd.Parameters.Add("@HealthFacility", SqlDbType.VarChar).Value = comHealthFacilityName.Text;

                    // Read in the SELECT results.
                    //
                    SqlDataReader sqlrdr = sqlcmd.ExecuteReader();//sqlrdr reads from the select from procedure
                    while (sqlrdr.Read())
                    {

                        if (sqlrdr.IsDBNull(0) == false)
                        {
                            PatientId = sqlrdr.GetValue(0).ToString();
                        }

                    }


                }

                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.ToString(), "Read database tables Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    sqlcnt.Close();
                }

        }
        public bool CheckIfFileRefandDSSIDMatchExists()
        {

            bool result = false;
            using (System.Data.SqlClient.SqlConnection sqlcnt = new System.Data.SqlClient.SqlConnection(Properties.Settings.Default.connection))
            {

                string sql = @"SELECT dbo.fnIT_F8B4A9F4F7334F0ABB83D253539A1D25
                                                    ( 
                                                    @HealthFacilityName
                                                    ,@FileREf	
                                                    ,@DSS_IDLong 
                                                                                                           
                                                    )";
                using (SqlCommand sqlcmd = new SqlCommand(sql, sqlcnt))
                {

                    //add parameters
                    sqlcmd.Parameters.AddWithValue("@HealthFacilityName", comHealthFacilityName.Text);
                    sqlcmd.Parameters.AddWithValue("@FileREf", txtFileRef.Text);
                    sqlcmd.Parameters.AddWithValue("@DSS_IDLong", txtDSSID.Text);
                    sqlcnt.Open();
                    result = (bool)sqlcmd.ExecuteScalar();
                    sqlcnt.Close();
                }

                return result;
            }

        }

        private void btnDoNotMatch_Click(object sender, EventArgs e)
        {

                InsertSearchInfo();
            
        }

        private void loadVisitInfoOptions()
        {
            //Load diagnosis

            string sql = @"EXEC spAP_CEA6A3CC06B543F392A4B5FE4B63A47B";

            DataTable dt = new DataTable();
            System.Data.SqlClient.SqlConnection sqlcnt = new System.Data.SqlClient.SqlConnection(Properties.Settings.Default.connection);
            try
            {
                sqlcnt.Open();
                // create data adapter

                SqlDataAdapter da = new SqlDataAdapter(sql, sqlcnt);
                // create dataset
                DataSet ds = new DataSet();
                // fill dataset
                da.Fill(ds, "tables");
                // get data table
                dt = ds.Tables["tables"];
            }

            catch (Exception Ex)
            {
                MessageBox.Show(Ex.ToString(), "Read database tables Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                sqlcnt.Close();

                List<string> OptionList = new List<string>();
                foreach (DataRow row in dt.Rows)
                {
                    foreach (DataColumn col in dt.Columns)
                    {
                        string Options = row[col].ToString();
                        if (!OptionList.Contains(Options))
                        {
                            OptionList.Add(Options);
                        }
                    }
                }
                ComVisitBy.AutoCompleteCustomSource.Clear();
                ComVisitBy.AutoCompleteCustomSource.AddRange(OptionList.ToArray());

                ComVisitBy.Items.Clear();
                for (int i = 0; i < OptionList.Count; i++)
                {
                    this.ComVisitBy.Items.AddRange(new object[] {
                                                   OptionList.ElementAt(i) });
                }
            }

        }

        private void ClearCheckBoxes()
        {

            cbFirstName.Checked = false;
            cbMiddleName.Checked = false;
            cbLastName.Checked = false;
            cbSex.Checked = false;
            cbYear.Checked = false;
            cbMonth.Checked = false;
            cbDay.Checked = false;
            cbVillage.Checked = false;
            cbSubVillage.Checked = false;
            cbTCLFirstName.Checked = false;
            cbTCLMiddleName.Checked = false;
            cbTCLLastName.Checked = false;
                          
                   
        }

        private void comVisitby_Enter(object sender, EventArgs e)
        {
            //loadVisitInfoOptions();
        }

        private void InsertSearchInfo()
        {

            Object value;
            value = DBNull.Value;

            using (System.Data.SqlClient.SqlConnection sqlcnt = new System.Data.SqlClient.SqlConnection(Properties.Settings.Default.connection))
                try
                {
                    // open connection
                    sqlcnt.Open();
                    // create command
                    SqlCommand sqlcmd = sqlcnt.CreateCommand();
                    // specify stored procedure to execute
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    sqlcmd.CommandText = "spAP_B3823673C7634A76A840BA9AEE338D83";
                    //add health facility parameter
                    sqlcmd.Parameters.Add("@RecordNo", SqlDbType.VarChar).Value = PatientId;
                    sqlcmd.Parameters.Add("@HealthFacilityName", SqlDbType.VarChar).Value = comHealthFacilityName.Text;
                    sqlcmd.Parameters.Add("@FileRef", SqlDbType.VarChar).Value = txtFileRef.Text;
                    sqlcmd.Parameters.Add("@UniqueCTCNumber", SqlDbType.VarChar).Value = txtUniqueCTCIDNumber.Text;             
                    sqlcmd.Parameters.Add("@SearchCriteria", SqlDbType.VarChar).Value = search_criteria;
                    sqlcmd.Parameters.Add("@SearchForMatchNotes", SqlDbType.VarChar).Value = txtSearchForMatchNotes.Text;
                    sqlcmd.ExecuteNonQuery();

                    {
                        MessageBox.Show("Search for match notes saved", "Save search for match notes", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.ToString(), "Save patient status Record Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    sqlcnt.Close();
                }

        }

        private void txtRegistrationDate_Enter(object sender, EventArgs e)
        {
            dtpRegistrationDate.Visible=true;
        }

        private void dtpRegistrationDate_Leave(object sender, EventArgs e)
        {
            txtRegistrationDate.Text = dtpRegistrationDate.Text;
            dtpRegistrationDate.Visible = false;
            comConsentStatus.Focus();
        }

        private void txtVisitDate_Enter(object sender, EventArgs e)
        {
            dtpVisitDate.Visible = true;
        }

        private void dtpVisitDate_Leave(object sender, EventArgs e)
        {
            txtVisitDate.Text = dtpVisitDate.Text;
            dtpVisitDate.Visible = false;
            ComVisitBy.Focus();
        }

        private void dgvVisitInformation_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            try
            {
                int nrows = dgvVisitInformation.RowCount;

                if (nrows > 0)
                {
                    int rowIndex = 0;
                    string VisitDate = "";
                    object value = dgvVisitInformation.Rows[e.RowIndex].Cells[0].Value;
                    VisitDate = value.ToString();
                    if (value is DBNull) { return; }

                    rowIndex = e.RowIndex;


                    if (dgvVisitInformation.Rows.Count > 0)
                    {
                        using (System.Data.SqlClient.SqlConnection sqlcnt = new System.Data.SqlClient.SqlConnection(Properties.Settings.Default.connection))
                            try
                            {


                                // open connection
                                sqlcnt.Open();
                                // create command
                                SqlCommand sqlcmd = sqlcnt.CreateCommand();
                                // specify stored procedure to execute
                                sqlcmd.CommandType = CommandType.StoredProcedure;
                                sqlcmd.CommandText = "spAP_C7193C66FC1E43B496D3B78D2E19E528";
                                //add health facility parameter
                                sqlcmd.Parameters.Add("@HealthFacilityName", SqlDbType.VarChar).Value =comHealthFacilityName.Text;
                                sqlcmd.Parameters.Add("@FileRef", SqlDbType.VarChar).Value =txtFileRef.Text ;
                                sqlcmd.Parameters.Add("@VisitDate", SqlDbType.VarChar).Value = VisitDate;
                                sqlcmd.ExecuteNonQuery();
                                dgvVisitInformation.Rows.RemoveAt(rowIndex);
                                Retrieve_VisitDateInfo();
                                //Refresh Patient history after deletion has taken place
                                
                                sqlcnt.Close();
                                //dgvDiagnosis.Rows.Clear();
                            }//end try


                            catch (Exception Ex)
                            {
                                MessageBox.Show(Ex.ToString(), "Delete diagnosis record Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            finally
                            {
                                // sqlcnt.Close();
                            }


                    }



                }

            }
            catch (Exception)
            { }
        }

        private void updatesDoBAndStartDate()
        {

            if (comYearofBirth.Text == "YYYY")
            {
                comYearofBirth.Text = "9999";

            }
            if (comMonthofBirth.Text == "MM")
            {
                comYearofBirth.Text = "99";

            }

            if (comDayofBirth.Text == "DD")
            {
                comYearofBirth.Text = "99";

            }

            if (comYearAtCurResident.Text == "YYYY")
            {
                comYearofBirth.Text = "9999";

            }
            if (comMonthAtCurResident.Text == "MM")
            {
                comYearofBirth.Text = "99";

            }

            if (comDayAtCurResident.Text == "DD")
            {
                comYearofBirth.Text = "99";

            }
        }
  
    }

       
    }

