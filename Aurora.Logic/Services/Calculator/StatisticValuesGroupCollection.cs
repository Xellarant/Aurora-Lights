// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.Services.Calculator.StatisticValuesGroupCollection
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Builder.Presentation.Services.Calculator;

public class StatisticValuesGroupCollection : List<StatisticValuesGroup>
{
  public bool ContainsGroup(string groupName)
  {
    if (groupName == null)
      return false;
    return groupName.StartsWith("-") ? this.Any<StatisticValuesGroup>((Func<StatisticValuesGroup, bool>) (x => x.GroupName.Equals(groupName.Substring(1, groupName.Length - 1)))) : this.Any<StatisticValuesGroup>((Func<StatisticValuesGroup, bool>) (x => x.GroupName.Equals(groupName)));
  }

  public void AddGroup(StatisticValuesGroup group)
  {
    if (this.ContainsGroup(group.GroupName))
    {
      StatisticValuesGroup group1 = this.GetGroup(group.GroupName);
      foreach (KeyValuePair<string, int> keyValuePair in group.GetValues())
        group1.AddValue(keyValuePair.Key, keyValuePair.Value);
    }
    else
      this.Add(group);
  }

  public StatisticValuesGroup GetGroup(string groupName, bool createNonExisting = true)
  {
    if (groupName.StartsWith("-"))
      groupName = groupName.Substring(1, groupName.Length - 1);
    if (this.ContainsGroup(groupName))
      return this.Single<StatisticValuesGroup>((Func<StatisticValuesGroup, bool>) (x => x.GroupName.Equals(groupName)));
    if (!createNonExisting)
      return (StatisticValuesGroup) null;
    StatisticValuesGroup group = new StatisticValuesGroup(groupName);
    this.Add(group);
    return group;
  }

  public int GetValue(string groupName)
  {
    return this.ContainsGroup(groupName) ? this.GetGroup(groupName).Sum() : 0;
  }
}
