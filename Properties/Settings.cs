// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.Properties.Settings
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

using System.CodeDom.Compiler;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.CompilerServices;

#nullable disable
namespace Builder.Presentation.Properties;

[CompilerGenerated]
[GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "16.4.0.0")]
internal sealed class Settings : ApplicationSettingsBase
{
  private static Settings defaultInstance = (Settings) SettingsBase.Synchronized((SettingsBase) new Settings());

  public static Settings Default => Settings.defaultInstance;

  [UserScopedSetting]
  [DebuggerNonUserCode]
  [DefaultSettingValue("Aurora Default")]
  public string Accent
  {
    get => (string) this[nameof (Accent)];
    set => this[nameof (Accent)] = (object) value;
  }

  [UserScopedSetting]
  [DebuggerNonUserCode]
  [DefaultSettingValue("Player One")]
  public string PlayerName
  {
    get => (string) this[nameof (PlayerName)];
    set => this[nameof (PlayerName)] = (object) value;
  }

  [UserScopedSetting]
  [DebuggerNonUserCode]
  [DefaultSettingValue("150")]
  public int CharactersCollectionSize
  {
    get => (int) this[nameof (CharactersCollectionSize)];
    set => this[nameof (CharactersCollectionSize)] = (object) value;
  }

  [UserScopedSetting]
  [DebuggerNonUserCode]
  [DefaultSettingValue("False")]
  public bool AllowEditableSheet
  {
    get => (bool) this[nameof (AllowEditableSheet)];
    set => this[nameof (AllowEditableSheet)] = (object) value;
  }

  [UserScopedSetting]
  [DebuggerNonUserCode]
  [DefaultSettingValue("True")]
  public bool IncludeSpellcards
  {
    get => (bool) this[nameof (IncludeSpellcards)];
    set => this[nameof (IncludeSpellcards)] = (object) value;
  }

  [UserScopedSetting]
  [DebuggerNonUserCode]
  [DefaultSettingValue("True")]
  public bool CharacterPanelAbilityScoresExpanded
  {
    get => (bool) this[nameof (CharacterPanelAbilityScoresExpanded)];
    set => this[nameof (CharacterPanelAbilityScoresExpanded)] = (object) value;
  }

  [UserScopedSetting]
  [DebuggerNonUserCode]
  [DefaultSettingValue("True")]
  public bool CharacterPanelStatisticsExpanded
  {
    get => (bool) this[nameof (CharacterPanelStatisticsExpanded)];
    set => this[nameof (CharacterPanelStatisticsExpanded)] = (object) value;
  }

  [UserScopedSetting]
  [DebuggerNonUserCode]
  [DefaultSettingValue("False")]
  public bool CharacterPanelSavingThrowsExpanded
  {
    get => (bool) this[nameof (CharacterPanelSavingThrowsExpanded)];
    set => this[nameof (CharacterPanelSavingThrowsExpanded)] = (object) value;
  }

  [UserScopedSetting]
  [DebuggerNonUserCode]
  [DefaultSettingValue("True")]
  public bool CharacterPanelSkillsExpanded
  {
    get => (bool) this[nameof (CharacterPanelSkillsExpanded)];
    set => this[nameof (CharacterPanelSkillsExpanded)] = (object) value;
  }

  [UserScopedSetting]
  [DebuggerNonUserCode]
  [DefaultSettingValue("True")]
  public bool CharacterPanelQuickActionsExpanded
  {
    get => (bool) this[nameof (CharacterPanelQuickActionsExpanded)];
    set => this[nameof (CharacterPanelQuickActionsExpanded)] = (object) value;
  }

  [UserScopedSetting]
  [DebuggerNonUserCode]
  [DefaultSettingValue("8")]
  public string DefaultFontSize
  {
    get => (string) this[nameof (DefaultFontSize)];
    set => this[nameof (DefaultFontSize)] = (object) value;
  }

  [UserScopedSetting]
  [DebuggerNonUserCode]
  [DefaultSettingValue("True")]
  public bool StartupCheckForUpdates
  {
    get => (bool) this[nameof (StartupCheckForUpdates)];
    set => this[nameof (StartupCheckForUpdates)] = (object) value;
  }

  [UserScopedSetting]
  [DebuggerNonUserCode]
  [DefaultSettingValue("True")]
  public bool StartupLoadCustomFiles
  {
    get => (bool) this[nameof (StartupLoadCustomFiles)];
    set => this[nameof (StartupLoadCustomFiles)] = (object) value;
  }

  [UserScopedSetting]
  [DebuggerNonUserCode]
  [DefaultSettingValue("1")]
  public int AbilitiesGenerationOption
  {
    get => (int) this[nameof (AbilitiesGenerationOption)];
    set => this[nameof (AbilitiesGenerationOption)] = (object) value;
  }

  [UserScopedSetting]
  [DebuggerNonUserCode]
  [DefaultSettingValue("False")]
  public bool DisplaySelectionExpanderColumnHeaders
  {
    get => (bool) this[nameof (DisplaySelectionExpanderColumnHeaders)];
    set => this[nameof (DisplaySelectionExpanderColumnHeaders)] = (object) value;
  }

  [UserScopedSetting]
  [DebuggerNonUserCode]
  [DefaultSettingValue("21")]
  public int SelectionExpanderGridRowSize
  {
    get => (int) this[nameof (SelectionExpanderGridRowSize)];
    set => this[nameof (SelectionExpanderGridRowSize)] = (object) value;
  }

  [UserScopedSetting]
  [DebuggerNonUserCode]
  [DefaultSettingValue("True")]
  public bool SearchMissingSourceOnline
  {
    get => (bool) this[nameof (SearchMissingSourceOnline)];
    set => this[nameof (SearchMissingSourceOnline)] = (object) value;
  }

  [UserScopedSetting]
  [DebuggerNonUserCode]
  [DefaultSettingValue("False")]
  public bool GenerateSheetOnCharacterChangedRegistered
  {
    get => (bool) this[nameof (GenerateSheetOnCharacterChangedRegistered)];
    set => this[nameof (GenerateSheetOnCharacterChangedRegistered)] = (object) value;
  }

  [UserScopedSetting]
  [DebuggerNonUserCode]
  [DefaultSettingValue("False")]
  public bool AutoNavigateNextSelectionWhenAvailable
  {
    get => (bool) this[nameof (AutoNavigateNextSelectionWhenAvailable)];
    set => this[nameof (AutoNavigateNextSelectionWhenAvailable)] = (object) value;
  }

  [UserScopedSetting]
  [DebuggerNonUserCode]
  [DefaultSettingValue("True")]
  public bool QuickSearchBarEnabled
  {
    get => (bool) this[nameof (QuickSearchBarEnabled)];
    set => this[nameof (QuickSearchBarEnabled)] = (object) value;
  }

  [UserScopedSetting]
  [DebuggerNonUserCode]
  [DefaultSettingValue("True")]
  public bool DisplayRemoveLevelConfirmation
  {
    get => (bool) this[nameof (DisplayRemoveLevelConfirmation)];
    set => this[nameof (DisplayRemoveLevelConfirmation)] = (object) value;
  }

  [UserScopedSetting]
  [DebuggerNonUserCode]
  [DefaultSettingValue("False")]
  public bool CharacterSheetOpenOnSave
  {
    get => (bool) this[nameof (CharacterSheetOpenOnSave)];
    set => this[nameof (CharacterSheetOpenOnSave)] = (object) value;
  }

  [UserScopedSetting]
  [DebuggerNonUserCode]
  [DefaultSettingValue("False")]
  public bool CharacterSheetAbilitiesFlipped
  {
    get => (bool) this[nameof (CharacterSheetAbilitiesFlipped)];
    set => this[nameof (CharacterSheetAbilitiesFlipped)] = (object) value;
  }

  [UserScopedSetting]
  [DebuggerNonUserCode]
  [DefaultSettingValue("0")]
  public double ShellWindowTop
  {
    get => (double) this[nameof (ShellWindowTop)];
    set => this[nameof (ShellWindowTop)] = (object) value;
  }

  [UserScopedSetting]
  [DebuggerNonUserCode]
  [DefaultSettingValue("0")]
  public double ShellWindowLeft
  {
    get => (double) this[nameof (ShellWindowLeft)];
    set => this[nameof (ShellWindowLeft)] = (object) value;
  }

  [UserScopedSetting]
  [DebuggerNonUserCode]
  [DefaultSettingValue("1200")]
  public double ShellWindowWidth
  {
    get => (double) this[nameof (ShellWindowWidth)];
    set => this[nameof (ShellWindowWidth)] = (object) value;
  }

  [UserScopedSetting]
  [DebuggerNonUserCode]
  [DefaultSettingValue("700")]
  public double ShellWindowHeight
  {
    get => (double) this[nameof (ShellWindowHeight)];
    set => this[nameof (ShellWindowHeight)] = (object) value;
  }

  [UserScopedSetting]
  [DebuggerNonUserCode]
  [DefaultSettingValue("False")]
  public bool ShellWindowState
  {
    get => (bool) this[nameof (ShellWindowState)];
    set => this[nameof (ShellWindowState)] = (object) value;
  }

  [UserScopedSetting]
  [DebuggerNonUserCode]
  [DefaultSettingValue("3")]
  public int MinimumAbilityBaseScore
  {
    get => (int) this[nameof (MinimumAbilityBaseScore)];
    set => this[nameof (MinimumAbilityBaseScore)] = (object) value;
  }

  [UserScopedSetting]
  [DebuggerNonUserCode]
  [DefaultSettingValue("30")]
  public int MaximumAbilityBaseScore
  {
    get => (int) this[nameof (MaximumAbilityBaseScore)];
    set => this[nameof (MaximumAbilityBaseScore)] = (object) value;
  }

  [UserScopedSetting]
  [DebuggerNonUserCode]
  [DefaultSettingValue("")]
  public string DocumentsRootDirectory
  {
    get => (string) this[nameof (DocumentsRootDirectory)];
    set => this[nameof (DocumentsRootDirectory)] = (object) value;
  }

  [UserScopedSetting]
  [DebuggerNonUserCode]
  [DefaultSettingValue("")]
  public string AdditionalCustomDirectory
  {
    get => (string) this[nameof (AdditionalCustomDirectory)];
    set => this[nameof (AdditionalCustomDirectory)] = (object) value;
  }

  [UserScopedSetting]
  [DebuggerNonUserCode]
  [DefaultSettingValue("False")]
  public bool Patreon
  {
    get => (bool) this[nameof (Patreon)];
    set => this[nameof (Patreon)] = (object) value;
  }

  [UserScopedSetting]
  [DebuggerNonUserCode]
  [DefaultSettingValue("True")]
  public bool IncludeItemcards
  {
    get => (bool) this[nameof (IncludeItemcards)];
    set => this[nameof (IncludeItemcards)] = (object) value;
  }

  [UserScopedSetting]
  [DebuggerNonUserCode]
  [DefaultSettingValue("True")]
  public bool SheetIncludeSpellCards
  {
    get => (bool) this[nameof (SheetIncludeSpellCards)];
    set => this[nameof (SheetIncludeSpellCards)] = (object) value;
  }

  [UserScopedSetting]
  [DebuggerNonUserCode]
  [DefaultSettingValue("False")]
  public bool SheetIncludeItemCards
  {
    get => (bool) this[nameof (SheetIncludeItemCards)];
    set => this[nameof (SheetIncludeItemCards)] = (object) value;
  }

  [UserScopedSetting]
  [DebuggerNonUserCode]
  [DefaultSettingValue("False")]
  public bool SheetIncludeFeatureCards
  {
    get => (bool) this[nameof (SheetIncludeFeatureCards)];
    set => this[nameof (SheetIncludeFeatureCards)] = (object) value;
  }

  [UserScopedSetting]
  [DebuggerNonUserCode]
  [DefaultSettingValue("False")]
  public bool SheetIncludeAttackCards
  {
    get => (bool) this[nameof (SheetIncludeAttackCards)];
    set => this[nameof (SheetIncludeAttackCards)] = (object) value;
  }

  [UserScopedSetting]
  [DebuggerNonUserCode]
  [DefaultSettingValue("False")]
  public bool SheetFormFillable
  {
    get => (bool) this[nameof (SheetFormFillable)];
    set => this[nameof (SheetFormFillable)] = (object) value;
  }

  [UserScopedSetting]
  [DebuggerNonUserCode]
  [DefaultSettingValue("Aurora Dark")]
  public string Theme
  {
    get => (string) this[nameof (Theme)];
    set => this[nameof (Theme)] = (object) value;
  }

  [UserScopedSetting]
  [DebuggerNonUserCode]
  [DefaultSettingValue("")]
  public string DefaultSourceRestrictions
  {
    get => (string) this[nameof (DefaultSourceRestrictions)];
    set => this[nameof (DefaultSourceRestrictions)] = (object) value;
  }

  [UserScopedSetting]
  [DebuggerNonUserCode]
  [DefaultSettingValue("True")]
  public bool ApplyDefaultSourceRestrictionsOnNewCharacter
  {
    get => (bool) this[nameof (ApplyDefaultSourceRestrictionsOnNewCharacter)];
    set => this[nameof (ApplyDefaultSourceRestrictionsOnNewCharacter)] = (object) value;
  }

  [UserScopedSetting]
  [DebuggerNonUserCode]
  [DefaultSettingValue("True")]
  public bool StartupCheckForContentUpdated
  {
    get => (bool) this[nameof (StartupCheckForContentUpdated)];
    set => this[nameof (StartupCheckForContentUpdated)] = (object) value;
  }

  [UserScopedSetting]
  [DebuggerNonUserCode]
  [DefaultSettingValue("False")]
  public bool SheetStartSpellCardsOnNewPage
  {
    get => (bool) this[nameof (SheetStartSpellCardsOnNewPage)];
    set => this[nameof (SheetStartSpellCardsOnNewPage)] = (object) value;
  }

  [UserScopedSetting]
  [DebuggerNonUserCode]
  [DefaultSettingValue("True")]
  public bool SheetStartItemCardsOnNewPage
  {
    get => (bool) this[nameof (SheetStartItemCardsOnNewPage)];
    set => this[nameof (SheetStartItemCardsOnNewPage)] = (object) value;
  }

  [UserScopedSetting]
  [DebuggerNonUserCode]
  [DefaultSettingValue("False")]
  public bool SheetStartAttackCardsOnNewPage
  {
    get => (bool) this[nameof (SheetStartAttackCardsOnNewPage)];
    set => this[nameof (SheetStartAttackCardsOnNewPage)] = (object) value;
  }

  [UserScopedSetting]
  [DebuggerNonUserCode]
  [DefaultSettingValue("True")]
  public bool SheetStartFeatureCardsOnNewPage
  {
    get => (bool) this[nameof (SheetStartFeatureCardsOnNewPage)];
    set => this[nameof (SheetStartFeatureCardsOnNewPage)] = (object) value;
  }

  [UserScopedSetting]
  [DebuggerNonUserCode]
  [DefaultSettingValue("True")]
  public bool SheetIncludeFormatting
  {
    get => (bool) this[nameof (SheetIncludeFormatting)];
    set => this[nameof (SheetIncludeFormatting)] = (object) value;
  }

  [UserScopedSetting]
  [DebuggerNonUserCode]
  [DefaultSettingValue("False")]
  public bool UseLegacySpellcastingPage
  {
    get => (bool) this[nameof (UseLegacySpellcastingPage)];
    set => this[nameof (UseLegacySpellcastingPage)] = (object) value;
  }

  [UserScopedSetting]
  [DebuggerNonUserCode]
  [DefaultSettingValue("False")]
  public bool SheetDescriptionAbbreviateUsage
  {
    get => (bool) this[nameof (SheetDescriptionAbbreviateUsage)];
    set => this[nameof (SheetDescriptionAbbreviateUsage)] = (object) value;
  }

  [UserScopedSetting]
  [DebuggerNonUserCode]
  [DefaultSettingValue("True")]
  public bool RequestAddAttackWhenEquippingWeapon
  {
    get => (bool) this[nameof (RequestAddAttackWhenEquippingWeapon)];
    set => this[nameof (RequestAddAttackWhenEquippingWeapon)] = (object) value;
  }

  [UserScopedSetting]
  [DebuggerNonUserCode]
  [DefaultSettingValue("True")]
  public bool SheetIncludeNonPreparedSpells
  {
    get => (bool) this[nameof (SheetIncludeNonPreparedSpells)];
    set => this[nameof (SheetIncludeNonPreparedSpells)] = (object) value;
  }

  [UserScopedSetting]
  [DebuggerNonUserCode]
  [DefaultSettingValue("False")]
  public bool UseLegacyDetailsPage
  {
    get => (bool) this[nameof (UseLegacyDetailsPage)];
    set => this[nameof (UseLegacyDetailsPage)] = (object) value;
  }

  [UserScopedSetting]
  [DebuggerNonUserCode]
  [DefaultSettingValue("False")]
  public bool UseLegactBackgroundPage
  {
    get => (bool) this[nameof (UseLegactBackgroundPage)];
    set => this[nameof (UseLegactBackgroundPage)] = (object) value;
  }

  [UserScopedSetting]
  [DebuggerNonUserCode]
  [DefaultSettingValue("True")]
  public bool ConfigurationUpgradeRequired
  {
    get => (bool) this[nameof (ConfigurationUpgradeRequired)];
    set => this[nameof (ConfigurationUpgradeRequired)] = (object) value;
  }

  [UserScopedSetting]
  [DebuggerNonUserCode]
  [DefaultSettingValue("True")]
  public bool UseDefaultAbilityScoreMaximum
  {
    get => (bool) this[nameof (UseDefaultAbilityScoreMaximum)];
    set => this[nameof (UseDefaultAbilityScoreMaximum)] = (object) value;
  }

  [UserScopedSetting]
  [DebuggerNonUserCode]
  [DefaultSettingValue("True")]
  public bool SheetFormattingActionSuffixBold
  {
    get => (bool) this[nameof (SheetFormattingActionSuffixBold)];
    set => this[nameof (SheetFormattingActionSuffixBold)] = (object) value;
  }

  [UserScopedSetting]
  [DebuggerNonUserCode]
  [DefaultSettingValue("True")]
  public bool FirstRun
  {
    get => (bool) this[nameof (FirstRun)];
    set => this[nameof (FirstRun)] = (object) value;
  }

  [UserScopedSetting]
  [DebuggerNonUserCode]
  [DefaultSettingValue("False")]
  public bool Bundle
  {
    get => (bool) this[nameof (Bundle)];
    set => this[nameof (Bundle)] = (object) value;
  }
}
