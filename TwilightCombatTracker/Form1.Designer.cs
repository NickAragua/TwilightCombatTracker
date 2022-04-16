
namespace TwilightCombatTracker
{
    partial class frmCombatTracker
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
            this.lstBlueFor = new System.Windows.Forms.ListBox();
            this.lstOpfor = new System.Windows.Forms.ListBox();
            this.txtCombatPreview = new System.Windows.Forms.TextBox();
            this.btnAddBluforUnit = new System.Windows.Forms.Button();
            this.btnCommitEngagement = new System.Windows.Forms.Button();
            this.btnAddOpfor = new System.Windows.Forms.Button();
            this.txtAccumulator = new System.Windows.Forms.TextBox();
            this.btnRollInit = new System.Windows.Forms.Button();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.btnManageGlobalModifiers = new System.Windows.Forms.Button();
            this.btnEndRound = new System.Windows.Forms.Button();
            this.lstEngagements = new System.Windows.Forms.ListBox();
            this.btnRunEngagement = new System.Windows.Forms.Button();
            this.btnSaveBlueUnits = new System.Windows.Forms.Button();
            this.btnLoadBlueUnits = new System.Windows.Forms.Button();
            this.btnSaveOpUnits = new System.Windows.Forms.Button();
            this.btnLoadOpUnits = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.btnEditBluforUnit = new System.Windows.Forms.Button();
            this.btnEditOpforUnit = new System.Windows.Forms.Button();
            this.btnFlipEngagement = new System.Windows.Forms.Button();
            this.btnClearLog = new System.Windows.Forms.Button();
            this.btnDeleteBlue = new System.Windows.Forms.Button();
            this.btnDeleteRed = new System.Windows.Forms.Button();
            this.btnDefectRed = new System.Windows.Forms.Button();
            this.btnDefectBlue = new System.Windows.Forms.Button();
            this.chkClearOnLoad = new System.Windows.Forms.CheckBox();
            this.chkAutoWithdraw = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // lstBlueFor
            // 
            this.lstBlueFor.FormattingEnabled = true;
            this.lstBlueFor.HorizontalScrollbar = true;
            this.lstBlueFor.Location = new System.Drawing.Point(0, 0);
            this.lstBlueFor.Name = "lstBlueFor";
            this.lstBlueFor.Size = new System.Drawing.Size(158, 290);
            this.lstBlueFor.TabIndex = 0;
            // 
            // lstOpfor
            // 
            this.lstOpfor.FormattingEnabled = true;
            this.lstOpfor.HorizontalScrollbar = true;
            this.lstOpfor.Location = new System.Drawing.Point(164, 0);
            this.lstOpfor.Name = "lstOpfor";
            this.lstOpfor.Size = new System.Drawing.Size(164, 290);
            this.lstOpfor.TabIndex = 1;
            // 
            // txtCombatPreview
            // 
            this.txtCombatPreview.Location = new System.Drawing.Point(351, 0);
            this.txtCombatPreview.Multiline = true;
            this.txtCombatPreview.Name = "txtCombatPreview";
            this.txtCombatPreview.ReadOnly = true;
            this.txtCombatPreview.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtCombatPreview.Size = new System.Drawing.Size(437, 113);
            this.txtCombatPreview.TabIndex = 2;
            // 
            // btnAddBluforUnit
            // 
            this.btnAddBluforUnit.Location = new System.Drawing.Point(0, 297);
            this.btnAddBluforUnit.Name = "btnAddBluforUnit";
            this.btnAddBluforUnit.Size = new System.Drawing.Size(75, 23);
            this.btnAddBluforUnit.TabIndex = 4;
            this.btnAddBluforUnit.Text = "Add Unit";
            this.btnAddBluforUnit.UseVisualStyleBackColor = true;
            // 
            // btnCommitEngagement
            // 
            this.btnCommitEngagement.Enabled = false;
            this.btnCommitEngagement.Location = new System.Drawing.Point(713, 119);
            this.btnCommitEngagement.Name = "btnCommitEngagement";
            this.btnCommitEngagement.Size = new System.Drawing.Size(75, 23);
            this.btnCommitEngagement.TabIndex = 5;
            this.btnCommitEngagement.Text = "Commit";
            this.btnCommitEngagement.UseVisualStyleBackColor = true;
            // 
            // btnAddOpfor
            // 
            this.btnAddOpfor.Location = new System.Drawing.Point(164, 297);
            this.btnAddOpfor.Name = "btnAddOpfor";
            this.btnAddOpfor.Size = new System.Drawing.Size(75, 23);
            this.btnAddOpfor.TabIndex = 6;
            this.btnAddOpfor.Text = "Add Unit";
            this.btnAddOpfor.UseVisualStyleBackColor = true;
            // 
            // txtAccumulator
            // 
            this.txtAccumulator.HideSelection = false;
            this.txtAccumulator.Location = new System.Drawing.Point(351, 148);
            this.txtAccumulator.MaxLength = 1000000;
            this.txtAccumulator.Multiline = true;
            this.txtAccumulator.Name = "txtAccumulator";
            this.txtAccumulator.ReadOnly = true;
            this.txtAccumulator.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtAccumulator.Size = new System.Drawing.Size(437, 142);
            this.txtAccumulator.TabIndex = 7;
            // 
            // btnRollInit
            // 
            this.btnRollInit.Location = new System.Drawing.Point(351, 297);
            this.btnRollInit.Name = "btnRollInit";
            this.btnRollInit.Size = new System.Drawing.Size(75, 23);
            this.btnRollInit.TabIndex = 8;
            this.btnRollInit.Text = "Roll Initiative";
            this.btnRollInit.UseVisualStyleBackColor = true;
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.DefaultExt = "json";
            // 
            // btnManageGlobalModifiers
            // 
            this.btnManageGlobalModifiers.Location = new System.Drawing.Point(713, 297);
            this.btnManageGlobalModifiers.Name = "btnManageGlobalModifiers";
            this.btnManageGlobalModifiers.Size = new System.Drawing.Size(75, 23);
            this.btnManageGlobalModifiers.TabIndex = 9;
            this.btnManageGlobalModifiers.Text = "Global Tags";
            this.btnManageGlobalModifiers.UseVisualStyleBackColor = true;
            // 
            // btnEndRound
            // 
            this.btnEndRound.Location = new System.Drawing.Point(795, 297);
            this.btnEndRound.Name = "btnEndRound";
            this.btnEndRound.Size = new System.Drawing.Size(75, 23);
            this.btnEndRound.TabIndex = 10;
            this.btnEndRound.Text = "Round Over";
            this.btnEndRound.UseVisualStyleBackColor = true;
            // 
            // lstEngagements
            // 
            this.lstEngagements.FormattingEnabled = true;
            this.lstEngagements.Location = new System.Drawing.Point(795, 0);
            this.lstEngagements.Name = "lstEngagements";
            this.lstEngagements.Size = new System.Drawing.Size(388, 290);
            this.lstEngagements.TabIndex = 11;
            // 
            // btnRunEngagement
            // 
            this.btnRunEngagement.Enabled = false;
            this.btnRunEngagement.Location = new System.Drawing.Point(1079, 296);
            this.btnRunEngagement.Name = "btnRunEngagement";
            this.btnRunEngagement.Size = new System.Drawing.Size(104, 23);
            this.btnRunEngagement.TabIndex = 12;
            this.btnRunEngagement.Text = "Run Engagement";
            this.btnRunEngagement.UseVisualStyleBackColor = true;
            // 
            // btnSaveBlueUnits
            // 
            this.btnSaveBlueUnits.Location = new System.Drawing.Point(0, 327);
            this.btnSaveBlueUnits.Name = "btnSaveBlueUnits";
            this.btnSaveBlueUnits.Size = new System.Drawing.Size(75, 23);
            this.btnSaveBlueUnits.TabIndex = 14;
            this.btnSaveBlueUnits.Text = "Save Units";
            this.btnSaveBlueUnits.UseVisualStyleBackColor = true;
            // 
            // btnLoadBlueUnits
            // 
            this.btnLoadBlueUnits.Location = new System.Drawing.Point(82, 327);
            this.btnLoadBlueUnits.Name = "btnLoadBlueUnits";
            this.btnLoadBlueUnits.Size = new System.Drawing.Size(75, 23);
            this.btnLoadBlueUnits.TabIndex = 15;
            this.btnLoadBlueUnits.Text = "Load Units";
            this.btnLoadBlueUnits.UseVisualStyleBackColor = true;
            // 
            // btnSaveOpUnits
            // 
            this.btnSaveOpUnits.Location = new System.Drawing.Point(164, 327);
            this.btnSaveOpUnits.Name = "btnSaveOpUnits";
            this.btnSaveOpUnits.Size = new System.Drawing.Size(75, 23);
            this.btnSaveOpUnits.TabIndex = 16;
            this.btnSaveOpUnits.Text = "Save Units";
            this.btnSaveOpUnits.UseVisualStyleBackColor = true;
            // 
            // btnLoadOpUnits
            // 
            this.btnLoadOpUnits.Location = new System.Drawing.Point(246, 327);
            this.btnLoadOpUnits.Name = "btnLoadOpUnits";
            this.btnLoadOpUnits.Size = new System.Drawing.Size(75, 23);
            this.btnLoadOpUnits.TabIndex = 17;
            this.btnLoadOpUnits.Text = "Load Units";
            this.btnLoadOpUnits.UseVisualStyleBackColor = true;
            // 
            // openFileDialog
            // 
            this.openFileDialog.DefaultExt = "json";
            // 
            // btnEditBluforUnit
            // 
            this.btnEditBluforUnit.Enabled = false;
            this.btnEditBluforUnit.Location = new System.Drawing.Point(82, 298);
            this.btnEditBluforUnit.Name = "btnEditBluforUnit";
            this.btnEditBluforUnit.Size = new System.Drawing.Size(75, 23);
            this.btnEditBluforUnit.TabIndex = 18;
            this.btnEditBluforUnit.Text = "Edit Unit";
            this.btnEditBluforUnit.UseVisualStyleBackColor = true;
            // 
            // btnEditOpforUnit
            // 
            this.btnEditOpforUnit.Enabled = false;
            this.btnEditOpforUnit.Location = new System.Drawing.Point(246, 298);
            this.btnEditOpforUnit.Name = "btnEditOpforUnit";
            this.btnEditOpforUnit.Size = new System.Drawing.Size(75, 23);
            this.btnEditOpforUnit.TabIndex = 19;
            this.btnEditOpforUnit.Text = "Edit Unit";
            this.btnEditOpforUnit.UseVisualStyleBackColor = true;
            // 
            // btnFlipEngagement
            // 
            this.btnFlipEngagement.Enabled = false;
            this.btnFlipEngagement.Location = new System.Drawing.Point(632, 119);
            this.btnFlipEngagement.Name = "btnFlipEngagement";
            this.btnFlipEngagement.Size = new System.Drawing.Size(75, 23);
            this.btnFlipEngagement.TabIndex = 20;
            this.btnFlipEngagement.Text = "Flip Attacker";
            this.btnFlipEngagement.UseVisualStyleBackColor = true;
            // 
            // btnClearLog
            // 
            this.btnClearLog.Location = new System.Drawing.Point(433, 298);
            this.btnClearLog.Name = "btnClearLog";
            this.btnClearLog.Size = new System.Drawing.Size(75, 23);
            this.btnClearLog.TabIndex = 21;
            this.btnClearLog.Text = "Clear Log";
            this.btnClearLog.UseVisualStyleBackColor = true;
            // 
            // btnDeleteBlue
            // 
            this.btnDeleteBlue.Enabled = false;
            this.btnDeleteBlue.Location = new System.Drawing.Point(0, 357);
            this.btnDeleteBlue.Name = "btnDeleteBlue";
            this.btnDeleteBlue.Size = new System.Drawing.Size(75, 23);
            this.btnDeleteBlue.TabIndex = 22;
            this.btnDeleteBlue.Text = "Delete Unit";
            this.btnDeleteBlue.UseVisualStyleBackColor = true;
            // 
            // btnDeleteRed
            // 
            this.btnDeleteRed.Enabled = false;
            this.btnDeleteRed.Location = new System.Drawing.Point(164, 357);
            this.btnDeleteRed.Name = "btnDeleteRed";
            this.btnDeleteRed.Size = new System.Drawing.Size(75, 23);
            this.btnDeleteRed.TabIndex = 23;
            this.btnDeleteRed.Text = "Delete Unit";
            this.btnDeleteRed.UseVisualStyleBackColor = true;
            // 
            // btnDefectRed
            // 
            this.btnDefectRed.Enabled = false;
            this.btnDefectRed.Location = new System.Drawing.Point(246, 357);
            this.btnDefectRed.Name = "btnDefectRed";
            this.btnDefectRed.Size = new System.Drawing.Size(75, 23);
            this.btnDefectRed.TabIndex = 24;
            this.btnDefectRed.Text = "Defect";
            this.btnDefectRed.UseVisualStyleBackColor = true;
            // 
            // btnDefectBlue
            // 
            this.btnDefectBlue.Enabled = false;
            this.btnDefectBlue.Location = new System.Drawing.Point(81, 356);
            this.btnDefectBlue.Name = "btnDefectBlue";
            this.btnDefectBlue.Size = new System.Drawing.Size(75, 23);
            this.btnDefectBlue.TabIndex = 25;
            this.btnDefectBlue.Text = "Defect";
            this.btnDefectBlue.UseVisualStyleBackColor = true;
            // 
            // chkClearOnLoad
            // 
            this.chkClearOnLoad.AutoSize = true;
            this.chkClearOnLoad.Location = new System.Drawing.Point(351, 327);
            this.chkClearOnLoad.Name = "chkClearOnLoad";
            this.chkClearOnLoad.Size = new System.Drawing.Size(141, 17);
            this.chkClearOnLoad.TabIndex = 26;
            this.chkClearOnLoad.Text = "Clear Force List on Load";
            this.chkClearOnLoad.UseVisualStyleBackColor = true;
            // 
            // chkAutoWithdraw
            // 
            this.chkAutoWithdraw.AutoSize = true;
            this.chkAutoWithdraw.Location = new System.Drawing.Point(351, 351);
            this.chkAutoWithdraw.Name = "chkAutoWithdraw";
            this.chkAutoWithdraw.Size = new System.Drawing.Size(137, 17);
            this.chkAutoWithdraw.TabIndex = 27;
            this.chkAutoWithdraw.Text = "Auto Withdraw at 50%?";
            this.chkAutoWithdraw.UseVisualStyleBackColor = true;
            // 
            // frmCombatTracker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1195, 410);
            this.Controls.Add(this.chkAutoWithdraw);
            this.Controls.Add(this.chkClearOnLoad);
            this.Controls.Add(this.btnDefectBlue);
            this.Controls.Add(this.btnDefectRed);
            this.Controls.Add(this.btnDeleteRed);
            this.Controls.Add(this.btnDeleteBlue);
            this.Controls.Add(this.btnClearLog);
            this.Controls.Add(this.btnFlipEngagement);
            this.Controls.Add(this.btnEditOpforUnit);
            this.Controls.Add(this.btnEditBluforUnit);
            this.Controls.Add(this.btnLoadOpUnits);
            this.Controls.Add(this.btnSaveOpUnits);
            this.Controls.Add(this.btnLoadBlueUnits);
            this.Controls.Add(this.btnSaveBlueUnits);
            this.Controls.Add(this.btnRunEngagement);
            this.Controls.Add(this.lstEngagements);
            this.Controls.Add(this.btnEndRound);
            this.Controls.Add(this.btnManageGlobalModifiers);
            this.Controls.Add(this.btnRollInit);
            this.Controls.Add(this.txtAccumulator);
            this.Controls.Add(this.btnAddOpfor);
            this.Controls.Add(this.btnCommitEngagement);
            this.Controls.Add(this.btnAddBluforUnit);
            this.Controls.Add(this.txtCombatPreview);
            this.Controls.Add(this.lstOpfor);
            this.Controls.Add(this.lstBlueFor);
            this.Name = "frmCombatTracker";
            this.Text = "Twilight Combat Tracker";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lstBlueFor;
        private System.Windows.Forms.ListBox lstOpfor;
        private System.Windows.Forms.TextBox txtCombatPreview;
        private System.Windows.Forms.Button btnAddBluforUnit;
        private System.Windows.Forms.Button btnCommitEngagement;
        private System.Windows.Forms.Button btnAddOpfor;
        private System.Windows.Forms.TextBox txtAccumulator;
        private System.Windows.Forms.Button btnRollInit;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.Button btnManageGlobalModifiers;
        private System.Windows.Forms.Button btnEndRound;
        private System.Windows.Forms.ListBox lstEngagements;
        private System.Windows.Forms.Button btnRunEngagement;
        private System.Windows.Forms.Button btnSaveBlueUnits;
        private System.Windows.Forms.Button btnLoadBlueUnits;
        private System.Windows.Forms.Button btnSaveOpUnits;
        private System.Windows.Forms.Button btnLoadOpUnits;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Button btnEditBluforUnit;
        private System.Windows.Forms.Button btnEditOpforUnit;
        private System.Windows.Forms.Button btnFlipEngagement;
        private System.Windows.Forms.Button btnClearLog;
        private System.Windows.Forms.Button btnDeleteBlue;
        private System.Windows.Forms.Button btnDeleteRed;
        private System.Windows.Forms.Button btnDefectRed;
        private System.Windows.Forms.Button btnDefectBlue;
        private System.Windows.Forms.CheckBox chkClearOnLoad;
        private System.Windows.Forms.CheckBox chkAutoWithdraw;
    }
}

