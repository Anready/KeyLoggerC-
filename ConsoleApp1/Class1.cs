using IWshRuntimeLibrary; //Класс для добавления в автозагрузку


namespace App
{
    internal class Class1
    {
        public void Main (string ShortcutPath, string TargetPath)
        {
            WshShell wshShell = new WshShell(); //создаем объект wsh shell

            IWshShortcut Shortcut = (IWshShortcut)wshShell.
                CreateShortcut(ShortcutPath);

            Shortcut.TargetPath = TargetPath; //путь к целевому файлу

            Shortcut.Save();
        }
    }
}
