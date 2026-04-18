// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.Utilities.GalleryUtilities
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

using System;
using System.IO;

#nullable disable
namespace Builder.Presentation.Utilities;

public static class GalleryUtilities
{
  public static string ConvertImageToBase64(string path)
  {
    return Convert.ToBase64String(File.ReadAllBytes(path));
  }

  public static bool SaveBase64AsImage(string base64, string outputPath)
  {
    File.WriteAllBytes(outputPath, Convert.FromBase64String(base64));
    return true;
  }
}
