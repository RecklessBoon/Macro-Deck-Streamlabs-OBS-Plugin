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
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtFormData = new SuchByte.MacroDeck.GUI.CustomControls.RoundedTextBox();
            this.ddlItem = new SuchByte.MacroDeck.GUI.CustomControls.ComboBox();
            this.lblSceneItem = new System.Windows.Forms.Label();
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
            this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel1.Controls.Add(this.txtFormData);
            this.panel1.Controls.Add(this.ddlItem);
            this.panel1.Controls.Add(this.lblSceneItem);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(3, 40);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 40, 3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(591, 173);
            this.panel1.TabIndex = 0;
            // 
            // txtFormData
            // 
            this.txtFormData.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFormData.AutoScroll = true;
            this.txtFormData.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.txtFormData.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtFormData.Icon = null;
            this.txtFormData.Location = new System.Drawing.Point(96, 30);
            this.txtFormData.MaxCharacters = 32767;
            this.txtFormData.Multiline = true;
            this.txtFormData.Name = "txtFormData";
            this.txtFormData.Padding = new System.Windows.Forms.Padding(8, 5, 8, 5);
            this.txtFormData.PasswordChar = false;
            this.txtFormData.PlaceHolderColor = System.Drawing.Color.Gray;
            this.txtFormData.PlaceHolderText = "";
            this.txtFormData.ReadOnly = false;
            this.txtFormData.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtFormData.SelectionStart = 0;
            this.txtFormData.Size = new System.Drawing.Size(492, 140);
            this.txtFormData.TabIndex = 8;
            this.txtFormData.TextAlignment = System.Windows.Forms.HorizontalAlignment.Left;
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
            this.ddlItem.Location = new System.Drawing.Point(96, 0);
            this.ddlItem.Name = "ddlItem";
            this.ddlItem.Size = new System.Drawing.Size(492, 24);
            this.ddlItem.TabIndex = 7;
            // 
            // lblSceneItem
            // 
            this.lblSceneItem.AutoSize = true;
            this.lblSceneItem.Location = new System.Drawing.Point(3, 0);
            this.lblSceneItem.Name = "lblSceneItem";
            this.lblSceneItem.Size = new System.Drawing.Size(56, 23);
            this.lblSceneItem.TabIndex = 6;
            this.lblSceneItem.Text = "Item:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 23);
            this.label2.TabIndex = 4;
            this.label2.Text = "Settings:";
            // 
            // SetSourcePropertiesActionConfigurator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "SetSourcePropertiesActionConfigurator";
            this.Size = new System.Drawing.Size(597, 216);
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
        private SuchByte.MacroDeck.GUI.CustomControls.RoundedTextBox txtFormData;
    }
}