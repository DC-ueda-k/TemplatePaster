using System;
using System.Collections.Generic;
using System.Text;

namespace TemplatePaster
{
  /// <summary>
  /// 貼り付けオブジェクト
  /// </summary>
  internal class PasteObject
  {
    /// <summary>
    /// 表示名
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 貼り付け文字列
    /// </summary>
    public string PasteString  { get; set; }
  }
}
