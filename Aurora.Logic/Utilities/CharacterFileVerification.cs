// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.Utilities.CharacterFileVerification
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

using Builder.Core.Logging;
using Builder.Data.Rules;
using Builder.Presentation.Interfaces;
using System.Diagnostics;
using System.Text;

#nullable disable
namespace Builder.Presentation.Utilities;

public static class CharacterFileVerification
{
  public static bool IsEqualCrC(string existing, ISelectionRuleExpander expander)
  {
    return existing.Equals(CharacterFileVerification.GenerateCrC(expander));
  }

  public static string GenerateCrC(SelectRule rule, int expanderNumber)
  {
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append(rule.ElementHeader.Id);
    stringBuilder.Append(rule.Attributes.Name);
    stringBuilder.Append(rule.Attributes.Type);
    stringBuilder.Append(rule.Attributes.RequiredLevel);
    stringBuilder.Append(expanderNumber);
    string s = $"{rule.ElementHeader.Id}{rule.Attributes.Name}{rule.Attributes.Type}{rule.Attributes.RequiredLevel}{expanderNumber}";
    if (rule.Attributes.Type.Equals("Spell") && rule.Attributes.ContainsSupports())
    {
      stringBuilder.Append(rule.Attributes.Supports);
      s += rule.Attributes.Supports;
    }
    if (!stringBuilder.ToString().Equals(s) && Debugger.IsAttached)
      Debugger.Break();
    byte[] hash = new Crc32().ComputeHash(Encoding.UTF8.GetBytes(s));
    string crC = "";
    foreach (byte num in hash)
      crC += num.ToString("x2");
    Logger.Debug($"CRC: {s} => {crC}");
    return crC;
  }

  public static string GenerateCrC(ISelectionRuleExpander expander)
  {
    return CharacterFileVerification.GenerateCrC(expander.SelectionRule, expander.Number);
  }

  public static string GetCrC(this SelectRule rule, int number)
  {
    return CharacterFileVerification.GenerateCrC(rule, number);
  }
}
