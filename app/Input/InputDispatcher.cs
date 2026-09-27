using GHelper.Peripherals;
using GHelper.Peripherals.Keyboard;
using GHelper.Peripherals.Mouse;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace GHelper.Input
{

    /// <summary>
    /// Registers global hotkeys for the mouse button bindings and keyboard
    /// launch-slot bindings of connected ASUS peripherals. Laptop hotkey
    /// handling has been removed from this peripherals-only build.
    /// </summary>
    public class InputDispatcher
    {
        KeyboardHook hook = new KeyboardHook();

        public InputDispatcher()
        {
            hook.KeyPressed += KeyPressed;
            RegisterKeys();
        }

        public void Init()
        {
            // No-op: there is no laptop keyboard to listen to in this build.
        }

        public void RegisterKeys()
        {
            hook.UnregisterAll();

            // Mouse button bindings are carried on F13.. virtual keys.
            foreach (ushort code in GetActiveMouseComboCarriers())
                hook.RegisterHotKey(ModifierKeys.None, Keys.F13 + (code - 0x0068));

            // Keyboard launch slots are carried on Ctrl+Shift+Alt+F1..
            for (int slot = 0; slot < AsusKeyboard.LaunchSlots; slot++)
                if (AsusKeyboard.LaunchCommand(slot).Length > 0)
                    hook.RegisterHotKey(ModifierKeys.Control | ModifierKeys.Shift | ModifierKeys.Alt, Keys.F1 + slot);
        }

        private static IEnumerable<ushort> GetActiveMouseComboCarriers()
        {
            var seen = new HashSet<ushort>();
            foreach (var m in PeripheralsProvider.SnapshotMice())
            {
                if (!m.HasButtonBindings() || m.ButtonBindings is null) continue;
                foreach (ushort code in m.ButtonBindings)
                    if (AsusMouse.CombosByCode.ContainsKey(code) && seen.Add(code))
                        yield return code;
            }
        }

        public static int[] ParseHexValues(string input)
        {
            string pattern = @"\b(0x[0-9A-Fa-f]{1,2}|[0-9A-Fa-f]{1,2})\b";

            if (!Regex.IsMatch(input, $"^{pattern}(\\s+{pattern})*$")) return new int[0];

            MatchCollection matches = Regex.Matches(input, pattern);

            int[] hexValues = new int[matches.Count];

            for (int i = 0; i < matches.Count; i++)
            {
                string hexValueStr = matches[i].Value;
                int hexValue = int.Parse(hexValueStr.StartsWith("0x", StringComparison.OrdinalIgnoreCase)
                    ? hexValueStr.Substring(2)
                    : hexValueStr, System.Globalization.NumberStyles.HexNumber);

                hexValues[i] = hexValue;
            }

            return hexValues;
        }

        static void RunKeyCommand(string command, bool launchOnNoKeys = true)
        {
            int[] hexKeys = ParseHexValues(command);

            switch (hexKeys.Length)
            {
                case 1:
                    KeyboardHook.KeyPress((Keys)hexKeys[0]);
                    break;
                case 2:
                    KeyboardHook.KeyKeyPress((Keys)hexKeys[0], (Keys)hexKeys[1]);
                    break;
                case 3:
                    KeyboardHook.KeyKeyKeyPress((Keys)hexKeys[0], (Keys)hexKeys[1], (Keys)hexKeys[2]);
                    break;
                case 4:
                    KeyboardHook.KeyKeyKeyKeyPress((Keys)hexKeys[0], (Keys)hexKeys[1], (Keys)hexKeys[2], (Keys)hexKeys[3]);
                    break;
                default:
                    if (launchOnNoKeys && !string.IsNullOrWhiteSpace(command)) LaunchProcess(command);
                    break;
            }
        }

        static void LaunchProcess(string command = "")
        {
            if (string.IsNullOrEmpty(command)) return;
            try
            {
                Process.Start(new ProcessStartInfo(command) { UseShellExecute = true });
            }
            catch (Exception ex)
            {
                Logger.WriteLine($"Failed to run: {command} {ex.Message}");
            }
        }

        public void KeyPressed(object? sender, KeyPressedEventArgs e)
        {
            Logger.WriteLine(e.Key.ToString() + " " + e.Modifier.ToString());

            if (e.Modifier == (ModifierKeys.Control | ModifierKeys.Shift | ModifierKeys.Alt)
                && e.Key >= Keys.F1 && e.Key < Keys.F1 + AsusKeyboard.LaunchSlots)
            {
                string command = AsusKeyboard.LaunchCommand(e.Key - Keys.F1);
                if (command.Length > 0)
                {
                    LaunchProcess(command);
                    return;
                }
            }

            if (e.Modifier == ModifierKeys.None && e.Key >= Keys.F13 && e.Key <= Keys.F24)
            {
                ushort code = (ushort)(0x68 + (e.Key - Keys.F13));
                if (AsusMouse.CombosByCode.TryGetValue(code, out var combo))
                {
                    RunKeyCommand(combo.ResolveCommand(), launchOnNoKeys: false);
                    return;
                }
            }
        }
    }
}
