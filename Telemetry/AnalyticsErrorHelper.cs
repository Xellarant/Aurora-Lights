// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.Telemetry.AnalyticsErrorHelper
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

using Microsoft.AppCenter.Crashes;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

#nullable disable
namespace Builder.Presentation.Telemetry;

public static class AnalyticsErrorHelper
{
  private static ErrorAttachmentLog CreateAttachment(string content, string filename = "attachment.txt")
  {
    return ErrorAttachmentLog.AttachmentWithText(content, filename);
  }

  public static Dictionary<string, string> CreateProperties(string key = null, string value = null)
  {
    if (key == null || value == null)
      return new Dictionary<string, string>();
    return new Dictionary<string, string>()
    {
      {
        key,
        value
      }
    };
  }

  public static void Exception(
    System.Exception ex,
    Dictionary<string, string> additionalProperties = null,
    string attachmentContent = null,
    [CallerMemberName] string method = "",
    [CallerLineNumber] int line = 0)
  {
    Dictionary<string, string> properties = new Dictionary<string, string>()
    {
      {
        nameof (method),
        method
      },
      {
        nameof (line),
        line.ToString()
      }
    };
    if (additionalProperties != null)
    {
      foreach (KeyValuePair<string, string> additionalProperty in additionalProperties)
        properties.Add(additionalProperty.Key, additionalProperty.Value);
    }
    if (string.IsNullOrWhiteSpace(attachmentContent))
    {
      Microsoft.AppCenter.Crashes.Crashes.TrackError(ex, (IDictionary<string, string>) properties);
    }
    else
    {
      ErrorAttachmentLog attachment = AnalyticsErrorHelper.CreateAttachment(attachmentContent);
      Microsoft.AppCenter.Crashes.Crashes.TrackError(ex, (IDictionary<string, string>) properties, attachment);
    }
  }
}
