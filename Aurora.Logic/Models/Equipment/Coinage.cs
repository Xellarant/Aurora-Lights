// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.Models.Equipment.Coinage
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

using Builder.Core;
using Builder.Core.Logging;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

#nullable disable
namespace Builder.Presentation.Models.Equipment;

public class Coinage : ObservableObject
{
  private long _calculationBase;
  private long _copper;
  private long _silver;
  private long _electrum;
  private long _gold;
  private long _platinum;

  public Coinage()
  {
    this._calculationBase = 0L;
    this._copper = 0L;
    this._silver = 0L;
    this._electrum = 0L;
    this._gold = 0L;
    this._platinum = 0L;
  }

  public long CalculationBase
  {
    get => this._calculationBase;
    private set
    {
      this.SetProperty<long>(ref this._calculationBase, value, nameof (CalculationBase));
      if (!this.ValidateBase())
        Logger.Warning($"CalculationBase does not equal the calculated [base:{this._calculationBase}] [calc:{this.CalculateBase(Coinage.CurrencyCoin.Copper, this.Copper) + this.CalculateBase(Coinage.CurrencyCoin.Silver, this.Silver) + this.CalculateBase(Coinage.CurrencyCoin.Electrum, this.Electrum) + this.CalculateBase(Coinage.CurrencyCoin.Gold, this.Gold) + this.CalculateBase(Coinage.CurrencyCoin.Platinum, this.Platinum)}]");
      this.OnPropertyChanged("DisplayCoinage");
    }
  }

  public long Copper
  {
    get => this._copper;
    set => this.SetProperty<long>(ref this._copper, value, nameof (Copper));
  }

  public long Silver
  {
    get => this._silver;
    set
    {
      long silver = this._silver;
      this.SetProperty<long>(ref this._silver, value, nameof (Silver));
    }
  }

  public long Electrum
  {
    get => this._electrum;
    set
    {
      long electrum = this._electrum;
      this.SetProperty<long>(ref this._electrum, value, nameof (Electrum));
    }
  }

  public long Gold
  {
    get => this._gold;
    set
    {
      long gold = this._gold;
      this.SetProperty<long>(ref this._gold, value, nameof (Gold));
    }
  }

  public long Platinum
  {
    get => this._platinum;
    set
    {
      long platinum = this._platinum;
      this.SetProperty<long>(ref this._platinum, value, nameof (Platinum));
    }
  }

  public Decimal DisplayCoinage => (Decimal) this._calculationBase / 100M;

  public void Clear()
  {
    this.CalculationBase = 0L;
    this.Copper = 0L;
    this.Silver = 0L;
    this.Electrum = 0L;
    this.Gold = 0L;
    this.Platinum = 0L;
  }

  public void Set(long copper, long silver, long electrum, long gold, long platinum)
  {
    this.Clear();
    this.Deposit(Coinage.CurrencyCoin.Copper, copper);
    this.Deposit(Coinage.CurrencyCoin.Silver, silver);
    this.Deposit(Coinage.CurrencyCoin.Electrum, electrum);
    this.Deposit(Coinage.CurrencyCoin.Gold, gold);
    this.Deposit(Coinage.CurrencyCoin.Platinum, platinum);
  }

  public long UpdateCalculationBaseFromOutside([CallerMemberName] string callerName = null)
  {
    if (callerName == null || callerName.Equals(nameof (Coinage)))
      return this.CalculationBase;
    long num1 = this.CalculateBase(Coinage.CurrencyCoin.Copper, this.Copper);
    long num2 = this.CalculateBase(Coinage.CurrencyCoin.Silver, this.Silver);
    long num3 = this.CalculateBase(Coinage.CurrencyCoin.Electrum, this.Electrum);
    long num4 = this.CalculateBase(Coinage.CurrencyCoin.Gold, this.Gold);
    long num5 = this.CalculateBase(Coinage.CurrencyCoin.Platinum, this.Platinum);
    long num6 = num2;
    this.CalculationBase = num1 + num6 + num3 + num4 + num5;
    this.OnPropertyChanged("DisplayCoinage");
    return this.CalculationBase;
  }

  public void Deposit(Coinage.CurrencyCoin coin, long amount = 1)
  {
    switch (coin)
    {
      case Coinage.CurrencyCoin.Copper:
        this.Copper += amount;
        this.CalculationBase += amount;
        break;
      case Coinage.CurrencyCoin.Silver:
        this.Silver += amount;
        this.CalculationBase += amount * 10L;
        break;
      case Coinage.CurrencyCoin.Electrum:
        this.Electrum += amount;
        this.CalculationBase += amount * 50L;
        break;
      case Coinage.CurrencyCoin.Gold:
        this.Gold += amount;
        this.CalculationBase += amount * 100L;
        break;
      case Coinage.CurrencyCoin.Platinum:
        this.Platinum += amount;
        this.CalculationBase += amount * 1000L;
        break;
    }
  }

  public bool Withdraw(Coinage.CurrencyCoin coin, long amount, bool withdrawLowerDenomination = true)
  {
    if (!withdrawLowerDenomination && !this.CanWithdrawCoin(coin, amount) || !this.CanWithdraw(coin, amount))
      return false;
    long withdrawAmount = this.CalculateBase(coin, amount);
    if (this.CanWithdrawCoin(coin, amount))
    {
      switch (coin)
      {
        case Coinage.CurrencyCoin.Copper:
          this.Copper -= amount;
          break;
        case Coinage.CurrencyCoin.Silver:
          this.Silver -= amount;
          break;
        case Coinage.CurrencyCoin.Electrum:
          this.Electrum -= amount;
          break;
        case Coinage.CurrencyCoin.Gold:
          this.Gold -= amount;
          break;
        case Coinage.CurrencyCoin.Platinum:
          this.Platinum -= amount;
          break;
      }
      this.CalculationBase -= withdrawAmount;
    }
    else
    {
      Coinage.CurrencyCoin coin1;
      for (coin1 = coin; !this.CanWithdrawUnderCoin(coin1, withdrawAmount); coin1 = this.Up(coin1))
      {
        if (coin1 == Coinage.CurrencyCoin.Platinum)
          this.ConvertDown(coin1, 1L);
      }
      this.WithdrawUnderCoin(coin1, withdrawAmount);
    }
    return true;
  }

  public bool HasSufficienctFunds(Coinage.CurrencyCoin coin, long amount)
  {
    return this.CanWithdraw(coin, amount);
  }

  private bool CanWithdraw(Coinage.CurrencyCoin coin, long amount)
  {
    return this.CalculateBase(coin, amount) <= this._calculationBase;
  }

  private bool CanWithdrawCoin(Coinage.CurrencyCoin coin, long amount)
  {
    switch (coin)
    {
      case Coinage.CurrencyCoin.Copper:
        return this.Copper >= amount;
      case Coinage.CurrencyCoin.Silver:
        return this.Silver >= amount;
      case Coinage.CurrencyCoin.Electrum:
        return this.Electrum >= amount;
      case Coinage.CurrencyCoin.Gold:
        return this.Gold >= amount;
      case Coinage.CurrencyCoin.Platinum:
        return this.Platinum >= amount;
      default:
        return false;
    }
  }

  private bool CanWithdrawUnderCoin(Coinage.CurrencyCoin coin, long withdrawAmount)
  {
    switch (coin)
    {
      case Coinage.CurrencyCoin.Copper:
        return false;
      case Coinage.CurrencyCoin.Silver:
        return withdrawAmount <= this.CalculateBase(Coinage.CurrencyCoin.Copper, this.Copper);
      case Coinage.CurrencyCoin.Electrum:
        return withdrawAmount <= this.CalculateBase(Coinage.CurrencyCoin.Copper, this.Copper) + this.CalculateBase(Coinage.CurrencyCoin.Silver, this.Silver);
      case Coinage.CurrencyCoin.Gold:
        return withdrawAmount <= this.CalculateBase(Coinage.CurrencyCoin.Copper, this.Copper) + this.CalculateBase(Coinage.CurrencyCoin.Silver, this.Silver) + this.CalculateBase(Coinage.CurrencyCoin.Electrum, this.Electrum);
      case Coinage.CurrencyCoin.Platinum:
        return withdrawAmount <= this.CalculateBase(Coinage.CurrencyCoin.Copper, this.Copper) + this.CalculateBase(Coinage.CurrencyCoin.Silver, this.Silver) + this.CalculateBase(Coinage.CurrencyCoin.Electrum, this.Electrum) + this.CalculateBase(Coinage.CurrencyCoin.Gold, this.Gold);
      default:
        return false;
    }
  }

  private long HowMuchUnderCoin(Coinage.CurrencyCoin coin)
  {
    switch (coin)
    {
      case Coinage.CurrencyCoin.Silver:
        return this.CalculateBase(Coinage.CurrencyCoin.Copper, this.Copper);
      case Coinage.CurrencyCoin.Electrum:
        return this.CalculateBase(Coinage.CurrencyCoin.Copper, this.Copper) + this.CalculateBase(Coinage.CurrencyCoin.Silver, this.Silver);
      case Coinage.CurrencyCoin.Gold:
        return this.CalculateBase(Coinage.CurrencyCoin.Copper, this.Copper) + this.CalculateBase(Coinage.CurrencyCoin.Silver, this.Silver) + this.CalculateBase(Coinage.CurrencyCoin.Electrum, this.Electrum);
      case Coinage.CurrencyCoin.Platinum:
        return this.CalculateBase(Coinage.CurrencyCoin.Copper, this.Copper) + this.CalculateBase(Coinage.CurrencyCoin.Silver, this.Silver) + this.CalculateBase(Coinage.CurrencyCoin.Electrum, this.Electrum) + this.CalculateBase(Coinage.CurrencyCoin.Gold, this.Gold);
      default:
        return 0;
    }
  }

  private void WithdrawUnderCoin(Coinage.CurrencyCoin coin, long withdrawAmount)
  {
    Stopwatch stopwatch = Stopwatch.StartNew();
    if (coin == Coinage.CurrencyCoin.Platinum || coin == Coinage.CurrencyCoin.Gold || coin == Coinage.CurrencyCoin.Electrum)
    {
      while (coin != Coinage.CurrencyCoin.Silver)
      {
        coin = this.Down(coin);
        while (!this.CanWithdrawUnderCoin(coin, withdrawAmount))
        {
          long required = withdrawAmount - this.HowMuchUnderCoin(coin);
          long basedOnRequiredBase = this.CalculateBasedOnRequiredBase(coin, required);
          this.ConvertDown(coin, Math.Max(1L, basedOnRequiredBase));
        }
      }
    }
    while (this.Copper < withdrawAmount)
    {
      this.CalculateBasedOnRequiredBase(Coinage.CurrencyCoin.Silver, withdrawAmount - this.Copper);
      this.ConvertDown(Coinage.CurrencyCoin.Silver, Math.Max(1L, (withdrawAmount - this.Copper) / 10L));
    }
    stopwatch.Stop();
    Logger.Warning($"WithdrawUnderCoin {stopwatch.ElapsedMilliseconds}ms");
    this.Copper -= withdrawAmount;
    this.CalculationBase -= withdrawAmount;
  }

  private bool ConvertDown(Coinage.CurrencyCoin coin, long amount)
  {
    switch (coin)
    {
      case Coinage.CurrencyCoin.Copper:
        this.ConvertDown(Coinage.CurrencyCoin.Silver, 1L);
        break;
      case Coinage.CurrencyCoin.Silver:
        if (this.Silver > 0L && this.Withdraw(Coinage.CurrencyCoin.Silver, amount, false))
        {
          this.Deposit(Coinage.CurrencyCoin.Copper, amount * 10L);
          break;
        }
        if (this.Silver > 0L)
        {
          long silver = this.Silver;
          this.Withdraw(Coinage.CurrencyCoin.Silver, silver, false);
          this.Deposit(Coinage.CurrencyCoin.Copper, silver * 10L);
          break;
        }
        this.ConvertDown(Coinage.CurrencyCoin.Electrum, 1L);
        break;
      case Coinage.CurrencyCoin.Electrum:
        if (this.Electrum > 0L && this.Withdraw(Coinage.CurrencyCoin.Electrum, amount, false))
        {
          this.Deposit(Coinage.CurrencyCoin.Silver, amount * 5L);
          break;
        }
        if (this.Electrum > 0L)
        {
          long electrum = this.Electrum;
          this.Withdraw(Coinage.CurrencyCoin.Electrum, electrum, false);
          this.Deposit(Coinage.CurrencyCoin.Silver, electrum * 5L);
          break;
        }
        this.ConvertDown(Coinage.CurrencyCoin.Gold, 1L);
        break;
      case Coinage.CurrencyCoin.Gold:
        if (this.Gold > 0L && this.Withdraw(Coinage.CurrencyCoin.Gold, amount, false))
        {
          this.Deposit(Coinage.CurrencyCoin.Electrum, amount * 2L);
          break;
        }
        if (this.Gold > 0L)
        {
          long gold = this.Gold;
          this.Withdraw(Coinage.CurrencyCoin.Gold, gold, false);
          this.Deposit(Coinage.CurrencyCoin.Electrum, gold * 2L);
          break;
        }
        this.ConvertDown(Coinage.CurrencyCoin.Platinum, 1L);
        break;
      case Coinage.CurrencyCoin.Platinum:
        if (this.Platinum > 0L && this.Withdraw(Coinage.CurrencyCoin.Platinum, amount, false))
        {
          this.Deposit(Coinage.CurrencyCoin.Gold, amount * 10L);
          break;
        }
        if (this.Platinum <= 0L)
          return false;
        long platinum = this.Platinum;
        this.Withdraw(Coinage.CurrencyCoin.Platinum, platinum, false);
        this.Deposit(Coinage.CurrencyCoin.Gold, platinum * 10L);
        break;
    }
    return true;
  }

  private void ConvertTo(Coinage.CurrencyCoin from, Coinage.CurrencyCoin to)
  {
    if (from == Coinage.CurrencyCoin.Copper && to == Coinage.CurrencyCoin.Silver)
    {
      this.Silver += this.Copper / 10L;
      this.Copper %= 10L;
    }
    if (from == Coinage.CurrencyCoin.Silver && to == Coinage.CurrencyCoin.Electrum)
    {
      this.ConvertTo(this.Down(from), from);
      this.Electrum += this.Silver / 5L;
      this.Silver %= 5L;
    }
    if (from == Coinage.CurrencyCoin.Electrum && to == Coinage.CurrencyCoin.Gold)
    {
      this.ConvertTo(this.Down(from), from);
      this.Gold += this.Electrum / 2L;
      this.Electrum %= 2L;
    }
    if (from != Coinage.CurrencyCoin.Gold || to != Coinage.CurrencyCoin.Platinum)
      return;
    this.ConvertTo(this.Down(from), from);
    this.Platinum += this.Gold / 10L;
    this.Gold %= 10L;
  }

  public void ConvertTo(Coinage.CurrencyCoin coin)
  {
    switch (coin)
    {
      case Coinage.CurrencyCoin.Copper:
        this.Copper += this.Silver * 10L;
        this.Copper += this.Electrum * 50L;
        this.Copper += this.Gold * 100L;
        this.Copper += this.Platinum * 1000L;
        this.Silver = 0L;
        this.Electrum = 0L;
        this.Gold = 0L;
        this.Platinum = 0L;
        break;
      case Coinage.CurrencyCoin.Silver:
        this.ConvertTo(Coinage.CurrencyCoin.Copper, Coinage.CurrencyCoin.Silver);
        this.Silver += this.Electrum * 5L;
        this.Silver += this.Gold * 10L;
        this.Silver += this.Platinum * 100L;
        this.Electrum = 0L;
        this.Gold = 0L;
        this.Platinum = 0L;
        break;
      case Coinage.CurrencyCoin.Electrum:
        this.ConvertTo(Coinage.CurrencyCoin.Silver, Coinage.CurrencyCoin.Electrum);
        this.Electrum += this.Gold * 2L;
        this.Electrum += this.Platinum * 20L;
        this.Gold = 0L;
        this.Platinum = 0L;
        break;
      case Coinage.CurrencyCoin.Gold:
        this.ConvertTo(Coinage.CurrencyCoin.Electrum, Coinage.CurrencyCoin.Gold);
        this.Gold += this.Platinum * 10L;
        this.Platinum = 0L;
        break;
      case Coinage.CurrencyCoin.Platinum:
        this.ConvertTo(Coinage.CurrencyCoin.Gold, Coinage.CurrencyCoin.Platinum);
        break;
      default:
        throw new ArgumentOutOfRangeException(nameof (coin), (object) coin, (string) null);
    }
  }

  private Coinage.CurrencyCoin Down(Coinage.CurrencyCoin coin)
  {
    switch (coin)
    {
      case Coinage.CurrencyCoin.Copper:
        return coin;
      case Coinage.CurrencyCoin.Silver:
        return Coinage.CurrencyCoin.Copper;
      case Coinage.CurrencyCoin.Electrum:
        return Coinage.CurrencyCoin.Silver;
      case Coinage.CurrencyCoin.Gold:
        return Coinage.CurrencyCoin.Electrum;
      case Coinage.CurrencyCoin.Platinum:
        return Coinage.CurrencyCoin.Gold;
      default:
        throw new ArgumentOutOfRangeException(nameof (coin), (object) coin, (string) null);
    }
  }

  private Coinage.CurrencyCoin Up(Coinage.CurrencyCoin coin)
  {
    switch (coin)
    {
      case Coinage.CurrencyCoin.Copper:
        return Coinage.CurrencyCoin.Silver;
      case Coinage.CurrencyCoin.Silver:
        return Coinage.CurrencyCoin.Electrum;
      case Coinage.CurrencyCoin.Electrum:
        return Coinage.CurrencyCoin.Gold;
      case Coinage.CurrencyCoin.Gold:
        return Coinage.CurrencyCoin.Platinum;
      case Coinage.CurrencyCoin.Platinum:
        return Coinage.CurrencyCoin.Platinum;
      default:
        throw new ArgumentOutOfRangeException(nameof (coin), (object) coin, (string) null);
    }
  }

  private long CalculateBase(Coinage.CurrencyCoin coin, long amount)
  {
    long num = 0;
    switch (coin)
    {
      case Coinage.CurrencyCoin.Copper:
        num = amount;
        break;
      case Coinage.CurrencyCoin.Silver:
        num = amount * 10L;
        break;
      case Coinage.CurrencyCoin.Electrum:
        num = amount * 50L;
        break;
      case Coinage.CurrencyCoin.Gold:
        num = amount * 100L;
        break;
      case Coinage.CurrencyCoin.Platinum:
        num = amount * 1000L;
        break;
    }
    return num;
  }

  private long CalculateBasedOnRequiredBase(Coinage.CurrencyCoin coin, long required)
  {
    long basedOnRequiredBase = 0;
    switch (coin)
    {
      case Coinage.CurrencyCoin.Copper:
        basedOnRequiredBase = required / 1L;
        break;
      case Coinage.CurrencyCoin.Silver:
        basedOnRequiredBase = required / 10L;
        break;
      case Coinage.CurrencyCoin.Electrum:
        basedOnRequiredBase = required / 50L;
        break;
      case Coinage.CurrencyCoin.Gold:
        basedOnRequiredBase = required / 100L;
        break;
      case Coinage.CurrencyCoin.Platinum:
        basedOnRequiredBase = required / 1000L;
        break;
    }
    return basedOnRequiredBase;
  }

  private bool ValidateBase()
  {
    return this._calculationBase == this.CalculateBase(Coinage.CurrencyCoin.Copper, this.Copper) + this.CalculateBase(Coinage.CurrencyCoin.Silver, this.Silver) + this.CalculateBase(Coinage.CurrencyCoin.Electrum, this.Electrum) + this.CalculateBase(Coinage.CurrencyCoin.Gold, this.Gold) + this.CalculateBase(Coinage.CurrencyCoin.Platinum, this.Platinum);
  }

  public override string ToString()
  {
    return $"{this.Copper}CP {this.Silver}SP {this.Electrum}EP {this.Gold}GP {this.Platinum}PP ({this.CalculationBase})";
  }

  public void ConvertToGold() => this.ConvertTo(Coinage.CurrencyCoin.Gold);

  public static Coinage.CurrencyCoin GetCurrencyCoinFromAbbreviation(string abbreviation)
  {
    if (string.IsNullOrWhiteSpace(abbreviation))
      throw new ArgumentException("currency abbreviation is empty");
    switch (abbreviation.ToLowerInvariant())
    {
      case "cp":
        return Coinage.CurrencyCoin.Copper;
      case "sp":
        return Coinage.CurrencyCoin.Silver;
      case "ep":
        return Coinage.CurrencyCoin.Electrum;
      case "gp":
        return Coinage.CurrencyCoin.Gold;
      case "pp":
        return Coinage.CurrencyCoin.Platinum;
      default:
        throw new ArgumentException($"currency abbreviation '{abbreviation}' doesn't exist");
    }
  }

  public enum CurrencyCoin
  {
    Copper,
    Silver,
    Electrum,
    Gold,
    Platinum,
  }
}
