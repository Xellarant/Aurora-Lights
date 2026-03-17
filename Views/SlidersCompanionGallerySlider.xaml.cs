// Decompiled with JetBrains decompiler
// Type: Builder.Presentation.Views.Sliders.CompanionGallerySlider
// Assembly: Aurora Builder, Version=1.0.166.7407, Culture=neutral, PublicKeyToken=null
// MVID: 09D35420-8FA0-4A71-9A21-FF952C48F8A3
// Assembly location: C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe

using Builder.Core.Logging;
using Builder.Presentation.Extensions;
using Builder.Presentation.Services.Data;
using MahApps.Metro.Controls;
using Microsoft.Win32;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Markup;

#nullable disable
namespace Builder.Presentation.Views.Sliders;

public partial class CompanionGallerySlider : Flyout, IComponentConnector
{
  private bool _contentLoaded;

  public CompanionGallerySlider()
  {
    this.InitializeComponent();
    this.IsOpenChanged += new RoutedEventHandler(this.SelectPortraitSliderIsOpenChanged);
  }

  private void SelectPortraitSliderIsOpenChanged(object sender, RoutedEventArgs e)
  {
    this.GetViewModel().InitializeAsync();
  }

  private void SelectPortraitClick(object sender, RoutedEventArgs e)
  {
    CharacterManager.Current.Character.Companion.Portrait.Content = this.GetViewModel<CompanionsGalleryViewModel>().SelectedPortrait;
    this.IsOpen = false;
  }

  private void BrowseClick(object sender, RoutedEventArgs e)
  {
    try
    {
      OpenFileDialog openFileDialog1 = new OpenFileDialog();
      openFileDialog1.Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png";
      OpenFileDialog openFileDialog2 = openFileDialog1;
      bool? nullable = openFileDialog2.ShowDialog();
      if (!nullable.HasValue || !nullable.Value)
        return;
      string galleryDirectory = DataManager.Current.UserDocumentsCompanionGalleryDirectory;
      FileInfo fileInfo = new FileInfo(openFileDialog2.FileName);
      string path2 = CharacterManager.Current.Character.Companion.Element.CreatureType.ToLower() + fileInfo.Name.ToLower();
      string str = Path.Combine(galleryDirectory, path2);
      if (!File.Exists(str))
      {
        using (Bitmap bitmap = new Bitmap(System.Drawing.Image.FromFile(fileInfo.FullName, false)))
          bitmap.Save(str);
      }
      CharacterManager.Current.Character.Companion.Portrait.Content = str;
      this.IsOpen = false;
    }
    catch (IOException ex)
    {
      Logger.Exception((Exception) ex, nameof (BrowseClick));
      int num = (int) MessageBox.Show(ex.Message, "IO Exception @ ChangePortrait");
    }
    catch (Exception ex)
    {
      Logger.Exception(ex, nameof (BrowseClick));
      int num = (int) MessageBox.Show(ex.Message, "Exception @ ChangePortrait");
    }
  }

  private void CancelClick(object sender, RoutedEventArgs e) => this.IsOpen = false;

  [DebuggerNonUserCode]
  [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
  public void InitializeComponent()
  {
    if (this._contentLoaded)
      return;
    this._contentLoaded = true;
    Application.LoadComponent((object) this, new Uri("/Aurora Builder;component/views/sliders/companiongalleryslider.xaml", UriKind.Relative));
  }

  [DebuggerNonUserCode]
  [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
  internal Delegate _CreateDelegate(Type delegateType, string handler)
  {
    return Delegate.CreateDelegate(delegateType, (object) this, handler);
  }

  [DebuggerNonUserCode]
  [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
  [EditorBrowsable(EditorBrowsableState.Never)]
  void IComponentConnector.Connect(int connectionId, object target)
  {
    switch (connectionId)
    {
      case 1:
        ((Control) target).MouseDoubleClick += new MouseButtonEventHandler(this.SelectPortraitClick);
        break;
      case 2:
        ((ButtonBase) target).Click += new RoutedEventHandler(this.SelectPortraitClick);
        break;
      case 3:
        ((ButtonBase) target).Click += new RoutedEventHandler(this.BrowseClick);
        break;
      case 4:
        ((ButtonBase) target).Click += new RoutedEventHandler(this.CancelClick);
        break;
      default:
        this._contentLoaded = true;
        break;
    }
  }
}
