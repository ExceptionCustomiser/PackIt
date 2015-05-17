namespace PackIt.GUI
{
    partial class ProjectControl
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
            this.lblFile = new System.Windows.Forms.Label();
            this.txbFolder = new System.Windows.Forms.TextBox();
            this.btnFolder = new System.Windows.Forms.Button();
            this.lbxTask = new System.Windows.Forms.ListBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.lblTask = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblFile
            // 
            this.lblFile.AutoSize = true;
            this.lblFile.Location = new System.Drawing.Point(3, 9);
            this.lblFile.Name = "lblFile";
            this.lblFile.Size = new System.Drawing.Size(23, 13);
            this.lblFile.TabIndex = 0;
            this.lblFile.Text = "File";
            // 
            // txbFolder
            // 
            this.txbFolder.Location = new System.Drawing.Point(91, 6);
            this.txbFolder.Name = "txbFolder";
            this.txbFolder.Size = new System.Drawing.Size(377, 20);
            this.txbFolder.TabIndex = 1;
            this.txbFolder.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txbFolder_KeyDown);
            // 
            // btnFolder
            // 
            this.btnFolder.Location = new System.Drawing.Point(474, 4);
            this.btnFolder.Name = "btnFolder";
            this.btnFolder.Size = new System.Drawing.Size(23, 23);
            this.btnFolder.TabIndex = 2;
            this.btnFolder.Text = "...";
            this.btnFolder.UseVisualStyleBackColor = true;
            this.btnFolder.Click += new System.EventHandler(this.btnFolder_Click);
            // 
            // lbxTask
            // 
            this.lbxTask.FormattingEnabled = true;
            this.lbxTask.Location = new System.Drawing.Point(91, 32);
            this.lbxTask.Name = "lbxTask";
            this.lbxTask.Size = new System.Drawing.Size(174, 95);
            this.lbxTask.TabIndex = 3;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(271, 32);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 4;
            this.btnAdd.Text = "+";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(271, 61);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(75, 23);
            this.btnRemove.TabIndex = 5;
            this.btnRemove.Text = "-";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // lblTask
            // 
            this.lblTask.AutoSize = true;
            this.lblTask.Location = new System.Drawing.Point(3, 32);
            this.lblTask.Name = "lblTask";
            this.lblTask.Size = new System.Drawing.Size(36, 13);
            this.lblTask.TabIndex = 6;
            this.lblTask.Text = "Tasks";
            // 
            // ProjectControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblTask);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.lbxTask);
            this.Controls.Add(this.btnFolder);
            this.Controls.Add(this.txbFolder);
            this.Controls.Add(this.lblFile);
            this.Name = "ProjectControl";
            this.Size = new System.Drawing.Size(500, 136);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblFile;
        private System.Windows.Forms.TextBox txbFolder;
        private System.Windows.Forms.Button btnFolder;
        private System.Windows.Forms.ListBox lbxTask;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Label lblTask;

    }
}
