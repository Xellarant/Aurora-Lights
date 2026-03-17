// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.Extensions.StringExtentions
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

using System;
using System.Globalization;
using System.IO;

#nullable disable
namespace Builder.Presentation.Extensions;

public static class StringExtentions
{
  public static string ToValueString(this int value)
  {
    return value < 0 ? value.ToString((IFormatProvider) CultureInfo.InvariantCulture) : "+" + value.ToString();
  }

  public static string ToLevelString(this string value)
  {
    if (string.IsNullOrWhiteSpace(value))
      return value;
    int num = int.Parse(value);
    switch (num)
    {
      case 0:
        return value;
      case 1:
        return "1st";
      case 2:
        return "2nd";
      case 3:
        return "3rd";
      default:
        return $"{num}th";
    }
  }

  public static string ToSafeFilename(this string value, bool lowercase = true)
  {
    foreach (char invalidFileNameChar in Path.GetInvalidFileNameChars())
      value = value.Replace(invalidFileNameChar.ToString(), "");
    return !lowercase ? value.Trim() : value.ToLower().Trim();
  }
}
