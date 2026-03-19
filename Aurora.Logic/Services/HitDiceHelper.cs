// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.Services.HitDiceHelper
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Builder.Presentation.Services;

public static class HitDiceHelper
{
  public static HitDiceHelper.HitDiceObject Parse(string input)
  {
    if (input == null)
      throw new ArgumentNullException(nameof (input));
    string str = input.Contains("d") ? input.Trim() : throw new ArgumentException("hit dice string input doesn't contain a 'd'");
    int count;
    int sides;
    if (str.StartsWith("d"))
    {
      count = 1;
      sides = int.Parse(str.Trim('d'));
    }
    else
    {
      string[] source = str.Split('d');
      string s1 = ((IEnumerable<string>) source).FirstOrDefault<string>();
      string s2 = ((IEnumerable<string>) source).LastOrDefault<string>();
      count = int.Parse(s1);
      sides = int.Parse(s2);
    }
    return new HitDiceHelper.HitDiceObject(sides, count);
  }

  public class HitDiceObject
  {
    private readonly int _sides;
    private readonly int _count;

    public HitDiceObject(int sides, int count = 1)
    {
      this._sides = sides;
      this._count = count;
    }

    public int GetSides() => this._sides;

    public int GetCount() => this._count;

    public int GetMinimumValue() => this._count;

    public int GetMaximumValue() => this._count * this._sides;

    public int GetAverageValue()
    {
      int num = this.GetMinimumValue() + this.GetMaximumValue();
      return this._count == 1 ? num / 2 + 1 : num / 2;
    }

    public int GetRandomValue()
    {
      return new Random().Next(this.GetMinimumValue(), this.GetMaximumValue() + 1);
    }

    public override string ToString() => $"{this._count}d{this._sides}";
  }
}
