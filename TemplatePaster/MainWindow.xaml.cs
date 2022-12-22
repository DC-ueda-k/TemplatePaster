using System;
using System.Windows;
using System.Windows.Input;
using System.Runtime.InteropServices;
using System.Windows.Interop;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Forms;
using Clipboard = System.Windows.Clipboard;
using MessageBox = System.Windows.MessageBox;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;
using System.IO;
using Newtonsoft.Json;

namespace TemplatePaster
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    /*
     * HotKey登録時に指定するID。
     * アプリケーションの場合は、0x0000～0xbfffの間で指定すること。
     * （共有DLLの場合は、0xc000～0xffffの間を使用する。）
     */
    private const int HOTKEY_ID1 = 0x0001;

    // HotKey Message ID
    private const int WM_HOTKEY = 0x0312;

    private IntPtr WindowHandle;

    /* 
     * HotKey登録を行う関数。
     * 失敗の場合は0（他で使用されている）、成功の場合は0以外の数値が返される。
     */
    [DllImport("user32.dll")]
    private static extern int RegisterHotKey(IntPtr hWnd, int id, int modKey1, int vKey);

    /*
     * HotKey解除を行う関数。
     * 失敗の場合は0、成功の場合は0以外の数値が返される。
     */
    [DllImport("user32.dll")]
    private static extern int UnregisterHotKey(IntPtr hWnd, int id);

    public MainWindow()
    {
      InitializeComponent();

      // WindowHandleを取得
      var host = new WindowInteropHelper(this);
      WindowHandle = host.Handle;

      // HotKeyを設定
      SetUpHotKey();
      ComponentDispatcher.ThreadPreprocessMessage += ComponentDispatcher_ThreadPreprocessMessage;

      // 設定ファイル読み込み
      var pasteObjectConfigStr = ReadPasteObjectConfigFile();

      // 設定ファイルをPasteObject型に変換
      var pasteObjects = JsonConvert.DeserializeObject<ObservableCollection<PasteObject>>(pasteObjectConfigStr);
      if (pasteObjects == null)
      {
        pasteObjects = new ObservableCollection<PasteObject>();
      }

      PasteObjectGrid.ItemsSource = pasteObjects;

      // データグリッドの先頭行を選択
      PasteObjectGrid.Focus();
      if (pasteObjects.Count > 0)
      {
        PasteObjectGrid.SelectedIndex = 0;
      }
    }

    // HotKey登録
    private void SetUpHotKey()
    {
      // Ctrl + 1 をHotKeyとして登録。
      var result1 = RegisterHotKey(WindowHandle, HOTKEY_ID1, (int)ModifierKeys.Control, KeyInterop.VirtualKeyFromKey(Key.D1));
      if (result1 == 0)
      {
        MessageBox.Show("HotKey1の登録に失敗しました。");
      }
    }

    // HotKeyが押された際の挙動を設定
    void ComponentDispatcher_ThreadPreprocessMessage(ref MSG msg, ref bool handled)
    {
      // HotKeyが押されたかどうかを判定
      if (msg.message != WM_HOTKEY) return;

      switch (msg.wParam.ToInt32())
      {
        case HOTKEY_ID1:
          ShowWindow(this);
          break;
        default:
          break;
      }
    }

    // ウィンドウの閉じるボタンを押したときの処理
    private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
    {
      // クローズ処理をキャンセルして、タスクバーの表示も消す
      e.Cancel = true;
      this.WindowState = System.Windows.WindowState.Minimized;
      this.ShowInTaskbar = false;
    }

    public void unregisterHotKey()
    {
      // HotKey解除
      UnregisterHotKey(WindowHandle, HOTKEY_ID1);
      ComponentDispatcher.ThreadPreprocessMessage -= ComponentDispatcher_ThreadPreprocessMessage;
    }

    public void ShowWindow(MainWindow win)
    {
      // ウィンドウ表示&最前面に持ってくる
      if (win.WindowState == System.Windows.WindowState.Minimized)
      {
        win.WindowState = System.Windows.WindowState.Normal;
      }

      win.Show();
      win.Activate();
      // タスクバーでの表示をする
      win.ShowInTaskbar = true;
    }

    /// <summary>
    /// グリッド行ダブルクリック時の処理
    /// </summary>
    /// <param name="sender">クリック元オブジェクト</param>
    /// <param name="e">ボタンイベント</param>
    private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
    {
      var row = (DataGridRow)sender;
      Paste((PasteObject)row.DataContext);
    }

    /// <summary>
    /// グリッド行キーボード押下時の処理
    /// </summary>
    /// <param name="sender">キーボード押下元オブジェクト</param>
    /// <param name="e">キーボードイベント</param>
    private void Row_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.Key == Key.Enter)
      {
        var row = (DataGridRow)sender;
        Paste((PasteObject)row.DataContext);
      }
    }

    /// <summary>
    /// PasteObject設定ファイルを読み込む
    /// </summary>
    /// <returns>設定ファイルの中身</returns>
    private static string ReadPasteObjectConfigFile()
    {
      var dt = "";
      var filePath = Directory.GetCurrentDirectory() + "\\PasteObjectConfig.json";

      // JSONファイルが無ければ作成する
      if (!File.Exists(filePath))
      {
        using var createdfile = File.Create(filePath);
      }

      using var sr = new StreamReader(filePath);
      dt = sr.ReadToEnd();

      return dt;
    }

    /// <summary>
    /// 文字列を貼り付ける
    /// </summary>
    /// <param name="data">貼り付けデータ</param>
    private void Paste(PasteObject data)
    {
      // クリップボードに貼り付け文字列をコピー
      Clipboard.SetText(data.PasteString);

      // 貼り付けウィンドウを閉じる
      this.WindowState = System.Windows.WindowState.Minimized;
      this.ShowInTaskbar = false;

      // Ctrl + V キーストロークを送る
      SendKeys.SendWait("^v");
    }

    /// <summary>
    /// 編集ボタン押下時
    /// </summary>
    private void EditButtonClick(object sender, RoutedEventArgs e)
    {
      MessageBox.Show("aaa");
    }
  }
}
