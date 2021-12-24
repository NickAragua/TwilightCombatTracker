
namespace TwilightCombatTracker
{
    partial class frmGlobalFlags
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
            this.lstBluModifiers = new System.Windows.Forms.ListBox();
            this.lstOpModifiers = new System.Windows.Forms.ListBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lstBluModifiers
            // 
            this.lstBluModifiers.FormattingEnabled = true;
            this.lstBluModifiers.Location = new System.Drawing.Point(12, 12);
            this.lstBluModifiers.Name = "lstBluModifiers";
            this.lstBluModifiers.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstBluModifiers.Size = new System.Drawing.Size(125, 264);
            this.lstBluModifiers.TabIndex = 0;
            // 
            // lstOpModifiers
            // 
            this.lstOpModifiers.FormattingEnabled = true;
            this.lstOpModifiers.Location = new System.Drawing.Point(143, 12);
            this.lstOpModifiers.Name = "lstOpModifiers";
            this.lstOpModifiers.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstOpModifiers.Size = new System.Drawing.Size(127, 264);
            this.lstOpModifiers.TabIndex = 1;
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(12, 282);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 2;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // frmGlobalFlags
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(279, 309);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.lstOpModifiers);
            this.Controls.Add(this.lstBluModifiers);
            this.Name = "frmGlobalFlags";
            this.Text = "Global Flags";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lstBluModifiers;
        private System.Windows.Forms.ListBox lstOpModifiers;
        private System.Windows.Forms.Button btnOk;
    }
}