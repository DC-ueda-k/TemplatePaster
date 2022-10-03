using System;
using System.Windows;

namespace TemplatePaster
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private MainWindow win = new MainWindow();

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var icon = GetResourceStream(new Uri("icon.ico", UriKind.Relative)).Stream;

            // タスクアイコンのコンテキストメニューにアイテムを追加
            var menu = new System.Windows.Forms.ContextMenuStrip();
            menu.Items.Add("終了", null, Exit_Click); // 左から：ツールチップ、？、発火するイベント

            var notifyIcon = new System.Windows.Forms.NotifyIcon
            {
                Visible = true,
                Icon = new System.Drawing.Icon(icon),
                Text = "TemplatePaster",  // ツールチップに表示される文字列
                ContextMenuStrip = menu
            };

            notifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(NotifyIcon_Click);
        }

        private void NotifyIcon_Click(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                // ウィンドウ表示&最前面に持ってくる
                if (win.WindowState == System.Windows.WindowState.Minimized)
                    win.WindowState = System.Windows.WindowState.Normal;

                win.Show();
                win.Activate();
                // タスクバーでの表示をする
                win.ShowInTaskbar = true;
            }
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            win.unregisterHotKey();
            Shutdown();
        }
    }
}
