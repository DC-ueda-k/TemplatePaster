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
  public partial class AddDialog : Window
  {
    private Action<PasteObject> m_AddAction;

    public AddDialog(Action<PasteObject> addAction)
    {
      InitializeComponent();
      m_AddAction = addAction;
    }

    private void okButton_Click(object sender, RoutedEventArgs e)
    {
      var addPasteObject = new PasteObject()
      {
        Name = NameTextBox.Text,
        PasteString = PasteStringTextBox.Text,
      };
      m_AddAction(addPasteObject);
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
