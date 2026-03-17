// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.Services.ClassProgressionManager
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

using Builder.Core.Logging;
using Builder.Data;
using Builder.Data.Elements;
using Builder.Data.Extensions;
using Builder.Data.Rules;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

#nullable disable
namespace Builder.Presentation.Services;

public class ClassProgressionManager : ProgressionManager
{
  private DiceService _dice;
  private int[] _averageHitPointsArray = new int[20];
  private int[] _randomHitPointsArray = new int[20];
  private ElementBase _classElement;

  public ClassProgressionManager(
    bool isMainClass,
    bool isMulticlass,
    int startingLevel,
    ElementBase classElement)
  {
    this._dice = new DiceService();
    this.IsMainClass = isMainClass;
    this.IsMulticlass = isMulticlass;
    this.StartingLevel = startingLevel;
    this.SetClass(classElement);
    this.ProgressionLevel = 1;
  }

  public bool IsObsolete { get; set; }

  public bool IsMainClass { get; }

  public bool IsMulticlass { get; }

  public int StartingLevel { get; }

  public ElementBase ClassElement
  {
    get => this._classElement;
    private set
    {
      this._classElement = value;
      if (this._classElement == null)
        return;
      this.HD = this._classElement.AsElement<Class>().HitDice;
      if (!this._classElement.Aquisition.WasSelected)
        return;
      this.SelectRule = this._classElement.Aquisition.SelectRule;
    }
  }

  public ElementBaseCollection LevelElements { get; } = new ElementBaseCollection();

  public string HD { get; private set; }

  public int GetHitDieValue()
  {
    switch (this.HD)
    {
      case "d10":
        return 10;
      case "d100":
        return 100;
      case "d12":
        return 12;
      case "d2":
        return 2;
      case "d20":
        return 20;
      case "d3":
        return 3;
      case "d4":
        return 4;
      case "d50":
        return 50;
      case "d6":
        return 6;
      case "d8":
        return 8;
      default:
        throw new ArgumentException("HD");
    }
  }

  public void SetClass(ElementBase element)
  {
    string hd = this.HD;
    this.ClassElement = element;
    if (element == null)
      return;
    this._averageHitPointsArray = this.GenerateAverageHitPointsArray(this.HD, this.IsMainClass);
    if (element.AsElement<Class>().HitDice != hd)
      this.GenerateRandomArray();
    if (this._randomHitPointsArray != null && ((IEnumerable<int>) this._randomHitPointsArray).Sum() != 0)
      return;
    this.GenerateRandomArray();
  }

  private void GenerateRandomArray()
  {
    Stopwatch stopwatch = Stopwatch.StartNew();
    Logger.Info($"gen random array {stopwatch.ElapsedMilliseconds} {string.Join<int>(",", (IEnumerable<int>) this._randomHitPointsArray)}");
    int[] result = (int[]) null;
    Task.Run((Action) (() => result = this.GenerateRandomHitPointsArray(this.HD, this.IsMainClass).Result)).Wait();
    this._randomHitPointsArray = result;
    Logger.Info($"gen random array complete {stopwatch.ElapsedMilliseconds} {string.Join<int>(",", (IEnumerable<int>) this._randomHitPointsArray)}");
  }

  public void RemoveClass() => this.ClassElement = (ElementBase) null;

  private async Task<int[]> GenerateRandomHitPointsArray(string hd, bool isMainClass)
  {
    List<int> array = new List<int>();
    for (int level = 1; level <= 20; ++level)
    {
      if (level == 1 & isMainClass)
      {
        if (hd != null)
        {
          switch (hd)
          {
            case "d10":
              array.Add(10);
              continue;
            case "d12":
              array.Add(12);
              continue;
            case "d2":
              array.Add(2);
              continue;
            case "d20":
              array.Add(20);
              continue;
            case "d4":
              array.Add(4);
              continue;
            case "d6":
              array.Add(6);
              continue;
            case "d8":
              array.Add(8);
              continue;
          }
        }
        throw new ArgumentException(nameof (hd));
      }
      List<int> intList;
      if (hd != null)
      {
        switch (hd)
        {
          case "d10":
            intList = array;
            intList.Add(await this._dice.D10());
            intList = (List<int>) null;
            continue;
          case "d12":
            intList = array;
            intList.Add(await this._dice.D12());
            intList = (List<int>) null;
            continue;
          case "d2":
            intList = array;
            intList.Add(await this._dice.D2());
            intList = (List<int>) null;
            continue;
          case "d20":
            intList = array;
            intList.Add(await this._dice.D20());
            intList = (List<int>) null;
            continue;
          case "d4":
            intList = array;
            intList.Add(await this._dice.D4());
            intList = (List<int>) null;
            continue;
          case "d6":
            intList = array;
            intList.Add(await this._dice.D6());
            intList = (List<int>) null;
            continue;
          case "d8":
            intList = array;
            intList.Add(await this._dice.D8());
            intList = (List<int>) null;
            continue;
        }
      }
      throw new ArgumentException(nameof (hd));
    }
    int[] array1 = array.ToArray();
    array = (List<int>) null;
    return array1;
  }

  private int[] GenerateAverageHitPointsArray(string hd, bool isMainClass)
  {
    List<int> intList = new List<int>();
    for (int index = 1; index <= 20; ++index)
    {
      if (index == 1 & isMainClass)
      {
        switch (hd)
        {
          case "d10":
            intList.Add(10);
            continue;
          case "d12":
            intList.Add(12);
            continue;
          case "d2":
            intList.Add(2);
            continue;
          case "d20":
            intList.Add(20);
            continue;
          case "d4":
            intList.Add(4);
            continue;
          case "d6":
            intList.Add(6);
            continue;
          case "d8":
            intList.Add(8);
            continue;
          default:
            throw new ArgumentException(nameof (hd));
        }
      }
      else
      {
        switch (hd)
        {
          case "d10":
            intList.Add(6);
            continue;
          case "d12":
            intList.Add(7);
            continue;
          case "d2":
            intList.Add(2);
            continue;
          case "d20":
            intList.Add(11);
            continue;
          case "d4":
            intList.Add(3);
            continue;
          case "d6":
            intList.Add(4);
            continue;
          case "d8":
            intList.Add(5);
            continue;
          default:
            throw new ArgumentException(nameof (hd));
        }
      }
    }
    return intList.ToArray();
  }

  public bool HasArchetype() => this.Elements.ContainsType("Archetype");

  public string GetClassLevelStatisticsName()
  {
    return "level:" + this.ClassElement.Name.ToLowerInvariant();
  }

  public int GetHitPoints()
  {
    int hitPoints = 0;
    if (CharacterManager.Current.ContainsAverageHitPointsOption())
    {
      for (int index = 0; index < this.ProgressionLevel; ++index)
        hitPoints += this._averageHitPointsArray[index];
    }
    else
    {
      for (int index = 0; index < this.ProgressionLevel; ++index)
        hitPoints += this._randomHitPointsArray[index];
    }
    return hitPoints;
  }

  public int[] GetRandomHitPointsArrayAsync() => this._randomHitPointsArray;

  public void SetRandomHitPointsArray(int[] array) => this._randomHitPointsArray = array;

  public SelectRule SelectRule { get; set; }

  public override string ToString() => $"{this.ClassElement} {this.ProgressionLevel}";
}
