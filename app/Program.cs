using GHelper.Helpers;
using GHelper.Input;
using GHelper.Peripherals;
using System.Diagnostics;
using System.Globalization;

namespace GHelper
{

    static class Program
    {
        public static NotifyIcon? trayIcon;
        public static PeripheralsForm? settingsForm;

        public static InputDispatcher? inputDispatcher;

        // The main entry point for the application
        [STAThread]
        public static void Main(string[] args)
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);

            AppDomain.CurrentDomain.UnhandledException += (s, e) => Logger.WriteLine("Unhandled: " + e.ExceptionObject);
            TaskScheduler.UnobservedTaskException += (s, e) => { Logger.WriteLine("Unobserved: " + e.Exception); e.SetObserved(); };

            string language = AppConfig.GetString("language");
            try
            {
                if (language != null && language.Length > 0)
                    Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(language);
                else
                {
                    var culture = CultureInfo.CurrentUICulture;
                    if (culture.ToString() == "kr") culture = CultureInfo.GetCultureInfo("ko");
                    Thread.CurrentThread.CurrentUICulture = culture;
                }
            }
            catch
            {
                Logger.WriteLine("Unknown Language: " + language);
            }

            Logger.WriteLine("----------------------");
            Logger.WriteLine("GHelper4Peripherals launched: " + CultureInfo.CurrentUICulture);

            ProcessHelper.CheckAlreadyRunning();
            ProcessHelper.SetPriority();

            Application.EnableVisualStyles();

            settingsForm = new PeripheralsForm();

            trayIcon = new NotifyIcon
            {
                Text = "GHelper4Peripherals",
                Icon = Properties.Resources.standard,
                Visible = true
            };

            var trayMenu = new ContextMenuStrip();
            trayMenu.Items.Add(Properties.Strings.OpenGHelper, null, (_, _) => settingsForm.Toggle());
            trayMenu.Items.Add(Properties.Strings.Quit, null, (_, _) => settingsForm.ExitApp());
            trayIcon.ContextMenuStrip = trayMenu;

            trayIcon.MouseClick += (_, e) =>
            {
                if (e.Button == MouseButtons.Left) settingsForm.Toggle();
            };

            inputDispatcher = new InputDispatcher();

            Task task = Task.Run(() =>
            {
                PeripheralsProvider.DetectAllAsusMice();
                PeripheralsProvider.DetectAllAsusKeyboards();
                PeripheralsProvider.DetectAllAsusHeadsets();
            });
            PeripheralsProvider.RegisterForDeviceEvents();

            settingsForm.Show();

            // Refresh peripheral battery levels periodically
            var batteryTimer = new System.Windows.Forms.Timer { Interval = 30000 };
            batteryTimer.Tick += (_, _) =>
            {
                if (settingsForm.Visible) Task.Run((Action)PeripheralsProvider.RefreshBatteryForAllDevices);
            };
            batteryTimer.Start();

            Application.ApplicationExit += OnExit;
            Application.Run();
        }

        static void OnExit(object? sender, EventArgs e)
        {
            PeripheralsProvider.UnregisterForDeviceEvents();

            if (trayIcon is not null)
            {
                trayIcon.Visible = false;
                trayIcon.Dispose();
            }
        }
    }
}
