namespace RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.UI.Controls
{
    partial class FirstActionConfigurator
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
            this.label1 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.txtField1 = new SuchByte.MacroDeck.GUI.CustomControls.RoundedTextBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.AutoSize = true;
            this.panel1.Controls.Add(this.txtField1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(3, 40);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 40, 3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(884, 40);
            this.panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 23);
            this.label1.TabIndex = 2;
            this.label1.Text = "Field:";
            // 
            // txtField1
            // 
            this.txtField1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.txtField1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtField1.Icon = null;
            this.txtField1.Location = new System.Drawing.Point(65, 3);
            this.txtField1.MaxCharacters = 32767;
            this.txtField1.Multiline = false;
            this.txtField1.Name = "txtField1";
            this.txtField1.Padding = new System.Windows.Forms.Padding(8, 5, 8, 5);
            this.txtField1.PasswordChar = false;
            this.txtField1.PlaceHolderColor = System.Drawing.Color.Gray;
            this.txtField1.PlaceHolderText = "";
            this.txtField1.ReadOnly = false;
            this.txtField1.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtField1.SelectionStart = 0;
            this.txtField1.Size = new System.Drawing.Size(816, 25);
            this.txtField1.TabIndex = 3;
            this.txtField1.TextAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // FirstActionConfigurator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "FirstActionConfigurator";
            this.Size = new System.Drawing.Size(887, 83);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolTip toolTip1;
        private SuchByte.MacroDeck.GUI.CustomControls.RoundedTextBox txtField1;
    }
}