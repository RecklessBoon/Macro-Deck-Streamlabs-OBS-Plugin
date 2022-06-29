using SuchByte.MacroDeck.GUI.CustomControls;

namespace RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.UI.Dialog
{
    public partial class Configurator : DialogForm
    {
        protected static Configuration _config;

        public Configurator(Configuration config)
        {
            _config = config ?? _config;
            InitializeComponent();

            txtToken.Text = _config.ClientSecret;
        }

        private void btnSave_Click(object sender, System.EventArgs e)
        {
            _config.ClientSecret = txtToken.Text;
            _config.Save();
        }
    }
}
