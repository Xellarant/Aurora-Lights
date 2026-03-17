// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.Models.Sheet.SkillField
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

#nullable disable
namespace Builder.Presentation.Models.Sheet;

public class SkillField : Field
{
  public string ProficientKey { get; }

  public bool IsProficient { get; set; }

  public SkillField(
    string key,
    string proficientKey,
    string value,
    bool isProficient,
    float fontSize = 8f)
    : base(key, value, fontSize)
  {
    this.ProficientKey = proficientKey;
    this.IsProficient = isProficient;
  }
}
