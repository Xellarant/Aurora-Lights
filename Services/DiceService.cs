// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.Services.DiceService
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

#nullable disable
namespace Builder.Presentation.Services;

internal class DiceService
{
  private const int RandomizeDelay = 50;
  private readonly Random _rnd;

  public DiceService() => this._rnd = new Random();

  public async Task<int> D2(int amount = 1) => await this.RollAsync(2, amount);

  public async Task<int> D3(int amount = 1) => await this.RollAsync(3, amount);

  public async Task<int> D4(int amount = 1) => await this.RollAsync(4, amount);

  public async Task<int> D6(int amount = 1) => await this.RollAsync(6, amount);

  public async Task<int> D8(int amount = 1) => await this.RollAsync(8, amount);

  public async Task<int> D10(int amount = 1) => await this.RollAsync(10, amount);

  public async Task<int> D12(int amount = 1) => await this.RollAsync(12, amount);

  public async Task<int> D20(int amount = 1) => await this.RollAsync(20, amount);

  public async Task<int> D30(int amount = 1) => await this.RollAsync(30, amount);

  public async Task<int> D100(int amount = 1) => await this.RollAsync(100, amount);

  private async Task<int> RollAsync(int sides, int amount = 1)
  {
    int result = 0;
    for (int i = 0; i < amount; ++i)
    {
      await Task.Delay(50);
      result += this._rnd.Next(sides) + 1;
    }
    return result;
  }

  public async Task<int> RandomizeAbilityScore()
  {
    List<int> results = new List<int>();
    for (int i = 0; i < 4; ++i)
    {
      await Task.Delay(50);
      results.Add(this._rnd.Next(6) + 1);
    }
    int num = results.Sum() - results.Min();
    results = (List<int>) null;
    return num;
  }
}
