
namespace JYORMClient
{
    partial class MainForm
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
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            this.btnCreateEntity = new Infragistics.Win.Misc.UltraButton();
            this.panelContent = new Infragistics.Win.Misc.UltraPanel();
            this.panelContent.ClientArea.SuspendLayout();
            this.panelContent.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCreateEntity
            // 
            appearance1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            appearance1.BackColor2 = System.Drawing.Color.DodgerBlue;
            appearance1.ForeColor = System.Drawing.Color.White;
            appearance1.TextHAlignAsString = "Center";
            appearance1.TextVAlignAsString = "Middle";
            this.btnCreateEntity.Appearance = appearance1;
            this.btnCreateEntity.Font = new System.Drawing.Font("微软雅黑", 20F);
            this.btnCreateEntity.Location = new System.Drawing.Point(41, 34);
            this.btnCreateEntity.Name = "btnCreateEntity";
            this.btnCreateEntity.Size = new System.Drawing.Size(221, 120);
            this.btnCreateEntity.TabIndex = 0;
            this.btnCreateEntity.Text = "生成实体类";
            // 
            // panelContent
            // 
            // 
            // panelContent.ClientArea
            // 
            this.panelContent.ClientArea.Controls.Add(this.btnCreateEntity);
            this.panelContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContent.Location = new System.Drawing.Point(0, 0);
            this.panelContent.Name = "panelContent";
            this.panelContent.Size = new System.Drawing.Size(800, 450);
            this.panelContent.TabIndex = 1;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panelContent);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.panelContent.ClientArea.ResumeLayout(false);
            this.panelContent.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Infragistics.Win.Misc.UltraButton btnCreateEntity;
        private Infragistics.Win.Misc.UltraPanel panelContent;
    }
}

