namespace RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.UI.Dialog
{
    partial class Configurator
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
            this.txtToken = new SuchByte.MacroDeck.GUI.CustomControls.RoundedTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSave = new SuchByte.MacroDeck.GUI.CustomControls.ButtonPrimary();
            this.SuspendLayout();
            // 
            // txtToken
            // 
            this.txtToken.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtToken.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.txtToken.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtToken.Icon = null;
            this.txtToken.Location = new System.Drawing.Point(57, 34);
            this.txtToken.MaxCharacters = 32767;
            this.txtToken.Multiline = false;
            this.txtToken.Name = "txtToken";
            this.txtToken.Padding = new System.Windows.Forms.Padding(8, 5, 8, 5);
            this.txtToken.PasswordChar = true;
            this.txtToken.PlaceHolderColor = System.Drawing.Color.Gray;
            this.txtToken.PlaceHolderText = "";
            this.txtToken.ReadOnly = false;
            this.txtToken.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtToken.SelectionStart = 0;
            this.txtToken.Size = new System.Drawing.Size(379, 25);
            this.txtToken.TabIndex = 2;
            this.txtToken.TextAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 34);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.label1.Size = new System.Drawing.Size(47, 26);
            this.label1.TabIndex = 3;
            this.label1.Text = "Token:";
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.BorderRadius = 8;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.HoverColor = System.Drawing.Color.Empty;
            this.btnSave.Icon = null;
            this.btnSave.Location = new System.Drawing.Point(286, 73);
            this.btnSave.Name = "btnSave";
            this.btnSave.Progress = 0;
            this.btnSave.ProgressColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(103)))), ((int)(((byte)(205)))));
            this.btnSave.Size = new System.Drawing.Size(150, 40);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.UseWindowsAccentColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // Configurator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(440, 117);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtToken);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "Configurator";
            this.Text = "Configurator";
            this.Controls.SetChildIndex(this.txtToken, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.btnSave, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SuchByte.MacroDeck.GUI.CustomControls.RoundedTextBox txtToken;
        private System.Windows.Forms.Label label1;
        private SuchByte.MacroDeck.GUI.CustomControls.ButtonPrimary btnSave;
    }
}