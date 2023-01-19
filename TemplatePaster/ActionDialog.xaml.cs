using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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

    private void okButton_Click(object sender, RoutedEventArgs e)
    {
      var pasteObject = new PasteObject()
      {
        Name = NameTextBox.Text,
        PasteString = PasteStringTextBox.Text,
      };
      m_Action(pasteObject);
      this.Close();
    }

    private void cancelButton_Click(object sender, RoutedEventArgs e)
    {
      NameTextBox.Text = "";
      PasteStringTextBox.Text = "";
      this.Close();
    }
  }
}
