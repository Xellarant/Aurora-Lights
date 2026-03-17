// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.Services.SelectionRuleCollectionService
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

using Builder.Data;
using Builder.Data.Rules;
using Builder.Presentation.Services.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

#nullable disable
namespace Builder.Presentation.Services;

public class SelectionRuleCollectionService
{
  private readonly ExpressionInterpreter _expressionInterpreter;
  private readonly SelectRule _rule;
  private readonly int _acquisitionLevel;
  private readonly ElementBaseCollection _baseCollection;
  private ElementBaseCollection _baseSupportsCollection;

  public SelectionRuleCollectionService(SelectRule rule)
  {
    this._rule = rule;
    this._acquisitionLevel = this._rule.Attributes.RequiredLevel;
    this._expressionInterpreter = new ExpressionInterpreter();
    this._expressionInterpreter.InitializeWithSelectionRule(this._rule);
    this._baseCollection = new ElementBaseCollection(DataManager.Current.ElementsCollection.Where<ElementBase>((Func<ElementBase, bool>) (element => element.Type.Equals(this._rule.Attributes.Type))));
  }

  protected virtual void Initialize()
  {
    this._baseSupportsCollection = new ElementBaseCollection(this._expressionInterpreter.EvaluateSupportsExpression<ElementBase>(this._rule.Attributes.Supports, (IEnumerable<ElementBase>) this._baseCollection, this._rule.Attributes.SupportsElementIdRange()));
  }

  public IEnumerable<ElementBase> GetSupportedCollection()
  {
    List<ElementBase> supportedCollection = new List<ElementBase>();
    supportedCollection.AddRange((IEnumerable<ElementBase>) this._baseSupportsCollection);
    return (IEnumerable<ElementBase>) supportedCollection;
  }

  public event EventHandler Evaluating;

  private async void EvaluateChanges()
  {
    this.OnEvaluating();
    await Task.Delay(1000);
  }

  protected virtual void OnEvaluating()
  {
    EventHandler evaluating = this.Evaluating;
    if (evaluating == null)
      return;
    evaluating((object) this, EventArgs.Empty);
  }
}
