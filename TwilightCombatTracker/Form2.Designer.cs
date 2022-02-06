
namespace TwilightCombatTracker
{
    partial class frmAddUnit
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
            this.txtUnitName = new System.Windows.Forms.TextBox();
            this.lstUnitTags = new System.Windows.Forms.ListBox();
            this.btnAddUnit = new System.Windows.Forms.Button();
            this.lstUnitEquipment = new System.Windows.Forms.ListBox();
            this.numSpeed = new System.Windows.Forms.NumericUpDown();
            this.numHealth = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cboBunker = new System.Windows.Forms.ComboBox();
            this.txtCombatXP = new System.Windows.Forms.TextBox();
            this.txtInitXP = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnPrev = new System.Windows.Forms.Button();
            this.btnSaveUnit = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numSpeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHealth)).BeginInit();
            this.SuspendLayout();
            // 
            // txtUnitName
            // 
            this.txtUnitName.Location = new System.Drawing.Point(12, 12);
            this.txtUnitName.Name = "txtUnitName";
            this.txtUnitName.Size = new System.Drawing.Size(172, 20);
            this.txtUnitName.TabIndex = 0;
            // 
            // lstUnitTags
            // 
            this.lstUnitTags.FormattingEnabled = true;
            this.lstUnitTags.Location = new System.Drawing.Point(12, 38);
            this.lstUnitTags.Name = "lstUnitTags";
            this.lstUnitTags.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstUnitTags.Size = new System.Drawing.Size(172, 95);
            this.lstUnitTags.TabIndex = 1;
            // 
            // btnAddUnit
            // 
            this.btnAddUnit.Location = new System.Drawing.Point(12, 398);
            this.btnAddUnit.Name = "btnAddUnit";
            this.btnAddUnit.Size = new System.Drawing.Size(75, 23);
            this.btnAddUnit.TabIndex = 2;
            this.btnAddUnit.Text = "Add Unit";
            this.btnAddUnit.UseVisualStyleBackColor = true;
            // 
            // lstUnitEquipment
            // 
            this.lstUnitEquipment.FormattingEnabled = true;
            this.lstUnitEquipment.Location = new System.Drawing.Point(12, 139);
            this.lstUnitEquipment.Name = "lstUnitEquipment";
            this.lstUnitEquipment.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstUnitEquipment.Size = new System.Drawing.Size(172, 121);
            this.lstUnitEquipment.TabIndex = 3;
            // 
            // numSpeed
            // 
            this.numSpeed.Location = new System.Drawing.Point(12, 266);
            this.numSpeed.Name = "numSpeed";
            this.numSpeed.Size = new System.Drawing.Size(75, 20);
            this.numSpeed.TabIndex = 5;
            this.numSpeed.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numHealth
            // 
            this.numHealth.Location = new System.Drawing.Point(12, 292);
            this.numHealth.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numHealth.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.numHealth.Name = "numHealth";
            this.numHealth.Size = new System.Drawing.Size(75, 20);
            this.numHealth.TabIndex = 6;
            this.numHealth.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(93, 268);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Speed";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(96, 294);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Health";
            // 
            // cboBunker
            // 
            this.cboBunker.FormattingEnabled = true;
            this.cboBunker.Location = new System.Drawing.Point(12, 318);
            this.cboBunker.Name = "cboBunker";
            this.cboBunker.Size = new System.Drawing.Size(121, 21);
            this.cboBunker.TabIndex = 9;
            // 
            // txtCombatXP
            // 
            this.txtCombatXP.Location = new System.Drawing.Point(12, 372);
            this.txtCombatXP.Name = "txtCombatXP";
            this.txtCombatXP.Size = new System.Drawing.Size(100, 20);
            this.txtCombatXP.TabIndex = 10;
            // 
            // txtInitXP
            // 
            this.txtInitXP.Location = new System.Drawing.Point(12, 345);
            this.txtInitXP.Name = "txtInitXP";
            this.txtInitXP.Size = new System.Drawing.Size(100, 20);
            this.txtInitXP.TabIndex = 11;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(118, 375);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Combat XP";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(118, 348);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Init XP";
            // 
            // btnNext
            // 
            this.btnNext.Enabled = false;
            this.btnNext.Location = new System.Drawing.Point(143, 398);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(41, 23);
            this.btnNext.TabIndex = 14;
            this.btnNext.Text = "Next";
            this.btnNext.UseVisualStyleBackColor = true;
            // 
            // btnPrev
            // 
            this.btnPrev.Enabled = false;
            this.btnPrev.Location = new System.Drawing.Point(93, 398);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(44, 23);
            this.btnPrev.TabIndex = 15;
            this.btnPrev.Text = "Prev";
            this.btnPrev.UseVisualStyleBackColor = true;
            // 
            // btnSaveUnit
            // 
            this.btnSaveUnit.Location = new System.Drawing.Point(12, 427);
            this.btnSaveUnit.Name = "btnSaveUnit";
            this.btnSaveUnit.Size = new System.Drawing.Size(75, 23);
            this.btnSaveUnit.TabIndex = 16;
            this.btnSaveUnit.Text = "Save Unit";
            this.btnSaveUnit.UseVisualStyleBackColor = true;
            // 
            // frmAddUnit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(190, 455);
            this.Controls.Add(this.btnSaveUnit);
            this.Controls.Add(this.btnPrev);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtInitXP);
            this.Controls.Add(this.txtCombatXP);
            this.Controls.Add(this.cboBunker);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numHealth);
            this.Controls.Add(this.numSpeed);
            this.Controls.Add(this.lstUnitEquipment);
            this.Controls.Add(this.btnAddUnit);
            this.Controls.Add(this.lstUnitTags);
            this.Controls.Add(this.txtUnitName);
            this.Name = "frmAddUnit";
            this.Text = "Add/Edit Unit";
            ((System.ComponentModel.ISupportInitialize)(this.numSpeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHealth)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtUnitName;
        private System.Windows.Forms.ListBox lstUnitTags;
        private System.Windows.Forms.Button btnAddUnit;
        private System.Windows.Forms.ListBox lstUnitEquipment;
        private System.Windows.Forms.NumericUpDown numSpeed;
        private System.Windows.Forms.NumericUpDown numHealth;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboBunker;
        private System.Windows.Forms.TextBox txtCombatXP;
        private System.Windows.Forms.TextBox txtInitXP;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnPrev;
        private System.Windows.Forms.Button btnSaveUnit;
    }
}