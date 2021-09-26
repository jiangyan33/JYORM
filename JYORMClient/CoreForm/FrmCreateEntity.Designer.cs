
namespace JYORMClient.CoreForm
{
    partial class FrmCreateEntity
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
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("", -1);
            this.panelTop = new Infragistics.Win.Misc.UltraPanel();
            this.panelContent = new Infragistics.Win.Misc.UltraPanel();
            this.panelBottom = new Infragistics.Win.Misc.UltraPanel();
            this.grid = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.panelTop.SuspendLayout();
            this.panelContent.ClientArea.SuspendLayout();
            this.panelContent.SuspendLayout();
            this.panelBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(800, 74);
            this.panelTop.TabIndex = 0;
            // 
            // panelContent
            // 
            // 
            // panelContent.ClientArea
            // 
            this.panelContent.ClientArea.Controls.Add(this.grid);
            this.panelContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContent.Location = new System.Drawing.Point(0, 74);
            this.panelContent.Name = "panelContent";
            this.panelContent.Size = new System.Drawing.Size(800, 312);
            this.panelContent.TabIndex = 1;
            // 
            // panelBottom
            // 
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Location = new System.Drawing.Point(0, 386);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(800, 64);
            this.panelBottom.TabIndex = 2;
            // 
            // grid
            // 
            appearance1.BackColor = System.Drawing.Color.White;
            this.grid.DisplayLayout.Appearance = appearance1;
            ultraGridBand1.Override.DefaultRowHeight = 40;
            ultraGridBand1.Override.RowSizing = Infragistics.Win.UltraWinGrid.RowSizing.Free;
            this.grid.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            this.grid.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            this.grid.DisplayLayout.NoDataSourceMessageText = "请直接开门后将耗材放入柜中！确保称重耗材放入正确的存放位置！扫码耗材无需扫码放入！";
            this.grid.DisplayLayout.Override.NoRowsInDataSourceMessageText = "没有取用记录";
            this.grid.DisplayLayout.Override.NoVisibleRowsMessageText = "没有匹配到相应的筛选结果";
            this.grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grid.Location = new System.Drawing.Point(0, 0);
            this.grid.Name = "grid";
            this.grid.Size = new System.Drawing.Size(800, 312);
            this.grid.TabIndex = 7;
            this.grid.Text = "ultraGrid1";
            // 
            // FrmCreateEntity
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panelContent);
            this.Controls.Add(this.panelBottom);
            this.Controls.Add(this.panelTop);
            this.Name = "FrmCreateEntity";
            this.Text = "FrmCreateEntity";
            this.Load += new System.EventHandler(this.FrmCreateEntity_Load);
            this.panelTop.ResumeLayout(false);
            this.panelContent.ClientArea.ResumeLayout(false);
            this.panelContent.ResumeLayout(false);
            this.panelBottom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Infragistics.Win.Misc.UltraPanel panelTop;
        private Infragistics.Win.Misc.UltraPanel panelContent;
        private Infragistics.Win.Misc.UltraPanel panelBottom;
        private Infragistics.Win.UltraWinGrid.UltraGrid grid;
    }
}