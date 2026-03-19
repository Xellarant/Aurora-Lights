// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.Services.DynamicExpressionConverter
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

#nullable disable
namespace Builder.Presentation.Services;

public class DynamicExpressionConverter : IExpressionConverter
{
  public const string SupportsIdMatchPattern = "(ID_[^+]\\w+)";
  public const string SupportsStringMatchPattern = "([/0-9-a-zA-Z_\\\\.\\\\$ \\\\w]+)";
  public const string RequirementsIdMatchPattern = "(ID_[^+]\\w+)";
  public const string BracketsExpression = "(\\[([^\\]])+])";
  public const string BracketsMatchExpression = "(\\[[^\\]]+])";

  public string SanitizeExpression(string expression)
  {
    if (expression.Contains(","))
      expression = expression.Replace(",", "&&");
    if (expression.Contains("&amp;"))
      expression = expression.Replace("&amp;", "&");
    expression = expression.Replace("&", "&&");
    while (expression.Contains("&&&"))
      expression = expression.Replace("&&&", "&&");
    expression = expression.Replace("|", "||");
    while (expression.Contains("|||"))
      expression = expression.Replace("|||", "||");
    return expression.Trim();
  }

  public string ConvertSupportsExpression(string expression, bool isRange = false)
  {
    if (isRange)
      return this.ReplaceDistinctPattern(expression, "(ID_[^+]\\w+)", (Func<string, string>) (match => $"element.Id.Equals(\"{match}\")"));
    expression = this.SanitizeExpression(expression);
    if ((expression.Contains("$(spellcasting:list)") || expression.Contains("$(spellcasting:slots)")) && Debugger.IsAttached)
      Debugger.Break();
    expression = this.ReplacePattern(expression, "([/0-9-a-zA-Z_\\\\.\\\\$ \\\\w]+)", (Func<string, string>) (match => $"element.Supports.Contains(\"{match}\")"));
    return expression;
  }

  public string ConvertRequirementsExpression(string expression)
  {
    return this.ConvertRequirementsExpression(expression, "ids");
  }

  public string ConvertRequirementsExpression(string expression, string listName)
  {
    expression = this.SanitizeExpression(expression);
    expression = this.ReplacePattern(expression, "(ID_[^+]\\w+)", (Func<string, string>) (match => $"evaluate.Contains({listName}, \"{match}\")"));
    expression = this.ReplacePattern(expression, "(\\[[^\\]]+])", (Func<string, string>) (match =>
    {
      KeyValuePair<string, string> bracketExpression = this.ParseBracketExpression(match);
      return $"evaluate.Require(\"{bracketExpression.Key}\", \"{bracketExpression.Value}\")";
    }));
    return expression;
  }

  public string ConvertEquippedExpression(string expression)
  {
    expression = this.SanitizeExpression(expression);
    expression = this.ReplacePattern(expression, "(\\[[^\\]]+])", (Func<string, string>) (match =>
    {
      KeyValuePair<string, string> bracketExpression = this.ParseBracketExpression(match);
      return $"evaluate.Equipped(\"{bracketExpression.Key}\", \"{bracketExpression.Value}\")";
    }));
    return expression;
  }

  public KeyValuePair<string, string> ParseBracketExpression(string input)
  {
    string str1 = input.Trim(' ', '[', ']');
    char ch = str1.Contains("=") ? '=' : ':';
    string str2 = ((IEnumerable<string>) str1.Split(ch)).LastOrDefault<string>();
    return new KeyValuePair<string, string>(str1.Replace($"{ch}{str2}", ""), str2);
  }

  private string ReplacePattern(
    string expression,
    string pattern,
    Func<string, string> handleReplace)
  {
    List<string> list = Regex.Matches(expression, pattern).Cast<Match>().Select<Match, string>((Func<Match, string>) (x => x.Value.Trim())).ToList<string>();
    string[] strArray = Regex.Split(expression, pattern);
    for (int index = 0; index < strArray.Length; ++index)
    {
      string str = strArray[index].Trim();
      strArray[index] = !string.IsNullOrWhiteSpace(str) ? (!list.Contains(str) ? str : handleReplace(str)) : str;
    }
    return string.Join("", strArray).Trim();
  }

  private string ReplaceDistinctPattern(
    string expression,
    string pattern,
    Func<string, string> handleReplace)
  {
    List<string> list = Regex.Matches(expression, pattern).Cast<Match>().Select<Match, string>((Func<Match, string>) (x => x.Value.Trim())).Distinct<string>().ToList<string>();
    string[] strArray = Regex.Split(expression, pattern);
    for (int index = 0; index < strArray.Length; ++index)
    {
      string str = strArray[index].Trim();
      strArray[index] = !string.IsNullOrWhiteSpace(str) ? (!list.Contains(str) ? str : handleReplace(str)) : str;
    }
    return string.Join("", strArray).Trim();
  }
}
