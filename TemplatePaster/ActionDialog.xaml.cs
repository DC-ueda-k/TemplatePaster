using System;
using System.Windows;

namespace TemplatePaster
{
    /// <summary>
    /// Dialog.xaml の相互作用ロジック
    /// </summary>
    public partial class ActionDialog : Window
    {
        /// <summary>
        /// OKボタン押下時の追加処理
        /// </summary>
        private Action<PasteObject> m_Action;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ActionDialog(Action<PasteObject> addAction, PasteObject initValue)
        {
            InitializeComponent();
            m_Action = addAction;
            NameTextBox.Text = initValue.Name;
            PasteStringTextBox.Text = initValue.PasteString;
            IsHiddenCheckBox.IsChecked = initValue.IsHidden;
        }

        /// <summary>
        /// OKボタン押下時の処理
        /// </summary>
        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            var pasteObject = new PasteObject()
            {
                Name = NameTextBox.Text,
                PasteString = PasteStringTextBox.Text,
                IsHidden = IsHiddenCheckBox.IsChecked ?? false,
            };
            m_Action(pasteObject);
            Close();
        }

        /// <summary>
        /// キャンセルボタン押下時の処理
        /// </summary>
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            NameTextBox.Text = "";
            PasteStringTextBox.Text = "";
            IsHiddenCheckBox.IsChecked = false;

            Close();
        }

        /// <summary>
        /// ウィンドウを閉じるときの処理
        /// </summary>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }
    }
}
