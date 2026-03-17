// Decompiled with JetBrains decompiler
// Type: XamlGeneratedNamespace.GeneratedInternalTypeHelper
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Windows.Markup;

#nullable disable
namespace XamlGeneratedNamespace;

[DebuggerNonUserCode]
[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
[EditorBrowsable(EditorBrowsableState.Never)]
public sealed class GeneratedInternalTypeHelper : InternalTypeHelper
{
  protected override object CreateInstance(Type type, CultureInfo culture)
  {
    return Activator.CreateInstance(type, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.CreateInstance, (Binder) null, (object[]) null, culture);
  }

  protected override object GetPropertyValue(
    PropertyInfo propertyInfo,
    object target,
    CultureInfo culture)
  {
    return propertyInfo.GetValue(target, BindingFlags.Default, (Binder) null, (object[]) null, culture);
  }

  protected override void SetPropertyValue(
    PropertyInfo propertyInfo,
    object target,
    object value,
    CultureInfo culture)
  {
    propertyInfo.SetValue(target, value, BindingFlags.Default, (Binder) null, (object[]) null, culture);
  }

  protected override Delegate CreateDelegate(Type delegateType, object target, string handler)
  {
    return (Delegate) target.GetType().InvokeMember("_CreateDelegate", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.InvokeMethod, (Binder) null, target, new object[2]
    {
      (object) delegateType,
      (object) handler
    }, (CultureInfo) null);
  }

  protected override void AddEventHandler(EventInfo eventInfo, object target, Delegate handler)
  {
    eventInfo.AddEventHandler(target, handler);
  }
}
