// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.Services.Calculator.StatisticsCalculatedResult
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

using Builder.Data.Strings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

#nullable disable
namespace Builder.Presentation.Services.Calculator;

public class StatisticsCalculatedResult
{
  private readonly Dictionary<string, int> _values;

  public StatisticsCalculatedResult(bool initializeDefaults = false)
  {
    this._values = new Dictionary<string, int>();
    if (!initializeDefaults)
      return;
    AuroraStatisticStrings _names = new AuroraStatisticStrings();
    foreach (string key in ((IEnumerable<PropertyInfo>) typeof (AuroraStatisticStrings).GetProperties()).Select<PropertyInfo, string>((Func<PropertyInfo, string>) (x => x.GetValue((object) _names).ToString())))
      this._values.Add(key, 0);
    foreach (KeyValuePair<string, int> keyValuePair in this._values.ToList<KeyValuePair<string, int>>())
    {
      if (keyValuePair.Key.ToLower().Contains(":passive"))
        this._values[keyValuePair.Key] = 10;
    }
  }

  public void AddValue(string statisticsName, int value)
  {
    if (this._values.ContainsKey(statisticsName))
      this._values[statisticsName] += value;
    else
      this._values.Add(statisticsName, value);
  }

  public bool ContainsValue(string statisticsName) => this._values.ContainsKey(statisticsName);

  public int GetValue(string statisticsName)
  {
    return this.ContainsValue(statisticsName) ? this._values[statisticsName] : throw new KeyNotFoundException(statisticsName);
  }

  public Dictionary<string, int> GetValues() => this._values;
}
