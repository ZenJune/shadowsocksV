namespace TestListener
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnTryOnce = new System.Windows.Forms.Button();
            this.btnTryMultTime = new System.Windows.Forms.Button();
            this.lblRst = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnTryOnce
            // 
            this.btnTryOnce.Location = new System.Drawing.Point(39, 35);
            this.btnTryOnce.Name = "btnTryOnce";
            this.btnTryOnce.Size = new System.Drawing.Size(148, 53);
            this.btnTryOnce.TabIndex = 0;
            this.btnTryOnce.Text = "btnTryOnce";
            this.btnTryOnce.UseVisualStyleBackColor = true;
            this.btnTryOnce.Click += new System.EventHandler(this.btnTryOnce_Click);
            // 
            // btnTryMultTime
            // 
            this.btnTryMultTime.Location = new System.Drawing.Point(39, 94);
            this.btnTryMultTime.Name = "btnTryMultTime";
            this.btnTryMultTime.Size = new System.Drawing.Size(148, 53);
            this.btnTryMultTime.TabIndex = 0;
            this.btnTryMultTime.Text = "btnTryMultTime";
            this.btnTryMultTime.UseVisualStyleBackColor = true;
            this.btnTryMultTime.Click += new System.EventHandler(this.btnTryMultTime_Click);
            // 
            // lblRst
            // 
            this.lblRst.AutoSize = true;
            this.lblRst.Location = new System.Drawing.Point(39, 13);
            this.lblRst.Name = "lblRst";
            this.lblRst.Size = new System.Drawing.Size(41, 12);
            this.lblRst.TabIndex = 1;
            this.lblRst.Text = "label1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lblRst);
            this.Controls.Add(this.btnTryMultTime);
            this.Controls.Add(this.btnTryOnce);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnTryOnce;
        private System.Windows.Forms.Button btnTryMultTime;
        private System.Windows.Forms.Label lblRst;
    }
}

