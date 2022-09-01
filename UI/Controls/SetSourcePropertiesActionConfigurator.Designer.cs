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
            this.propertiesControlContainer = new System.Windows.Forms.Panel();
            this.pnlCaptureStateContainer = new System.Windows.Forms.Panel();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.btnCaptureState = new SuchByte.MacroDeck.GUI.CustomControls.ButtonPrimary();
            this.propertiesScrollContainer = new System.Windows.Forms.Panel();
            this.tblSourceStateContainer = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.ddlItem = new SuchByte.MacroDeck.GUI.CustomControls.ComboBox();
            this.lblSceneItem = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.propertiesControlContainer.SuspendLayout();
            this.pnlCaptureStateContainer.SuspendLayout();
            this.propertiesScrollContainer.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolTip1
            // 
            this.toolTip1.AutomaticDelay = 250;
            this.toolTip1.AutoPopDelay = 10000000;
            this.toolTip1.InitialDelay = 250;
            this.toolTip1.IsBalloon = true;
            this.toolTip1.ReshowDelay = 50;
            this.toolTip1.ToolTipTitle = "How to Capture";
            // 
            // propertiesControlContainer
            // 
            this.propertiesControlContainer.Controls.Add(this.pnlCaptureStateContainer);
            this.propertiesControlContainer.Controls.Add(this.propertiesScrollContainer);
            this.propertiesControlContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertiesControlContainer.Location = new System.Drawing.Point(109, 33);
            this.propertiesControlContainer.Name = "propertiesControlContainer";
            this.propertiesControlContainer.Size = new System.Drawing.Size(305, 159);
            this.propertiesControlContainer.TabIndex = 8;
            // 
            // pnlCaptureStateContainer
            // 
            this.pnlCaptureStateContainer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlCaptureStateContainer.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnlCaptureStateContainer.Controls.Add(this.linkLabel1);
            this.pnlCaptureStateContainer.Controls.Add(this.btnCaptureState);
            this.pnlCaptureStateContainer.Location = new System.Drawing.Point(0, 0);
            this.pnlCaptureStateContainer.Name = "pnlCaptureStateContainer";
            this.pnlCaptureStateContainer.Padding = new System.Windows.Forms.Padding(0, 3, 0, 6);
            this.pnlCaptureStateContainer.Size = new System.Drawing.Size(305, 33);
            this.pnlCaptureStateContainer.TabIndex = 4;
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.linkLabel1.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(103)))), ((int)(((byte)(205)))));
            this.linkLabel1.Location = new System.Drawing.Point(286, 3);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(19, 23);
            this.linkLabel1.TabIndex = 2;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "?";
            this.linkLabel1.MouseEnter += new System.EventHandler(this.showTooltip);
            this.linkLabel1.MouseLeave += new System.EventHandler(this.hideTooltip);
            // 
            // btnCaptureState
            // 
            this.btnCaptureState.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCaptureState.BorderRadius = 5;
            this.btnCaptureState.FlatAppearance.BorderSize = 0;
            this.btnCaptureState.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCaptureState.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnCaptureState.ForeColor = System.Drawing.Color.White;
            this.btnCaptureState.HoverColor = System.Drawing.Color.Empty;
            this.btnCaptureState.Icon = null;
            this.btnCaptureState.Location = new System.Drawing.Point(0, 3);
            this.btnCaptureState.Name = "btnCaptureState";
            this.btnCaptureState.Progress = 0;
            this.btnCaptureState.ProgressColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(103)))), ((int)(((byte)(205)))));
            this.btnCaptureState.Size = new System.Drawing.Size(280, 24);
            this.btnCaptureState.TabIndex = 1;
            this.btnCaptureState.Text = "Capture State";
            this.btnCaptureState.UseVisualStyleBackColor = true;
            this.btnCaptureState.UseWindowsAccentColor = true;
            this.btnCaptureState.Click += new System.EventHandler(this.onCaptureState);
            // 
            // propertiesScrollContainer
            // 
            this.propertiesScrollContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.propertiesScrollContainer.AutoScroll = true;
            this.propertiesScrollContainer.Controls.Add(this.tblSourceStateContainer);
            this.propertiesScrollContainer.Location = new System.Drawing.Point(0, 36);
            this.propertiesScrollContainer.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.propertiesScrollContainer.Name = "propertiesScrollContainer";
            this.propertiesScrollContainer.Size = new System.Drawing.Size(305, 123);
            this.propertiesScrollContainer.TabIndex = 1;
            // 
            // tblSourceStateContainer
            // 
            this.tblSourceStateContainer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tblSourceStateContainer.AutoSize = true;
            this.tblSourceStateContainer.ColumnCount = 2;
            this.tblSourceStateContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tblSourceStateContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tblSourceStateContainer.Location = new System.Drawing.Point(0, 0);
            this.tblSourceStateContainer.Name = "tblSourceStateContainer";
            this.tblSourceStateContainer.RowCount = 1;
            this.tblSourceStateContainer.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblSourceStateContainer.Size = new System.Drawing.Size(305, 116);
            this.tblSourceStateContainer.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(3, 33);
            this.label2.Margin = new System.Windows.Forms.Padding(3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 159);
            this.label2.TabIndex = 4;
            this.label2.Text = "Properties:";
            // 
            // ddlItem
            // 
            this.ddlItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.ddlItem.BorderRadius = 8;
            this.ddlItem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ddlItem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlItem.ForeColor = System.Drawing.Color.White;
            this.ddlItem.FormattingEnabled = true;
            this.ddlItem.Location = new System.Drawing.Point(109, 3);
            this.ddlItem.Name = "ddlItem";
            this.ddlItem.Size = new System.Drawing.Size(305, 24);
            this.ddlItem.TabIndex = 7;
            // 
            // lblSceneItem
            // 
            this.lblSceneItem.AutoSize = true;
            this.lblSceneItem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSceneItem.Location = new System.Drawing.Point(3, 3);
            this.lblSceneItem.Margin = new System.Windows.Forms.Padding(3);
            this.lblSceneItem.Name = "lblSceneItem";
            this.lblSceneItem.Padding = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.lblSceneItem.Size = new System.Drawing.Size(100, 24);
            this.lblSceneItem.TabIndex = 6;
            this.lblSceneItem.Text = "Item:";
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
            this.tableLayoutPanel1.Controls.Add(this.propertiesControlContainer, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(417, 195);
            this.tableLayoutPanel1.TabIndex = 11;
            // 
            // SetSourcePropertiesActionConfigurator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "SetSourcePropertiesActionConfigurator";
            this.Size = new System.Drawing.Size(417, 195);
            this.propertiesControlContainer.ResumeLayout(false);
            this.pnlCaptureStateContainer.ResumeLayout(false);
            this.pnlCaptureStateContainer.PerformLayout();
            this.propertiesScrollContainer.ResumeLayout(false);
            this.propertiesScrollContainer.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Panel propertiesControlContainer;
        private System.Windows.Forms.Panel propertiesScrollContainer;
        private System.Windows.Forms.TableLayoutPanel tblSourceStateContainer;
        private System.Windows.Forms.Label label2;
        private SuchByte.MacroDeck.GUI.CustomControls.ComboBox ddlItem;
        private System.Windows.Forms.Label lblSceneItem;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel pnlCaptureStateContainer;
        private SuchByte.MacroDeck.GUI.CustomControls.ButtonPrimary btnCaptureState;
        private System.Windows.Forms.LinkLabel linkLabel1;
    }
}