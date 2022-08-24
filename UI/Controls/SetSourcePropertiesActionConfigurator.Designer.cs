namespace RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.UI.Controls
{
    partial class SetSourcePropertiesActionConfigurator
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
                DuplicateSource = null;
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
            this.components = new System.ComponentModel.Container();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblSceneItem = new System.Windows.Forms.Label();
            this.ddlItem = new SuchByte.MacroDeck.GUI.CustomControls.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.frmProperties = new RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.UI.Controls.OBS.OBSFormDataControl();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.lblSceneItem, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.ddlItem, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(477, 142);
            this.tableLayoutPanel1.TabIndex = 11;
            // 
            // lblSceneItem
            // 
            this.lblSceneItem.AutoSize = true;
            this.lblSceneItem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSceneItem.Location = new System.Drawing.Point(3, 3);
            this.lblSceneItem.Margin = new System.Windows.Forms.Padding(3);
            this.lblSceneItem.Name = "lblSceneItem";
            this.lblSceneItem.Padding = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.lblSceneItem.Size = new System.Drawing.Size(84, 24);
            this.lblSceneItem.TabIndex = 6;
            this.lblSceneItem.Text = "Item:";
            // 
            // ddlItem
            // 
            this.ddlItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.ddlItem.BorderRadius = 8;
            this.ddlItem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ddlItem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlItem.ForeColor = System.Drawing.Color.White;
            this.ddlItem.FormattingEnabled = true;
            this.ddlItem.Location = new System.Drawing.Point(93, 3);
            this.ddlItem.Name = "ddlItem";
            this.ddlItem.Size = new System.Drawing.Size(381, 24);
            this.ddlItem.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(3, 33);
            this.label2.Margin = new System.Windows.Forms.Padding(3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 106);
            this.label2.TabIndex = 4;
            this.label2.Text = "Settings:";
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.frmProperties);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(93, 33);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(381, 106);
            this.panel1.TabIndex = 8;
            // 
            // frmProperties
            // 
            this.frmProperties.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.frmProperties.AutoSize = true;
            this.frmProperties.BackColor = System.Drawing.Color.Transparent;
            this.frmProperties.BorderColor = System.Drawing.Color.Black;
            this.frmProperties.BorderWidth = 1;
            this.frmProperties.Location = new System.Drawing.Point(0, 0);
            this.frmProperties.Margin = new System.Windows.Forms.Padding(0);
            this.frmProperties.Name = "frmProperties";
            this.frmProperties.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.frmProperties.Size = new System.Drawing.Size(381, 55);
            this.frmProperties.TabIndex = 12;
            this.frmProperties.Value = null;
            // 
            // SetSourcePropertiesActionConfigurator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "SetSourcePropertiesActionConfigurator";
            this.Size = new System.Drawing.Size(477, 142);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lblSceneItem;
        private SuchByte.MacroDeck.GUI.CustomControls.ComboBox ddlItem;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private OBS.OBSFormDataControl frmProperties;
    }
}