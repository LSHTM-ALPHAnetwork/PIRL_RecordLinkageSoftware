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
        string SearchSessionID = DateTime.UtcNow.ToString("yyyyMMddHHmmss");
        string PatientId = "";

       

        string adssID = "";
        string birthyear = "";
        string score=""; 
        string ranknogap="";
        string rankgap = "";
        string rownumber = "";
        float namescore = 0;
        string Indiduallocation = "";
        bool KeepSearching = true;
        bool PatientEditActive = false;
        Boolean OneMatchNotes = true;
       // string MatchStatus = "";
        DataTable DssIndividuals = new DataTable();//holds adss individuals data retrieved from the healthfacility database
        DataRow AdssIndividualRow;
       // bool checkSearchIfClicked = false;
        string search_criteria = "";
        Boolean ValidateID = true;
        Boolean FirstVisit = true;
        Boolean VisitSaved = false;
        Boolean TrueAttempt = false;
        Boolean HTCIDokay = true;
        Boolean TGRFokay = true;
        

        private void fmMain_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'kisesaDSSClinicLinkSystemDataSet11.v_Villages' table. You can move, or remove it, as needed.
            this.v_VillagesTableAdapter.Fill(this.kisesaDSSClinicLinkSystemDataSet11.v_Villages);
            
            
            // TODO: This line of code loads data into the 'kisesaDSSClinicLinkSystemDataSet2.Villages' table. You can move, or remove it, as needed.
              comConsentStatus.Text = "CONSENTED";
              comHealthFacilityName.Text = "KISESA";
              txtRegistrationDate.Text=DateTime.UtcNow.ToString("dd-MM-yyyy");
              txtVisitDate.Text = DateTime.UtcNow.ToString("dd-MM-yyyy");
              ComVisitBy.Text = "PATIENT";

            

              btnDoNotMatch.Text = "Click 'Save' on Patient Registry page";
              btnDoNotMatch.Enabled = false;
              btnAssignMatch.Text = "Click 'Save' on Patient Registry page";
              btnAssignMatch.Enabled = false;
              btnSaveVisitDate.Text = "Click 'Save for Search'";
              btnSaveVisitDate.Enabled = false;
              rdoTargetOther.Enabled = true;
              rdoTargetPatient.Enabled = true;
              cbNeverDSS.Enabled = true;
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
            comConsentStatus.Text = "CONSENTED";
            ComVisitBy.Text = "PATIENT";
            KeepSearching = true;
            OneMatchNotes = true;
            SearchSessionID = DateTime.UtcNow.ToString("yyyyMMddHHmmss");
            txtRegistrationDate.Text = DateTime.UtcNow.ToString("dd-MM-yyyy");
            txtVisitDate.Text = DateTime.UtcNow.ToString("dd-MM-yyyy");
            rdoTargetPatient.Checked = true;
            cbNeverDSS.Checked = false;
            HTCIDokay = true;
            TGRFokay = true;
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
            comConsentStatus.Text = "CONSENTED";
            KeepSearching = true;
            OneMatchNotes = true;
            rdoTargetPatient.Checked = true;
            cbNeverDSS.Checked = false;
            HTCIDokay = true;
            TGRFokay = true;
            SearchSessionID=DateTime.UtcNow.ToString("yyyyMMddHHmmss");
            comVillage.SelectedIndex = 0;
            try
            {
                comSubVillage.SelectedIndex = 0;
            }
            catch (Exception Ex)
            {
                
            }
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
            if (OneMatchNotes == true)
            {
                btnDoNotMatch.Text = "Click 'Save' on Patient Registry page";
                btnDoNotMatch.Enabled = false;
            }

            btnAssignMatch.Text = "Click 'Save' on Patient Registry page";
            btnAssignMatch.Enabled = false;
            btnSaveVisitDate.Text = "Click 'Save for Search'";
            btnSaveVisitDate.Enabled = false;

            rdoTargetOther.Enabled = true;
            rdoTargetPatient.Enabled = true;
            cbNeverDSS.Enabled = true;
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
            txtUniqueCTCIDNumber.Enabled = false;
            txtFileRef.Enabled = false;
            txtCTCIDInfant.Enabled = false;
            txtTGRFormNumber.Enabled = false;
            txtUniqueHTCID.Enabled = false;
            txtUniqueANCID.Enabled = false;
            txtANCIDInfant.Enabled = false;
            txtHEIDInfant.Enabled = false;

            btnSaveVisitDate.Text = "Save Visit Date";
            btnSaveVisitDate.Enabled = true;

            rdoTargetOther.Enabled = false;
            rdoTargetPatient.Enabled = false;
            cbNeverDSS.Enabled = false;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            /* THIS IS WHERE AN AUTOMATIC RUN OF EXPORTDATA SHOULD OCCUR
             * IF WE WANT TO HAVE BACKUP OF TXT FILES TO OCCUR AFTER EVERY SESSION */


            Boolean SessionOK = true;

            if (PatientEditActive == true && TrueAttempt == true)
            {
                DialogResult result1 = MessageBox.Show("You must click 'Save for Search' before ending the session." + Environment.NewLine + Environment.NewLine + "Unapaswa kubofya 'Save for Search' kabla hujamaliza mada.", "Record Not Saved", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                SessionOK = result1 == DialogResult.Yes;
            }

            if (VisitSaved == false && FirstVisit == true && TrueAttempt == true && PatientEditActive == false)
            {
                DialogResult result1 = MessageBox.Show("You need to save a visit date before ending session." + Environment.NewLine + Environment.NewLine + "Unatakiwa kuhifadhi tarehe ya hudhurio kabla ya kumaliza mada.", "Visit Date", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                SessionOK = result1 == DialogResult.Yes;
            }

            if (comConsentStatus.Text == "CONSENTED" && FirstVisit == true && TrueAttempt == true && PatientEditActive == false && VisitSaved == true)
            {
                DialogResult result1 = MessageBox.Show("CONSENT NEEDED: Ask the patient to sign the appropriate consent form. Select 'Yes' if the patient has signed the form. Select 'No' if the patient refused to sign the form and change the Consent Status to 'REFUSED.'" + Environment.NewLine + Environment.NewLine + "MUHIMU SANA: Mwombe mgonjwa aweke sahihi kwenye fomu husika ya ridhaa. Chagua 'Yes' kama mgonjwa ameweka sahihi kwenye fomu. Chagua 'No' kama mgonjwa amekataa kuweka sahihi kwenye fomu na badili hali ya Consent Status to 'REFUSED.'", "Consent Form", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                SessionOK = result1 == DialogResult.Yes;
            }

            if (SessionOK && PatientEditActive == false)
            {
                ClearPatientDetails();
                ClearLinkInformation();
                EnablePatientDetatils();
                //checkSearchIfClicked = false;
                TrueAttempt = false;
                VisitSaved = false;

                txtUniqueCTCIDNumber.Text = "";
                txtUniqueCTCIDNumber.Enabled = true;
                this.txtUniqueCTCIDNumber.ForeColor = System.Drawing.Color.Black;

                txtCTCIDInfant.Text = "";
                txtCTCIDInfant.Enabled = true;
                this.txtCTCIDInfant.ForeColor = System.Drawing.Color.Black;

                txtFileRef.Text = "";
                txtFileRef.Enabled = true;
                this.txtFileRef.ForeColor = System.Drawing.Color.Black;

                txtTGRFormNumber.Text = "";
                txtTGRFormNumber.Enabled = true;
                this.txtTGRFormNumber.ForeColor = System.Drawing.Color.Black;

                txtUniqueHTCID.Text = "";
                txtUniqueHTCID.Enabled = true;
                this.txtUniqueHTCID.ForeColor = System.Drawing.Color.Black;

                txtUniqueANCID.Text = "";
                txtUniqueANCID.Enabled = true;
                this.txtUniqueANCID.ForeColor = System.Drawing.Color.Black;

                txtANCIDInfant.Text = "";
                txtANCIDInfant.Enabled = true;
                this.txtANCIDInfant.ForeColor = System.Drawing.Color.Black;

                txtHEIDInfant.Text = "";
                txtHEIDInfant.Enabled = true;
                this.txtHEIDInfant.ForeColor = System.Drawing.Color.Black;

                comVillage.SelectedIndex = 0;
                try
                {
                    comSubVillage.SelectedIndex = 0;
                }
                catch (Exception Ex)
                {

                }
                txtRegistrationDate.Text = DateTime.UtcNow.ToString("dd-MM-yyyy");
                txtVisitDate.Text = DateTime.UtcNow.ToString("dd-MM-yyyy");
            }




        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            if (comHealthFacilityName.Text != "" && (txtFirstName.TextLength > 0))
            {
                Object value;
                value = DBNull.Value;
                bool skipcontrol = false;
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
                    sqlcmd.Parameters.Add("@SessionID", SqlDbType.Char).Value = SearchSessionID;
                    // add other parameters
                   
                    foreach (Control ctrl in this.tpPatientDetails.Controls)
                    {
                        
                        if (ctrl.GetType() == typeof(System.Windows.Forms.GroupBox) & ctrl.Name != "gpMatch_Status" & ctrl.Name != "gbVisitInformation")
                        {

                            foreach (Control gctrl in ctrl.Controls)
                            {
                                skipcontrol = false;
                                if (gctrl.Tag != null)
                                {
                                    if (gctrl.Tag.ToString().Substring(0, 1) == "@")
                                    {
                                        if (gctrl.Name.ToString().Substring(0, 3) != "rdo" && gctrl.Text.Length == 0 && gctrl.Name.ToString().Substring(0, 2) != "cb")
                                        {
                                            value = DBNull.Value;
                                        }
                                        else if (
                                            (
                                            gctrl.Tag.ToString() == "@UniqueCTCIDNumber" && txtUniqueCTCIDNumber.Text == "  -  -    -      ")
                                            || (gctrl.Tag.ToString() == "@FileRef" && txtFileRef.Text == "")
                                            || (gctrl.Tag.ToString() == "@CTCIDInfant" && txtCTCIDInfant.Text == "  -  -    -      -   ")
                                            || (gctrl.Tag.ToString() == "@TGRFormNumber" && txtTGRFormNumber.Text == "")
                                            || (gctrl.Tag.ToString() == "@UniqueHTCID" && txtUniqueHTCID.Text == "")
                                            || (gctrl.Tag.ToString() == "@UniqueANCID" && txtUniqueANCID.Text == "")
                                            || (gctrl.Tag.ToString() == "@ANCIDInfant" && txtANCIDInfant.Text == "")
                                            || (gctrl.Tag.ToString() == "@HEIDInfant" && txtHEIDInfant.Text == "")
                                            )
                                            
                                        {
                                            value = "";
                                        }
                                        else if (gctrl.Tag.ToString()=="@SearchTarget")
                                        {
                                            RadioButton rctrl = (RadioButton)gctrl;
                                            if (rctrl.Checked)
                                            {
                                                value = rctrl.Name.Substring(9);
                                            }
                                            else
                                            {
                                                skipcontrol = true;
                                            }
                                        }
                                        else if (gctrl.Tag.ToString() == "@NeverDSS")
                                        {
                                            CheckBox rctrl = (CheckBox)gctrl;
                                            if (rctrl.Checked)
                                            {
                                                value = "1";
                                            }
                                            else
                                            {
                                                value = "0";
                                            }
                                        }
                                        else
                                        {
                                            value = gctrl.Text;
                                        }
                                        if (!skipcontrol)
                                        {
                                            SqlParameter para = new SqlParameter(gctrl.Tag.ToString(), value);
                                            sqlcmd.Parameters.Add(para);
                                        }
                                    }

                                }

                            }
                        }

                    }
                    sqlcmd.ExecuteNonQuery();
                    DisablePatientDetatils();
                    Select_RecordNo();
                    PassLinkingVariables();

                    ClearCheckBoxes();
                    CheckDefaultBoxes();

                    TrueAttempt = true;
                    PatientEditActive = false;
                    comConsentStatus_SelectedIndexChanged(comConsentStatus, new EventArgs());

                    

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
                    sqlcmd.Parameters.Add("@UniqueCTCIDNumber", SqlDbType.VarChar).Value = txtUniqueCTCIDNumber.Text;
                    sqlcmd.Parameters.Add("@TGRFN", SqlDbType.VarChar).Value = txtTGRFormNumber.Text;
                    sqlcmd.Parameters.Add("@FileRef", SqlDbType.VarChar).Value = txtFileRef.Text;
                    sqlcmd.Parameters.Add("@CTCInfant", SqlDbType.VarChar).Value = txtCTCIDInfant.Text;
                    sqlcmd.Parameters.Add("@UniqueHTC", SqlDbType.VarChar).Value = txtUniqueHTCID.Text;
                    sqlcmd.Parameters.Add("@UniqueANC", SqlDbType.VarChar).Value = txtUniqueANCID.Text;
                    sqlcmd.Parameters.Add("@ANCInfant", SqlDbType.VarChar).Value = txtANCIDInfant.Text;
                    sqlcmd.Parameters.Add("@HEIDInfant", SqlDbType.VarChar).Value = txtHEIDInfant.Text;

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
                            txtFileRef.Text = sqlrdr.GetValue(3).ToString();
                        }
                        if (sqlrdr.IsDBNull(4) == false)
                        {
                            txtCTCIDInfant.Text = sqlrdr.GetValue(4).ToString();
                        }
                        if (sqlrdr.IsDBNull(5) == false)
                        {
                            txtUniqueHTCID.Text = sqlrdr.GetValue(5).ToString();
                        }
                        if (sqlrdr.IsDBNull(6) == false)
                        {
                            txtUniqueANCID.Text = sqlrdr.GetValue(6).ToString();
                        }
                        if (sqlrdr.IsDBNull(7) == false)
                        {
                            txtANCIDInfant.Text = sqlrdr.GetValue(7).ToString();
                        }
                        if (sqlrdr.IsDBNull(8) == false)
                        {
                            txtHEIDInfant.Text = sqlrdr.GetValue(8).ToString();
                        }
                        if (sqlrdr.IsDBNull(9) == false)
                        {
                            txtFirstName.Text = sqlrdr.GetValue(9).ToString();
                        }
                        if (sqlrdr.IsDBNull(10) == false)
                        {
                            txtMiddleName.Text = sqlrdr.GetValue(10).ToString();
                        }
                        if (sqlrdr.IsDBNull(11) == false)
                        {
                            txtLastName.Text = sqlrdr.GetValue(11).ToString();
                        }
                        if (sqlrdr.IsDBNull(12) == false)
                        {
                            comSex.Text = sqlrdr.GetValue(12).ToString();
                        }
                        if (sqlrdr.IsDBNull(13) == false)
                        {
                            comYearofBirth.Text = sqlrdr.GetValue(13).ToString();
                        }
                        if (sqlrdr.IsDBNull(14) == false)
                        {
                            comMonthofBirth.Text = sqlrdr.GetValue(14).ToString();
                        }
                        if (sqlrdr.IsDBNull(15) == false)
                        {
                            comDayofBirth.Text = sqlrdr.GetValue(15).ToString();
                        }
                        if (sqlrdr.IsDBNull(16) == false && sqlrdr.GetValue(16).ToString() != "(none)")
                        {
                            comVillage.Text = sqlrdr.GetValue(16).ToString();
                        }
                        if (sqlrdr.IsDBNull(17) == false && sqlrdr.GetValue(17).ToString() != "(none)")
                        {
                            //comSubVillage.SelectedValue = sqlrdr.GetValue(17).ToString();
                            comSubVillage.Text = sqlrdr.GetValue(17).ToString();
                        }
                        if (sqlrdr.IsDBNull(18) == false)
                        {
                            comYearAtCurResident.Text = sqlrdr.GetValue(18).ToString();
                        }
                        if (sqlrdr.IsDBNull(19) == false)
                        {
                            if (sqlrdr.GetValue(19).ToString() == "1")
                            {
                                cbNeverDSS.Checked = true;
                            }
                            else
                            {
                                cbNeverDSS.Checked = false;
                            }
                        }
                        //if (sqlrdr.IsDBNull(19) == false)
                        //{
                        //    comMonthAtCurResident.Text = sqlrdr.GetValue(19).ToString();
                        //}
                        //if (sqlrdr.IsDBNull(20) == false)
                        //{
                        //    comDayAtCurResident.Text = sqlrdr.GetValue(20).ToString();
                        //}
                        if (sqlrdr.IsDBNull(20) == false)
                        {
                            txtTCLFirstName.Text = sqlrdr.GetValue(20).ToString();
                        }
                        if (sqlrdr.IsDBNull(21) == false)
                        {
                            txtTCLMiddleName.Text = sqlrdr.GetValue(21).ToString();
                        }
                        if (sqlrdr.IsDBNull(22) == false)
                        {
                            txtTCLLastName.Text = sqlrdr.GetValue(22).ToString();
                        }
                        if (sqlrdr.IsDBNull(23) == false)
                        {
                            txtHHMemberFirstName.Text = sqlrdr.GetValue(23).ToString();
                        }
                        if (sqlrdr.IsDBNull(24) == false)
                        {
                            txtHHMemberMiddleName.Text = sqlrdr.GetValue(24).ToString();
                        }
                        if (sqlrdr.IsDBNull(25) == false)
                        {
                            txtHHMemberLastName.Text = sqlrdr.GetValue(25).ToString();
                        }
                        if (sqlrdr.IsDBNull(26) == false)
                        {
                            txtTelNumber.Text = sqlrdr.GetValue(26).ToString();
                        }
                        if (sqlrdr.IsDBNull(27) == false)
                        {
                            comConsentStatus.Text = sqlrdr.GetValue(27).ToString();
                        }
                        if (sqlrdr.IsDBNull(28) == false)
                        {
                            PatientId = sqlrdr.GetValue(28).ToString();
                        }
                        if (sqlrdr.IsDBNull(29) == false)
                        {
                            if (sqlrdr.GetValue(29).ToString() == "Other")
                            {
                                rdoTargetOther.Checked = true;
                            }
                            else
                            {
                                rdoTargetPatient.Checked = true;
                            }
                        }

                        
                        //DisablePatientDetatils();
                        PassLinkingVariables();
                        Retrieve_VisitDateInfo();
                        
                        /*
                        txtUniqueCTCIDNumber.Enabled = false;
                        txtFileRef.Enabled = false;
                        txtCTCIDInfant.Enabled = false;
                        txtTGRFormNumber.Enabled = false;
                        txtUniqueHTCID.Enabled = false;
                        txtUniqueANCID.Enabled = false;
                        txtANCIDInfant.Enabled = false;
                        txtHEIDInfant.Enabled = false;

                        this.txtUniqueCTCIDNumber.ForeColor = System.Drawing.Color.Black;
                        this.txtFileRef.ForeColor = System.Drawing.Color.Black;
                        this.txtCTCIDInfant.ForeColor = System.Drawing.Color.Black;
                        this.txtTGRFormNumber.ForeColor = System.Drawing.Color.Black;
                        this.txtUniqueHTCID.ForeColor = System.Drawing.Color.Black;
                        this.txtUniqueANCID.ForeColor = System.Drawing.Color.Black;
                        this.txtANCIDInfant.ForeColor = System.Drawing.Color.Black;
                        this.txtHEIDInfant.ForeColor = System.Drawing.Color.Black;
                        */
                       
                        DisplayMatchStatus();
                      
                        CheckDefaultBoxes();
                        KeepSearching = false;


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


            ////this messagebox is to find out when CONSENTED is stored as a RecordNo in T3
            //if (PatientId == "CONSENTED")
            //{
            //    MessageBox.Show("Call Masanja now! You found a bug that is very important to him.", "Masanja", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //}
           
                
        
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


            txtMatchStatus.Clear();
            //txtLastSearchDate.Clear();
            txtComment.Clear();
            
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
                    sqlcmd.Parameters.Add("@HealthFacilityName", SqlDbType.VarChar).Value = comHealthFacilityName.Text;
                    sqlcmd.Parameters.Add("@UniqueCTCIDNumber", SqlDbType.VarChar).Value = txtUniqueCTCIDNumber.Text;
                    sqlcmd.Parameters.Add("@TGRFN", SqlDbType.VarChar).Value = txtTGRFormNumber.Text;
                    sqlcmd.Parameters.Add("@FileRef", SqlDbType.VarChar).Value = txtFileRef.Text;
                    sqlcmd.Parameters.Add("@CTCInfant", SqlDbType.VarChar).Value = txtCTCIDInfant.Text;
                    sqlcmd.Parameters.Add("@UniqueHTC", SqlDbType.VarChar).Value = txtUniqueHTCID.Text;
                    sqlcmd.Parameters.Add("@UniqueANC", SqlDbType.VarChar).Value = txtUniqueANCID.Text;
                    sqlcmd.Parameters.Add("@ANCInfant", SqlDbType.VarChar).Value = txtANCIDInfant.Text;
                    sqlcmd.Parameters.Add("@HEIDInfant", SqlDbType.VarChar).Value = txtHEIDInfant.Text;
                    
                    // Read in the SELECT results.
                    //
                    SqlDataReader sqlrdr = sqlcmd.ExecuteReader();
                    while (sqlrdr.Read())
                    {
                        if (sqlrdr.IsDBNull(0) == false)
                        {
                            txtMatchStatus.Text = sqlrdr.GetValue(0).ToString();
                        }
                        
                        if (sqlrdr.IsDBNull(1) == false)
                        {
                //            txtLastSearchDate.Text = sqlrdr.GetValue(1).ToString();
                        }
                        if (sqlrdr.IsDBNull(2) == false)
                        {
                            txtComment.Text = sqlrdr.GetValue(2).ToString();
                            txtSearchForMatchNotes.Text = sqlrdr.GetValue(2).ToString();
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

        
        private void btnEdit_Click(object sender, EventArgs e)
        {
            EnablePatientDetatils();
            txtUniqueCTCIDNumber.Enabled = true;
            txtFileRef.Enabled = true;
            txtCTCIDInfant.Enabled = true;
            txtTGRFormNumber.Enabled = true;
            txtUniqueHTCID.Enabled = true;
            txtUniqueANCID.Enabled = true;
            txtANCIDInfant.Enabled = true;
            txtHEIDInfant.Enabled = true;
            PatientEditActive = true;

   
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
            txtVillageLink.Text = comVillage.Text;
            if (comSubVillage.SelectedValue != null)
            {
                txtSubVillageLink.Text = comSubVillage.SelectedValue.ToString();
            }
            else
            {
                txtSubVillageLink.Text = comSubVillage.Text;
            }
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
            btnAssignMatch.Enabled = false;
            search_criteria = ""; //reset search criteria
            Search4DSSindividualPotentialMatches();
           // checkSearchIfClicked = true;
        }

      
        

        private void btnSaveVisitDate_Click(object sender, EventArgs e)
        {
            FollowUPVisitDate();
            VisitSaved = true;
            // When a repeat patient comes in, and all that is done is Save Visit Date, we still want it saved off in T1. 
            //btnSave_Click(btnSave, new EventArgs());
            //If we type that ^, everything works except RecordNo in T4 is just (previous RecordNo when SaveVisitDate was pressed) + 1
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
                        sqlcmd.Parameters.Add("@UniqueCTCIDNumber", SqlDbType.VarChar).Value = txtUniqueCTCIDNumber.Text;
                        sqlcmd.Parameters.Add("@TGRFN", SqlDbType.VarChar).Value = txtTGRFormNumber.Text;
                        sqlcmd.Parameters.Add("@FileRef", SqlDbType.VarChar).Value = txtFileRef.Text;
                        sqlcmd.Parameters.Add("@CTCInfant", SqlDbType.VarChar).Value = txtCTCIDInfant.Text;
                        sqlcmd.Parameters.Add("@UniqueHTC", SqlDbType.VarChar).Value = txtUniqueHTCID.Text;
                        sqlcmd.Parameters.Add("@UniqueANC", SqlDbType.VarChar).Value = txtUniqueANCID.Text;
                        sqlcmd.Parameters.Add("@ANCInfant", SqlDbType.VarChar).Value = txtANCIDInfant.Text;
                        sqlcmd.Parameters.Add("@HEIDInfant", SqlDbType.VarChar).Value = txtHEIDInfant.Text;
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
                    sqlcmd.Parameters.Add("@UniqueCTCIDNumber", SqlDbType.VarChar).Value = txtUniqueCTCIDNumber.Text;
                    sqlcmd.Parameters.Add("@TGRFN", SqlDbType.VarChar).Value = txtTGRFormNumber.Text;
                    sqlcmd.Parameters.Add("@FileRef", SqlDbType.VarChar).Value = txtFileRef.Text;
                    sqlcmd.Parameters.Add("@CTCInfant", SqlDbType.VarChar).Value = txtCTCIDInfant.Text;
                    sqlcmd.Parameters.Add("@UniqueHTC", SqlDbType.VarChar).Value = txtUniqueHTCID.Text;
                    sqlcmd.Parameters.Add("@UniqueANC", SqlDbType.VarChar).Value = txtUniqueANCID.Text;
                    sqlcmd.Parameters.Add("@ANCInfant", SqlDbType.VarChar).Value = txtANCIDInfant.Text;
                    sqlcmd.Parameters.Add("@HEIDInfant", SqlDbType.VarChar).Value = txtHEIDInfant.Text;
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


        
        private void HealthID_Leave(object sender, EventArgs e)
        {
            string txtIDValue;
            TextBox txtIDCheck;
            MaskedTextBox txtMaskedIDCheck;

            HTCIDCheck();
            TGRFCheck();

            if (ValidateID == true & HTCIDokay == true & TGRFokay == true)
            {
                try
                {
                    txtIDCheck = (TextBox)sender;
                    txtIDValue = txtIDCheck.Text;
                }
                catch
                {
                    txtMaskedIDCheck = (MaskedTextBox)sender;
                    txtIDValue = txtMaskedIDCheck.Text;
                }

                if (txtIDValue != "")
                {
                    string input = Microsoft.VisualBasic.Interaction.InputBox("Please type again the Clinic ID you just entered" + Environment.NewLine + Environment.NewLine + "Tafadhari andika tena utambulisho wa klinki uliyoingiza.", "ID Check", "", -1, -1);
                    
                    if (input == txtIDValue)
                    {
                        ValidateID = false;
                        if (KeepSearching == true)
                        {
                            RetrievePatientDetails();
                        }
                        if (txtFirstName.Text == "")
                        {
                            FirstVisit = true;
                        }
                        else
                        {
                            FirstVisit = false;
                            MessageBox.Show("This is a REPEAT patient." + Environment.NewLine + Environment.NewLine + "1. Check to see all Clinic IDs are entered. (the patient may have more than one ID, and it could be in a different clinic)" + Environment.NewLine + "2. Save a new visit date." + Environment.NewLine + "3. Review previous match notes." + Environment.NewLine + "4. You do NOT need consent for this patient.", "Repeat Patient", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        ValidateID = true;
                    }

                    else
                    {
                        MessageBox.Show("The IDs did not match. Please try again." + Environment.NewLine + Environment.NewLine + "Utambulisho haukufanana. Tafadhari jaribu tena.", "Clinic ID Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                        try
                        {
                            txtIDCheck = (TextBox)sender;
                            txtIDCheck.Text = "";
                        }
                        catch
                        {
                            txtMaskedIDCheck = (MaskedTextBox)sender;
                            txtMaskedIDCheck.Text = "";
                        }
                    }
                }
            }

        }

        private void HTCIDCheck()
        {
            int HTCint;
            int HTCmod;
            int mod = 97;

            if (txtUniqueHTCID.Text != "")
            {
                HTCIDokay = false;

                if (Int32.TryParse(txtUniqueHTCID.Text, out HTCint))
                {
                    HTCmod = HTCint % mod;

                    if (HTCmod == 1)
                    {
                        HTCIDokay = true;
                    }
                    else
                    {
                        MessageBox.Show("This is not a valid HTC number. Please try again." + Environment.NewLine + Environment.NewLine + "Tafadhari jaribu tena.", "HTC ID Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtUniqueHTCID.Text = "";
                        txtUniqueHTCID.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("This is not a valid HTC number. Please try again." + Environment.NewLine + Environment.NewLine + "Tafadhari jaribu tena.", "HTC ID Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtUniqueHTCID.Text = "";
                    txtUniqueHTCID.Focus();
                }
            }
        }

        private void TGRFCheck()
        {
            int TGRFint;
            int TGRFmod;
            int mod = 97;

            if (txtTGRFormNumber.Text != "")
            {
                TGRFokay = false;

                if (Int32.TryParse(txtTGRFormNumber.Text, out TGRFint))
                {
                    TGRFmod = TGRFint % mod;

                    if (TGRFmod == 1)
                    {
                        TGRFokay = true;
                    }
                    else
                    {
                        MessageBox.Show("This is not a valid Tazama Green Referral Form number. Please try again." + Environment.NewLine + Environment.NewLine + "Tafadhari jaribu tena.", "TGRF Number Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtTGRFormNumber.Text = "";
                        txtTGRFormNumber.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("This is not a valid Tazama Green Referral Form number. Please try again." + Environment.NewLine + Environment.NewLine + "Tafadhari jaribu tena.", "TGRF Number Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtTGRFormNumber.Text = "";
                    txtTGRFormNumber.Focus();
                }
            }
        }



        //private void ClearsPatientDetailsExceptFileRef()
        //{

        //    foreach (Control ctrl in this.tpPatientDetails.Controls)
        //    {
        //        if (ctrl.GetType() == typeof(System.Windows.Forms.GroupBox))
        //        {
        //            foreach (Control gctrl in ctrl.Controls)
        //            {
        //                if (gctrl.GetType() == typeof(System.Windows.Forms.TextBox) | gctrl.GetType() == typeof(System.Windows.Forms.ComboBox))
        //                {
        //                    if (gctrl.Name != "txtFileRef")
        //                    {
        //                        gctrl.Text = "";
        //                    }

        //                }

        //            }

        //        }

        //    }
        //    dgvVisitInformation.Rows.Clear();
        //    PatientId = "";
        //    comConsentStatus.Text = "NOT ASKED";              
                  
        //}

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
            Boolean birthyeartest =true;
            Boolean IDOK = true;
            Boolean LowNameScoreOK = true;
            String searchedbirthyear="";

            if (txtUniqueCTCIDNumber.Text == ""
                && txtFileRef.Text == ""
                && txtCTCIDInfant.Text == ""
                && txtTGRFormNumber.Text == ""
                && txtUniqueHTCID.Text == ""
                && txtUniqueANCID.Text == ""
                && txtANCIDInfant.Text == ""
                && txtHEIDInfant.Text == ""
                && comConsentStatus.Text == "CONSENTED")
            {
                DialogResult result1 = MessageBox.Show("You have not entered any Clinic ID for this patient.  Are you sure you want to proceed?" + Environment.NewLine + Environment.NewLine + "Hujaingiza utambulisho wa kliniki ya mgonjwa huyu. Una hakika unataka kuendelea?", "Clinic ID Missing", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                IDOK = result1 == DialogResult.Yes;
            }            

            if (comYearofBirth.Text == "YYYY" || comYearofBirth.Text=="") 
            {
                searchedbirthyear = birthyear;
            }
            else
            {
                searchedbirthyear = comYearofBirth.Text;
            }
            if (Math.Abs(int.Parse(searchedbirthyear) - int.Parse(birthyear)) > 10 && IDOK)
            {
                DialogResult result1 = MessageBox.Show("The record you selected has a VERY different birth year.  Are you sure you want to proceed?" + Environment.NewLine + Environment.NewLine + " Kumbukumbu uliyochagua ina tofauti kubwa ya mwaka wa kuzaliwa. Una hakika unataka kuendelea?", "Birth Year Gap", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                birthyeartest = result1 == DialogResult.Yes;
            }
            
            if (namescore <= 1.6 && IDOK && birthyeartest)
            {
                DialogResult result1 = MessageBox.Show("The record you selected has a very low Match Score.  Are you sure you want to proceed?" + Environment.NewLine + Environment.NewLine + "Kumbukumbu uliyochagua ina kiwango cha chini. Una hakika unataka kuendelea?", "Bad Name Match", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                LowNameScoreOK = result1 == DialogResult.Yes;
            }
           


            if (CheckIfFileRefandDSSIDMatchExists() != true && birthyeartest==true && IDOK && LowNameScoreOK)
            {
                //MatchStatus = "LINKED";
                InsertMatch();
                //DisplayMatchStatus();
            }
            else if (CheckIfFileRefandDSSIDMatchExists() == true)
            {
                MessageBox.Show("This Clinic ID has already been matched to this DSS Record." + Environment.NewLine + Environment.NewLine + "Utambulisho wa kliniki hii tayari unafanana na kumbukumbu hii ya utafiti wa kidemograpfia.");
            }
        }

        public bool CheckIfMatchExists()
        {

            bool result = false;
            using (System.Data.SqlClient.SqlConnection sqlcnt = new System.Data.SqlClient.SqlConnection(Properties.Settings.Default.connection))
            {

                string sql = @"SELECT dbo.fnIT_A777B73E407843E59BC129D2C097C684
                               (@HealthFacilityName
                                ,@FileRef 
                                ,@UniqueCTCIDNumber
                                ,@TGRFN 
                                ,@CTCInfant
                                ,@UniqueHTC 
                                ,@UniqueANC 
                                ,@ANCInfant
                                ,@HEIDInfant
                                )";
                using (SqlCommand sqlcmd = new SqlCommand(sql, sqlcnt))
                {
          
                    //add parameters
                    sqlcmd.Parameters.AddWithValue("@HealthFacilityName", comHealthFacilityName.Text);
                    sqlcmd.Parameters.AddWithValue("@UniqueCTCIDNumber", txtUniqueCTCIDNumber.Text);
                    sqlcmd.Parameters.AddWithValue("@TGRFN", txtTGRFormNumber.Text);
                    sqlcmd.Parameters.AddWithValue("@FileRef", txtFileRef.Text);
                    sqlcmd.Parameters.AddWithValue("@CTCInfant", txtCTCIDInfant.Text);
                    sqlcmd.Parameters.AddWithValue("@UniqueHTC", txtUniqueHTCID.Text);
                    sqlcmd.Parameters.AddWithValue("@UniqueANC", txtUniqueANCID.Text);
                    sqlcmd.Parameters.AddWithValue("@ANCInfant", txtANCIDInfant.Text);
                    sqlcmd.Parameters.AddWithValue("@HEIDInfant", txtHEIDInfant.Text);
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
                        sqlcmd.Parameters.Add("@UniqueCTCIDNumber", SqlDbType.VarChar).Value = txtUniqueCTCIDNumber.Text;
                        sqlcmd.Parameters.Add("@TGRFN", SqlDbType.VarChar).Value = txtTGRFormNumber.Text;
                        sqlcmd.Parameters.Add("@FileRef", SqlDbType.VarChar).Value = txtFileRef.Text;
                        sqlcmd.Parameters.Add("@CTCInfant", SqlDbType.VarChar).Value = txtCTCIDInfant.Text;
                        sqlcmd.Parameters.Add("@UniqueHTC", SqlDbType.VarChar).Value = txtUniqueHTCID.Text;
                        sqlcmd.Parameters.Add("@UniqueANC", SqlDbType.VarChar).Value = txtUniqueANCID.Text;
                        sqlcmd.Parameters.Add("@ANCInfant", SqlDbType.VarChar).Value = txtANCIDInfant.Text;
                        sqlcmd.Parameters.Add("@HEIDInfant", SqlDbType.VarChar).Value = txtHEIDInfant.Text;
                        sqlcmd.Parameters.Add("@SearchCriteria", SqlDbType.VarChar).Value = search_criteria;
                        sqlcmd.Parameters.Add("@DSS_IDLong", SqlDbType.VarChar).Value = txtDSSID.Text;
                        sqlcmd.Parameters.Add("@Score", SqlDbType.VarChar).Value = score;
                        sqlcmd.Parameters.Add("@ScoreRankGap", SqlDbType.VarChar).Value = rankgap;
                        sqlcmd.Parameters.Add("@ScoreRankNoGap", SqlDbType.VarChar).Value = ranknogap;
                        sqlcmd.Parameters.Add("@ScoreRankIter", SqlDbType.VarChar).Value = rownumber;
                       
                        sqlcmd.ExecuteNonQuery();

                        if (txtDSSID.Text.Length>0)
                        {
                            MessageBox.Show("Match assigned." + Environment.NewLine + Environment.NewLine + "Mfanano umekubaliana.", "Match", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                birthyear = AdssIndividualRow.ItemArray.GetValue(3).ToString().Substring(0,4);
                score=AdssIndividualRow.ItemArray.GetValue(11).ToString();
                rankgap=AdssIndividualRow.ItemArray.GetValue(14).ToString();
                ranknogap=AdssIndividualRow.ItemArray.GetValue(15).ToString();
                rownumber = AdssIndividualRow.ItemArray.GetValue(16).ToString();
                namescore = float.Parse(AdssIndividualRow.ItemArray.GetValue(17).ToString());
                txtDSSID.Clear();
                dgvMemberships.Rows.Clear();
                txtDSSID.Text = adssID;
                Indiduallocation = AdssIndividualRow.ItemArray.GetValue(13).ToString();
                Search4ADSSMembersThatStaysWithIndividual_UsingADSSID_location();
                if (comConsentStatus.Text != "NOT ASKED")
                  btnAssignMatch.Enabled = true;
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



        /* what is this doing? */
        public bool CheckIfFileRefandDSSIDMatchExists()
        {

            bool result = false;
            using (System.Data.SqlClient.SqlConnection sqlcnt = new System.Data.SqlClient.SqlConnection(Properties.Settings.Default.connection))
            {

                string sql = @"SELECT dbo.fnIT_F8B4A9F4F7334F0ABB83D253539A1D25
                                                    ( 
                                                    @HealthFacilityName
                                                    ,@FileRef
                                                    ,@UniqueCTCIDNumber
                                                    ,@TGRFN
                                                    ,@CTCInfant
                                                    ,@UniqueHTC 
                                                    ,@UniqueANC
                                                    ,@ANCInfant 
                                                    ,@HEIDInfant
                                                    ,@DSS_IDLong                                                 
                                                    )";
                using (SqlCommand sqlcmd = new SqlCommand(sql, sqlcnt))
                {

                    //add parameters
                    sqlcmd.Parameters.AddWithValue("@HealthFacilityName", comHealthFacilityName.Text);
                    sqlcmd.Parameters.AddWithValue("@UniqueCTCIDNumber", txtUniqueCTCIDNumber.Text);
                    sqlcmd.Parameters.AddWithValue("@TGRFN", txtTGRFormNumber.Text);
                    sqlcmd.Parameters.AddWithValue("@FileRef", txtFileRef.Text);
                    sqlcmd.Parameters.AddWithValue("@CTCInfant", txtCTCIDInfant.Text);
                    sqlcmd.Parameters.AddWithValue("@UniqueHTC", txtUniqueHTCID.Text);
                    sqlcmd.Parameters.AddWithValue("@UniqueANC", txtUniqueANCID.Text);
                    sqlcmd.Parameters.AddWithValue("@ANCInfant", txtANCIDInfant.Text);
                    sqlcmd.Parameters.AddWithValue("@HEIDInfant", txtHEIDInfant.Text);
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
            Boolean IDOK = true;
           
            

            if (   txtUniqueCTCIDNumber.Text == "" 
                && txtCTCIDInfant.Text == "" 
                && txtFileRef.Text == ""
                && txtTGRFormNumber.Text == ""
                && txtUniqueHTCID.Text == ""
                && txtUniqueANCID.Text == ""
                && txtANCIDInfant.Text == ""
                && txtHEIDInfant.Text == ""
                && comConsentStatus.Text == "CONSENTED") 
            {
                DialogResult result1 = MessageBox.Show("You have not saved a Clinic ID for this patient.  Are you sure you want to proceed?" + Environment.NewLine + Environment.NewLine + "Hujahifadhi utambulisho wa kliniki wa huyu mgonjwa. Una hakika unataka kuendelea?", "Clinic ID Missing", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                IDOK = result1 == DialogResult.Yes;
            }
            
            
            if (IDOK)
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
        private void CheckDefaultBoxes()
        {

            cbFirstName.Checked = (txtFirstName.Text.Length>0);
            cbMiddleName.Checked = (txtMiddleName.Text.Length > 0); ;
            cbLastName.Checked = (txtLastName.Text.Length > 0); ;
            cbSex.Checked = (comSex.Text.Length > 0); 
            cbYear.Checked = (comYearofBirth.Text.Length>0 && comYearofBirth.Text != "YYYY");
            cbMonth.Checked = (comMonthofBirth.Text.Length > 0 && comMonthofBirth.Text != "MM");
            cbDay.Checked = (comDayofBirth.Text.Length > 0 && comDayofBirth.Text != "DD");
            cbVillage.Checked = (comVillage.Text.Length > 0 && comVillage.Text != "(none)");
            cbSubVillage.Checked = (comSubVillage.Text.Length > 0 && comSubVillage.Text != "(none)");
            cbTCLFirstName.Checked = (txtTCLFirstName.Text.Length >0);
            cbTCLMiddleName.Checked = (txtTCLMiddleName.Text.Length > 0);
            cbTCLLastName.Checked = (txtTCLLastName.Text.Length > 0);


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

            DialogResult result1 = MessageBox.Show("You can only save Match Notes once per session. Please make sure all match notes related to this session are entered. Are you sure you want to proceed?" + Environment.NewLine + Environment.NewLine + "Unaweza kuhifadhi tu maelezo yanayofanana kwa kila mada. Tafadhari hakikisha maelezo yote yanayofanana kuhusiana na mada hii yameingizwa. Una hakika unataka kuendelea?", "Match Notes", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            OneMatchNotes = result1 == DialogResult.Yes;
            if (OneMatchNotes == true)
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
                        sqlcmd.Parameters.Add("@UniqueCTCIDNumber", SqlDbType.VarChar).Value = txtUniqueCTCIDNumber.Text;
                        sqlcmd.Parameters.Add("@TGRFN", SqlDbType.VarChar).Value = txtTGRFormNumber.Text;
                        sqlcmd.Parameters.Add("@FileRef", SqlDbType.VarChar).Value = txtFileRef.Text;
                        sqlcmd.Parameters.Add("@CTCInfant", SqlDbType.VarChar).Value = txtCTCIDInfant.Text;
                        sqlcmd.Parameters.Add("@UniqueHTC", SqlDbType.VarChar).Value = txtUniqueHTCID.Text;
                        sqlcmd.Parameters.Add("@UniqueANC", SqlDbType.VarChar).Value = txtUniqueANCID.Text;
                        sqlcmd.Parameters.Add("@ANCInfant", SqlDbType.VarChar).Value = txtANCIDInfant.Text;
                        sqlcmd.Parameters.Add("@HEIDInfant", SqlDbType.VarChar).Value = txtHEIDInfant.Text;
                        sqlcmd.Parameters.Add("@SearchCriteria", SqlDbType.VarChar).Value = search_criteria;
                        sqlcmd.Parameters.Add("@SearchForMatchNotes", SqlDbType.VarChar).Value = txtSearchForMatchNotes.Text;
                        sqlcmd.ExecuteNonQuery();

                        {
                            MessageBox.Show("Match notes saved." + Environment.NewLine + Environment.NewLine + "Maelezo yanayofanana yamehifadhiwa.", "Match Notes", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            btnDoNotMatch.Text = "Match Notes have been saved";
                            btnDoNotMatch.Enabled = false;
                            OneMatchNotes = false;
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


        /* WE DO NOT NEED THE FIELDWORKERS TO BE ABLE TO DELETE VISIT DATES  
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
                                sqlcmd.Parameters.Add("@UniqueCTCIDNumber", SqlDbType.VarChar).Value = txtUniqueCTCIDNumber.Text;
                                sqlcmd.Parameters.Add("@TGRFN", SqlDbType.VarChar).Value = txtTGRFormNumber.Text;
                                sqlcmd.Parameters.Add("@FileRef", SqlDbType.VarChar).Value = txtFileRef.Text;
                                sqlcmd.Parameters.Add("@CTCInfant", SqlDbType.VarChar).Value = txtCTCIDInfant.Text;
                                sqlcmd.Parameters.Add("@UniqueHTC", SqlDbType.VarChar).Value = txtUniqueHTCID.Text;
                                sqlcmd.Parameters.Add("@UniqueANC", SqlDbType.VarChar).Value = txtUniqueANCID.Text;
                                sqlcmd.Parameters.Add("@ANCInfant", SqlDbType.VarChar).Value = txtANCIDInfant.Text;
                                sqlcmd.Parameters.Add("@HEIDInfant", SqlDbType.VarChar).Value = txtHEIDInfant.Text;
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
        */



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
            //if (comMonthAtCurResident.Text == "MM")
            //{
            //    comYearofBirth.Text = "99";

            //}

            //if (comDayAtCurResident.Text == "DD")
            //{
            //    comYearofBirth.Text = "99";

            //}
        }

        private void comVillage_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dtTest = new DataTable();
            DataTable dt = new DataTable();
            System.Data.SqlClient.SqlConnection sqlcnt = new System.Data.SqlClient.SqlConnection(Properties.Settings.Default.connection);
            sqlcnt.Open();
            SqlCommand cmd=new SqlCommand();
            cmd.Connection = sqlcnt;
            cmd.CommandText="spGetSubVillages";
            cmd.CommandType=CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Village",comVillage.Text);
            SqlDataAdapter sdaTest = new SqlDataAdapter(cmd);
            DataSet dsTest=new DataSet();
            sdaTest.Fill(dsTest);
            //binding data to combobox
            comSubVillage.DataSource = dsTest.Tables[0];
            comSubVillage.DisplayMember="VisibleName";
            comSubVillage.ValueMember = "SubVillage";
            sqlcnt.Close();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label41_Click(object sender, EventArgs e)
        {

        }

        private void label45_Click(object sender, EventArgs e)
        {

        }

        private void txtFileRef_TextChanged(object sender, EventArgs e)
        {

        }

        private void label23_Click(object sender, EventArgs e)
        {

        }

        private void comHealthFacilityDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comHealthFacilityDepartment.Text != "")
            {
                tcPatientInfoSystem.Enabled = true;
            }
            else
            {
                tcPatientInfoSystem.Enabled = false;
            }
        }

        private void comConsentStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (PatientEditActive == false)
            {
                //if (comConsentStatus.Text == "NOT ASKED")
                //{
                //    if (OneMatchNotes == true)
                //    {
                //        btnDoNotMatch.Text = "You must ask for consent to proceed";
                //        btnDoNotMatch.Enabled = false;
                //    }
                //    btnAssignMatch.Text = "You must ask for consent to proceed";
                //    btnAssignMatch.Enabled = false;
                //}
                if (comConsentStatus.Text == "CONSENTED")
                {
                    if (OneMatchNotes == true)
                    {
                        btnDoNotMatch.Text = "Save match notes";
                        btnDoNotMatch.Enabled = true;
                    }
                    btnAssignMatch.Text = "Assign match";
                    btnAssignMatch.Enabled = true;
                }
                if (comConsentStatus.Text == "REFUSED")
                {
                    if (OneMatchNotes == true)
                    {
                        btnDoNotMatch.Text = "Save match notes";
                        btnDoNotMatch.Enabled = true;
                    }
                    btnAssignMatch.Text = "Assign match";
                    btnAssignMatch.Enabled = true;
                }
                //if (comConsentStatus.Text == "NOT IN DSS AREA")
                //{
                //    if (OneMatchNotes == true)
                //    {
                //        btnDoNotMatch.Text = "Save match notes";
                //        btnDoNotMatch.Enabled = true;
                //    }
                //    btnAssignMatch.Text = "You must ask for consent to proceed";
                //    btnAssignMatch.Enabled = false;
                //}
            }

        }



      

   
  
    }

       
    }

