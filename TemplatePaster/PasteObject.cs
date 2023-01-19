using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TemplatePaster
{
  /// <summary>
  /// 貼り付けオブジェクト
  /// </summary>
  public class PasteObject
  {
    /// <summary>
    /// 表示名
    /// </summary>
    [JsonProperty("name")]
    public string Name { get; set; }

    /// <summary>
    /// 貼り付け文字列
    /// </summary>
    [JsonProperty("pasteString")]
    public string PasteString { get; set; }
  }
}
