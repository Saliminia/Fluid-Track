namespace DMR
{
    partial class MudVolManagementForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MudVolManagementForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("dfs1");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("dfs2");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Drilling Fluid Systems", new System.Windows.Forms.TreeNode[] {
            treeNode4,
            treeNode5});
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tcMudVolMan = new System.Windows.Forms.TabControl();
            this.tpDFSs = new System.Windows.Forms.TabPage();
            this.lblSelectedDFS = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnDfsCheckDFSs = new System.Windows.Forms.Button();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.tcDfs = new System.Windows.Forms.TabControl();
            this.tpDfsRecTrans = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnRecRemove = new System.Windows.Forms.Button();
            this.btnRecAdd = new System.Windows.Forms.Button();
            this.btnRecRefresh = new System.Windows.Forms.Button();
            this.btnRecAutoCompute = new System.Windows.Forms.Button();
            this.btnRecApply = new System.Windows.Forms.Button();
            this.txtRecTotalVol = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.dgvRec = new System.Windows.Forms.DataGridView();
            this.recID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column30 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column24 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column28 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnTransRemove = new System.Windows.Forms.Button();
            this.btnTransAdd = new System.Windows.Forms.Button();
            this.btnTransRefresh = new System.Windows.Forms.Button();
            this.btnTransAutoCompute = new System.Windows.Forms.Button();
            this.btnTransApply = new System.Windows.Forms.Button();
            this.txtTransTotalVol = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dgvTrans = new System.Windows.Forms.DataGridView();
            this.transID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column31 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column25 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column29 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tpDfsBuild = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.grbStartBuiltRainGain = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtStartVol = new System.Windows.Forms.TextBox();
            this.txtGainVol = new System.Windows.Forms.TextBox();
            this.btnGeneralStartVol = new System.Windows.Forms.Button();
            this.btnGeneralApply = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtRainVol = new System.Windows.Forms.TextBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.btnBuiltUsingOthersAutoCompute = new System.Windows.Forms.Button();
            this.btnBuiltUsingRemove = new System.Windows.Forms.Button();
            this.btnBuiltUsingAdd = new System.Windows.Forms.Button();
            this.btnBuiltUsingRefresh = new System.Windows.Forms.Button();
            this.btnBuiltUsingApply = new System.Windows.Forms.Button();
            this.txtBuiltUsingTotalVol = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.dgvBuiltUsing = new System.Windows.Forms.DataGridView();
            this.builtUsingID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column33 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column23 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn17 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.btnBuiltVolRemove = new System.Windows.Forms.Button();
            this.btnBuiltVolAdd = new System.Windows.Forms.Button();
            this.btnBuiltVolRefresh = new System.Windows.Forms.Button();
            this.btnBuiltVolApply = new System.Windows.Forms.Button();
            this.txtBuiltVolTotalVol = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dgvBuiltVol = new System.Windows.Forms.DataGridView();
            this.builtVolID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column32 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn19 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn20 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn21 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn22 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn23 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tpDfsTreated = new System.Windows.Forms.TabPage();
            this.groupBox12 = new System.Windows.Forms.GroupBox();
            this.btnTreatRemove = new System.Windows.Forms.Button();
            this.btnTreatAdd = new System.Windows.Forms.Button();
            this.btnTreatRefresh = new System.Windows.Forms.Button();
            this.btnTreatApply = new System.Windows.Forms.Button();
            this.txtTreatTotalVol = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.dgvTreat = new System.Windows.Forms.DataGridView();
            this.treatedID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column34 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn31 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn32 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn33 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column18 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column19 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column20 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column21 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn34 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn35 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn36 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn37 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn38 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tpDfsDisp = new System.Windows.Forms.TabPage();
            this.btnDispRetApply = new System.Windows.Forms.Button();
            this.grbDipRetDaily = new System.Windows.Forms.GroupBox();
            this.label24 = new System.Windows.Forms.Label();
            this.txtDailyStringDisp = new System.Windows.Forms.TextBox();
            this.groupBox13 = new System.Windows.Forms.GroupBox();
            this.label25 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.txtDailyOver = new System.Windows.Forms.TextBox();
            this.txtDailyWaste = new System.Windows.Forms.TextBox();
            this.txtDailyPitMW = new System.Windows.Forms.TextBox();
            this.txtDailyPitVol = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.grbDipRetAtTheEnd = new System.Windows.Forms.GroupBox();
            this.groupBox14 = new System.Windows.Forms.GroupBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.txtAtEndMW = new System.Windows.Forms.TextBox();
            this.txtAtEndVol = new System.Windows.Forms.TextBox();
            this.txtAtEndMaxFR = new System.Windows.Forms.TextBox();
            this.txtAtEndMinFR = new System.Windows.Forms.TextBox();
            this.txtAtEndDepth = new System.Windows.Forms.TextBox();
            this.groupBox15 = new System.Windows.Forms.GroupBox();
            this.label31 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.txtAtEndOver = new System.Windows.Forms.TextBox();
            this.txtAtEndWaste = new System.Windows.Forms.TextBox();
            this.txtAtEndPitMW = new System.Windows.Forms.TextBox();
            this.txtAtEndPitVol = new System.Windows.Forms.TextBox();
            this.label34 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.trvDFSs = new System.Windows.Forms.TreeView();
            this.tpPit = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox18 = new System.Windows.Forms.GroupBox();
            this.dgvTotalWellVol = new System.Windows.Forms.DataGridView();
            this.totalWellID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column26 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnTotalWellVolRefresh = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnCMTApply = new System.Windows.Forms.Button();
            this.btnCMTRefresh = new System.Windows.Forms.Button();
            this.dgvPitTankCMT_Spacer = new System.Windows.Forms.DataGridView();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column22 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox17 = new System.Windows.Forms.GroupBox();
            this.btnPitTankCheckVolumes = new System.Windows.Forms.Button();
            this.dgvPitTank = new System.Windows.Forms.DataGridView();
            this.pitVolId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column27 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pitID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnPitTankApply = new System.Windows.Forms.Button();
            this.btnPitTankRefresh = new System.Windows.Forms.Button();
            this.tpTotalVolMan = new System.Windows.Forms.TabPage();
            this.btnTotalVolManRefresh = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label50 = new System.Windows.Forms.Label();
            this.label51 = new System.Windows.Forms.Label();
            this.label52 = new System.Windows.Forms.Label();
            this.label53 = new System.Windows.Forms.Label();
            this.label54 = new System.Windows.Forms.Label();
            this.txtTVM_CD_MinsTotalCir = new System.Windows.Forms.TextBox();
            this.txtTVM_CD_BblTotalCir = new System.Windows.Forms.TextBox();
            this.txtTVM_CD_MinsBottomUp = new System.Windows.Forms.TextBox();
            this.txtTVM_CD_MinsToBit = new System.Windows.Forms.TextBox();
            this.txtTVM_CD_BblBottomUp = new System.Windows.Forms.TextBox();
            this.txtTVM_CD_BblToBit = new System.Windows.Forms.TextBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.label61 = new System.Windows.Forms.Label();
            this.txtTVM_BVM_DispVol = new System.Windows.Forms.TextBox();
            this.label60 = new System.Windows.Forms.Label();
            this.txtTVM_BVM_RetVol = new System.Windows.Forms.TextBox();
            this.label59 = new System.Windows.Forms.Label();
            this.txtTVM_BVM_AddVol = new System.Windows.Forms.TextBox();
            this.label55 = new System.Windows.Forms.Label();
            this.txtTVM_BVM_StartVol = new System.Windows.Forms.TextBox();
            this.label56 = new System.Windows.Forms.Label();
            this.label57 = new System.Windows.Forms.Label();
            this.label58 = new System.Windows.Forms.Label();
            this.label62 = new System.Windows.Forms.Label();
            this.label500 = new System.Windows.Forms.Label();
            this.txtTVM_BVM_BuildVol = new System.Windows.Forms.TextBox();
            this.label64 = new System.Windows.Forms.Label();
            this.txtTVM_BVM_EndVol = new System.Windows.Forms.TextBox();
            this.txtTVM_BVM_LossVol = new System.Windows.Forms.TextBox();
            this.txtTVM_BVM_TransVol = new System.Windows.Forms.TextBox();
            this.txtTVM_BVM_Gain = new System.Windows.Forms.TextBox();
            this.txtTVM_BVM_RecVol = new System.Windows.Forms.TextBox();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.lblTVM_HVM_StringVol = new System.Windows.Forms.Label();
            this.txtTVM_HVM_StringVol = new System.Windows.Forms.TextBox();
            this.label66 = new System.Windows.Forms.Label();
            this.txtTVM_HVM_AnnVol = new System.Windows.Forms.TextBox();
            this.label67 = new System.Windows.Forms.Label();
            this.txtTVM_HVM_DrillVol = new System.Windows.Forms.TextBox();
            this.label68 = new System.Windows.Forms.Label();
            this.label69 = new System.Windows.Forms.Label();
            this.txtTVM_HVM_TotalVol = new System.Windows.Forms.TextBox();
            this.txtTVM_HVM_BelVol = new System.Windows.Forms.TextBox();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.label700 = new System.Windows.Forms.Label();
            this.label71 = new System.Windows.Forms.Label();
            this.label600 = new System.Windows.Forms.Label();
            this.txtTVM_SVM_PitVol = new System.Windows.Forms.TextBox();
            this.txtTVM_SVM_TreVol = new System.Windows.Forms.TextBox();
            this.txtTVM_SVM_SurVol = new System.Windows.Forms.TextBox();
            this.ctxDFSMnu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ctxDFSMnuRemove = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.ctxDFSMnuSetAsCurrent = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxDFSMnu_Parent = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ctxDFSMnu_ParentAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.tcMudVolMan.SuspendLayout();
            this.tpDFSs.SuspendLayout();
            this.tcDfs.SuspendLayout();
            this.tpDfsRecTrans.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRec)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTrans)).BeginInit();
            this.tpDfsBuild.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.grbStartBuiltRainGain.SuspendLayout();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBuiltUsing)).BeginInit();
            this.groupBox10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBuiltVol)).BeginInit();
            this.tpDfsTreated.SuspendLayout();
            this.groupBox12.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTreat)).BeginInit();
            this.tpDfsDisp.SuspendLayout();
            this.grbDipRetDaily.SuspendLayout();
            this.groupBox13.SuspendLayout();
            this.grbDipRetAtTheEnd.SuspendLayout();
            this.groupBox14.SuspendLayout();
            this.groupBox15.SuspendLayout();
            this.tpPit.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.groupBox18.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTotalWellVol)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPitTankCMT_Spacer)).BeginInit();
            this.groupBox17.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPitTank)).BeginInit();
            this.tpTotalVolMan.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.ctxDFSMnu.SuspendLayout();
            this.ctxDFSMnu_Parent.SuspendLayout();
            this.SuspendLayout();
            // 
            // tcMudVolMan
            // 
            this.tcMudVolMan.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tcMudVolMan.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tcMudVolMan.Controls.Add(this.tpDFSs);
            this.tcMudVolMan.Controls.Add(this.tpPit);
            this.tcMudVolMan.Controls.Add(this.tpTotalVolMan);
            this.tcMudVolMan.Location = new System.Drawing.Point(12, 12);
            this.tcMudVolMan.Name = "tcMudVolMan";
            this.tcMudVolMan.SelectedIndex = 0;
            this.tcMudVolMan.Size = new System.Drawing.Size(976, 474);
            this.tcMudVolMan.TabIndex = 34;
            // 
            // tpDFSs
            // 
            this.tpDFSs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tpDFSs.Controls.Add(this.lblSelectedDFS);
            this.tpDFSs.Controls.Add(this.label1);
            this.tpDFSs.Controls.Add(this.btnDfsCheckDFSs);
            this.tpDFSs.Controls.Add(this.tcDfs);
            this.tpDFSs.Controls.Add(this.trvDFSs);
            this.tpDFSs.ImageIndex = 0;
            this.tpDFSs.Location = new System.Drawing.Point(4, 25);
            this.tpDFSs.Name = "tpDFSs";
            this.tpDFSs.Padding = new System.Windows.Forms.Padding(3);
            this.tpDFSs.Size = new System.Drawing.Size(968, 445);
            this.tpDFSs.TabIndex = 5;
            this.tpDFSs.Tag = "Prev. Title: Drilling Fluid Systems ==> rename in data base if needed";
            this.tpDFSs.Text = "Mud Volume Accounting";
            this.tpDFSs.UseVisualStyleBackColor = true;
            // 
            // lblSelectedDFS
            // 
            this.lblSelectedDFS.AutoSize = true;
            this.lblSelectedDFS.Location = new System.Drawing.Point(273, 6);
            this.lblSelectedDFS.Name = "lblSelectedDFS";
            this.lblSelectedDFS.Size = new System.Drawing.Size(25, 13);
            this.lblSelectedDFS.TabIndex = 189;
            this.lblSelectedDFS.Text = "      ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(191, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 189;
            this.label1.Text = "Selected DFS:";
            // 
            // btnDfsCheckDFSs
            // 
            this.btnDfsCheckDFSs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDfsCheckDFSs.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDfsCheckDFSs.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDfsCheckDFSs.ImageKey = "point_hand.png";
            this.btnDfsCheckDFSs.ImageList = this.imageList;
            this.btnDfsCheckDFSs.Location = new System.Drawing.Point(49, 406);
            this.btnDfsCheckDFSs.Name = "btnDfsCheckDFSs";
            this.btnDfsCheckDFSs.Size = new System.Drawing.Size(95, 23);
            this.btnDfsCheckDFSs.TabIndex = 1;
            this.btnDfsCheckDFSs.Text = "Check DFSs";
            this.btnDfsCheckDFSs.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDfsCheckDFSs.UseVisualStyleBackColor = true;
            this.btnDfsCheckDFSs.Click += new System.EventHandler(this.btnDfsCheckDFSs_Click);
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "Project");
            this.imageList.Images.SetKeyName(1, "Rig");
            this.imageList.Images.SetKeyName(2, "Well");
            this.imageList.Images.SetKeyName(3, "Onshore");
            this.imageList.Images.SetKeyName(4, "Offshore");
            this.imageList.Images.SetKeyName(5, "Add");
            this.imageList.Images.SetKeyName(6, "add2");
            this.imageList.Images.SetKeyName(7, "Remove");
            this.imageList.Images.SetKeyName(8, "remove2");
            this.imageList.Images.SetKeyName(9, "Edit");
            this.imageList.Images.SetKeyName(10, "Check");
            this.imageList.Images.SetKeyName(11, "warning");
            this.imageList.Images.SetKeyName(12, "Right");
            this.imageList.Images.SetKeyName(13, "left");
            this.imageList.Images.SetKeyName(14, "Up");
            this.imageList.Images.SetKeyName(15, "Down");
            this.imageList.Images.SetKeyName(16, "report");
            this.imageList.Images.SetKeyName(17, "revision");
            this.imageList.Images.SetKeyName(18, "refresh");
            this.imageList.Images.SetKeyName(19, "forbidden.png");
            this.imageList.Images.SetKeyName(20, "point_hand.png");
            this.imageList.Images.SetKeyName(21, "noFilter");
            this.imageList.Images.SetKeyName(22, "applyFilter");
            this.imageList.Images.SetKeyName(23, "bookmark.png");
            this.imageList.Images.SetKeyName(24, "flask.png");
            this.imageList.Images.SetKeyName(25, "eject.png");
            this.imageList.Images.SetKeyName(26, "user_clear.png");
            this.imageList.Images.SetKeyName(27, "open");
            // 
            // tcDfs
            // 
            this.tcDfs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tcDfs.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tcDfs.Controls.Add(this.tpDfsRecTrans);
            this.tcDfs.Controls.Add(this.tpDfsBuild);
            this.tcDfs.Controls.Add(this.tpDfsTreated);
            this.tcDfs.Controls.Add(this.tpDfsDisp);
            this.tcDfs.Location = new System.Drawing.Point(190, 32);
            this.tcDfs.Name = "tcDfs";
            this.tcDfs.SelectedIndex = 0;
            this.tcDfs.Size = new System.Drawing.Size(770, 405);
            this.tcDfs.TabIndex = 4;
            // 
            // tpDfsRecTrans
            // 
            this.tpDfsRecTrans.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tpDfsRecTrans.Controls.Add(this.tableLayoutPanel1);
            this.tpDfsRecTrans.Location = new System.Drawing.Point(4, 25);
            this.tpDfsRecTrans.Name = "tpDfsRecTrans";
            this.tpDfsRecTrans.Padding = new System.Windows.Forms.Padding(3);
            this.tpDfsRecTrans.Size = new System.Drawing.Size(762, 376);
            this.tpDfsRecTrans.TabIndex = 0;
            this.tpDfsRecTrans.Text = "Rec./Trans.";
            this.tpDfsRecTrans.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.groupBox4, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox3, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(754, 368);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox4.Controls.Add(this.btnRecRemove);
            this.groupBox4.Controls.Add(this.btnRecAdd);
            this.groupBox4.Controls.Add(this.btnRecRefresh);
            this.groupBox4.Controls.Add(this.btnRecAutoCompute);
            this.groupBox4.Controls.Add(this.btnRecApply);
            this.groupBox4.Controls.Add(this.txtRecTotalVol);
            this.groupBox4.Controls.Add(this.label13);
            this.groupBox4.Controls.Add(this.dgvRec);
            this.groupBox4.Location = new System.Drawing.Point(3, 3);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(615, 178);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Received Volume";
            // 
            // btnRecRemove
            // 
            this.btnRecRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRecRemove.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnRecRemove.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRecRemove.ImageKey = "Remove";
            this.btnRecRemove.ImageList = this.imageList;
            this.btnRecRemove.Location = new System.Drawing.Point(66, 149);
            this.btnRecRemove.Name = "btnRecRemove";
            this.btnRecRemove.Size = new System.Drawing.Size(75, 23);
            this.btnRecRemove.TabIndex = 2;
            this.btnRecRemove.Text = "Remove";
            this.btnRecRemove.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnRecRemove.UseVisualStyleBackColor = true;
            this.btnRecRemove.Click += new System.EventHandler(this.btnRecRemove_Click);
            // 
            // btnRecAdd
            // 
            this.btnRecAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRecAdd.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnRecAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRecAdd.ImageKey = "add2";
            this.btnRecAdd.ImageList = this.imageList;
            this.btnRecAdd.Location = new System.Drawing.Point(8, 149);
            this.btnRecAdd.Name = "btnRecAdd";
            this.btnRecAdd.Size = new System.Drawing.Size(52, 23);
            this.btnRecAdd.TabIndex = 1;
            this.btnRecAdd.Text = "Add";
            this.btnRecAdd.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnRecAdd.UseVisualStyleBackColor = true;
            this.btnRecAdd.Click += new System.EventHandler(this.btnRecAdd_Click);
            // 
            // btnRecRefresh
            // 
            this.btnRecRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRecRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnRecRefresh.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRecRefresh.ImageKey = "refresh";
            this.btnRecRefresh.ImageList = this.imageList;
            this.btnRecRefresh.Location = new System.Drawing.Point(538, 149);
            this.btnRecRefresh.Name = "btnRecRefresh";
            this.btnRecRefresh.Size = new System.Drawing.Size(71, 23);
            this.btnRecRefresh.TabIndex = 5;
            this.btnRecRefresh.Text = "Refresh";
            this.btnRecRefresh.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnRecRefresh.UseVisualStyleBackColor = true;
            this.btnRecRefresh.Click += new System.EventHandler(this.btnRecRefresh_Click);
            // 
            // btnRecAutoCompute
            // 
            this.btnRecAutoCompute.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRecAutoCompute.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnRecAutoCompute.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRecAutoCompute.ImageKey = "point_hand.png";
            this.btnRecAutoCompute.ImageList = this.imageList;
            this.btnRecAutoCompute.Location = new System.Drawing.Point(228, 149);
            this.btnRecAutoCompute.Name = "btnRecAutoCompute";
            this.btnRecAutoCompute.Size = new System.Drawing.Size(104, 23);
            this.btnRecAutoCompute.TabIndex = 3;
            this.btnRecAutoCompute.Text = "Auto Compute";
            this.btnRecAutoCompute.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnRecAutoCompute.UseVisualStyleBackColor = true;
            this.btnRecAutoCompute.Click += new System.EventHandler(this.btnRecAutoCompute_Click);
            // 
            // btnRecApply
            // 
            this.btnRecApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRecApply.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnRecApply.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRecApply.ImageKey = "Check";
            this.btnRecApply.ImageList = this.imageList;
            this.btnRecApply.Location = new System.Drawing.Point(428, 149);
            this.btnRecApply.Name = "btnRecApply";
            this.btnRecApply.Size = new System.Drawing.Size(104, 23);
            this.btnRecApply.TabIndex = 4;
            this.btnRecApply.Text = "Apply Changes";
            this.btnRecApply.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnRecApply.UseVisualStyleBackColor = true;
            this.btnRecApply.Click += new System.EventHandler(this.btnRecApply_Click);
            // 
            // txtRecTotalVol
            // 
            this.txtRecTotalVol.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtRecTotalVol.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRecTotalVol.Location = new System.Drawing.Point(115, 121);
            this.txtRecTotalVol.Name = "txtRecTotalVol";
            this.txtRecTotalVol.ReadOnly = true;
            this.txtRecTotalVol.Size = new System.Drawing.Size(89, 20);
            this.txtRecTotalVol.TabIndex = 189;
            this.txtRecTotalVol.Text = "num";
            // 
            // label13
            // 
            this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(7, 123);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(84, 13);
            this.label13.TabIndex = 188;
            this.label13.Text = "Total Volume(?):";
            // 
            // dgvRec
            // 
            this.dgvRec.AllowUserToAddRows = false;
            this.dgvRec.AllowUserToDeleteRows = false;
            dataGridViewCellStyle11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.dgvRec.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle11;
            this.dgvRec.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvRec.BackgroundColor = System.Drawing.Color.White;
            this.dgvRec.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRec.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.recID,
            this.Column30,
            this.Column1,
            this.Column24,
            this.dataGridViewTextBoxColumn6,
            this.Column28,
            this.dataGridViewTextBoxColumn7});
            this.dgvRec.EnableHeadersVisualStyles = false;
            this.dgvRec.Location = new System.Drawing.Point(8, 19);
            this.dgvRec.MultiSelect = false;
            this.dgvRec.Name = "dgvRec";
            this.dgvRec.Size = new System.Drawing.Size(601, 96);
            this.dgvRec.TabIndex = 0;
            this.dgvRec.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvRec_CellMouseClick);
            this.dgvRec.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvRec_CellValueChanged);
            // 
            // recID
            // 
            this.recID.HeaderText = "";
            this.recID.Name = "recID";
            this.recID.ReadOnly = true;
            this.recID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.recID.Visible = false;
            // 
            // Column30
            // 
            this.Column30.HeaderText = "Seq.";
            this.Column30.Name = "Column30";
            this.Column30.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.Column30.Width = 50;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Project/Rig/Well/Hole";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.Column1.Width = 150;
            // 
            // Column24
            // 
            this.Column24.HeaderText = "recLocationID";
            this.Column24.Name = "Column24";
            this.Column24.ReadOnly = true;
            this.Column24.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.Column24.Visible = false;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.HeaderText = "Volume (su)";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // Column28
            // 
            this.Column28.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column28.HeaderText = "Chemicals Conc. & Used";
            this.Column28.Name = "Column28";
            this.Column28.ReadOnly = true;
            this.Column28.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn7.HeaderText = "MW (su)";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox3.Controls.Add(this.btnTransRemove);
            this.groupBox3.Controls.Add(this.btnTransAdd);
            this.groupBox3.Controls.Add(this.btnTransRefresh);
            this.groupBox3.Controls.Add(this.btnTransAutoCompute);
            this.groupBox3.Controls.Add(this.btnTransApply);
            this.groupBox3.Controls.Add(this.txtTransTotalVol);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.dgvTrans);
            this.groupBox3.Location = new System.Drawing.Point(3, 187);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(615, 178);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Transferred Volume";
            // 
            // btnTransRemove
            // 
            this.btnTransRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnTransRemove.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnTransRemove.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTransRemove.ImageKey = "Remove";
            this.btnTransRemove.ImageList = this.imageList;
            this.btnTransRemove.Location = new System.Drawing.Point(66, 149);
            this.btnTransRemove.Name = "btnTransRemove";
            this.btnTransRemove.Size = new System.Drawing.Size(75, 23);
            this.btnTransRemove.TabIndex = 2;
            this.btnTransRemove.Text = "Remove";
            this.btnTransRemove.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnTransRemove.UseVisualStyleBackColor = true;
            this.btnTransRemove.Click += new System.EventHandler(this.btnTransRemove_Click);
            // 
            // btnTransAdd
            // 
            this.btnTransAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnTransAdd.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnTransAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTransAdd.ImageKey = "add2";
            this.btnTransAdd.ImageList = this.imageList;
            this.btnTransAdd.Location = new System.Drawing.Point(8, 149);
            this.btnTransAdd.Name = "btnTransAdd";
            this.btnTransAdd.Size = new System.Drawing.Size(52, 23);
            this.btnTransAdd.TabIndex = 1;
            this.btnTransAdd.Text = "Add";
            this.btnTransAdd.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnTransAdd.UseVisualStyleBackColor = true;
            this.btnTransAdd.Click += new System.EventHandler(this.btnTransAdd_Click);
            // 
            // btnTransRefresh
            // 
            this.btnTransRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTransRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnTransRefresh.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTransRefresh.ImageKey = "refresh";
            this.btnTransRefresh.ImageList = this.imageList;
            this.btnTransRefresh.Location = new System.Drawing.Point(538, 149);
            this.btnTransRefresh.Name = "btnTransRefresh";
            this.btnTransRefresh.Size = new System.Drawing.Size(71, 23);
            this.btnTransRefresh.TabIndex = 5;
            this.btnTransRefresh.Text = "Refresh";
            this.btnTransRefresh.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnTransRefresh.UseVisualStyleBackColor = true;
            this.btnTransRefresh.Click += new System.EventHandler(this.btnTransRefresh_Click);
            // 
            // btnTransAutoCompute
            // 
            this.btnTransAutoCompute.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTransAutoCompute.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnTransAutoCompute.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTransAutoCompute.ImageKey = "point_hand.png";
            this.btnTransAutoCompute.ImageList = this.imageList;
            this.btnTransAutoCompute.Location = new System.Drawing.Point(228, 149);
            this.btnTransAutoCompute.Name = "btnTransAutoCompute";
            this.btnTransAutoCompute.Size = new System.Drawing.Size(104, 23);
            this.btnTransAutoCompute.TabIndex = 3;
            this.btnTransAutoCompute.Text = "Auto Compute";
            this.btnTransAutoCompute.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnTransAutoCompute.UseVisualStyleBackColor = true;
            this.btnTransAutoCompute.Click += new System.EventHandler(this.btnTransAutoCompute_Click);
            // 
            // btnTransApply
            // 
            this.btnTransApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTransApply.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnTransApply.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTransApply.ImageKey = "Check";
            this.btnTransApply.ImageList = this.imageList;
            this.btnTransApply.Location = new System.Drawing.Point(428, 149);
            this.btnTransApply.Name = "btnTransApply";
            this.btnTransApply.Size = new System.Drawing.Size(104, 23);
            this.btnTransApply.TabIndex = 4;
            this.btnTransApply.Text = "Apply Changes";
            this.btnTransApply.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnTransApply.UseVisualStyleBackColor = true;
            this.btnTransApply.Click += new System.EventHandler(this.btnTransApply_Click);
            // 
            // txtTransTotalVol
            // 
            this.txtTransTotalVol.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtTransTotalVol.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTransTotalVol.Location = new System.Drawing.Point(115, 121);
            this.txtTransTotalVol.Name = "txtTransTotalVol";
            this.txtTransTotalVol.ReadOnly = true;
            this.txtTransTotalVol.Size = new System.Drawing.Size(89, 20);
            this.txtTransTotalVol.TabIndex = 189;
            this.txtTransTotalVol.Text = "num";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 123);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 13);
            this.label3.TabIndex = 188;
            this.label3.Text = "Total Volume(?):";
            // 
            // dgvTrans
            // 
            this.dgvTrans.AllowUserToAddRows = false;
            this.dgvTrans.AllowUserToDeleteRows = false;
            dataGridViewCellStyle12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.dgvTrans.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle12;
            this.dgvTrans.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvTrans.BackgroundColor = System.Drawing.Color.White;
            this.dgvTrans.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTrans.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.transID,
            this.Column31,
            this.dataGridViewTextBoxColumn1,
            this.Column25,
            this.dataGridViewTextBoxColumn2,
            this.Column29,
            this.dataGridViewTextBoxColumn5});
            this.dgvTrans.EnableHeadersVisualStyles = false;
            this.dgvTrans.Location = new System.Drawing.Point(10, 19);
            this.dgvTrans.MultiSelect = false;
            this.dgvTrans.Name = "dgvTrans";
            this.dgvTrans.Size = new System.Drawing.Size(599, 96);
            this.dgvTrans.TabIndex = 0;
            this.dgvTrans.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvTrans_CellMouseClick);
            this.dgvTrans.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTrans_CellValueChanged);
            // 
            // transID
            // 
            this.transID.HeaderText = "";
            this.transID.Name = "transID";
            this.transID.ReadOnly = true;
            this.transID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.transID.Visible = false;
            // 
            // Column31
            // 
            this.Column31.HeaderText = "Seq.";
            this.Column31.Name = "Column31";
            this.Column31.Width = 50;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Project/Rig/Well/Hole";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.dataGridViewTextBoxColumn1.Width = 150;
            // 
            // Column25
            // 
            this.Column25.HeaderText = "transLocationID";
            this.Column25.Name = "Column25";
            this.Column25.ReadOnly = true;
            this.Column25.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.Column25.Visible = false;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Volume (su)";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // Column29
            // 
            this.Column29.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column29.HeaderText = "Chemicals Conc. & Used";
            this.Column29.Name = "Column29";
            this.Column29.ReadOnly = true;
            this.Column29.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn5.HeaderText = "MW (su)";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // tpDfsBuild
            // 
            this.tpDfsBuild.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tpDfsBuild.Controls.Add(this.tableLayoutPanel2);
            this.tpDfsBuild.Location = new System.Drawing.Point(4, 25);
            this.tpDfsBuild.Name = "tpDfsBuild";
            this.tpDfsBuild.Padding = new System.Windows.Forms.Padding(3);
            this.tpDfsBuild.Size = new System.Drawing.Size(762, 376);
            this.tpDfsBuild.TabIndex = 1;
            this.tpDfsBuild.Text = "Start/Built";
            this.tpDfsBuild.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.grbStartBuiltRainGain, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.groupBox6, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.groupBox10, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(754, 368);
            this.tableLayoutPanel2.TabIndex = 7;
            // 
            // grbStartBuiltRainGain
            // 
            this.grbStartBuiltRainGain.Controls.Add(this.label6);
            this.grbStartBuiltRainGain.Controls.Add(this.txtStartVol);
            this.grbStartBuiltRainGain.Controls.Add(this.txtGainVol);
            this.grbStartBuiltRainGain.Controls.Add(this.btnGeneralStartVol);
            this.grbStartBuiltRainGain.Controls.Add(this.btnGeneralApply);
            this.grbStartBuiltRainGain.Controls.Add(this.label8);
            this.grbStartBuiltRainGain.Controls.Add(this.label10);
            this.grbStartBuiltRainGain.Controls.Add(this.txtRainVol);
            this.grbStartBuiltRainGain.Location = new System.Drawing.Point(3, 3);
            this.grbStartBuiltRainGain.Name = "grbStartBuiltRainGain";
            this.grbStartBuiltRainGain.Size = new System.Drawing.Size(638, 44);
            this.grbStartBuiltRainGain.TabIndex = 6;
            this.grbStartBuiltRainGain.TabStop = false;
            this.grbStartBuiltRainGain.Tag = "General";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 21);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(68, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Start Vol. (?):";
            // 
            // txtStartVol
            // 
            this.txtStartVol.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtStartVol.Location = new System.Drawing.Point(86, 19);
            this.txtStartVol.Name = "txtStartVol";
            this.txtStartVol.Size = new System.Drawing.Size(60, 20);
            this.txtStartVol.TabIndex = 0;
            this.txtStartVol.TextChanged += new System.EventHandler(this.txtBuiltGeneral_TextChanged);
            // 
            // txtGainVol
            // 
            this.txtGainVol.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtGainVol.Location = new System.Drawing.Point(448, 19);
            this.txtGainVol.Name = "txtGainVol";
            this.txtGainVol.Size = new System.Drawing.Size(60, 20);
            this.txtGainVol.TabIndex = 3;
            this.txtGainVol.TextChanged += new System.EventHandler(this.txtBuiltGeneral_TextChanged);
            // 
            // btnGeneralStartVol
            // 
            this.btnGeneralStartVol.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGeneralStartVol.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnGeneralStartVol.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGeneralStartVol.ImageKey = "left";
            this.btnGeneralStartVol.ImageList = this.imageList;
            this.btnGeneralStartVol.Location = new System.Drawing.Point(152, 19);
            this.btnGeneralStartVol.Name = "btnGeneralStartVol";
            this.btnGeneralStartVol.Size = new System.Drawing.Size(34, 20);
            this.btnGeneralStartVol.TabIndex = 1;
            this.btnGeneralStartVol.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnGeneralStartVol.UseVisualStyleBackColor = true;
            this.btnGeneralStartVol.Click += new System.EventHandler(this.btnGeneralStartVol_Click);
            // 
            // btnGeneralApply
            // 
            this.btnGeneralApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGeneralApply.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnGeneralApply.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGeneralApply.ImageKey = "Check";
            this.btnGeneralApply.ImageList = this.imageList;
            this.btnGeneralApply.Location = new System.Drawing.Point(528, 15);
            this.btnGeneralApply.Name = "btnGeneralApply";
            this.btnGeneralApply.Size = new System.Drawing.Size(104, 23);
            this.btnGeneralApply.TabIndex = 4;
            this.btnGeneralApply.Text = "Apply Changes";
            this.btnGeneralApply.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnGeneralApply.UseVisualStyleBackColor = true;
            this.btnGeneralApply.Click += new System.EventHandler(this.btnGeneralApply_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(212, 21);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(68, 13);
            this.label8.TabIndex = 0;
            this.label8.Text = "Rain Vol. (?):";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(369, 21);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(68, 13);
            this.label10.TabIndex = 0;
            this.label10.Text = "Gain Vol. (?):";
            // 
            // txtRainVol
            // 
            this.txtRainVol.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRainVol.Location = new System.Drawing.Point(291, 19);
            this.txtRainVol.Name = "txtRainVol";
            this.txtRainVol.Size = new System.Drawing.Size(60, 20);
            this.txtRainVol.TabIndex = 2;
            this.txtRainVol.TextChanged += new System.EventHandler(this.txtBuiltGeneral_TextChanged);
            // 
            // groupBox6
            // 
            this.groupBox6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox6.Controls.Add(this.btnBuiltUsingOthersAutoCompute);
            this.groupBox6.Controls.Add(this.btnBuiltUsingRemove);
            this.groupBox6.Controls.Add(this.btnBuiltUsingAdd);
            this.groupBox6.Controls.Add(this.btnBuiltUsingRefresh);
            this.groupBox6.Controls.Add(this.btnBuiltUsingApply);
            this.groupBox6.Controls.Add(this.txtBuiltUsingTotalVol);
            this.groupBox6.Controls.Add(this.label11);
            this.groupBox6.Controls.Add(this.dgvBuiltUsing);
            this.groupBox6.Location = new System.Drawing.Point(3, 212);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(748, 153);
            this.groupBox6.TabIndex = 5;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Using other Drilling Fluid Systems";
            // 
            // btnBuiltUsingOthersAutoCompute
            // 
            this.btnBuiltUsingOthersAutoCompute.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBuiltUsingOthersAutoCompute.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnBuiltUsingOthersAutoCompute.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBuiltUsingOthersAutoCompute.ImageKey = "point_hand.png";
            this.btnBuiltUsingOthersAutoCompute.ImageList = this.imageList;
            this.btnBuiltUsingOthersAutoCompute.Location = new System.Drawing.Point(313, 124);
            this.btnBuiltUsingOthersAutoCompute.Name = "btnBuiltUsingOthersAutoCompute";
            this.btnBuiltUsingOthersAutoCompute.Size = new System.Drawing.Size(104, 23);
            this.btnBuiltUsingOthersAutoCompute.TabIndex = 3;
            this.btnBuiltUsingOthersAutoCompute.Text = "Auto Compute";
            this.btnBuiltUsingOthersAutoCompute.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnBuiltUsingOthersAutoCompute.UseVisualStyleBackColor = true;
            this.btnBuiltUsingOthersAutoCompute.Click += new System.EventHandler(this.btnBuiltUsingOthersAutoCompute_Click);
            // 
            // btnBuiltUsingRemove
            // 
            this.btnBuiltUsingRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnBuiltUsingRemove.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnBuiltUsingRemove.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBuiltUsingRemove.ImageKey = "Remove";
            this.btnBuiltUsingRemove.ImageList = this.imageList;
            this.btnBuiltUsingRemove.Location = new System.Drawing.Point(68, 124);
            this.btnBuiltUsingRemove.Name = "btnBuiltUsingRemove";
            this.btnBuiltUsingRemove.Size = new System.Drawing.Size(75, 23);
            this.btnBuiltUsingRemove.TabIndex = 2;
            this.btnBuiltUsingRemove.Text = "Remove";
            this.btnBuiltUsingRemove.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnBuiltUsingRemove.UseVisualStyleBackColor = true;
            this.btnBuiltUsingRemove.Click += new System.EventHandler(this.btnBuiltUsingRemove_Click);
            // 
            // btnBuiltUsingAdd
            // 
            this.btnBuiltUsingAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnBuiltUsingAdd.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnBuiltUsingAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBuiltUsingAdd.ImageKey = "add2";
            this.btnBuiltUsingAdd.ImageList = this.imageList;
            this.btnBuiltUsingAdd.Location = new System.Drawing.Point(10, 124);
            this.btnBuiltUsingAdd.Name = "btnBuiltUsingAdd";
            this.btnBuiltUsingAdd.Size = new System.Drawing.Size(52, 23);
            this.btnBuiltUsingAdd.TabIndex = 1;
            this.btnBuiltUsingAdd.Text = "Add";
            this.btnBuiltUsingAdd.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnBuiltUsingAdd.UseVisualStyleBackColor = true;
            this.btnBuiltUsingAdd.Click += new System.EventHandler(this.btnBuiltUsingAdd_Click);
            // 
            // btnBuiltUsingRefresh
            // 
            this.btnBuiltUsingRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBuiltUsingRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnBuiltUsingRefresh.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBuiltUsingRefresh.ImageKey = "refresh";
            this.btnBuiltUsingRefresh.ImageList = this.imageList;
            this.btnBuiltUsingRefresh.Location = new System.Drawing.Point(671, 124);
            this.btnBuiltUsingRefresh.Name = "btnBuiltUsingRefresh";
            this.btnBuiltUsingRefresh.Size = new System.Drawing.Size(71, 23);
            this.btnBuiltUsingRefresh.TabIndex = 5;
            this.btnBuiltUsingRefresh.Text = "Refresh";
            this.btnBuiltUsingRefresh.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnBuiltUsingRefresh.UseVisualStyleBackColor = true;
            this.btnBuiltUsingRefresh.Click += new System.EventHandler(this.btnBuiltUsingRefresh_Click);
            // 
            // btnBuiltUsingApply
            // 
            this.btnBuiltUsingApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBuiltUsingApply.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnBuiltUsingApply.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBuiltUsingApply.ImageKey = "Check";
            this.btnBuiltUsingApply.ImageList = this.imageList;
            this.btnBuiltUsingApply.Location = new System.Drawing.Point(561, 124);
            this.btnBuiltUsingApply.Name = "btnBuiltUsingApply";
            this.btnBuiltUsingApply.Size = new System.Drawing.Size(104, 23);
            this.btnBuiltUsingApply.TabIndex = 4;
            this.btnBuiltUsingApply.Text = "Apply Changes";
            this.btnBuiltUsingApply.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnBuiltUsingApply.UseVisualStyleBackColor = true;
            this.btnBuiltUsingApply.Click += new System.EventHandler(this.btnBuiltUsingApply_Click);
            // 
            // txtBuiltUsingTotalVol
            // 
            this.txtBuiltUsingTotalVol.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtBuiltUsingTotalVol.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBuiltUsingTotalVol.Location = new System.Drawing.Point(117, 96);
            this.txtBuiltUsingTotalVol.Name = "txtBuiltUsingTotalVol";
            this.txtBuiltUsingTotalVol.ReadOnly = true;
            this.txtBuiltUsingTotalVol.Size = new System.Drawing.Size(89, 20);
            this.txtBuiltUsingTotalVol.TabIndex = 193;
            this.txtBuiltUsingTotalVol.Text = "num";
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(9, 98);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(84, 13);
            this.label11.TabIndex = 192;
            this.label11.Text = "Total Volume(?):";
            // 
            // dgvBuiltUsing
            // 
            this.dgvBuiltUsing.AllowUserToAddRows = false;
            this.dgvBuiltUsing.AllowUserToDeleteRows = false;
            dataGridViewCellStyle13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.dgvBuiltUsing.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle13;
            this.dgvBuiltUsing.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvBuiltUsing.BackgroundColor = System.Drawing.Color.White;
            this.dgvBuiltUsing.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBuiltUsing.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.builtUsingID,
            this.Column33,
            this.Column23,
            this.dataGridViewTextBoxColumn8,
            this.dataGridViewTextBoxColumn11,
            this.dataGridViewTextBoxColumn12,
            this.dataGridViewTextBoxColumn17});
            this.dgvBuiltUsing.EnableHeadersVisualStyles = false;
            this.dgvBuiltUsing.Location = new System.Drawing.Point(8, 19);
            this.dgvBuiltUsing.MultiSelect = false;
            this.dgvBuiltUsing.Name = "dgvBuiltUsing";
            this.dgvBuiltUsing.Size = new System.Drawing.Size(734, 69);
            this.dgvBuiltUsing.TabIndex = 0;
            this.dgvBuiltUsing.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvBuiltUsing_CellMouseClick);
            this.dgvBuiltUsing.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvBuiltUsing_CellValueChanged);
            // 
            // builtUsingID
            // 
            this.builtUsingID.HeaderText = "";
            this.builtUsingID.Name = "builtUsingID";
            this.builtUsingID.ReadOnly = true;
            this.builtUsingID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.builtUsingID.Visible = false;
            // 
            // Column33
            // 
            this.Column33.HeaderText = "Seq.";
            this.Column33.Name = "Column33";
            this.Column33.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.Column33.Width = 50;
            // 
            // Column23
            // 
            this.Column23.HeaderText = "DfsID_BU";
            this.Column23.Name = "Column23";
            this.Column23.ReadOnly = true;
            this.Column23.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.Column23.Visible = false;
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.HeaderText = "Drilling Fluid System";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.ReadOnly = true;
            this.dataGridViewTextBoxColumn8.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.dataGridViewTextBoxColumn8.Width = 200;
            // 
            // dataGridViewTextBoxColumn11
            // 
            this.dataGridViewTextBoxColumn11.HeaderText = "Used Vol. (?)";
            this.dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
            this.dataGridViewTextBoxColumn11.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // dataGridViewTextBoxColumn12
            // 
            this.dataGridViewTextBoxColumn12.HeaderText = "Chemicals Consumption & Vol.";
            this.dataGridViewTextBoxColumn12.Name = "dataGridViewTextBoxColumn12";
            this.dataGridViewTextBoxColumn12.ReadOnly = true;
            this.dataGridViewTextBoxColumn12.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.dataGridViewTextBoxColumn12.Width = 250;
            // 
            // dataGridViewTextBoxColumn17
            // 
            this.dataGridViewTextBoxColumn17.HeaderText = "MW (su)";
            this.dataGridViewTextBoxColumn17.Name = "dataGridViewTextBoxColumn17";
            this.dataGridViewTextBoxColumn17.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // groupBox10
            // 
            this.groupBox10.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox10.Controls.Add(this.btnBuiltVolRemove);
            this.groupBox10.Controls.Add(this.btnBuiltVolAdd);
            this.groupBox10.Controls.Add(this.btnBuiltVolRefresh);
            this.groupBox10.Controls.Add(this.btnBuiltVolApply);
            this.groupBox10.Controls.Add(this.txtBuiltVolTotalVol);
            this.groupBox10.Controls.Add(this.label4);
            this.groupBox10.Controls.Add(this.dgvBuiltVol);
            this.groupBox10.Location = new System.Drawing.Point(3, 53);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new System.Drawing.Size(657, 153);
            this.groupBox10.TabIndex = 5;
            this.groupBox10.TabStop = false;
            this.groupBox10.Text = "Built Volume";
            // 
            // btnBuiltVolRemove
            // 
            this.btnBuiltVolRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnBuiltVolRemove.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnBuiltVolRemove.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBuiltVolRemove.ImageKey = "Remove";
            this.btnBuiltVolRemove.ImageList = this.imageList;
            this.btnBuiltVolRemove.Location = new System.Drawing.Point(66, 124);
            this.btnBuiltVolRemove.Name = "btnBuiltVolRemove";
            this.btnBuiltVolRemove.Size = new System.Drawing.Size(75, 23);
            this.btnBuiltVolRemove.TabIndex = 2;
            this.btnBuiltVolRemove.Text = "Remove";
            this.btnBuiltVolRemove.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnBuiltVolRemove.UseVisualStyleBackColor = true;
            this.btnBuiltVolRemove.Click += new System.EventHandler(this.btnBuiltVolRemove_Click);
            // 
            // btnBuiltVolAdd
            // 
            this.btnBuiltVolAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnBuiltVolAdd.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnBuiltVolAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBuiltVolAdd.ImageKey = "add2";
            this.btnBuiltVolAdd.ImageList = this.imageList;
            this.btnBuiltVolAdd.Location = new System.Drawing.Point(8, 124);
            this.btnBuiltVolAdd.Name = "btnBuiltVolAdd";
            this.btnBuiltVolAdd.Size = new System.Drawing.Size(52, 23);
            this.btnBuiltVolAdd.TabIndex = 1;
            this.btnBuiltVolAdd.Text = "Add";
            this.btnBuiltVolAdd.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnBuiltVolAdd.UseVisualStyleBackColor = true;
            this.btnBuiltVolAdd.Click += new System.EventHandler(this.btnBuiltVolAdd_Click);
            // 
            // btnBuiltVolRefresh
            // 
            this.btnBuiltVolRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBuiltVolRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnBuiltVolRefresh.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBuiltVolRefresh.ImageKey = "refresh";
            this.btnBuiltVolRefresh.ImageList = this.imageList;
            this.btnBuiltVolRefresh.Location = new System.Drawing.Point(580, 124);
            this.btnBuiltVolRefresh.Name = "btnBuiltVolRefresh";
            this.btnBuiltVolRefresh.Size = new System.Drawing.Size(71, 23);
            this.btnBuiltVolRefresh.TabIndex = 4;
            this.btnBuiltVolRefresh.Text = "Refresh";
            this.btnBuiltVolRefresh.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnBuiltVolRefresh.UseVisualStyleBackColor = true;
            this.btnBuiltVolRefresh.Click += new System.EventHandler(this.btnBuiltVolRefresh_Click);
            // 
            // btnBuiltVolApply
            // 
            this.btnBuiltVolApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBuiltVolApply.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnBuiltVolApply.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBuiltVolApply.ImageKey = "Check";
            this.btnBuiltVolApply.ImageList = this.imageList;
            this.btnBuiltVolApply.Location = new System.Drawing.Point(470, 124);
            this.btnBuiltVolApply.Name = "btnBuiltVolApply";
            this.btnBuiltVolApply.Size = new System.Drawing.Size(104, 23);
            this.btnBuiltVolApply.TabIndex = 3;
            this.btnBuiltVolApply.Text = "Apply Changes";
            this.btnBuiltVolApply.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnBuiltVolApply.UseVisualStyleBackColor = true;
            this.btnBuiltVolApply.Click += new System.EventHandler(this.btnBuiltVolApply_Click);
            // 
            // txtBuiltVolTotalVol
            // 
            this.txtBuiltVolTotalVol.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtBuiltVolTotalVol.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBuiltVolTotalVol.Location = new System.Drawing.Point(115, 96);
            this.txtBuiltVolTotalVol.Name = "txtBuiltVolTotalVol";
            this.txtBuiltVolTotalVol.ReadOnly = true;
            this.txtBuiltVolTotalVol.Size = new System.Drawing.Size(89, 20);
            this.txtBuiltVolTotalVol.TabIndex = 193;
            this.txtBuiltVolTotalVol.Text = "num";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 98);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 13);
            this.label4.TabIndex = 192;
            this.label4.Text = "Total Volume(?):";
            // 
            // dgvBuiltVol
            // 
            this.dgvBuiltVol.AllowUserToAddRows = false;
            this.dgvBuiltVol.AllowUserToDeleteRows = false;
            dataGridViewCellStyle14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.dgvBuiltVol.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle14;
            this.dgvBuiltVol.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvBuiltVol.BackgroundColor = System.Drawing.Color.White;
            this.dgvBuiltVol.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBuiltVol.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.builtVolID,
            this.Column32,
            this.dataGridViewTextBoxColumn19,
            this.dataGridViewTextBoxColumn20,
            this.Column10,
            this.Column11,
            this.Column12,
            this.Column13,
            this.dataGridViewTextBoxColumn21,
            this.dataGridViewTextBoxColumn22,
            this.dataGridViewTextBoxColumn23});
            this.dgvBuiltVol.EnableHeadersVisualStyles = false;
            this.dgvBuiltVol.Location = new System.Drawing.Point(8, 19);
            this.dgvBuiltVol.MultiSelect = false;
            this.dgvBuiltVol.Name = "dgvBuiltVol";
            this.dgvBuiltVol.Size = new System.Drawing.Size(643, 69);
            this.dgvBuiltVol.TabIndex = 0;
            this.dgvBuiltVol.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvBuiltVol_CellMouseClick);
            this.dgvBuiltVol.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvBuiltVol_CellValueChanged);
            // 
            // builtVolID
            // 
            this.builtVolID.HeaderText = "";
            this.builtVolID.Name = "builtVolID";
            this.builtVolID.ReadOnly = true;
            this.builtVolID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.builtVolID.Visible = false;
            // 
            // Column32
            // 
            this.Column32.HeaderText = "Seq.";
            this.Column32.Name = "Column32";
            this.Column32.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.Column32.Width = 50;
            // 
            // dataGridViewTextBoxColumn19
            // 
            this.dataGridViewTextBoxColumn19.HeaderText = "Chemicals Consumption & Vol.";
            this.dataGridViewTextBoxColumn19.Name = "dataGridViewTextBoxColumn19";
            this.dataGridViewTextBoxColumn19.ReadOnly = true;
            this.dataGridViewTextBoxColumn19.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.dataGridViewTextBoxColumn19.Width = 150;
            // 
            // dataGridViewTextBoxColumn20
            // 
            this.dataGridViewTextBoxColumn20.HeaderText = "Added Water (?)";
            this.dataGridViewTextBoxColumn20.Name = "dataGridViewTextBoxColumn20";
            this.dataGridViewTextBoxColumn20.ReadOnly = true;
            this.dataGridViewTextBoxColumn20.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // Column10
            // 
            this.Column10.HeaderText = "sea";
            this.Column10.Name = "Column10";
            this.Column10.ReadOnly = true;
            this.Column10.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.Column10.Visible = false;
            // 
            // Column11
            // 
            this.Column11.HeaderText = "drill";
            this.Column11.Name = "Column11";
            this.Column11.ReadOnly = true;
            this.Column11.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.Column11.Visible = false;
            // 
            // Column12
            // 
            this.Column12.HeaderText = "de";
            this.Column12.Name = "Column12";
            this.Column12.ReadOnly = true;
            this.Column12.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.Column12.Visible = false;
            // 
            // Column13
            // 
            this.Column13.HeaderText = "local";
            this.Column13.Name = "Column13";
            this.Column13.ReadOnly = true;
            this.Column13.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.Column13.Visible = false;
            // 
            // dataGridViewTextBoxColumn21
            // 
            this.dataGridViewTextBoxColumn21.HeaderText = "Added Oil (?)";
            this.dataGridViewTextBoxColumn21.Name = "dataGridViewTextBoxColumn21";
            this.dataGridViewTextBoxColumn21.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // dataGridViewTextBoxColumn22
            // 
            this.dataGridViewTextBoxColumn22.HeaderText = "MW (su)";
            this.dataGridViewTextBoxColumn22.Name = "dataGridViewTextBoxColumn22";
            this.dataGridViewTextBoxColumn22.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // dataGridViewTextBoxColumn23
            // 
            this.dataGridViewTextBoxColumn23.HeaderText = "Volume (su)";
            this.dataGridViewTextBoxColumn23.Name = "dataGridViewTextBoxColumn23";
            this.dataGridViewTextBoxColumn23.ReadOnly = true;
            this.dataGridViewTextBoxColumn23.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // tpDfsTreated
            // 
            this.tpDfsTreated.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tpDfsTreated.Controls.Add(this.groupBox12);
            this.tpDfsTreated.Location = new System.Drawing.Point(4, 25);
            this.tpDfsTreated.Name = "tpDfsTreated";
            this.tpDfsTreated.Size = new System.Drawing.Size(762, 376);
            this.tpDfsTreated.TabIndex = 5;
            this.tpDfsTreated.Text = "Treated";
            this.tpDfsTreated.UseVisualStyleBackColor = true;
            // 
            // groupBox12
            // 
            this.groupBox12.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox12.Controls.Add(this.btnTreatRemove);
            this.groupBox12.Controls.Add(this.btnTreatAdd);
            this.groupBox12.Controls.Add(this.btnTreatRefresh);
            this.groupBox12.Controls.Add(this.btnTreatApply);
            this.groupBox12.Controls.Add(this.txtTreatTotalVol);
            this.groupBox12.Controls.Add(this.label12);
            this.groupBox12.Controls.Add(this.dgvTreat);
            this.groupBox12.Location = new System.Drawing.Point(3, 3);
            this.groupBox12.Name = "groupBox12";
            this.groupBox12.Size = new System.Drawing.Size(754, 368);
            this.groupBox12.TabIndex = 7;
            this.groupBox12.TabStop = false;
            this.groupBox12.Text = "Treated Volume";
            // 
            // btnTreatRemove
            // 
            this.btnTreatRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnTreatRemove.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnTreatRemove.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTreatRemove.ImageKey = "Remove";
            this.btnTreatRemove.ImageList = this.imageList;
            this.btnTreatRemove.Location = new System.Drawing.Point(66, 339);
            this.btnTreatRemove.Name = "btnTreatRemove";
            this.btnTreatRemove.Size = new System.Drawing.Size(75, 23);
            this.btnTreatRemove.TabIndex = 2;
            this.btnTreatRemove.Text = "Remove";
            this.btnTreatRemove.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnTreatRemove.UseVisualStyleBackColor = true;
            this.btnTreatRemove.Click += new System.EventHandler(this.btnTreatRemove_Click);
            // 
            // btnTreatAdd
            // 
            this.btnTreatAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnTreatAdd.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnTreatAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTreatAdd.ImageKey = "add2";
            this.btnTreatAdd.ImageList = this.imageList;
            this.btnTreatAdd.Location = new System.Drawing.Point(8, 339);
            this.btnTreatAdd.Name = "btnTreatAdd";
            this.btnTreatAdd.Size = new System.Drawing.Size(52, 23);
            this.btnTreatAdd.TabIndex = 1;
            this.btnTreatAdd.Text = "Add";
            this.btnTreatAdd.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnTreatAdd.UseVisualStyleBackColor = true;
            this.btnTreatAdd.Click += new System.EventHandler(this.btnTreatAdd_Click);
            // 
            // btnTreatRefresh
            // 
            this.btnTreatRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTreatRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnTreatRefresh.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTreatRefresh.ImageKey = "refresh";
            this.btnTreatRefresh.ImageList = this.imageList;
            this.btnTreatRefresh.Location = new System.Drawing.Point(677, 339);
            this.btnTreatRefresh.Name = "btnTreatRefresh";
            this.btnTreatRefresh.Size = new System.Drawing.Size(71, 23);
            this.btnTreatRefresh.TabIndex = 4;
            this.btnTreatRefresh.Text = "Refresh";
            this.btnTreatRefresh.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnTreatRefresh.UseVisualStyleBackColor = true;
            this.btnTreatRefresh.Click += new System.EventHandler(this.btnTreatRefresh_Click);
            // 
            // btnTreatApply
            // 
            this.btnTreatApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTreatApply.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnTreatApply.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTreatApply.ImageKey = "Check";
            this.btnTreatApply.ImageList = this.imageList;
            this.btnTreatApply.Location = new System.Drawing.Point(567, 339);
            this.btnTreatApply.Name = "btnTreatApply";
            this.btnTreatApply.Size = new System.Drawing.Size(104, 23);
            this.btnTreatApply.TabIndex = 3;
            this.btnTreatApply.Text = "Apply Changes";
            this.btnTreatApply.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnTreatApply.UseVisualStyleBackColor = true;
            this.btnTreatApply.Click += new System.EventHandler(this.btnTreatApply_Click);
            // 
            // txtTreatTotalVol
            // 
            this.txtTreatTotalVol.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtTreatTotalVol.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTreatTotalVol.Location = new System.Drawing.Point(115, 311);
            this.txtTreatTotalVol.Name = "txtTreatTotalVol";
            this.txtTreatTotalVol.ReadOnly = true;
            this.txtTreatTotalVol.Size = new System.Drawing.Size(89, 20);
            this.txtTreatTotalVol.TabIndex = 199;
            this.txtTreatTotalVol.Text = "num";
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(7, 313);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(84, 13);
            this.label12.TabIndex = 198;
            this.label12.Text = "Total Volume(?):";
            // 
            // dgvTreat
            // 
            this.dgvTreat.AllowUserToAddRows = false;
            this.dgvTreat.AllowUserToDeleteRows = false;
            dataGridViewCellStyle15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.dgvTreat.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle15;
            this.dgvTreat.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvTreat.BackgroundColor = System.Drawing.Color.White;
            this.dgvTreat.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTreat.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.treatedID,
            this.Column34,
            this.dataGridViewTextBoxColumn31,
            this.dataGridViewTextBoxColumn32,
            this.dataGridViewTextBoxColumn33,
            this.Column18,
            this.Column19,
            this.Column20,
            this.Column21,
            this.dataGridViewTextBoxColumn34,
            this.dataGridViewTextBoxColumn35,
            this.dataGridViewTextBoxColumn36,
            this.dataGridViewTextBoxColumn37,
            this.dataGridViewTextBoxColumn38});
            this.dgvTreat.EnableHeadersVisualStyles = false;
            this.dgvTreat.Location = new System.Drawing.Point(8, 19);
            this.dgvTreat.MultiSelect = false;
            this.dgvTreat.Name = "dgvTreat";
            this.dgvTreat.Size = new System.Drawing.Size(740, 284);
            this.dgvTreat.TabIndex = 0;
            this.dgvTreat.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvTreat_CellMouseClick);
            this.dgvTreat.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTreat_CellValueChanged);
            // 
            // treatedID
            // 
            this.treatedID.HeaderText = "";
            this.treatedID.Name = "treatedID";
            this.treatedID.ReadOnly = true;
            this.treatedID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.treatedID.Visible = false;
            // 
            // Column34
            // 
            this.Column34.HeaderText = "Seq.";
            this.Column34.Name = "Column34";
            this.Column34.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.Column34.Width = 50;
            // 
            // dataGridViewTextBoxColumn31
            // 
            this.dataGridViewTextBoxColumn31.HeaderText = "Volume for Treatment (?)";
            this.dataGridViewTextBoxColumn31.Name = "dataGridViewTextBoxColumn31";
            this.dataGridViewTextBoxColumn31.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // dataGridViewTextBoxColumn32
            // 
            this.dataGridViewTextBoxColumn32.HeaderText = "Chemicals Consumption & Vol.";
            this.dataGridViewTextBoxColumn32.Name = "dataGridViewTextBoxColumn32";
            this.dataGridViewTextBoxColumn32.ReadOnly = true;
            this.dataGridViewTextBoxColumn32.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.dataGridViewTextBoxColumn32.Width = 150;
            // 
            // dataGridViewTextBoxColumn33
            // 
            this.dataGridViewTextBoxColumn33.HeaderText = "Added Water (?)";
            this.dataGridViewTextBoxColumn33.Name = "dataGridViewTextBoxColumn33";
            this.dataGridViewTextBoxColumn33.ReadOnly = true;
            this.dataGridViewTextBoxColumn33.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // Column18
            // 
            this.Column18.HeaderText = "sea";
            this.Column18.Name = "Column18";
            this.Column18.ReadOnly = true;
            this.Column18.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.Column18.Visible = false;
            // 
            // Column19
            // 
            this.Column19.HeaderText = "drill";
            this.Column19.Name = "Column19";
            this.Column19.ReadOnly = true;
            this.Column19.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.Column19.Visible = false;
            // 
            // Column20
            // 
            this.Column20.HeaderText = "de";
            this.Column20.Name = "Column20";
            this.Column20.ReadOnly = true;
            this.Column20.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.Column20.Visible = false;
            // 
            // Column21
            // 
            this.Column21.HeaderText = "local";
            this.Column21.Name = "Column21";
            this.Column21.ReadOnly = true;
            this.Column21.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.Column21.Visible = false;
            // 
            // dataGridViewTextBoxColumn34
            // 
            this.dataGridViewTextBoxColumn34.HeaderText = "Added Oil (?)";
            this.dataGridViewTextBoxColumn34.Name = "dataGridViewTextBoxColumn34";
            this.dataGridViewTextBoxColumn34.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // dataGridViewTextBoxColumn35
            // 
            this.dataGridViewTextBoxColumn35.HeaderText = "MW (su)";
            this.dataGridViewTextBoxColumn35.Name = "dataGridViewTextBoxColumn35";
            this.dataGridViewTextBoxColumn35.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // dataGridViewTextBoxColumn36
            // 
            this.dataGridViewTextBoxColumn36.HeaderText = "Added Volume (su)";
            this.dataGridViewTextBoxColumn36.Name = "dataGridViewTextBoxColumn36";
            this.dataGridViewTextBoxColumn36.ReadOnly = true;
            this.dataGridViewTextBoxColumn36.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // dataGridViewTextBoxColumn37
            // 
            this.dataGridViewTextBoxColumn37.HeaderText = "Final Volume (su)";
            this.dataGridViewTextBoxColumn37.Name = "dataGridViewTextBoxColumn37";
            this.dataGridViewTextBoxColumn37.ReadOnly = true;
            this.dataGridViewTextBoxColumn37.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // dataGridViewTextBoxColumn38
            // 
            this.dataGridViewTextBoxColumn38.HeaderText = "Reason Of Treatment";
            this.dataGridViewTextBoxColumn38.Name = "dataGridViewTextBoxColumn38";
            this.dataGridViewTextBoxColumn38.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // tpDfsDisp
            // 
            this.tpDfsDisp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tpDfsDisp.Controls.Add(this.btnDispRetApply);
            this.tpDfsDisp.Controls.Add(this.grbDipRetDaily);
            this.tpDfsDisp.Controls.Add(this.grbDipRetAtTheEnd);
            this.tpDfsDisp.Location = new System.Drawing.Point(4, 25);
            this.tpDfsDisp.Name = "tpDfsDisp";
            this.tpDfsDisp.Size = new System.Drawing.Size(762, 376);
            this.tpDfsDisp.TabIndex = 3;
            this.tpDfsDisp.Text = "Disp./Ret.";
            this.tpDfsDisp.UseVisualStyleBackColor = true;
            // 
            // btnDispRetApply
            // 
            this.btnDispRetApply.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDispRetApply.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDispRetApply.ImageKey = "Check";
            this.btnDispRetApply.ImageList = this.imageList;
            this.btnDispRetApply.Location = new System.Drawing.Point(594, 224);
            this.btnDispRetApply.Name = "btnDispRetApply";
            this.btnDispRetApply.Size = new System.Drawing.Size(104, 23);
            this.btnDispRetApply.TabIndex = 0;
            this.btnDispRetApply.Text = "Apply Changes";
            this.btnDispRetApply.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDispRetApply.UseVisualStyleBackColor = true;
            this.btnDispRetApply.Click += new System.EventHandler(this.btnDispRetApply_Click);
            // 
            // grbDipRetDaily
            // 
            this.grbDipRetDaily.Controls.Add(this.label24);
            this.grbDipRetDaily.Controls.Add(this.txtDailyStringDisp);
            this.grbDipRetDaily.Controls.Add(this.groupBox13);
            this.grbDipRetDaily.Location = new System.Drawing.Point(3, 6);
            this.grbDipRetDaily.Name = "grbDipRetDaily";
            this.grbDipRetDaily.Size = new System.Drawing.Size(249, 212);
            this.grbDipRetDaily.TabIndex = 10;
            this.grbDipRetDaily.TabStop = false;
            this.grbDipRetDaily.Text = "Daily";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(6, 182);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(82, 13);
            this.label24.TabIndex = 0;
            this.label24.Text = "String Disp. Vol.";
            // 
            // txtDailyStringDisp
            // 
            this.txtDailyStringDisp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDailyStringDisp.Location = new System.Drawing.Point(94, 179);
            this.txtDailyStringDisp.Name = "txtDailyStringDisp";
            this.txtDailyStringDisp.Size = new System.Drawing.Size(60, 20);
            this.txtDailyStringDisp.TabIndex = 0;
            this.txtDailyStringDisp.TextChanged += new System.EventHandler(this.txtDispRet_TextChanged);
            // 
            // groupBox13
            // 
            this.groupBox13.Controls.Add(this.label25);
            this.groupBox13.Controls.Add(this.label15);
            this.groupBox13.Controls.Add(this.label27);
            this.groupBox13.Controls.Add(this.txtDailyOver);
            this.groupBox13.Controls.Add(this.txtDailyWaste);
            this.groupBox13.Controls.Add(this.txtDailyPitMW);
            this.groupBox13.Controls.Add(this.txtDailyPitVol);
            this.groupBox13.Controls.Add(this.label16);
            this.groupBox13.Controls.Add(this.label29);
            this.groupBox13.Location = new System.Drawing.Point(6, 26);
            this.groupBox13.Name = "groupBox13";
            this.groupBox13.Size = new System.Drawing.Size(230, 140);
            this.groupBox13.TabIndex = 9;
            this.groupBox13.TabStop = false;
            this.groupBox13.Text = "Mud Return Volume\t";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(18, 111);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(60, 13);
            this.label25.TabIndex = 0;
            this.label25.Text = "Overboard:";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(18, 86);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(56, 13);
            this.label15.TabIndex = 0;
            this.label15.Text = "Waste Pit:";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(18, 60);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(27, 13);
            this.label27.TabIndex = 0;
            this.label27.Text = "Pits:";
            // 
            // txtDailyOver
            // 
            this.txtDailyOver.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDailyOver.Location = new System.Drawing.Point(84, 109);
            this.txtDailyOver.Name = "txtDailyOver";
            this.txtDailyOver.Size = new System.Drawing.Size(60, 20);
            this.txtDailyOver.TabIndex = 2;
            this.txtDailyOver.TextChanged += new System.EventHandler(this.txtDispRet_TextChanged);
            // 
            // txtDailyWaste
            // 
            this.txtDailyWaste.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDailyWaste.Location = new System.Drawing.Point(84, 83);
            this.txtDailyWaste.Name = "txtDailyWaste";
            this.txtDailyWaste.Size = new System.Drawing.Size(60, 20);
            this.txtDailyWaste.TabIndex = 1;
            this.txtDailyWaste.TextChanged += new System.EventHandler(this.txtDispRet_TextChanged);
            // 
            // txtDailyPitMW
            // 
            this.txtDailyPitMW.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDailyPitMW.Location = new System.Drawing.Point(157, 57);
            this.txtDailyPitMW.Name = "txtDailyPitMW";
            this.txtDailyPitMW.Size = new System.Drawing.Size(60, 20);
            this.txtDailyPitMW.TabIndex = 3;
            this.txtDailyPitMW.TextChanged += new System.EventHandler(this.txtDispRet_TextChanged);
            // 
            // txtDailyPitVol
            // 
            this.txtDailyPitVol.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDailyPitVol.Location = new System.Drawing.Point(84, 57);
            this.txtDailyPitVol.Name = "txtDailyPitVol";
            this.txtDailyPitVol.Size = new System.Drawing.Size(60, 20);
            this.txtDailyPitVol.TabIndex = 0;
            this.txtDailyPitVol.TextChanged += new System.EventHandler(this.txtDispRet_TextChanged);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(165, 32);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(47, 13);
            this.label16.TabIndex = 0;
            this.label16.Text = "MW (su)";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(81, 32);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(45, 13);
            this.label29.TabIndex = 0;
            this.label29.Text = "Vol. (su)";
            // 
            // grbDipRetAtTheEnd
            // 
            this.grbDipRetAtTheEnd.Controls.Add(this.groupBox14);
            this.grbDipRetAtTheEnd.Controls.Add(this.groupBox15);
            this.grbDipRetAtTheEnd.Location = new System.Drawing.Point(258, 6);
            this.grbDipRetAtTheEnd.Name = "grbDipRetAtTheEnd";
            this.grbDipRetAtTheEnd.Size = new System.Drawing.Size(457, 212);
            this.grbDipRetAtTheEnd.TabIndex = 10;
            this.grbDipRetAtTheEnd.TabStop = false;
            this.grbDipRetAtTheEnd.Text = "At The End of The Hole";
            // 
            // groupBox14
            // 
            this.groupBox14.Controls.Add(this.label18);
            this.groupBox14.Controls.Add(this.label20);
            this.groupBox14.Controls.Add(this.label21);
            this.groupBox14.Controls.Add(this.label22);
            this.groupBox14.Controls.Add(this.label23);
            this.groupBox14.Controls.Add(this.txtAtEndMW);
            this.groupBox14.Controls.Add(this.txtAtEndVol);
            this.groupBox14.Controls.Add(this.txtAtEndMaxFR);
            this.groupBox14.Controls.Add(this.txtAtEndMinFR);
            this.groupBox14.Controls.Add(this.txtAtEndDepth);
            this.groupBox14.Location = new System.Drawing.Point(242, 26);
            this.groupBox14.Name = "groupBox14";
            this.groupBox14.Size = new System.Drawing.Size(209, 161);
            this.groupBox14.TabIndex = 8;
            this.groupBox14.TabStop = false;
            this.groupBox14.Text = "Mud Displacement Volume";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(10, 132);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(45, 13);
            this.label18.TabIndex = 0;
            this.label18.Text = "MW (?):";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(10, 106);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(60, 13);
            this.label20.TabIndex = 0;
            this.label20.Text = "Volume (?):";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(10, 77);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(96, 13);
            this.label21.TabIndex = 0;
            this.label21.Text = "Max Flow Rate (?):";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(10, 51);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(93, 13);
            this.label22.TabIndex = 0;
            this.label22.Text = "Min Flow Rate (?):";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(10, 25);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(54, 13);
            this.label23.TabIndex = 0;
            this.label23.Text = "Depth (?):";
            // 
            // txtAtEndMW
            // 
            this.txtAtEndMW.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAtEndMW.Location = new System.Drawing.Point(143, 132);
            this.txtAtEndMW.Name = "txtAtEndMW";
            this.txtAtEndMW.Size = new System.Drawing.Size(60, 20);
            this.txtAtEndMW.TabIndex = 4;
            this.txtAtEndMW.TextChanged += new System.EventHandler(this.txtDispRet_TextChanged);
            // 
            // txtAtEndVol
            // 
            this.txtAtEndVol.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAtEndVol.Location = new System.Drawing.Point(143, 106);
            this.txtAtEndVol.Name = "txtAtEndVol";
            this.txtAtEndVol.Size = new System.Drawing.Size(60, 20);
            this.txtAtEndVol.TabIndex = 3;
            this.txtAtEndVol.TextChanged += new System.EventHandler(this.txtDispRet_TextChanged);
            // 
            // txtAtEndMaxFR
            // 
            this.txtAtEndMaxFR.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAtEndMaxFR.Location = new System.Drawing.Point(143, 77);
            this.txtAtEndMaxFR.Name = "txtAtEndMaxFR";
            this.txtAtEndMaxFR.Size = new System.Drawing.Size(60, 20);
            this.txtAtEndMaxFR.TabIndex = 2;
            this.txtAtEndMaxFR.TextChanged += new System.EventHandler(this.txtDispRet_TextChanged);
            // 
            // txtAtEndMinFR
            // 
            this.txtAtEndMinFR.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAtEndMinFR.Location = new System.Drawing.Point(143, 51);
            this.txtAtEndMinFR.Name = "txtAtEndMinFR";
            this.txtAtEndMinFR.Size = new System.Drawing.Size(60, 20);
            this.txtAtEndMinFR.TabIndex = 1;
            this.txtAtEndMinFR.TextChanged += new System.EventHandler(this.txtDispRet_TextChanged);
            // 
            // txtAtEndDepth
            // 
            this.txtAtEndDepth.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAtEndDepth.Location = new System.Drawing.Point(143, 25);
            this.txtAtEndDepth.Name = "txtAtEndDepth";
            this.txtAtEndDepth.Size = new System.Drawing.Size(60, 20);
            this.txtAtEndDepth.TabIndex = 0;
            this.txtAtEndDepth.TextChanged += new System.EventHandler(this.txtDispRet_TextChanged);
            // 
            // groupBox15
            // 
            this.groupBox15.Controls.Add(this.label31);
            this.groupBox15.Controls.Add(this.label32);
            this.groupBox15.Controls.Add(this.label33);
            this.groupBox15.Controls.Add(this.txtAtEndOver);
            this.groupBox15.Controls.Add(this.txtAtEndWaste);
            this.groupBox15.Controls.Add(this.txtAtEndPitMW);
            this.groupBox15.Controls.Add(this.txtAtEndPitVol);
            this.groupBox15.Controls.Add(this.label34);
            this.groupBox15.Controls.Add(this.label35);
            this.groupBox15.Location = new System.Drawing.Point(6, 26);
            this.groupBox15.Name = "groupBox15";
            this.groupBox15.Size = new System.Drawing.Size(230, 161);
            this.groupBox15.TabIndex = 8;
            this.groupBox15.TabStop = false;
            this.groupBox15.Text = "Mud Return Volume\t";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(18, 111);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(60, 13);
            this.label31.TabIndex = 0;
            this.label31.Text = "Overboard:";
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(18, 85);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(56, 13);
            this.label32.TabIndex = 0;
            this.label32.Text = "Waste Pit:";
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(18, 60);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(27, 13);
            this.label33.TabIndex = 0;
            this.label33.Text = "Pits:";
            // 
            // txtAtEndOver
            // 
            this.txtAtEndOver.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAtEndOver.Location = new System.Drawing.Point(84, 109);
            this.txtAtEndOver.Name = "txtAtEndOver";
            this.txtAtEndOver.Size = new System.Drawing.Size(60, 20);
            this.txtAtEndOver.TabIndex = 2;
            this.txtAtEndOver.TextChanged += new System.EventHandler(this.txtDispRet_TextChanged);
            // 
            // txtAtEndWaste
            // 
            this.txtAtEndWaste.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAtEndWaste.Location = new System.Drawing.Point(84, 83);
            this.txtAtEndWaste.Name = "txtAtEndWaste";
            this.txtAtEndWaste.Size = new System.Drawing.Size(60, 20);
            this.txtAtEndWaste.TabIndex = 1;
            this.txtAtEndWaste.TextChanged += new System.EventHandler(this.txtDispRet_TextChanged);
            // 
            // txtAtEndPitMW
            // 
            this.txtAtEndPitMW.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAtEndPitMW.Location = new System.Drawing.Point(157, 57);
            this.txtAtEndPitMW.Name = "txtAtEndPitMW";
            this.txtAtEndPitMW.Size = new System.Drawing.Size(60, 20);
            this.txtAtEndPitMW.TabIndex = 3;
            this.txtAtEndPitMW.TextChanged += new System.EventHandler(this.txtDispRet_TextChanged);
            // 
            // txtAtEndPitVol
            // 
            this.txtAtEndPitVol.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAtEndPitVol.Location = new System.Drawing.Point(84, 57);
            this.txtAtEndPitVol.Name = "txtAtEndPitVol";
            this.txtAtEndPitVol.Size = new System.Drawing.Size(60, 20);
            this.txtAtEndPitVol.TabIndex = 0;
            this.txtAtEndPitVol.TextChanged += new System.EventHandler(this.txtDispRet_TextChanged);
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(165, 32);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(47, 13);
            this.label34.TabIndex = 0;
            this.label34.Text = "MW (su)";
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Location = new System.Drawing.Point(81, 32);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(45, 13);
            this.label35.TabIndex = 0;
            this.label35.Text = "Vol. (su)";
            // 
            // trvDFSs
            // 
            this.trvDFSs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.trvDFSs.Location = new System.Drawing.Point(6, 6);
            this.trvDFSs.Name = "trvDFSs";
            treeNode4.Name = "Node1";
            treeNode4.Text = "dfs1";
            treeNode5.Name = "Node2";
            treeNode5.Text = "dfs2";
            treeNode6.Name = "Node0";
            treeNode6.Text = "Drilling Fluid Systems";
            this.trvDFSs.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode6});
            this.trvDFSs.Size = new System.Drawing.Size(178, 394);
            this.trvDFSs.TabIndex = 0;
            this.trvDFSs.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.trvDFSs_NodeMouseClick);
            this.trvDFSs.MouseDown += new System.Windows.Forms.MouseEventHandler(this.trvDFSs_MouseDown);
            // 
            // tpPit
            // 
            this.tpPit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tpPit.Controls.Add(this.tableLayoutPanel3);
            this.tpPit.ImageIndex = 0;
            this.tpPit.Location = new System.Drawing.Point(4, 25);
            this.tpPit.Name = "tpPit";
            this.tpPit.Size = new System.Drawing.Size(968, 445);
            this.tpPit.TabIndex = 4;
            this.tpPit.Text = "Pit Volume";
            this.tpPit.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.groupBox18, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.groupBox1, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.groupBox17, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(966, 443);
            this.tableLayoutPanel3.TabIndex = 205;
            // 
            // groupBox18
            // 
            this.groupBox18.Controls.Add(this.dgvTotalWellVol);
            this.groupBox18.Controls.Add(this.btnTotalWellVolRefresh);
            this.groupBox18.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox18.Location = new System.Drawing.Point(3, 224);
            this.groupBox18.Name = "groupBox18";
            this.groupBox18.Size = new System.Drawing.Size(477, 216);
            this.groupBox18.TabIndex = 204;
            this.groupBox18.TabStop = false;
            this.groupBox18.Text = "Total Well Volume";
            // 
            // dgvTotalWellVol
            // 
            this.dgvTotalWellVol.AllowUserToAddRows = false;
            this.dgvTotalWellVol.AllowUserToDeleteRows = false;
            dataGridViewCellStyle16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.dgvTotalWellVol.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle16;
            this.dgvTotalWellVol.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvTotalWellVol.BackgroundColor = System.Drawing.Color.White;
            this.dgvTotalWellVol.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTotalWellVol.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.totalWellID,
            this.Column26,
            this.Column6,
            this.Column7,
            this.Column8,
            this.Column9});
            this.dgvTotalWellVol.EnableHeadersVisualStyles = false;
            this.dgvTotalWellVol.Location = new System.Drawing.Point(6, 19);
            this.dgvTotalWellVol.MultiSelect = false;
            this.dgvTotalWellVol.Name = "dgvTotalWellVol";
            this.dgvTotalWellVol.ReadOnly = true;
            this.dgvTotalWellVol.Size = new System.Drawing.Size(456, 162);
            this.dgvTotalWellVol.TabIndex = 0;
            this.dgvTotalWellVol.Tag = "MW contains x/y info where x and y are related";
            this.dgvTotalWellVol.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvTotalWellVol_CellMouseClick);
            // 
            // totalWellID
            // 
            this.totalWellID.HeaderText = "";
            this.totalWellID.Name = "totalWellID";
            this.totalWellID.ReadOnly = true;
            this.totalWellID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.totalWellID.Visible = false;
            // 
            // Column26
            // 
            this.Column26.HeaderText = "dfsAutoID";
            this.Column26.Name = "Column26";
            this.Column26.ReadOnly = true;
            this.Column26.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.Column26.Visible = false;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "Drilling Fluid System";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.Column6.Width = 130;
            // 
            // Column7
            // 
            this.Column7.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column7.HeaderText = "Volume (?)";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            this.Column7.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // Column8
            // 
            this.Column8.HeaderText = "Vol. After Merge (?)";
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            this.Column8.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.Column8.Visible = false;
            this.Column8.Width = 140;
            // 
            // Column9
            // 
            this.Column9.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column9.HeaderText = "Merge";
            this.Column9.Name = "Column9";
            this.Column9.ReadOnly = true;
            this.Column9.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.Column9.Visible = false;
            // 
            // btnTotalWellVolRefresh
            // 
            this.btnTotalWellVolRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnTotalWellVolRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnTotalWellVolRefresh.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTotalWellVolRefresh.ImageKey = "refresh";
            this.btnTotalWellVolRefresh.ImageList = this.imageList;
            this.btnTotalWellVolRefresh.Location = new System.Drawing.Point(6, 187);
            this.btnTotalWellVolRefresh.Name = "btnTotalWellVolRefresh";
            this.btnTotalWellVolRefresh.Size = new System.Drawing.Size(71, 23);
            this.btnTotalWellVolRefresh.TabIndex = 1;
            this.btnTotalWellVolRefresh.Text = "Refresh";
            this.btnTotalWellVolRefresh.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnTotalWellVolRefresh.UseVisualStyleBackColor = true;
            this.btnTotalWellVolRefresh.Click += new System.EventHandler(this.btnTotalWellVolRefresh_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnCMTApply);
            this.groupBox1.Controls.Add(this.btnCMTRefresh);
            this.groupBox1.Controls.Add(this.dgvPitTankCMT_Spacer);
            this.groupBox1.Location = new System.Drawing.Point(486, 3);
            this.groupBox1.Name = "groupBox1";
            this.tableLayoutPanel3.SetRowSpan(this.groupBox1, 2);
            this.groupBox1.Size = new System.Drawing.Size(477, 274);
            this.groupBox1.TabIndex = 113;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "CMT && Spacer";
            // 
            // btnCMTApply
            // 
            this.btnCMTApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCMTApply.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCMTApply.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCMTApply.ImageKey = "Check";
            this.btnCMTApply.ImageList = this.imageList;
            this.btnCMTApply.Location = new System.Drawing.Point(6, 241);
            this.btnCMTApply.Name = "btnCMTApply";
            this.btnCMTApply.Size = new System.Drawing.Size(104, 23);
            this.btnCMTApply.TabIndex = 1;
            this.btnCMTApply.Text = "Apply Changes";
            this.btnCMTApply.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCMTApply.UseVisualStyleBackColor = true;
            this.btnCMTApply.Click += new System.EventHandler(this.btnCMTApply_Click);
            // 
            // btnCMTRefresh
            // 
            this.btnCMTRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCMTRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCMTRefresh.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCMTRefresh.ImageKey = "refresh";
            this.btnCMTRefresh.ImageList = this.imageList;
            this.btnCMTRefresh.Location = new System.Drawing.Point(116, 241);
            this.btnCMTRefresh.Name = "btnCMTRefresh";
            this.btnCMTRefresh.Size = new System.Drawing.Size(71, 23);
            this.btnCMTRefresh.TabIndex = 2;
            this.btnCMTRefresh.Text = "Refresh";
            this.btnCMTRefresh.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCMTRefresh.UseVisualStyleBackColor = true;
            this.btnCMTRefresh.Click += new System.EventHandler(this.btnCMTRefresh_Click);
            // 
            // dgvPitTankCMT_Spacer
            // 
            this.dgvPitTankCMT_Spacer.AllowUserToAddRows = false;
            this.dgvPitTankCMT_Spacer.AllowUserToDeleteRows = false;
            dataGridViewCellStyle17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.dgvPitTankCMT_Spacer.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle17;
            this.dgvPitTankCMT_Spacer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvPitTankCMT_Spacer.BackgroundColor = System.Drawing.Color.White;
            this.dgvPitTankCMT_Spacer.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPitTankCMT_Spacer.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column4,
            this.Column5,
            this.Column22});
            this.dgvPitTankCMT_Spacer.EnableHeadersVisualStyles = false;
            this.dgvPitTankCMT_Spacer.Location = new System.Drawing.Point(6, 19);
            this.dgvPitTankCMT_Spacer.MultiSelect = false;
            this.dgvPitTankCMT_Spacer.Name = "dgvPitTankCMT_Spacer";
            this.dgvPitTankCMT_Spacer.RowHeadersWidth = 100;
            dataGridViewCellStyle18.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPitTankCMT_Spacer.RowsDefaultCellStyle = dataGridViewCellStyle18;
            this.dgvPitTankCMT_Spacer.Size = new System.Drawing.Size(465, 216);
            this.dgvPitTankCMT_Spacer.TabIndex = 0;
            this.dgvPitTankCMT_Spacer.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvPitTankCMT_Spacer_CellMouseClick);
            this.dgvPitTankCMT_Spacer.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPitTankCMT_Spacer_CellValueChanged);
            // 
            // Column4
            // 
            this.Column4.FillWeight = 137.0558F;
            this.Column4.HeaderText = "Vol (bbl?)";
            this.Column4.Name = "Column4";
            this.Column4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.Column4.Width = 90;
            // 
            // Column5
            // 
            this.Column5.FillWeight = 58.44891F;
            this.Column5.HeaderText = "Weight (?)";
            this.Column5.Name = "Column5";
            this.Column5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.Column5.Width = 90;
            // 
            // Column22
            // 
            this.Column22.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column22.FillWeight = 104.4953F;
            this.Column22.HeaderText = "Used";
            this.Column22.Name = "Column22";
            this.Column22.ReadOnly = true;
            this.Column22.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // groupBox17
            // 
            this.groupBox17.Controls.Add(this.btnPitTankCheckVolumes);
            this.groupBox17.Controls.Add(this.dgvPitTank);
            this.groupBox17.Controls.Add(this.btnPitTankApply);
            this.groupBox17.Controls.Add(this.btnPitTankRefresh);
            this.groupBox17.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox17.Location = new System.Drawing.Point(3, 3);
            this.groupBox17.Name = "groupBox17";
            this.groupBox17.Size = new System.Drawing.Size(477, 215);
            this.groupBox17.TabIndex = 204;
            this.groupBox17.TabStop = false;
            this.groupBox17.Text = "Pit Volume";
            // 
            // btnPitTankCheckVolumes
            // 
            this.btnPitTankCheckVolumes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPitTankCheckVolumes.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPitTankCheckVolumes.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPitTankCheckVolumes.ImageKey = "point_hand.png";
            this.btnPitTankCheckVolumes.ImageList = this.imageList;
            this.btnPitTankCheckVolumes.Location = new System.Drawing.Point(348, 186);
            this.btnPitTankCheckVolumes.Name = "btnPitTankCheckVolumes";
            this.btnPitTankCheckVolumes.Size = new System.Drawing.Size(114, 23);
            this.btnPitTankCheckVolumes.TabIndex = 3;
            this.btnPitTankCheckVolumes.Text = "Check DFS Vol.";
            this.btnPitTankCheckVolumes.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPitTankCheckVolumes.UseVisualStyleBackColor = true;
            this.btnPitTankCheckVolumes.Click += new System.EventHandler(this.btnPitTankCheckVolumes_Click);
            // 
            // dgvPitTank
            // 
            this.dgvPitTank.AllowUserToAddRows = false;
            this.dgvPitTank.AllowUserToDeleteRows = false;
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.dgvPitTank.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle10;
            this.dgvPitTank.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvPitTank.BackgroundColor = System.Drawing.Color.White;
            this.dgvPitTank.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPitTank.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.pitVolId,
            this.dataGridViewTextBoxColumn3,
            this.Column2,
            this.Column27,
            this.dataGridViewTextBoxColumn4,
            this.Column3,
            this.pitID});
            this.dgvPitTank.EnableHeadersVisualStyles = false;
            this.dgvPitTank.Location = new System.Drawing.Point(6, 19);
            this.dgvPitTank.MultiSelect = false;
            this.dgvPitTank.Name = "dgvPitTank";
            this.dgvPitTank.Size = new System.Drawing.Size(456, 161);
            this.dgvPitTank.TabIndex = 0;
            this.dgvPitTank.Tag = "MW contains x/y info where x and y are related";
            this.dgvPitTank.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvPitTank_CellMouseClick);
            this.dgvPitTank.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPitTank_CellValueChanged);
            // 
            // pitVolId
            // 
            this.pitVolId.HeaderText = "";
            this.pitVolId.Name = "pitVolId";
            this.pitVolId.ReadOnly = true;
            this.pitVolId.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.pitVolId.Visible = false;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn3.HeaderText = "Pit";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column2.HeaderText = "Current Mud Vol. (bbl?)";
            this.Column2.Name = "Column2";
            this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // Column27
            // 
            this.Column27.HeaderText = "dfsAutoID";
            this.Column27.Name = "Column27";
            this.Column27.ReadOnly = true;
            this.Column27.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.Column27.Visible = false;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn4.HeaderText = "Current Drilling Fluid System";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "MW (?)";
            this.Column3.Name = "Column3";
            this.Column3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // pitID
            // 
            this.pitID.HeaderText = "";
            this.pitID.Name = "pitID";
            this.pitID.ReadOnly = true;
            this.pitID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.pitID.Visible = false;
            // 
            // btnPitTankApply
            // 
            this.btnPitTankApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPitTankApply.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPitTankApply.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPitTankApply.ImageKey = "Check";
            this.btnPitTankApply.ImageList = this.imageList;
            this.btnPitTankApply.Location = new System.Drawing.Point(6, 186);
            this.btnPitTankApply.Name = "btnPitTankApply";
            this.btnPitTankApply.Size = new System.Drawing.Size(104, 23);
            this.btnPitTankApply.TabIndex = 1;
            this.btnPitTankApply.Text = "Apply Changes";
            this.btnPitTankApply.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPitTankApply.UseVisualStyleBackColor = true;
            this.btnPitTankApply.Click += new System.EventHandler(this.btnPitTankApply_Click);
            // 
            // btnPitTankRefresh
            // 
            this.btnPitTankRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPitTankRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPitTankRefresh.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPitTankRefresh.ImageKey = "refresh";
            this.btnPitTankRefresh.ImageList = this.imageList;
            this.btnPitTankRefresh.Location = new System.Drawing.Point(116, 186);
            this.btnPitTankRefresh.Name = "btnPitTankRefresh";
            this.btnPitTankRefresh.Size = new System.Drawing.Size(71, 23);
            this.btnPitTankRefresh.TabIndex = 2;
            this.btnPitTankRefresh.Text = "Refresh";
            this.btnPitTankRefresh.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPitTankRefresh.UseVisualStyleBackColor = true;
            this.btnPitTankRefresh.Click += new System.EventHandler(this.btnPitTankRefresh_Click);
            // 
            // tpTotalVolMan
            // 
            this.tpTotalVolMan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tpTotalVolMan.Controls.Add(this.btnTotalVolManRefresh);
            this.tpTotalVolMan.Controls.Add(this.groupBox2);
            this.tpTotalVolMan.Controls.Add(this.groupBox7);
            this.tpTotalVolMan.Controls.Add(this.groupBox8);
            this.tpTotalVolMan.Controls.Add(this.groupBox9);
            this.tpTotalVolMan.ImageIndex = 0;
            this.tpTotalVolMan.Location = new System.Drawing.Point(4, 25);
            this.tpTotalVolMan.Name = "tpTotalVolMan";
            this.tpTotalVolMan.Size = new System.Drawing.Size(968, 445);
            this.tpTotalVolMan.TabIndex = 8;
            this.tpTotalVolMan.Text = "Total Vol Management";
            this.tpTotalVolMan.UseVisualStyleBackColor = true;
            // 
            // btnTotalVolManRefresh
            // 
            this.btnTotalVolManRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnTotalVolManRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnTotalVolManRefresh.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTotalVolManRefresh.ImageKey = "refresh";
            this.btnTotalVolManRefresh.ImageList = this.imageList;
            this.btnTotalVolManRefresh.Location = new System.Drawing.Point(12, 413);
            this.btnTotalVolManRefresh.Name = "btnTotalVolManRefresh";
            this.btnTotalVolManRefresh.Size = new System.Drawing.Size(71, 23);
            this.btnTotalVolManRefresh.TabIndex = 0;
            this.btnTotalVolManRefresh.Text = "Refresh";
            this.btnTotalVolManRefresh.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnTotalVolManRefresh.UseVisualStyleBackColor = true;
            this.btnTotalVolManRefresh.Click += new System.EventHandler(this.btnTotalVolManRefresh_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label50);
            this.groupBox2.Controls.Add(this.label51);
            this.groupBox2.Controls.Add(this.label52);
            this.groupBox2.Controls.Add(this.label53);
            this.groupBox2.Controls.Add(this.label54);
            this.groupBox2.Controls.Add(this.txtTVM_CD_MinsTotalCir);
            this.groupBox2.Controls.Add(this.txtTVM_CD_BblTotalCir);
            this.groupBox2.Controls.Add(this.txtTVM_CD_MinsBottomUp);
            this.groupBox2.Controls.Add(this.txtTVM_CD_MinsToBit);
            this.groupBox2.Controls.Add(this.txtTVM_CD_BblBottomUp);
            this.groupBox2.Controls.Add(this.txtTVM_CD_BblToBit);
            this.groupBox2.Location = new System.Drawing.Point(518, 15);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(434, 105);
            this.groupBox2.TabIndex = 123;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Circulation Data";
            // 
            // label50
            // 
            this.label50.AutoSize = true;
            this.label50.Location = new System.Drawing.Point(322, 26);
            this.label50.Name = "label50";
            this.label50.Size = new System.Drawing.Size(83, 13);
            this.label50.TabIndex = 112;
            this.label50.Text = "Total Circulation";
            // 
            // label51
            // 
            this.label51.AutoSize = true;
            this.label51.Location = new System.Drawing.Point(200, 26);
            this.label51.Name = "label51";
            this.label51.Size = new System.Drawing.Size(57, 13);
            this.label51.TabIndex = 112;
            this.label51.Text = "Bottom Up";
            // 
            // label52
            // 
            this.label52.AutoSize = true;
            this.label52.Location = new System.Drawing.Point(76, 26);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(35, 13);
            this.label52.TabIndex = 112;
            this.label52.Text = "To Bit";
            // 
            // label53
            // 
            this.label53.AutoSize = true;
            this.label53.Location = new System.Drawing.Point(6, 45);
            this.label53.Name = "label53";
            this.label53.Size = new System.Drawing.Size(30, 13);
            this.label53.TabIndex = 112;
            this.label53.Text = "BBL:";
            // 
            // label54
            // 
            this.label54.AutoSize = true;
            this.label54.Location = new System.Drawing.Point(6, 71);
            this.label54.Name = "label54";
            this.label54.Size = new System.Drawing.Size(32, 13);
            this.label54.TabIndex = 112;
            this.label54.Text = "Mins:";
            // 
            // txtTVM_CD_MinsTotalCir
            // 
            this.txtTVM_CD_MinsTotalCir.BackColor = System.Drawing.SystemColors.Control;
            this.txtTVM_CD_MinsTotalCir.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTVM_CD_MinsTotalCir.Location = new System.Drawing.Point(316, 69);
            this.txtTVM_CD_MinsTotalCir.Name = "txtTVM_CD_MinsTotalCir";
            this.txtTVM_CD_MinsTotalCir.ReadOnly = true;
            this.txtTVM_CD_MinsTotalCir.Size = new System.Drawing.Size(100, 20);
            this.txtTVM_CD_MinsTotalCir.TabIndex = 121;
            // 
            // txtTVM_CD_BblTotalCir
            // 
            this.txtTVM_CD_BblTotalCir.BackColor = System.Drawing.SystemColors.Control;
            this.txtTVM_CD_BblTotalCir.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTVM_CD_BblTotalCir.Location = new System.Drawing.Point(316, 45);
            this.txtTVM_CD_BblTotalCir.Name = "txtTVM_CD_BblTotalCir";
            this.txtTVM_CD_BblTotalCir.ReadOnly = true;
            this.txtTVM_CD_BblTotalCir.Size = new System.Drawing.Size(100, 20);
            this.txtTVM_CD_BblTotalCir.TabIndex = 121;
            // 
            // txtTVM_CD_MinsBottomUp
            // 
            this.txtTVM_CD_MinsBottomUp.BackColor = System.Drawing.SystemColors.Control;
            this.txtTVM_CD_MinsBottomUp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTVM_CD_MinsBottomUp.Location = new System.Drawing.Point(181, 69);
            this.txtTVM_CD_MinsBottomUp.Name = "txtTVM_CD_MinsBottomUp";
            this.txtTVM_CD_MinsBottomUp.ReadOnly = true;
            this.txtTVM_CD_MinsBottomUp.Size = new System.Drawing.Size(100, 20);
            this.txtTVM_CD_MinsBottomUp.TabIndex = 121;
            // 
            // txtTVM_CD_MinsToBit
            // 
            this.txtTVM_CD_MinsToBit.BackColor = System.Drawing.SystemColors.Control;
            this.txtTVM_CD_MinsToBit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTVM_CD_MinsToBit.Location = new System.Drawing.Point(49, 69);
            this.txtTVM_CD_MinsToBit.Name = "txtTVM_CD_MinsToBit";
            this.txtTVM_CD_MinsToBit.ReadOnly = true;
            this.txtTVM_CD_MinsToBit.Size = new System.Drawing.Size(100, 20);
            this.txtTVM_CD_MinsToBit.TabIndex = 121;
            // 
            // txtTVM_CD_BblBottomUp
            // 
            this.txtTVM_CD_BblBottomUp.BackColor = System.Drawing.SystemColors.Control;
            this.txtTVM_CD_BblBottomUp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTVM_CD_BblBottomUp.Location = new System.Drawing.Point(181, 45);
            this.txtTVM_CD_BblBottomUp.Name = "txtTVM_CD_BblBottomUp";
            this.txtTVM_CD_BblBottomUp.ReadOnly = true;
            this.txtTVM_CD_BblBottomUp.Size = new System.Drawing.Size(100, 20);
            this.txtTVM_CD_BblBottomUp.TabIndex = 121;
            // 
            // txtTVM_CD_BblToBit
            // 
            this.txtTVM_CD_BblToBit.BackColor = System.Drawing.SystemColors.Control;
            this.txtTVM_CD_BblToBit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTVM_CD_BblToBit.Location = new System.Drawing.Point(49, 45);
            this.txtTVM_CD_BblToBit.Name = "txtTVM_CD_BblToBit";
            this.txtTVM_CD_BblToBit.ReadOnly = true;
            this.txtTVM_CD_BblToBit.Size = new System.Drawing.Size(100, 20);
            this.txtTVM_CD_BblToBit.TabIndex = 121;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.label61);
            this.groupBox7.Controls.Add(this.txtTVM_BVM_DispVol);
            this.groupBox7.Controls.Add(this.label60);
            this.groupBox7.Controls.Add(this.txtTVM_BVM_RetVol);
            this.groupBox7.Controls.Add(this.label59);
            this.groupBox7.Controls.Add(this.txtTVM_BVM_AddVol);
            this.groupBox7.Controls.Add(this.label55);
            this.groupBox7.Controls.Add(this.txtTVM_BVM_StartVol);
            this.groupBox7.Controls.Add(this.label56);
            this.groupBox7.Controls.Add(this.label57);
            this.groupBox7.Controls.Add(this.label58);
            this.groupBox7.Controls.Add(this.label62);
            this.groupBox7.Controls.Add(this.label500);
            this.groupBox7.Controls.Add(this.txtTVM_BVM_BuildVol);
            this.groupBox7.Controls.Add(this.label64);
            this.groupBox7.Controls.Add(this.txtTVM_BVM_EndVol);
            this.groupBox7.Controls.Add(this.txtTVM_BVM_LossVol);
            this.groupBox7.Controls.Add(this.txtTVM_BVM_TransVol);
            this.groupBox7.Controls.Add(this.txtTVM_BVM_Gain);
            this.groupBox7.Controls.Add(this.txtTVM_BVM_RecVol);
            this.groupBox7.Location = new System.Drawing.Point(256, 15);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(256, 290);
            this.groupBox7.TabIndex = 124;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Balance Volume Management";
            // 
            // label61
            // 
            this.label61.AutoSize = true;
            this.label61.Location = new System.Drawing.Point(5, 183);
            this.label61.Name = "label61";
            this.label61.Size = new System.Drawing.Size(110, 13);
            this.label61.TabIndex = 135;
            this.label61.Text = "Displaced Volume (?):";
            // 
            // txtTVM_BVM_DispVol
            // 
            this.txtTVM_BVM_DispVol.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTVM_BVM_DispVol.Location = new System.Drawing.Point(177, 181);
            this.txtTVM_BVM_DispVol.Name = "txtTVM_BVM_DispVol";
            this.txtTVM_BVM_DispVol.ReadOnly = true;
            this.txtTVM_BVM_DispVol.Size = new System.Drawing.Size(69, 20);
            this.txtTVM_BVM_DispVol.TabIndex = 136;
            // 
            // label60
            // 
            this.label60.AutoSize = true;
            this.label60.Location = new System.Drawing.Point(5, 157);
            this.label60.Name = "label60";
            this.label60.Size = new System.Drawing.Size(95, 13);
            this.label60.TabIndex = 133;
            this.label60.Text = "Return Volume (?):";
            // 
            // txtTVM_BVM_RetVol
            // 
            this.txtTVM_BVM_RetVol.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTVM_BVM_RetVol.Location = new System.Drawing.Point(177, 155);
            this.txtTVM_BVM_RetVol.Name = "txtTVM_BVM_RetVol";
            this.txtTVM_BVM_RetVol.ReadOnly = true;
            this.txtTVM_BVM_RetVol.Size = new System.Drawing.Size(69, 20);
            this.txtTVM_BVM_RetVol.TabIndex = 134;
            // 
            // label59
            // 
            this.label59.AutoSize = true;
            this.label59.Location = new System.Drawing.Point(5, 131);
            this.label59.Name = "label59";
            this.label59.Size = new System.Drawing.Size(152, 13);
            this.label59.TabIndex = 131;
            this.label59.Text = "Added Vol. in Treated Mud (?):";
            // 
            // txtTVM_BVM_AddVol
            // 
            this.txtTVM_BVM_AddVol.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTVM_BVM_AddVol.Location = new System.Drawing.Point(177, 129);
            this.txtTVM_BVM_AddVol.Name = "txtTVM_BVM_AddVol";
            this.txtTVM_BVM_AddVol.ReadOnly = true;
            this.txtTVM_BVM_AddVol.Size = new System.Drawing.Size(69, 20);
            this.txtTVM_BVM_AddVol.TabIndex = 132;
            // 
            // label55
            // 
            this.label55.AutoSize = true;
            this.label55.Location = new System.Drawing.Point(5, 28);
            this.label55.Name = "label55";
            this.label55.Size = new System.Drawing.Size(85, 13);
            this.label55.TabIndex = 124;
            this.label55.Text = "Start Volume (?):";
            // 
            // txtTVM_BVM_StartVol
            // 
            this.txtTVM_BVM_StartVol.BackColor = System.Drawing.SystemColors.Control;
            this.txtTVM_BVM_StartVol.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTVM_BVM_StartVol.Location = new System.Drawing.Point(177, 23);
            this.txtTVM_BVM_StartVol.Name = "txtTVM_BVM_StartVol";
            this.txtTVM_BVM_StartVol.ReadOnly = true;
            this.txtTVM_BVM_StartVol.Size = new System.Drawing.Size(69, 20);
            this.txtTVM_BVM_StartVol.TabIndex = 130;
            // 
            // label56
            // 
            this.label56.AutoSize = true;
            this.label56.Location = new System.Drawing.Point(5, 79);
            this.label56.Name = "label56";
            this.label56.Size = new System.Drawing.Size(109, 13);
            this.label56.TabIndex = 121;
            this.label56.Text = "Received Volume (?):";
            // 
            // label57
            // 
            this.label57.AutoSize = true;
            this.label57.Location = new System.Drawing.Point(5, 105);
            this.label57.Name = "label57";
            this.label57.Size = new System.Drawing.Size(86, 13);
            this.label57.TabIndex = 121;
            this.label57.Text = "Build Volume (?):";
            // 
            // label58
            // 
            this.label58.AutoSize = true;
            this.label58.Location = new System.Drawing.Point(5, 261);
            this.label58.Name = "label58";
            this.label58.Size = new System.Drawing.Size(82, 13);
            this.label58.TabIndex = 123;
            this.label58.Text = "End Volume (?):";
            // 
            // label62
            // 
            this.label62.AutoSize = true;
            this.label62.Location = new System.Drawing.Point(5, 235);
            this.label62.Name = "label62";
            this.label62.Size = new System.Drawing.Size(117, 13);
            this.label62.TabIndex = 122;
            this.label62.Text = "Transferred Volume (?):";
            // 
            // label500
            // 
            this.label500.AutoSize = true;
            this.label500.Location = new System.Drawing.Point(5, 53);
            this.label500.Name = "label500";
            this.label500.Size = new System.Drawing.Size(85, 13);
            this.label500.TabIndex = 121;
            this.label500.Text = "Gain Volume (?):";
            // 
            // txtTVM_BVM_BuildVol
            // 
            this.txtTVM_BVM_BuildVol.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTVM_BVM_BuildVol.Location = new System.Drawing.Point(177, 103);
            this.txtTVM_BVM_BuildVol.Name = "txtTVM_BVM_BuildVol";
            this.txtTVM_BVM_BuildVol.ReadOnly = true;
            this.txtTVM_BVM_BuildVol.Size = new System.Drawing.Size(69, 20);
            this.txtTVM_BVM_BuildVol.TabIndex = 122;
            // 
            // label64
            // 
            this.label64.AutoSize = true;
            this.label64.Location = new System.Drawing.Point(5, 209);
            this.label64.Name = "label64";
            this.label64.Size = new System.Drawing.Size(99, 13);
            this.label64.TabIndex = 121;
            this.label64.Text = "Losses  Volume (?):";
            // 
            // txtTVM_BVM_EndVol
            // 
            this.txtTVM_BVM_EndVol.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTVM_BVM_EndVol.Location = new System.Drawing.Point(177, 259);
            this.txtTVM_BVM_EndVol.Name = "txtTVM_BVM_EndVol";
            this.txtTVM_BVM_EndVol.ReadOnly = true;
            this.txtTVM_BVM_EndVol.Size = new System.Drawing.Size(69, 20);
            this.txtTVM_BVM_EndVol.TabIndex = 127;
            // 
            // txtTVM_BVM_LossVol
            // 
            this.txtTVM_BVM_LossVol.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTVM_BVM_LossVol.Location = new System.Drawing.Point(177, 207);
            this.txtTVM_BVM_LossVol.Name = "txtTVM_BVM_LossVol";
            this.txtTVM_BVM_LossVol.ReadOnly = true;
            this.txtTVM_BVM_LossVol.Size = new System.Drawing.Size(69, 20);
            this.txtTVM_BVM_LossVol.TabIndex = 128;
            // 
            // txtTVM_BVM_TransVol
            // 
            this.txtTVM_BVM_TransVol.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTVM_BVM_TransVol.Location = new System.Drawing.Point(177, 233);
            this.txtTVM_BVM_TransVol.Name = "txtTVM_BVM_TransVol";
            this.txtTVM_BVM_TransVol.ReadOnly = true;
            this.txtTVM_BVM_TransVol.Size = new System.Drawing.Size(69, 20);
            this.txtTVM_BVM_TransVol.TabIndex = 126;
            // 
            // txtTVM_BVM_Gain
            // 
            this.txtTVM_BVM_Gain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTVM_BVM_Gain.Location = new System.Drawing.Point(177, 51);
            this.txtTVM_BVM_Gain.Name = "txtTVM_BVM_Gain";
            this.txtTVM_BVM_Gain.ReadOnly = true;
            this.txtTVM_BVM_Gain.Size = new System.Drawing.Size(69, 20);
            this.txtTVM_BVM_Gain.TabIndex = 122;
            // 
            // txtTVM_BVM_RecVol
            // 
            this.txtTVM_BVM_RecVol.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTVM_BVM_RecVol.Location = new System.Drawing.Point(177, 77);
            this.txtTVM_BVM_RecVol.Name = "txtTVM_BVM_RecVol";
            this.txtTVM_BVM_RecVol.ReadOnly = true;
            this.txtTVM_BVM_RecVol.Size = new System.Drawing.Size(69, 20);
            this.txtTVM_BVM_RecVol.TabIndex = 122;
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.lblTVM_HVM_StringVol);
            this.groupBox8.Controls.Add(this.txtTVM_HVM_StringVol);
            this.groupBox8.Controls.Add(this.label66);
            this.groupBox8.Controls.Add(this.txtTVM_HVM_AnnVol);
            this.groupBox8.Controls.Add(this.label67);
            this.groupBox8.Controls.Add(this.txtTVM_HVM_DrillVol);
            this.groupBox8.Controls.Add(this.label68);
            this.groupBox8.Controls.Add(this.label69);
            this.groupBox8.Controls.Add(this.txtTVM_HVM_TotalVol);
            this.groupBox8.Controls.Add(this.txtTVM_HVM_BelVol);
            this.groupBox8.Location = new System.Drawing.Point(12, 150);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(238, 155);
            this.groupBox8.TabIndex = 125;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Hole Volume Management";
            // 
            // lblTVM_HVM_StringVol
            // 
            this.lblTVM_HVM_StringVol.AutoSize = true;
            this.lblTVM_HVM_StringVol.Location = new System.Drawing.Point(6, 21);
            this.lblTVM_HVM_StringVol.Name = "lblTVM_HVM_StringVol";
            this.lblTVM_HVM_StringVol.Size = new System.Drawing.Size(90, 13);
            this.lblTVM_HVM_StringVol.TabIndex = 114;
            this.lblTVM_HVM_StringVol.Text = "String Volume (?):";
            // 
            // txtTVM_HVM_StringVol
            // 
            this.txtTVM_HVM_StringVol.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTVM_HVM_StringVol.Location = new System.Drawing.Point(152, 19);
            this.txtTVM_HVM_StringVol.Name = "txtTVM_HVM_StringVol";
            this.txtTVM_HVM_StringVol.ReadOnly = true;
            this.txtTVM_HVM_StringVol.Size = new System.Drawing.Size(69, 20);
            this.txtTVM_HVM_StringVol.TabIndex = 120;
            // 
            // label66
            // 
            this.label66.AutoSize = true;
            this.label66.Location = new System.Drawing.Point(6, 47);
            this.label66.Name = "label66";
            this.label66.Size = new System.Drawing.Size(101, 13);
            this.label66.TabIndex = 115;
            this.label66.Text = "Annulus Volume (?):";
            // 
            // txtTVM_HVM_AnnVol
            // 
            this.txtTVM_HVM_AnnVol.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTVM_HVM_AnnVol.Location = new System.Drawing.Point(152, 45);
            this.txtTVM_HVM_AnnVol.Name = "txtTVM_HVM_AnnVol";
            this.txtTVM_HVM_AnnVol.ReadOnly = true;
            this.txtTVM_HVM_AnnVol.Size = new System.Drawing.Size(69, 20);
            this.txtTVM_HVM_AnnVol.TabIndex = 119;
            // 
            // label67
            // 
            this.label67.AutoSize = true;
            this.label67.Location = new System.Drawing.Point(6, 129);
            this.label67.Name = "label67";
            this.label67.Size = new System.Drawing.Size(94, 13);
            this.label67.TabIndex = 111;
            this.label67.Text = "Drilling Volume (?):";
            // 
            // txtTVM_HVM_DrillVol
            // 
            this.txtTVM_HVM_DrillVol.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTVM_HVM_DrillVol.Location = new System.Drawing.Point(152, 127);
            this.txtTVM_HVM_DrillVol.Name = "txtTVM_HVM_DrillVol";
            this.txtTVM_HVM_DrillVol.ReadOnly = true;
            this.txtTVM_HVM_DrillVol.Size = new System.Drawing.Size(69, 20);
            this.txtTVM_HVM_DrillVol.TabIndex = 116;
            // 
            // label68
            // 
            this.label68.AutoSize = true;
            this.label68.Location = new System.Drawing.Point(6, 101);
            this.label68.Name = "label68";
            this.label68.Size = new System.Drawing.Size(111, 13);
            this.label68.TabIndex = 113;
            this.label68.Text = "Total Well Volume (?):";
            // 
            // label69
            // 
            this.label69.AutoSize = true;
            this.label69.Location = new System.Drawing.Point(6, 73);
            this.label69.Name = "label69";
            this.label69.Size = new System.Drawing.Size(107, 13);
            this.label69.TabIndex = 110;
            this.label69.Text = "Below Bit Volume (?):";
            // 
            // txtTVM_HVM_TotalVol
            // 
            this.txtTVM_HVM_TotalVol.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTVM_HVM_TotalVol.Location = new System.Drawing.Point(152, 99);
            this.txtTVM_HVM_TotalVol.Name = "txtTVM_HVM_TotalVol";
            this.txtTVM_HVM_TotalVol.ReadOnly = true;
            this.txtTVM_HVM_TotalVol.Size = new System.Drawing.Size(69, 20);
            this.txtTVM_HVM_TotalVol.TabIndex = 117;
            // 
            // txtTVM_HVM_BelVol
            // 
            this.txtTVM_HVM_BelVol.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTVM_HVM_BelVol.Location = new System.Drawing.Point(152, 71);
            this.txtTVM_HVM_BelVol.Name = "txtTVM_HVM_BelVol";
            this.txtTVM_HVM_BelVol.ReadOnly = true;
            this.txtTVM_HVM_BelVol.Size = new System.Drawing.Size(69, 20);
            this.txtTVM_HVM_BelVol.TabIndex = 118;
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.label700);
            this.groupBox9.Controls.Add(this.label71);
            this.groupBox9.Controls.Add(this.label600);
            this.groupBox9.Controls.Add(this.txtTVM_SVM_PitVol);
            this.groupBox9.Controls.Add(this.txtTVM_SVM_TreVol);
            this.groupBox9.Controls.Add(this.txtTVM_SVM_SurVol);
            this.groupBox9.Location = new System.Drawing.Point(12, 15);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(238, 105);
            this.groupBox9.TabIndex = 126;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "Surface Volume Management";
            // 
            // label700
            // 
            this.label700.AutoSize = true;
            this.label700.Location = new System.Drawing.Point(6, 79);
            this.label700.Name = "label700";
            this.label700.Size = new System.Drawing.Size(80, 13);
            this.label700.TabIndex = 121;
            this.label700.Text = "Pits Volume (?):";
            // 
            // label71
            // 
            this.label71.AutoSize = true;
            this.label71.Location = new System.Drawing.Point(6, 25);
            this.label71.Name = "label71";
            this.label71.Size = new System.Drawing.Size(100, 13);
            this.label71.TabIndex = 121;
            this.label71.Text = "Treated Volume (?):";
            // 
            // label600
            // 
            this.label600.AutoSize = true;
            this.label600.Location = new System.Drawing.Point(6, 53);
            this.label600.Name = "label600";
            this.label600.Size = new System.Drawing.Size(124, 13);
            this.label600.TabIndex = 121;
            this.label600.Text = "Surface Circ. Volume (?):";
            // 
            // txtTVM_SVM_PitVol
            // 
            this.txtTVM_SVM_PitVol.BackColor = System.Drawing.SystemColors.Control;
            this.txtTVM_SVM_PitVol.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTVM_SVM_PitVol.Location = new System.Drawing.Point(152, 77);
            this.txtTVM_SVM_PitVol.Name = "txtTVM_SVM_PitVol";
            this.txtTVM_SVM_PitVol.ReadOnly = true;
            this.txtTVM_SVM_PitVol.Size = new System.Drawing.Size(69, 20);
            this.txtTVM_SVM_PitVol.TabIndex = 122;
            // 
            // txtTVM_SVM_TreVol
            // 
            this.txtTVM_SVM_TreVol.BackColor = System.Drawing.SystemColors.Control;
            this.txtTVM_SVM_TreVol.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTVM_SVM_TreVol.Location = new System.Drawing.Point(152, 23);
            this.txtTVM_SVM_TreVol.Name = "txtTVM_SVM_TreVol";
            this.txtTVM_SVM_TreVol.ReadOnly = true;
            this.txtTVM_SVM_TreVol.Size = new System.Drawing.Size(69, 20);
            this.txtTVM_SVM_TreVol.TabIndex = 122;
            // 
            // txtTVM_SVM_SurVol
            // 
            this.txtTVM_SVM_SurVol.BackColor = System.Drawing.SystemColors.Control;
            this.txtTVM_SVM_SurVol.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTVM_SVM_SurVol.Location = new System.Drawing.Point(152, 51);
            this.txtTVM_SVM_SurVol.Name = "txtTVM_SVM_SurVol";
            this.txtTVM_SVM_SurVol.ReadOnly = true;
            this.txtTVM_SVM_SurVol.Size = new System.Drawing.Size(69, 20);
            this.txtTVM_SVM_SurVol.TabIndex = 122;
            // 
            // ctxDFSMnu
            // 
            this.ctxDFSMnu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ctxDFSMnuRemove,
            this.toolStripSeparator3,
            this.ctxDFSMnuSetAsCurrent});
            this.ctxDFSMnu.Name = "contextMenuStrip1";
            this.ctxDFSMnu.Size = new System.Drawing.Size(148, 54);
            // 
            // ctxDFSMnuRemove
            // 
            this.ctxDFSMnuRemove.Name = "ctxDFSMnuRemove";
            this.ctxDFSMnuRemove.Size = new System.Drawing.Size(147, 22);
            this.ctxDFSMnuRemove.Text = "Remove";
            this.ctxDFSMnuRemove.Click += new System.EventHandler(this.ctxDFSMnuRemove_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(144, 6);
            // 
            // ctxDFSMnuSetAsCurrent
            // 
            this.ctxDFSMnuSetAsCurrent.Name = "ctxDFSMnuSetAsCurrent";
            this.ctxDFSMnuSetAsCurrent.Size = new System.Drawing.Size(147, 22);
            this.ctxDFSMnuSetAsCurrent.Text = "Set as Current";
            this.ctxDFSMnuSetAsCurrent.Click += new System.EventHandler(this.ctxDFSMnuSetAsCurrent_Click);
            // 
            // ctxDFSMnu_Parent
            // 
            this.ctxDFSMnu_Parent.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ctxDFSMnu_ParentAdd});
            this.ctxDFSMnu_Parent.Name = "contextMenuStrip1";
            this.ctxDFSMnu_Parent.Size = new System.Drawing.Size(97, 26);
            // 
            // ctxDFSMnu_ParentAdd
            // 
            this.ctxDFSMnu_ParentAdd.Name = "ctxDFSMnu_ParentAdd";
            this.ctxDFSMnu_ParentAdd.Size = new System.Drawing.Size(96, 22);
            this.ctxDFSMnu_ParentAdd.Text = "Add";
            this.ctxDFSMnu_ParentAdd.Click += new System.EventHandler(this.ctxDFSMnu_ParentAdd_Click);
            // 
            // MudVolManagementForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 490);
            this.Controls.Add(this.tcMudVolMan);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MinimumSize = new System.Drawing.Size(1000, 490);
            this.Name = "MudVolManagementForm";
            this.Text = "Status:";
            this.Load += new System.EventHandler(this.MudVolForm_Load);
            this.tcMudVolMan.ResumeLayout(false);
            this.tpDFSs.ResumeLayout(false);
            this.tpDFSs.PerformLayout();
            this.tcDfs.ResumeLayout(false);
            this.tpDfsRecTrans.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRec)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTrans)).EndInit();
            this.tpDfsBuild.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.grbStartBuiltRainGain.ResumeLayout(false);
            this.grbStartBuiltRainGain.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBuiltUsing)).EndInit();
            this.groupBox10.ResumeLayout(false);
            this.groupBox10.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBuiltVol)).EndInit();
            this.tpDfsTreated.ResumeLayout(false);
            this.groupBox12.ResumeLayout(false);
            this.groupBox12.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTreat)).EndInit();
            this.tpDfsDisp.ResumeLayout(false);
            this.grbDipRetDaily.ResumeLayout(false);
            this.grbDipRetDaily.PerformLayout();
            this.groupBox13.ResumeLayout(false);
            this.groupBox13.PerformLayout();
            this.grbDipRetAtTheEnd.ResumeLayout(false);
            this.groupBox14.ResumeLayout(false);
            this.groupBox14.PerformLayout();
            this.groupBox15.ResumeLayout(false);
            this.groupBox15.PerformLayout();
            this.tpPit.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.groupBox18.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTotalWellVol)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPitTankCMT_Spacer)).EndInit();
            this.groupBox17.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPitTank)).EndInit();
            this.tpTotalVolMan.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            this.ctxDFSMnu.ResumeLayout(false);
            this.ctxDFSMnu_Parent.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tcMudVolMan;
        private System.Windows.Forms.TabPage tpPit;
        private System.Windows.Forms.Button btnPitTankApply;
        private System.Windows.Forms.TabPage tpDFSs;
        private System.Windows.Forms.DataGridView dgvPitTank;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TabPage tpTotalVolMan;
        private System.Windows.Forms.DataGridView dgvPitTankCMT_Spacer;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label50;
        private System.Windows.Forms.Label label51;
        private System.Windows.Forms.Label label52;
        private System.Windows.Forms.Label label53;
        private System.Windows.Forms.Label label54;
        private System.Windows.Forms.TextBox txtTVM_CD_MinsTotalCir;
        private System.Windows.Forms.TextBox txtTVM_CD_BblTotalCir;
        private System.Windows.Forms.TextBox txtTVM_CD_MinsBottomUp;
        private System.Windows.Forms.TextBox txtTVM_CD_MinsToBit;
        private System.Windows.Forms.TextBox txtTVM_CD_BblBottomUp;
        private System.Windows.Forms.TextBox txtTVM_CD_BblToBit;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Label label61;
        private System.Windows.Forms.TextBox txtTVM_BVM_DispVol;
        private System.Windows.Forms.Label label60;
        private System.Windows.Forms.TextBox txtTVM_BVM_RetVol;
        private System.Windows.Forms.Label label59;
        private System.Windows.Forms.TextBox txtTVM_BVM_AddVol;
        private System.Windows.Forms.Label label55;
        private System.Windows.Forms.TextBox txtTVM_BVM_StartVol;
        private System.Windows.Forms.Label label56;
        private System.Windows.Forms.Label label57;
        private System.Windows.Forms.Label label58;
        private System.Windows.Forms.Label label62;
        private System.Windows.Forms.Label label500;
        private System.Windows.Forms.TextBox txtTVM_BVM_BuildVol;
        private System.Windows.Forms.Label label64;
        private System.Windows.Forms.TextBox txtTVM_BVM_EndVol;
        private System.Windows.Forms.TextBox txtTVM_BVM_LossVol;
        private System.Windows.Forms.TextBox txtTVM_BVM_TransVol;
        private System.Windows.Forms.TextBox txtTVM_BVM_Gain;
        private System.Windows.Forms.TextBox txtTVM_BVM_RecVol;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.Label lblTVM_HVM_StringVol;
        private System.Windows.Forms.TextBox txtTVM_HVM_StringVol;
        private System.Windows.Forms.Label label66;
        private System.Windows.Forms.TextBox txtTVM_HVM_AnnVol;
        private System.Windows.Forms.Label label67;
        private System.Windows.Forms.TextBox txtTVM_HVM_DrillVol;
        private System.Windows.Forms.Label label68;
        private System.Windows.Forms.Label label69;
        private System.Windows.Forms.TextBox txtTVM_HVM_TotalVol;
        private System.Windows.Forms.TextBox txtTVM_HVM_BelVol;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.Label label700;
        private System.Windows.Forms.Label label71;
        private System.Windows.Forms.TextBox txtTVM_SVM_PitVol;
        private System.Windows.Forms.TextBox txtTVM_SVM_TreVol;
        private System.Windows.Forms.TextBox txtTVM_SVM_SurVol;
        private System.Windows.Forms.Button btnPitTankRefresh;
        private System.Windows.Forms.Button btnTotalVolManRefresh;
        private System.Windows.Forms.Label label600;
        private System.Windows.Forms.TabControl tcDfs;
        private System.Windows.Forms.TabPage tpDfsRecTrans;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.DataGridView dgvRec;
        private System.Windows.Forms.TabPage tpDfsBuild;
        private System.Windows.Forms.GroupBox grbStartBuiltRainGain;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtStartVol;
        private System.Windows.Forms.TextBox txtGainVol;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtRainVol;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.DataGridView dgvBuiltUsing;
        private System.Windows.Forms.GroupBox groupBox10;
        private System.Windows.Forms.DataGridView dgvBuiltVol;
        private System.Windows.Forms.TabPage tpDfsTreated;
        private System.Windows.Forms.GroupBox groupBox12;
        private System.Windows.Forms.DataGridView dgvTreat;
        private System.Windows.Forms.TabPage tpDfsDisp;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.TextBox txtDailyStringDisp;
        private System.Windows.Forms.GroupBox groupBox13;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.TextBox txtDailyOver;
        private System.Windows.Forms.TextBox txtDailyWaste;
        private System.Windows.Forms.TextBox txtDailyPitMW;
        private System.Windows.Forms.TextBox txtDailyPitVol;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.GroupBox groupBox14;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.TextBox txtAtEndMW;
        private System.Windows.Forms.TextBox txtAtEndVol;
        private System.Windows.Forms.TextBox txtAtEndMaxFR;
        private System.Windows.Forms.TextBox txtAtEndMinFR;
        private System.Windows.Forms.TextBox txtAtEndDepth;
        private System.Windows.Forms.GroupBox groupBox15;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.TextBox txtAtEndOver;
        private System.Windows.Forms.TextBox txtAtEndWaste;
        private System.Windows.Forms.TextBox txtAtEndPitMW;
        private System.Windows.Forms.TextBox txtAtEndPitVol;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.TreeView trvDFSs;
        private System.Windows.Forms.Button btnRecAdd;
        private System.Windows.Forms.Button btnRecRefresh;
        private System.Windows.Forms.Button btnRecApply;
        private System.Windows.Forms.TextBox txtRecTotalVol;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button btnRecRemove;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnTransRemove;
        private System.Windows.Forms.Button btnTransAdd;
        private System.Windows.Forms.Button btnTransRefresh;
        private System.Windows.Forms.Button btnTransApply;
        private System.Windows.Forms.TextBox txtTransTotalVol;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnBuiltUsingRemove;
        private System.Windows.Forms.Button btnBuiltUsingAdd;
        private System.Windows.Forms.Button btnBuiltUsingRefresh;
        private System.Windows.Forms.Button btnBuiltUsingApply;
        private System.Windows.Forms.TextBox txtBuiltUsingTotalVol;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button btnBuiltVolRemove;
        private System.Windows.Forms.Button btnBuiltVolAdd;
        private System.Windows.Forms.Button btnBuiltVolRefresh;
        private System.Windows.Forms.Button btnBuiltVolApply;
        private System.Windows.Forms.TextBox txtBuiltVolTotalVol;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnTreatRemove;
        private System.Windows.Forms.Button btnTreatAdd;
        private System.Windows.Forms.Button btnTreatRefresh;
        private System.Windows.Forms.Button btnTreatApply;
        private System.Windows.Forms.TextBox txtTreatTotalVol;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.DataGridView dgvTrans;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.GroupBox grbDipRetDaily;
        private System.Windows.Forms.GroupBox grbDipRetAtTheEnd;
        private System.Windows.Forms.Button btnDispRetApply;
        private System.Windows.Forms.ContextMenuStrip ctxDFSMnu;
        private System.Windows.Forms.ToolStripMenuItem ctxDFSMnuRemove;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem ctxDFSMnuSetAsCurrent;
        private System.Windows.Forms.ContextMenuStrip ctxDFSMnu_Parent;
        private System.Windows.Forms.ToolStripMenuItem ctxDFSMnu_ParentAdd;
        private System.Windows.Forms.Button btnGeneralApply;
        private System.Windows.Forms.GroupBox groupBox18;
        private System.Windows.Forms.DataGridView dgvTotalWellVol;
        private System.Windows.Forms.GroupBox groupBox17;
        private System.Windows.Forms.Button btnTotalWellVolRefresh;
        private System.Windows.Forms.Button btnCMTApply;
        private System.Windows.Forms.Button btnCMTRefresh;
        private System.Windows.Forms.Label lblSelectedDFS;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Button btnGeneralStartVol;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column22;
        private System.Windows.Forms.Button btnTransAutoCompute;
        private System.Windows.Forms.Button btnBuiltUsingOthersAutoCompute;
        private System.Windows.Forms.DataGridViewTextBoxColumn recID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column30;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column24;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column28;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn transID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column31;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column25;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column29;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn builtVolID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column32;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn19;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn20;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column11;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column12;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column13;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn21;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn22;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn23;
        private System.Windows.Forms.DataGridViewTextBoxColumn treatedID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column34;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn31;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn32;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn33;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column18;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column19;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column20;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column21;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn34;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn35;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn36;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn37;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn38;
        private System.Windows.Forms.DataGridViewTextBoxColumn pitVolId;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column27;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn pitID;
        private System.Windows.Forms.Button btnRecAutoCompute;
        private System.Windows.Forms.DataGridViewTextBoxColumn builtUsingID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column33;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column23;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn12;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn17;
        private System.Windows.Forms.DataGridViewTextBoxColumn totalWellID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column26;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.Button btnDfsCheckDFSs;
        private System.Windows.Forms.Button btnPitTankCheckVolumes;
    }
}