using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Input;
using SuchByte.MacroDeck.GUI.CustomControls;
using SuchByte.MacroDeck.GUI;
using SuchByte.MacroDeck.Variables;

namespace RecklessBoon.MacroDeck.Streamlabs_OBS_Plugin.UI
{
    public static class MainWindowHelper
    {
        public static event EventHandler<EventArgs> StatusButtonClicked;

        private static ContentSelectorButton statusButton = new ContentSelectorButton();
        private static ToolTip statusToolTip = new ToolTip();

        private static MainWindow mainWindow;

        private static Task connectingStatusLoop;
        private static CancellationTokenSource connectingStatusLoopCTS;

        public static void MacroDeck_OnMainWindowLoad(object sender, EventArgs e)
        {
            try
            {
                mainWindow = sender as MainWindow;
                statusButton = new ContentSelectorButton
                {
                    BackgroundImageLayout = ImageLayout.Zoom
                };
                UpdateStatusButton(PluginCache.Client != null && PluginCache.Client.IsStarted);
                statusButton.Click += StatusButton_Click;
                mainWindow.contentButtonPanel.Controls.Add(statusButton);
            }
            catch (Exception ex)
            {
                AppLogger.Error("Unexpected Exception:\n{0}", ex);
            }
        }

        public static void StatusButton_Click(object sender, EventArgs e)
        {
            StatusButtonClicked?.Invoke(sender, e);
        }

        public static async Task StartStatusLoopAsync()
        {
            try
            {
                if (connectingStatusLoop == null || connectingStatusLoop.Status != TaskStatus.Running)
                {
                    connectingStatusLoopCTS = new CancellationTokenSource();
                    CancellationToken ct = connectingStatusLoopCTS.Token;

                    var RPCClient = PluginCache.Client;
                    connectingStatusLoop = Task.Run(() =>
                    {
                        do
                        {
                            if (connectingStatusLoopCTS.IsCancellationRequested) return;
                            RPCClient = PluginCache.Client;
                            UpdateStatusButton(true);
                            Thread.Sleep(750);

                            if (connectingStatusLoopCTS.IsCancellationRequested) return;
                            UpdateStatusButton(false);
                            Thread.Sleep(750);
                        } while ((RPCClient == null || !RPCClient.IsStarted) && !connectingStatusLoopCTS.IsCancellationRequested);
                    }, ct);

                    await connectingStatusLoop;
                    UpdateStatusButton(RPCClient != null && RPCClient.IsStarted);
                }
            }
            catch (Exception ex)
            {
                AppLogger.Error("Unexpected Exception:\n{0}", ex);
            }
        }

        public static void CancelConnectingStatusLoop()
        {
            if (connectingStatusLoopCTS != null)
            {
                connectingStatusLoopCTS.Cancel();
            }
        }

        /**
         * Pad a bitmap with default padding
         */
        public static Bitmap PadBitmap(Bitmap bm)
        {
            return PadBitmap(bm, 1.3f, 1.3f);
        }

        /**
         * Pad a bitmap with equal percentage on x and y axis
         */
        public static Bitmap PadBitmap(Bitmap bm, float padding)
        {
            return PadBitmap(bm, padding, padding);
        }

        /**
         * Pad a bitmap with explicit percentage on x and y axis
         */
        public static Bitmap PadBitmap(Bitmap bm, float xPadding, float yPadding)
        {
            Bitmap paddedBm = new Bitmap((int)(bm.Width * xPadding), (int)(bm.Height * yPadding));
            using (Graphics graphics = Graphics.FromImage(paddedBm))
            {
                graphics.Clear(Color.Transparent);
                int x = (paddedBm.Width - bm.Width) / 2;
                int y = (paddedBm.Height - bm.Height) / 2;
                graphics.DrawImage(bm, x, y, bm.Width, bm.Height);
            }
            return paddedBm;
        }

        public static void UpdateStatusButton(bool connected)
        {
            try
            {
                if (mainWindow != null && mainWindow.IsHandleCreated)
                {
                    mainWindow.BeginInvoke(new Action(() =>
                    {
                        Bitmap bm = connected ? Properties.Resources.streamlabs_app_icon_64x64 : Properties.Resources.streamlabs_app_icon_gray_64x64;
                        statusButton.BackgroundImage = PadBitmap(bm, 1.35f);
                        statusToolTip.SetToolTip(statusButton, (connected ? " Connected" : "Disconnected"));
                    }));
                }
            }
            catch (Exception ex)
            {
                AppLogger.Error("Unexpected Exception:\n{0}", ex);
            }
        }
    }
}
