// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.Models.Sheet.Field
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

#nullable disable
namespace Builder.Presentation.Models.Sheet;

public class Field
{
  public string Key { get; }

  public string Value { get; set; }

  public float FontSize { get; set; }

  public Field(string key, string value, float fontSize = 8f)
  {
    this.Key = key;
    this.Value = value;
    this.FontSize = fontSize;
  }

  public override string ToString() => $"[{this.Key}]:[{this.Value}]";
}
