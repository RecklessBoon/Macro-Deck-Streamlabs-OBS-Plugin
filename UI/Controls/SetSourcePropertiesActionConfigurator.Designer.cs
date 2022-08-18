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
            this.panel1 = new System.Windows.Forms.Panel();
            this.frmProperties = new RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.UI.Controls.OBS.OBSFormDataControl();
            this.lblSceneItem = new System.Windows.Forms.Label();
            this.ddlItem = new SuchByte.MacroDeck.GUI.CustomControls.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.frmProperties);
            this.panel1.Controls.Add(this.lblSceneItem);
            this.panel1.Controls.Add(this.ddlItem);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(3, 40);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 40, 3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(861, 70);
            this.panel1.TabIndex = 0;
            // 
            // frmProperties
            // 
            this.frmProperties.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.frmProperties.AutoSize = true;
            this.frmProperties.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.frmProperties.Location = new System.Drawing.Point(94, 35);
            this.frmProperties.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.frmProperties.Name = "frmProperties";
            this.frmProperties.Size = new System.Drawing.Size(761, 21);
            this.frmProperties.TabIndex = 8;
            this.frmProperties.AutoSize = true;
            this.frmProperties.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowOnly;
            this.frmProperties.Value = null;
            // 
            // lblSceneItem
            // 
            this.lblSceneItem.AutoSize = true;
            this.lblSceneItem.Location = new System.Drawing.Point(28, 3);
            this.lblSceneItem.Name = "lblSceneItem";
            this.lblSceneItem.Size = new System.Drawing.Size(56, 23);
            this.lblSceneItem.TabIndex = 6;
            this.lblSceneItem.Text = "Item:";
            // 
            // ddlItem
            // 
            this.ddlItem.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ddlItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.ddlItem.BorderRadius = 8;
            this.ddlItem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlItem.ForeColor = System.Drawing.Color.White;
            this.ddlItem.FormattingEnabled = true;
            this.ddlItem.Location = new System.Drawing.Point(90, 3);
            this.ddlItem.Name = "ddlItem";
            this.ddlItem.Size = new System.Drawing.Size(765, 24);
            this.ddlItem.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 23);
            this.label2.TabIndex = 4;
            this.label2.Text = "Settings:";
            // 
            // SetSourcePropertiesActionConfigurator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.panel1);
            this.Name = "SetSourcePropertiesActionConfigurator";
            this.Size = new System.Drawing.Size(867, 113);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label label2;
        private SuchByte.MacroDeck.GUI.CustomControls.ComboBox ddlItem;
        private System.Windows.Forms.Label lblSceneItem;
        private OBS.OBSFormDataControl frmProperties;
    }
}