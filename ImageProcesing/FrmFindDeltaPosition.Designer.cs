namespace ImageProcesing
{
    partial class FrmFindDeltaPosition
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnGenMap = new System.Windows.Forms.Button();
            this.btnCalculate = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.ctrImageViewer = new ImageProcesing.Controls.CtrImageViewer();
            this.ctrOutput = new ImageProcesing.Controls.CtrOutput();
            this.txtPathImageEnd = new ImageProcesing.Controls.CtrFileTextBox();
            this.txtPathImageStart = new ImageProcesing.Controls.CtrFileTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ctrImageViewer)).BeginInit();
            this.SuspendLayout();
            // 
            // btnGenMap
            // 
            this.btnGenMap.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGenMap.Location = new System.Drawing.Point(775, 12);
            this.btnGenMap.Name = "btnGenMap";
            this.btnGenMap.Size = new System.Drawing.Size(114, 26);
            this.btnGenMap.TabIndex = 3;
            this.btnGenMap.Text = "GenMap";
            this.btnGenMap.UseVisualStyleBackColor = true;
            this.btnGenMap.Click += new System.EventHandler(this.btnGenMap_Click);
            // 
            // btnCalculate
            // 
            this.btnCalculate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCalculate.Location = new System.Drawing.Point(775, 44);
            this.btnCalculate.Name = "btnCalculate";
            this.btnCalculate.Size = new System.Drawing.Size(114, 26);
            this.btnCalculate.TabIndex = 4;
            this.btnCalculate.Text = "Calculate";
            this.btnCalculate.UseVisualStyleBackColor = true;
            this.btnCalculate.Click += new System.EventHandler(this.btnCalculate_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(12, 76);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.ctrImageViewer);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.ctrOutput);
            this.splitContainer1.Size = new System.Drawing.Size(877, 920);
            this.splitContainer1.SplitterDistance = 672;
            this.splitContainer1.TabIndex = 5;
            // 
            // ctrImageViewer
            // 
            this.ctrImageViewer.BackColor = System.Drawing.Color.Black;
            this.ctrImageViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctrImageViewer.ImageShow = null;
            this.ctrImageViewer.Location = new System.Drawing.Point(0, 0);
            this.ctrImageViewer.Name = "ctrImageViewer";
            this.ctrImageViewer.Size = new System.Drawing.Size(877, 672);
            this.ctrImageViewer.TabIndex = 0;
            this.ctrImageViewer.TabStop = false;
            // 
            // ctrOutput
            // 
            this.ctrOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctrOutput.Location = new System.Drawing.Point(0, 0);
            this.ctrOutput.Name = "ctrOutput";
            this.ctrOutput.Size = new System.Drawing.Size(877, 244);
            this.ctrOutput.TabIndex = 0;
            this.ctrOutput.Text = "ctrOutput1";
            // 
            // txtPathImageEnd
            // 
            this.txtPathImageEnd.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPathImageEnd.Location = new System.Drawing.Point(12, 44);
            this.txtPathImageEnd.Name = "txtPathImageEnd";
            this.txtPathImageEnd.Size = new System.Drawing.Size(757, 26);
            this.txtPathImageEnd.TabIndex = 1;
            // 
            // txtPathImageStart
            // 
            this.txtPathImageStart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPathImageStart.Location = new System.Drawing.Point(12, 12);
            this.txtPathImageStart.Name = "txtPathImageStart";
            this.txtPathImageStart.Size = new System.Drawing.Size(757, 26);
            this.txtPathImageStart.TabIndex = 0;
            // 
            // FrmFindDeltaPosition
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(901, 1008);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.btnCalculate);
            this.Controls.Add(this.btnGenMap);
            this.Controls.Add(this.txtPathImageEnd);
            this.Controls.Add(this.txtPathImageStart);
            this.Name = "FrmFindDeltaPosition";
            this.Text = "FindDeltaPosition";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ctrImageViewer)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.CtrFileTextBox txtPathImageStart;
        private Controls.CtrFileTextBox txtPathImageEnd;
        private System.Windows.Forms.Button btnGenMap;
        private System.Windows.Forms.Button btnCalculate;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private Controls.CtrImageViewer ctrImageViewer;
        private Controls.CtrOutput ctrOutput;
    }
}

