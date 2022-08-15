namespace RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.UI.Controls.OBS
{
    partial class OBSFormDataControl
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
            this.tblPropertyContainer = new System.Windows.Forms.TableLayoutPanel();
            this.SuspendLayout();
            // 
            // tblPropertyContainer
            // 
            this.tblPropertyContainer.AutoSize = true;
            this.tblPropertyContainer.ColumnCount = 2;
            this.tblPropertyContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tblPropertyContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tblPropertyContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblPropertyContainer.Location = new System.Drawing.Point(0, 0);
            this.tblPropertyContainer.Name = "tblPropertyContainer";
            this.tblPropertyContainer.RowCount = 1;
            this.tblPropertyContainer.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblPropertyContainer.Size = new System.Drawing.Size(384, 38);
            this.tblPropertyContainer.TabIndex = 0;
            // 
            // OBSFormDataControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.Controls.Add(this.tblPropertyContainer);
            this.Name = "OBSFormDataControl";
            this.Size = new System.Drawing.Size(384, 38);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tblPropertyContainer;
    }
}
