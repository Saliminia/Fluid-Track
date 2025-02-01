using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;

namespace DMR
{
    public partial class frmAccount : Form
    {
        const string CLASS_NAME = "frmAccount";

        frmMain mainForm = null;
        //=======================================================
        public enum InteractMode
        {
            Add,
            Edit
        }
        //=======================================================
        InteractMode curInteractMode;
        string userIDtoEditOrAdd = "";

        //=======================================================
        public frmAccount(InteractMode interactMode, string userID_AddEdit, frmMain mainForm)
        {
            this.mainForm = mainForm;

            InitializeComponent();

            curInteractMode = interactMode;
            userIDtoEditOrAdd = userID_AddEdit;
        }
        //-------------------------------------------------------
        private void frmAccount_Load(object sender, EventArgs e)
        {
            if (curInteractMode == InteractMode.Add)
            {
                if (mainForm.curUserRole.userID.ToLower() != "admin")
                    Close();

                btnOK.Text = "Add";

                cboxRole.SelectedIndex = 0;

                lblPassNew.Visible = false;
                txtPassNew.Visible = false;
                lblPassRep.Visible = false;
                txtPassRep.Visible = false;

                txtPassCur.Text = "123456";

                txtID.ReadOnly = false;
                txtID.Text = userIDtoEditOrAdd;
                txtID.Focus();
            }
            else
            {
                if (!(mainForm.curUserRole.userID.ToLower() == "admin" /*Editor*/&& userIDtoEditOrAdd.ToLower() != "admin"/*under edit*/))
                {
                    cboxRole.Visible = false;
                    lblUserRole.Visible = false;
                }

                string query = "select FirstName,LastName,RoleName,userImage from at_User where ID = \'" + userIDtoEditOrAdd.Replace("\'", "\'\'") + "\'";

                DataSet ds = new DataSet();

                if (ConnectionManager.ExecQuery(query, ref ds, 1))
                {
                    txtID.Text = userIDtoEditOrAdd;
                    txtFirstName.Text = ds.Tables[0].Rows[0][0].ToString();
                    txtLastName.Text = ds.Tables[0].Rows[0][1].ToString();
                    cboxRole.SelectedItem = ds.Tables[0].Rows[0][2].ToString();

                    if (ds.Tables[0].Rows[0][3] != DBNull.Value)
                    {
                        Image img = SqlImageHelper.ByteArrayToImage((byte[])ds.Tables[0].Rows[0][3]);
                        pboxImage.BackgroundImage = img;
                    }

                    ds.Dispose();
                }

            }
        }
        //-------------------------------------------------------
        private void btnOK_Click(object sender, EventArgs e)
        {
            string givenUserID = StringManipulation.ReplaceBadNameChars(txtID.Text.Trim());

            if (givenUserID.Trim() == "")
            {
                InformationManager.Set_Info("Invalid User ID");
                return;
            }

            string role = "None";

            if (cboxRole.SelectedIndex != -1)
                role = cboxRole.SelectedItem.ToString();

            if (curInteractMode == InteractMode.Add)//######################################################
            {
                if (mainForm.curUserRole.userID.ToLower() != "admin")
                    return;

                if (givenUserID.ToLower() == "admin")
                    return;

                string pass = txtPassCur.Text/*do not trim*/;
                if (pass.Length < 6)
                {
                    InformationManager.Set_Info("Length of password must be greater than or equal to 6");
                    return;
                }

                pass = EncoderDecoder.EncodePassword(pass);

                //prc_at_User_insert
                {
                    List<ProcedureParam> prms = new List<ProcedureParam>();
                    prms.Add(new ProcedureParam("@ID", givenUserID, 50));
                    prms.Add(new ProcedureParam("@Pass", pass, 50));
                    prms.Add(new ProcedureParam("@FirstName", txtFirstName.Text, 30));
                    prms.Add(new ProcedureParam("@LastName", txtLastName.Text, 30));

                    string simpErr;
                    Errors critErr;
                    Int64 resultStat;

                    int ret = Procedures.CallProcedure("prc_at_User_insert", prms, out simpErr, out critErr, out resultStat);

                    if (ret != 0)
                    {
                        if (ret == 1)
                            InformationManager.Set_Info(simpErr);
                        else
                            InformationManager.Set_Info(critErr);

                        return;
                    }
                }

                //prc_at_User_update_RoleName
                {
                    List<ProcedureParam> prms = new List<ProcedureParam>();
                    prms.Add(new ProcedureParam("@ID", givenUserID, 50));
                    prms.Add(new ProcedureParam("@RoleName", role, 30));

                    string simpErr;
                    Errors critErr;
                    Int64 resultStat;

                    int ret = Procedures.CallProcedure("prc_at_User_update_RoleName", prms, out simpErr, out critErr, out resultStat);

                    if (ret != 0)
                    {
                        if (ret == 1)
                            InformationManager.Set_Info(simpErr);
                        else
                            InformationManager.Set_Info(critErr);

                        return;
                    }
                }
            }
            else//######################################################
            {
                string newPass = txtPassNew.Text/*do not trim*/;
                string pass = txtPassCur.Text;

                if (newPass != "" || pass != "")
                {
                    if (newPass.Length < 6)
                    {
                        InformationManager.Set_Info("Length of new password must be greater than or equal to 6");
                        return;
                    }

                    if (newPass != txtPassRep.Text)
                    {
                        InformationManager.Set_Info("New Password Mismatched");
                        return;
                    }


                    pass = EncoderDecoder.EncodePassword(pass);
                    newPass = EncoderDecoder.EncodePassword(newPass);


                    List<ProcedureParam> prms = new List<ProcedureParam>();
                    prms.Add(new ProcedureParam("@ID", givenUserID, 50));
                    prms.Add(new ProcedureParam("@Pass", pass, 50));
                    prms.Add(new ProcedureParam("@Pass_New", newPass, 50));

                    string simpErr;
                    Errors critErr;
                    Int64 resultStat;

                    int ret = Procedures.CallProcedure("prc_at_User_update_Pass", prms, out simpErr, out critErr, out resultStat);

                    if (ret != 0)
                    {
                        if (ret == 1)
                            InformationManager.Set_Info(simpErr);
                        else
                            InformationManager.Set_Info(critErr);

                        return;
                    }
                }

                //prc_at_User_update_Name
                {
                    List<ProcedureParam> prms = new List<ProcedureParam>();
                    prms.Add(new ProcedureParam("@ID", givenUserID, 50));
                    prms.Add(new ProcedureParam("@FirstName", txtFirstName.Text, 30));
                    prms.Add(new ProcedureParam("@LastName", txtLastName.Text, 30));

                    string simpErr;
                    Errors critErr;
                    Int64 resultStat;

                    int ret = Procedures.CallProcedure("prc_at_User_update_Name", prms, out simpErr, out critErr, out resultStat);

                    if (ret != 0)
                    {
                        if (ret == 1)
                            InformationManager.Set_Info(simpErr);
                        else
                            InformationManager.Set_Info(critErr);

                        return;
                    }
                }


                //prc_at_User_update_RoleName
                if (mainForm.curUserRole.userID.ToLower() == "admin" && givenUserID.ToLower() != "admin")
                {
                    List<ProcedureParam> prms = new List<ProcedureParam>();
                    prms.Add(new ProcedureParam("@ID", givenUserID, 50));
                    prms.Add(new ProcedureParam("@RoleName", role, 30));

                    string simpErr;
                    Errors critErr;
                    Int64 resultStat;

                    int ret = Procedures.CallProcedure("prc_at_User_update_RoleName", prms, out simpErr, out critErr, out resultStat);

                    if (ret != 0)
                    {
                        if (ret == 1)
                            InformationManager.Set_Info(simpErr);
                        else
                            InformationManager.Set_Info(critErr);

                        return;
                    }
                }
            }

            //================================

            //prc_at_User_update_Image
            {
                List<ProcedureParam> prms = new List<ProcedureParam>();
                prms.Add(new ProcedureParam("@ID", givenUserID, 50));

                if (pboxImage.BackgroundImage != null)
                    prms.Add(new ProcedureParam("@userImage", pboxImage.BackgroundImage));
                else
                    prms.Add(new ProcedureParam("@userImage", ProcedureParam.ParamType.PT_Image));//null

                string simpErr;
                Errors critErr;
                Int64 resultStat;

                int ret = Procedures.CallProcedure("prc_at_User_update_Image", prms, out simpErr, out critErr, out resultStat);

                if (ret != 0)
                {
                    if (ret == 1)
                        InformationManager.Set_Info(simpErr);
                    else
                        InformationManager.Set_Info(critErr);

                    return;
                }
            }


            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }
        //-------------------------------------------------------
        private void btnClearImage_Click(object sender, EventArgs e)
        {
            pboxImage.BackgroundImage = null;
        }
        //-------------------------------------------------------
        private void btnLoadImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.Filter = "Image|*.png;*.jpg;*.bmp";
            if (ofd.ShowDialog() != DialogResult.OK)
                return;

            Image img = Image.FromFile(ofd.FileName);

            if (img.Width > 400 || img.Height > 400)
            {
                InformationManager.Set_Info("Image must be of size at most 400x400 pixels ");
                return;
            }

            pboxImage.BackgroundImage = img;
        }
        //-------------------------------------------------------
    }
}
