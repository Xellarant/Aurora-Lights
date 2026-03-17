// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.Interfaces.ISupportExpanders
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

using System.Collections.Generic;
using System.Collections.ObjectModel;

#nullable disable
namespace Builder.Presentation.Interfaces;

public interface ISupportExpanders
{
  string Name { get; }

  IEnumerable<string> Listings { get; }

  ObservableCollection<ISelectionRuleExpander> Expanders { get; set; }

  void AddExpander(ISelectionRuleExpander expander);
}
