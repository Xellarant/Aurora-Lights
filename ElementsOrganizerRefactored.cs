// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.ElementsOrganizerRefactored
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

using Builder.Core.Logging;
using Builder.Data;
using Builder.Data.Rules;
using Builder.Presentation.Services;
using DynamicExpresso;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

#nullable disable
namespace Builder.Presentation;

public class ElementsOrganizerRefactored
{
  private readonly ExpressionInterpreter _interpreter;
  private readonly ElementBaseCollection _elements;

  public ElementsOrganizerRefactored(IEnumerable<ElementBase> elements = null)
  {
    this._interpreter = new ExpressionInterpreter();
    if (elements == null)
      return;
    this._elements = new ElementBaseCollection(elements);
  }

  public void Initialize(IEnumerable<ElementBase> elements)
  {
    this._elements.Clear();
    this._elements.AddRange(elements);
  }

  public bool ContainsElementType(string type)
  {
    return this._elements.Any<ElementBase>((Func<ElementBase, bool>) (x => x.Type.Equals(type)));
  }

  public IEnumerable<ElementBase> GetElementsAs(string type, bool includeDuplicates = true)
  {
    List<ElementBase> elementsAs = new List<ElementBase>();
    if (includeDuplicates)
    {
      foreach (ElementBase original in this._elements.Where<ElementBase>((Func<ElementBase, bool>) (element => element.Type.Equals(type))))
        elementsAs.Add(original.Copy<ElementBase>());
      return (IEnumerable<ElementBase>) elementsAs;
    }
    foreach (ElementBase original in this._elements.Where<ElementBase>((Func<ElementBase, bool>) (element => element.Type.Equals(type))).GroupBy<ElementBase, string>((Func<ElementBase, string>) (x => x.Id)).Select<IGrouping<string, ElementBase>, ElementBase>((Func<IGrouping<string, ElementBase>, ElementBase>) (x => x.First<ElementBase>())))
      elementsAs.Add(original.Copy<ElementBase>());
    return (IEnumerable<ElementBase>) elementsAs;
  }

  public IEnumerable<T> GetElementsAs<T>(bool includeDuplicates = true) where T : ElementBase
  {
    List<T> elementsAs = new List<T>();
    if (includeDuplicates)
    {
      foreach (T original in this._elements.Where<ElementBase>((Func<ElementBase, bool>) (element => element is T)).Cast<T>())
        elementsAs.Add(original.Copy<T>());
      return (IEnumerable<T>) elementsAs;
    }
    foreach (T original in this._elements.Where<ElementBase>((Func<ElementBase, bool>) (element => element is T)).GroupBy<ElementBase, string>((Func<ElementBase, string>) (x => x.Id)).Select<IGrouping<string, ElementBase>, ElementBase>((Func<IGrouping<string, ElementBase>, ElementBase>) (x => x.First<ElementBase>())).Cast<T>())
      elementsAs.Add(original.Copy<T>());
    return (IEnumerable<T>) elementsAs;
  }

  public ElementBase GetElement(string id)
  {
    return this._elements.FirstOrDefault<ElementBase>((Func<ElementBase, bool>) (x => x.Id.Equals(id))).Copy<ElementBase>();
  }

  public IEnumerable<ElementBase> GetElements()
  {
    List<ElementBase> elements = new List<ElementBase>();
    foreach (ElementBase element in (Collection<ElementBase>) this._elements)
      elements.Add(element.Copy<ElementBase>());
    return (IEnumerable<ElementBase>) elements;
  }

  public ElementBaseCollection GetSupportedElements(SelectRule rule)
  {
    bool containsElementIDs = rule.Attributes.ContainsSupports() && !rule.Attributes.Supports.Contains("||") && rule.Attributes.Supports.Contains("|");
    return this.GetSupportedElements(rule, containsElementIDs);
  }

  private ElementBaseCollection GetSupportedElements(SelectRule rule, bool containsElementIDs)
  {
    IEnumerable<ElementBase> elementBases = this._elements.Where<ElementBase>((Func<ElementBase, bool>) (x => x.Type.Equals(rule.Attributes.Type)));
    if (!rule.Attributes.ContainsSupports())
      return new ElementBaseCollection(elementBases);
    if (!containsElementIDs)
      return new ElementBaseCollection(this._interpreter.EvaluateSupportsExpression<ElementBase>(rule.Attributes.Supports, elementBases));
    List<string> stringList = new List<string>();
    foreach (Match match in Regex.Matches(rule.Attributes.Supports, "([-a-zA-Z \\w]+)").Cast<Match>())
    {
      if (!stringList.Contains(match.Value))
        stringList.Add(match.Value);
    }
    string expressionText;
    if (containsElementIDs)
    {
      string str = rule.Attributes.Supports;
      foreach (string oldValue in stringList)
        str = str.Replace(oldValue, $"x.Id.Equals(\"{oldValue}\")");
      expressionText = str.Replace("|", "||");
    }
    else
    {
      expressionText = rule.Attributes.Supports;
      foreach (string oldValue in stringList)
        expressionText = expressionText.Replace(oldValue, $"x.Supports.Contains(\"{oldValue}\")");
    }
    Interpreter interpreter = new Interpreter();
    interpreter.EnableAssignment(AssignmentOperators.None);
    Logger.Debug($"interpreting the {expressionText} with {rule}");
    Expression<Func<ElementBase, bool>> asExpression = interpreter.ParseAsExpression<Func<ElementBase, bool>>(expressionText, "x");
    return new ElementBaseCollection((IEnumerable<ElementBase>) elementBases.AsQueryable<ElementBase>().Where<ElementBase>(asExpression));
  }
}
