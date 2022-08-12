using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.UI.Controls
{
    public partial class OBSFormDataControl : UserControl
    {
        protected Dictionary<string, object> FormData;

        public OBSFormDataControl(Dictionary<string, object> formData)
        {
            InitializeComponent();
            FormData = formData;

            PopulateForm();
        }

        private void PopulateForm()
        {
            foreach(KeyValuePair<string, object> item in FormData)
            {

            }
        }
    }
}
