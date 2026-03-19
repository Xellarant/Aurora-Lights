// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.Models.NewFolder1.FillableBackgroundCharacteristics
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

#nullable disable
namespace Builder.Presentation.Models.NewFolder1;

public class FillableBackgroundCharacteristics
{
  public FillableField Traits { get; } = new FillableField();

  public FillableField Ideals { get; } = new FillableField();

  public FillableField Bonds { get; } = new FillableField();

  public FillableField Flaws { get; } = new FillableField();

  public void Clear(bool clearOriginalContent = false)
  {
    this.Traits.Clear(clearOriginalContent);
    this.Ideals.Clear(clearOriginalContent);
    this.Bonds.Clear(clearOriginalContent);
    this.Flaws.Clear(clearOriginalContent);
  }
}
