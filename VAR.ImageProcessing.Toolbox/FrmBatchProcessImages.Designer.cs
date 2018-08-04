namespace VAR.ImageProcessing.Toolbox
{
    partial class FrmBatchProcessImages
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
            this.btnProcess = new System.Windows.Forms.Button();
            this.ctrOutput = new VAR.ImageProcessing.Toolbox.Controls.CtrOutput();
            this.ctrFileTextBox = new VAR.ImageProcessing.Toolbox.Controls.CtrFileTextBox();
            this.txtDestDirectory = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnProcess
            // 
            this.btnProcess.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnProcess.Location = new System.Drawing.Point(648, 12);
            this.btnProcess.Name = "btnProcess";
            this.btnProcess.Size = new System.Drawing.Size(140, 27);
            this.btnProcess.TabIndex = 2;
            this.btnProcess.Text = "Process";
            this.btnProcess.UseVisualStyleBackColor = true;
            this.btnProcess.Click += new System.EventHandler(this.btnProcess_Click);
            // 
            // ctrOutput
            // 
            this.ctrOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ctrOutput.Location = new System.Drawing.Point(12, 77);
            this.ctrOutput.Name = "ctrOutput";
            this.ctrOutput.Size = new System.Drawing.Size(776, 459);
            this.ctrOutput.TabIndex = 1;
            this.ctrOutput.Text = "ctrOutput1";
            // 
            // ctrFileTextBox
            // 
            this.ctrFileTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ctrFileTextBox.DirectoryChooser = true;
            this.ctrFileTextBox.Location = new System.Drawing.Point(13, 13);
            this.ctrFileTextBox.Name = "ctrFileTextBox";
            this.ctrFileTextBox.Size = new System.Drawing.Size(629, 26);
            this.ctrFileTextBox.TabIndex = 0;
            // 
            // txtDestDirectory
            // 
            this.txtDestDirectory.Location = new System.Drawing.Point(13, 45);
            this.txtDestDirectory.Name = "txtDestDirectory";
            this.txtDestDirectory.Size = new System.Drawing.Size(189, 26);
            this.txtDestDirectory.TabIndex = 3;
            this.txtDestDirectory.Text = "Processed";
            // 
            // FrmBatchProcessImages
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 548);
            this.Controls.Add(this.txtDestDirectory);
            this.Controls.Add(this.btnProcess);
            this.Controls.Add(this.ctrOutput);
            this.Controls.Add(this.ctrFileTextBox);
            this.Name = "FrmBatchProcessImages";
            this.Text = "BatchProcessImages";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.CtrFileTextBox ctrFileTextBox;
        private Controls.CtrOutput ctrOutput;
        private System.Windows.Forms.Button btnProcess;
        private System.Windows.Forms.TextBox txtDestDirectory;
    }
}