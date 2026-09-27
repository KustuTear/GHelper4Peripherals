using GHelper.Peripherals;
using GHelper.Peripherals.Headset;
using GHelper.Peripherals.Keyboard;
using GHelper.Peripherals.Mouse;
using GHelper.Properties;
using GHelper.UI;

namespace GHelper
{
    public partial class PeripheralsForm : RForm
    {
        AsusMouseSettings? mouseSettings;
        AsusKeyboardSettings? keyboardSettings;
        AsusHeadsetSettings? headsetSettings;

        bool exiting = false;

        public PeripheralsForm()
        {
            InitializeComponent();
            InitTheme(true);

            Text = "GHelper4Peripherals";
            buttonQuit.Text = Properties.Strings.Quit;
            labelTitle.Text = "GHelper4Peripherals";

            buttonPeripheral1.Click += ButtonPeripheral_Click;
            buttonPeripheral2.Click += ButtonPeripheral_Click;
            buttonPeripheral3.Click += ButtonPeripheral_Click;
            buttonPeripheral1.MouseEnter += ButtonPeripheral_MouseEnter;
            buttonPeripheral2.MouseEnter += ButtonPeripheral_MouseEnter;
            buttonPeripheral3.MouseEnter += ButtonPeripheral_MouseEnter;
            buttonQuit.Click += (_, _) => ExitApp();

            VisualizePeripherals();
        }

        public void Toggle()
        {
            if (Visible)
            {
                Hide();
            }
            else
            {
                Show();
                VisualizePeripherals();
            }
        }

        public void ExitApp()
        {
            exiting = true;
            Application.Exit();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (!exiting)
            {
                e.Cancel = true;
                Hide();
                return;
            }
            base.OnFormClosing(e);
        }

        // Kept for compatibility with the peripheral settings windows; the
        // peripherals-only app has no laptop keyboard, so this is a no-op.
        public void UpdateKeyboardLabel()
        {
        }

        private static List<IPeripheral> VisiblePeripherals()
        {
            List<IPeripheral> lp = PeripheralsProvider.AllPeripherals();
            return lp.Count > 3 ? lp.OrderByDescending(p => p.IsDeviceReady).ToList() : lp;
        }

        public void VisualizePeripherals()
        {
            if (IsDisposed) return;

            if (!PeripheralsProvider.IsAnyPeripheralConnect())
            {
                panelPeripherals.Visible = false;
                return;
            }

            RButton[] buttons = new RButton[] { buttonPeripheral1, buttonPeripheral2, buttonPeripheral3 };

            List<IPeripheral> lp = VisiblePeripherals();

            for (int i = 0; i < lp.Count && i < buttons.Length; ++i)
            {
                IPeripheral m = lp.ElementAt(i);
                RButton b = buttons[i];

                string id = m.GetDisplayName();
                bool ready = m.IsDeviceReady;
                bool hasBat = m.HasBattery();
                bool charging = ready && hasBat && m.Charging;
                int level = (ready && hasBat) ? Math.Min(5, (m.Battery + 10) / 20) : -1;
                bool showPercent = AppConfig.Is("mouse_battery") && ready && hasBat;
                int cacheBattery = showPercent ? m.Battery : -1;
                var state = (id, ready, charging, level, cacheBattery, b.ForeColor.ToArgb());

                if (b.Tag is ValueTuple<string, bool, bool, int, int, int> prev && prev.Equals(state) && b.Visible)
                    continue;

                b.Text = showPercent ? id + "\n" + m.Battery + "%" : id;

                Image? baseIcon = m.DeviceType() switch
                {
                    PeripheralType.Mouse => Properties.Resources.icons8_maus_48,
                    PeripheralType.Keyboard => Properties.Resources.icons8_keyboard_48,
                    PeripheralType.Headset => Properties.Resources.icons8_headphones_48,
                    _ => null,
                };

                if (baseIcon is not null)
                {
                    int ih = baseIcon.Height;
                    int iw = Math.Min(baseIcon.Width, ih);
                    Image composed = ControlHelper.TintImage(baseIcon, b.ForeColor);
                    if (!ready)
                    {
                        composed = ControlHelper.OverlayBadge(composed, Properties.Resources.icons8_cancel_48, RForm.colorTurbo, iconWidth: iw, iconHeight: ih);
                    }
                    else if (hasBat)
                    {
                        if (charging)
                            composed = ControlHelper.OverlayBadge(composed, Properties.Resources.icons8_flash_48, RForm.colorEco, iconWidth: iw, iconHeight: ih);

                        Color barColor = level <= 1 ? RForm.colorTurbo
                                       : level <= 3 ? RForm.colorStandard
                                       : RForm.colorEco;
                        composed = ControlHelper.OverlayChargeBars(composed, level, 5, barColor, iconWidth: iw, iconHeight: ih);
                    }

                    b.Image = ControlHelper.ResizeImage(composed, ControlHelper.Scale);
                }

                b.Tag = state;
                b.Visible = true;
            }

            for (int i = lp.Count; i < buttons.Length; ++i)
            {
                buttons[i].Visible = false;
            }

            panelPeripherals.Visible = true;
        }

        private void ButtonPeripheral_MouseEnter(object? sender, EventArgs e)
        {
            int index = 0;
            if (sender == buttonPeripheral2) index = 1;
            if (sender == buttonPeripheral3) index = 2;

            var lp = VisiblePeripherals();
            if (index >= lp.Count) return;
            IPeripheral iph = lp.ElementAt(index);

            if (iph is not null && !iph.IsDeviceReady)
            {
                // Refresh battery on hover if the device is marked as "Not Ready"
                iph.ReadBattery();
            }
        }

        private void ButtonPeripheral_Click(object? sender, EventArgs e)
        {
            if (mouseSettings is not null)
            {
                mouseSettings.Close();
                return;
            }

            if (keyboardSettings is not null)
            {
                keyboardSettings.Close();
                return;
            }

            if (headsetSettings is not null)
            {
                headsetSettings.Close();
                return;
            }

            int index = 0;
            if (sender == buttonPeripheral2) index = 1;
            if (sender == buttonPeripheral3) index = 2;

            var lp = VisiblePeripherals();
            if (index >= lp.Count) return;

            IPeripheral iph = lp.ElementAt(index);

            if (iph is null)
            {
                return;
            }

            if (iph.DeviceType() == PeripheralType.Mouse)
            {
                AsusMouse? am = iph as AsusMouse;
                if (am is null || !am.IsDeviceReady)
                {
                    return;
                }
                mouseSettings = new AsusMouseSettings(am);
                mouseSettings.TopMost = AppConfig.Is("topmost");
                mouseSettings.FormClosed += MouseSettings_FormClosed;
                mouseSettings.Disposed += MouseSettings_Disposed;
                if (!mouseSettings.IsDisposed)
                {
                    mouseSettings.Show();
                }
                else
                {
                    mouseSettings = null;
                }
            }

            if (iph.DeviceType() == PeripheralType.Keyboard)
            {
                AsusKeyboard? kb = iph as AsusKeyboard;
                if (kb is null || !kb.IsDeviceReady)
                {
                    return;
                }
                ShowKeyboardSettings(kb);
            }

            if (iph.DeviceType() == PeripheralType.Headset)
            {
                AsusHeadset? hs = iph as AsusHeadset;
                if (hs is null || !hs.IsDeviceReady)
                {
                    return;
                }
                headsetSettings = new AsusHeadsetSettings(hs);
                headsetSettings.TopMost = AppConfig.Is("topmost");
                headsetSettings.FormClosed += HeadsetSettings_FormClosed;
                headsetSettings.Disposed += HeadsetSettings_Disposed;
                if (!headsetSettings.IsDisposed)
                {
                    headsetSettings.Show();
                }
                else
                {
                    headsetSettings = null;
                }
            }
        }

        private void HeadsetSettings_Disposed(object? sender, EventArgs e) => headsetSettings = null;
        private void HeadsetSettings_FormClosed(object? sender, FormClosedEventArgs e) => headsetSettings = null;

        private void ShowKeyboardSettings(AsusKeyboard kb)
        {
            AsusKeyboardSettings.RequestReopen = ShowKeyboardSettings;
            keyboardSettings = new AsusKeyboardSettings(kb);
            keyboardSettings.TopMost = AppConfig.Is("topmost");
            keyboardSettings.FormClosed += KeyboardSettings_FormClosed;
            keyboardSettings.Disposed += KeyboardSettings_Disposed;
            if (!keyboardSettings.IsDisposed)
            {
                keyboardSettings.Show();
            }
            else
            {
                keyboardSettings = null;
            }
        }

        private void KeyboardSettings_Disposed(object? sender, EventArgs e) => keyboardSettings = null;
        private void KeyboardSettings_FormClosed(object? sender, FormClosedEventArgs e) => keyboardSettings = null;

        private void MouseSettings_Disposed(object? sender, EventArgs e) => mouseSettings = null;
        private void MouseSettings_FormClosed(object? sender, FormClosedEventArgs e) => mouseSettings = null;
    }
}
