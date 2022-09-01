using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.Actions;
using RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.Model;
using RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.Model.OBS.Property;
using RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.UI.Controls.OBS;
using SuchByte.MacroDeck.GUI;
using SuchByte.MacroDeck.GUI.CustomControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Forms;

namespace RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.UI.Controls
{
    public partial class SetSourcePropertiesActionConfigurator : ActionConfigControl
    {
        internal class SourceOption
        {
            public Source Source { get; set; }

            public SourceOption(Source value)
            {
                Source = value;
            }

            public override string ToString() => string.Format("{0}", Source.Name);
        }

        public System.Drawing.Color BorderColor { get; set; } = System.Drawing.Color.Black;
        public int BorderWidth { get; set; } = 1;

        public JObject SourceState { get; set; }

        // Add a variable for the instance of your action to get access to the Configuration etc.
        private SetSourcePropertiesAction _macroDeckAction;

        protected SetSourcePropertiesActionConfig _config;

        protected Source SelectedSource { get; set; }

        public SetSourcePropertiesActionConfigurator(SetSourcePropertiesAction action, ActionConfigurator actionConfigurator)
        {
            InitializeComponent();

            this.Dock = DockStyle.Fill;
            tblSourceStateContainer.CellPaint += TblPropertyContainer_CellPaint;

            this._macroDeckAction = action;
            try
            {
                this._config = action.Configuration != null ? JsonConvert.DeserializeObject<SetSourcePropertiesActionConfig>(action.Configuration) : null;
            }
            catch (Exception) { }

            _ = PopulateSourcesAsync();
            // Items populate from scene change

            if (this._config != null)
            {
                PopulateProperties(this._config.FormData);
            }
        }

        private void TblPropertyContainer_CellPaint(object sender, TableLayoutCellPaintEventArgs e)
        {
            var topLeft = e.CellBounds.Location;
            var topRight = new Point(e.CellBounds.Right, e.CellBounds.Top);
            e.Graphics.DrawLine(new Pen(BorderColor, BorderWidth), topLeft, topRight);
        }

        protected async Task PopulateSourcesAsync()
        {
            var sources = await PluginCache.SourcesService.GetSourcesAsync();
            var source_opts = new Dictionary<string, SourceOption>();
            foreach (var source in sources)
            {
                source_opts.Add(source.Id, new SourceOption(source));
            }
            ddlItem.DataSource = source_opts.ToList();
            ddlItem.DisplayMember = "Value";
            ddlItem.ValueMember = "Key";

            if (_config != null && _config.Source != default(Source))
            {
                ddlItem.SelectedValue = _config.Source.Id;
            }
        }

        private void PopulateTable(TableLayoutPanel table, JObject settings)
        {
            DrawingHelper.BeginControlUpdate(table);
            foreach (var keyval in settings)
            {
                table.Controls.Add(new Label { Text = keyval.Key, AutoSize = true, Dock = DockStyle.Fill, Margin = new Padding(0, 6, 0, 6) });
                AddTableValueControl(table, keyval.Value);
            }
            DrawingHelper.EndControlUpdate(table);
        }

        private void AddTableValueControl(TableLayoutPanel table, JToken value)
        {
            if (value.Type == JTokenType.Object)
            {
                var subTable = new TableLayoutPanel { RowCount = 1, ColumnCount = 2, Dock = DockStyle.Fill, Margin = new Padding(0, 6, 0, 6) };
                subTable.CellPaint += TblPropertyContainer_CellPaint;
                subTable.Controls.Clear();
                PopulateTable(subTable, (JObject)value);
                table.Controls.Add(subTable);
            }
            else if (value.Type == JTokenType.Array)
            {
                foreach (var item in (JArray)value)
                {
                    AddTableValueControl(table, item);
                }
            }
            else
            {
                table.Controls.Add(new TextBox { 
                    Text = value.ToString(), 
                    AutoSize = true, 
                    Dock = DockStyle.Fill, 
                    Margin = new Padding(0, 6, 0, 6), 
                    ReadOnly = true, 
                    BorderStyle = BorderStyle.None, 
                    BackColor = System.Drawing.Color.FromArgb(255, 45, 45, 45),
                    ForeColor = System.Drawing.Color.White,
                    Font = new System.Drawing.Font(TextBox.DefaultFont.FontFamily, 12)
                });
            }
        }

        private void PopulateProperties(JObject settings)
        {
            
            tblSourceStateContainer.InvokeIfRequired(tbl => {
                DrawingHelper.BeginControlUpdate(tbl);
                tbl.Controls.Clear();
                DrawingHelper.BeginControlUpdate(tbl);
            });
            this.InvokeIfRequired(that => that.PopulateTable(tblSourceStateContainer, settings));
        }

        private void onCaptureState(object sender, EventArgs e)
        {
            var opt = (SourceOption)((KeyValuePair<string, SourceOption>)ddlItem.SelectedItem).Value;
            _ = Task.Run(async () =>
            {
                SourceState = await opt.Source.GetSettingsAsync();
                PopulateProperties(SourceState);
            });
        }

        public override bool OnActionSave()
        {
            var selectedSourceOption = (SourceOption)((KeyValuePair<string, SourceOption>)ddlItem.SelectedItem).Value;

            var config = new SetSourcePropertiesActionConfig
            {
                Source = selectedSourceOption.Source,
                FormData = SourceState
            };
            var json = JsonConvert.SerializeObject(config);
            this._macroDeckAction.ConfigurationSummary = string.Format("Update source {0} properties", selectedSourceOption.Source.Name); // Set a summary of the configuration that gets displayed in the ButtonConfigurator item
            this._macroDeckAction.Configuration = json;

            return true; // Return true if the action was configured successfully; This closes the ActionConfigurator
        }

        private void showTooltip(object sender, EventArgs e)
        {
            toolTip1.SetToolTip((Control)sender, @"
Within Streamlabs Desktop:
1) Open the properties for the source selected above
2) Set the properties to what you want it to become when this action runs
   NOTE: Saving these changes in SLOBS is NOT necessary

Once set, click this button to capture those settings in the configuration
You should see the properties appear below the button for the state captured
Repeat as necessary");
        }

        private void hideTooltip(object sender, EventArgs e)
        {
            toolTip1.SetToolTip((Control)sender, null);
        }
    }
}
