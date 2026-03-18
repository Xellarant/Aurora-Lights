// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.Elements.SelectionCollectionService
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

using Builder.Data;
using Builder.Data.Rules;
using Builder.Presentation.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

#nullable disable
namespace Builder.Presentation.Elements;

[Obsolete("new implementation but not done yet!")]
public class SelectionCollectionService
{
  private readonly ExpressionInterpreter _expressionInterpreter;
  private readonly List<ElementBase> _baseCollection;
  private readonly SelectRule _selectionRule;
  private readonly ISourceRestrictionsProvider _sourceRestrictionsProvider;

  public bool PopulatingCollection { get; set; }

  public bool IgnoreRequirements { get; set; }

  public bool IgnoreSourceRestrictions { get; set; }

  public int AcquisitionLevel { get; }

  public bool ContainsSupports { get; }

  public bool IsElementsRange { get; }

  public List<ElementBase> SupportedCollection { get; protected set; }

  public SelectionCollectionService(
    SelectRule selectionRule,
    ISourceRestrictionsProvider sourceRestrictionsProvider)
  {
    this._selectionRule = selectionRule ?? throw new ArgumentNullException(nameof (selectionRule));
    this._sourceRestrictionsProvider = sourceRestrictionsProvider;
    this._baseCollection = new List<ElementBase>();
    this._expressionInterpreter = new ExpressionInterpreter();
    this._expressionInterpreter.InitializeWithSelectionRule(this._selectionRule);
    this.AcquisitionLevel = this._selectionRule.Attributes.RequiredLevel;
    this.ContainsSupports = this._selectionRule.Attributes.ContainsSupports();
    this.IsElementsRange = this._selectionRule.Attributes.SupportsElementIdRange();
    this.SupportedCollection = new List<ElementBase>();
  }

  public event EventHandler<ElementCollectionEventArgs> BaseCollectionPopulated;

  public event EventHandler<ElementCollectionEventArgs> CollectionPopulated;

  public async Task InitializeAsync() => await this.PopulateBaseCollectionAsync();

  public async Task<List<ElementBase>> PopulateAsync()
  {
    this.PopulatingCollection = true;
    await Task.Run(async () => await PopulateAsyncCore());
    this.PopulatingCollection = false;
    this.OnCollectionPopulated(this.SupportedCollection);
    return this.SupportedCollection;
  }

  protected async Task PopulateBaseCollectionAsync()
  {
    this.PopulatingCollection = true;
    await Task.Run(async () => await PopulateBaseCollectionAsyncCore());
    this.PopulatingCollection = false;
    this.OnBaseCollectionPopulated(this._baseCollection);
  }

  // Stub implementations - original logic was in compiler-generated closures
  // that dotPeek could not reconstruct. These are no-ops until the logic
  // can be recovered from the assembly via another approach.
  private Task PopulateAsyncCore() => Task.CompletedTask;
  private Task PopulateBaseCollectionAsyncCore() => Task.CompletedTask;

  protected List<ElementBase> RemoveSourceRestrictedElements(List<ElementBase> selectionElements)
  {
    List<string> list1 = this._sourceRestrictionsProvider.GetRestrictedElements().ToList<string>();
    List<string> list2 = this._sourceRestrictionsProvider.GetUndefinedRestrictedSources().ToList<string>();
    List<ElementBase> elementBaseList = new List<ElementBase>();
    foreach (ElementBase selectionElement in selectionElements)
    {
      if (list1.Contains(selectionElement.Id))
        elementBaseList.Add(selectionElement);
      else if (list2.Contains(selectionElement.Source))
        elementBaseList.Add(selectionElement);
    }
    foreach (ElementBase elementBase in elementBaseList)
      selectionElements.Remove(elementBase);
    return selectionElements;
  }

  protected List<ElementBase> GetAcceptedRequirementElements(
    List<ElementBase> elements,
    List<string> idRange)
  {
    List<ElementBase> requirementElements = new List<ElementBase>();
    foreach (ElementBase element in elements)
    {
      if (element.HasRequirements)
      {
        if (this._expressionInterpreter.EvaluateElementRequirementsExpression(element.Requirements, (IEnumerable<string>) idRange))
          requirementElements.Add(element);
      }
      else
        requirementElements.Add(element);
    }
    return requirementElements;
  }

  protected virtual void OnBaseCollectionPopulated(List<ElementBase> elements)
  {
    EventHandler<ElementCollectionEventArgs> collectionPopulated = this.BaseCollectionPopulated;
    if (collectionPopulated == null)
      return;
    collectionPopulated((object) this, new ElementCollectionEventArgs(elements));
  }

  protected virtual void OnCollectionPopulated(List<ElementBase> elements)
  {
    EventHandler<ElementCollectionEventArgs> collectionPopulated = this.CollectionPopulated;
    if (collectionPopulated == null)
      return;
    collectionPopulated((object) this, new ElementCollectionEventArgs(elements));
  }
}
