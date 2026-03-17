// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.Models.Sheet.SpellField
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

#nullable disable
namespace Builder.Presentation.Models.Sheet;

public class SpellField : Field
{
  public string PreparedKey { get; }

  public bool IsPrepared { get; set; }

  public SpellField(
    string key,
    string preparedKey,
    string value = "",
    bool isPrepared = false,
    float fontSize = 8f)
    : base(key, value, fontSize)
  {
    this.PreparedKey = preparedKey;
    this.IsPrepared = isPrepared;
  }

  public override string ToString()
  {
    return base.ToString() + $" | [{this.PreparedKey}]:[{this.IsPrepared}]";
  }
}
