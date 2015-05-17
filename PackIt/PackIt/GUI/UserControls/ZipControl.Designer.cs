namespace PackIt.GUI
{
    partial class ZipControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnTo = new System.Windows.Forms.Button();
            this.txbTo = new System.Windows.Forms.TextBox();
            this.lblTo = new System.Windows.Forms.Label();
            this.btnFrom = new System.Windows.Forms.Button();
            this.txbFrom = new System.Windows.Forms.TextBox();
            this.lblFrom = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnTo
            // 
            this.btnTo.Location = new System.Drawing.Point(476, 32);
            this.btnTo.Name = "btnTo";
            this.btnTo.Size = new System.Drawing.Size(23, 23);
            this.btnTo.TabIndex = 14;
            this.btnTo.Text = "...";
            this.btnTo.UseVisualStyleBackColor = true;
            // 
            // txbTo
            // 
            this.txbTo.Location = new System.Drawing.Point(93, 34);
            this.txbTo.Name = "txbTo";
            this.txbTo.Size = new System.Drawing.Size(377, 20);
            this.txbTo.TabIndex = 13;
            // 
            // lblTo
            // 
            this.lblTo.AutoSize = true;
            this.lblTo.Location = new System.Drawing.Point(5, 37);
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new System.Drawing.Size(20, 13);
            this.lblTo.TabIndex = 12;
            this.lblTo.Text = "To";
            // 
            // btnFrom
            // 
            this.btnFrom.Location = new System.Drawing.Point(476, 3);
            this.btnFrom.Name = "btnFrom";
            this.btnFrom.Size = new System.Drawing.Size(23, 23);
            this.btnFrom.TabIndex = 11;
            this.btnFrom.Text = "...";
            this.btnFrom.UseVisualStyleBackColor = true;
            // 
            // txbFrom
            // 
            this.txbFrom.Location = new System.Drawing.Point(93, 5);
            this.txbFrom.Name = "txbFrom";
            this.txbFrom.Size = new System.Drawing.Size(377, 20);
            this.txbFrom.TabIndex = 10;
            // 
            // lblFrom
            // 
            this.lblFrom.AutoSize = true;
            this.lblFrom.Location = new System.Drawing.Point(5, 8);
            this.lblFrom.Name = "lblFrom";
            this.lblFrom.Size = new System.Drawing.Size(30, 13);
            this.lblFrom.TabIndex = 9;
            this.lblFrom.Text = "From";
            // 
            // ZipControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnTo);
            this.Controls.Add(this.txbTo);
            this.Controls.Add(this.lblTo);
            this.Controls.Add(this.btnFrom);
            this.Controls.Add(this.txbFrom);
            this.Controls.Add(this.lblFrom);
            this.Name = "ZipControl";
            this.Size = new System.Drawing.Size(507, 150);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnTo;
        private System.Windows.Forms.TextBox txbTo;
        private System.Windows.Forms.Label lblTo;
        private System.Windows.Forms.Button btnFrom;
        private System.Windows.Forms.TextBox txbFrom;
        private System.Windows.Forms.Label lblFrom;
    }
}
