// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.Services.IExpressionConverter
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

#nullable disable
namespace Builder.Presentation.Services;

public interface IExpressionConverter
{
  string SanitizeExpression(string expression);

  string ConvertSupportsExpression(string expression, bool isRange = false);

  string ConvertRequirementsExpression(string expression);

  string ConvertRequirementsExpression(string expression, string listName);

  string ConvertEquippedExpression(string expression);
}
