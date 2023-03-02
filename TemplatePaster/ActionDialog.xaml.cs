using System;
using System.Windows;

namespace TemplatePaster
{
  /// <summary>
  /// Dialog.xaml の相互作用ロジック
  /// </summary>
  public partial class ActionDialog : Window
  {
    private Action<PasteObject> m_Action;

    public ActionDialog(Action<PasteObject> addAction, PasteObject initValue)
    {
      InitializeComponent();
      m_Action = addAction;
      NameTextBox.Text = initValue.Name;
      PasteStringTextBox.Text = initValue.PasteString;
    }

    private void OkButton_Click(object sender, RoutedEventArgs e)
    {
      var pasteObject = new PasteObject()
      {
        Name = NameTextBox.Text,
        PasteString = PasteStringTextBox.Text,
      };
      m_Action(pasteObject);
      Close();
    }

    private void CancelButton_Click(object sender, RoutedEventArgs e)
    {
      NameTextBox.Text = "";
      PasteStringTextBox.Text = "";
      Close();
    }

    private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
    {
      e.Cancel = true;
      Hide();
    }
  }
}
