   M S B A M L   `   `   `  ????D  @ServerApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:ServerApp.View.PagesServerApp.View.Pages  5      E'clr-namespace:ServerApp.Properties.LangServerApp.Properties.Lang  5      '    !ServerApp.View.Pages.SettingsPage   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      4local"clr-namespace:ServerApp.View.Pages     5      8lang'clr-namespace:ServerApp.Properties.Lang     5         	MainColor  N? 
Background#  ?   5
       N? 
FontFamily$ DM Sans)?5       N? Title$ SettingsPage??5      .??5      ?  ? RowDefinitions 5   
   ?? 5      $	??100 ?6   ?? 5      ?? 5      $	??100 ?6   .+?5   
   ?? $   ServerApp.Properties.Lang.Lang   Settings#??Z 6   $??25(?5      $??Center=?5      $??SemiBold"?5      $??Center=?5      ?? 5   
    ?? Orientation$ 
Horizontal=?6   $
??Left=?5      $??Center=?5      .+?5      ?? $??🖥️??6   $??20 20 10 20q?5         ToolTipPcInfoButton ? ToolTip# Z 5      $??5q?5       ??5      ?? *CheckPcFeaturesCommand+5       ?? 5!      $	??✔??6   $??10 20 20 20q?5$         ToolTipListAppButton# Z 5%      $??5q?5&      y?5"      ?? *
Visibility+??5#      ?? *OpenListAppCommand+5&      5'      ? 5)   
   $??1??6   $??	10 0 10 0q?5*      	 ? ColumnDefinitions	 5+      ?? 5,      $
??auto ?6#   ?? 5-       5/      ?? 50      ?? 51      ?? 52      .+?54      ?? 
   TextBlockSelectLang#??Z
 6   $??18(?55       $
??Left=?56       $??Center=?57       ?? 58      $??1??6   $	??250??5:      ??59      ?? *LanguageList+ ?? SelectedItem 5;      ?? *SelectedLanguage+?? 5<         TextBlockSelectTheme#??Z 6   $??1??5=      $??18(?5>       $
??Left=?5?       $??Center=?5@       ?? 5A      $??1??6   $??1??5B      $	??250??5C      ??5D      ?? *	ThemeList+ 5E      ?? *SelectedTheme+?? 5F         TextBlockNetwork#??Z 6   $??2??5H      $??18(?5I       $
??Left=?5J       $??Center=?5K       y?5G      ?? *
Visibility+5K       ?? 5L      $ 
Horizontal=?6   $??Center=?5N      $??Center=?5O      $??2??5P      $??1??5Q      y?5M      ?? *
Visibility+.+?5R      ?? $??IP??6   $??Center=?5S      $??18(?5T      $
??10 0q?6*   ?? 5U      $??Center=?6   $??18(?5V      $	??190??5Z       5W      ?? *SelectedLocalIp+ 5X      ?? *SelectedLocalIp+??5Y      ?? *LocalIpList+5Z      ?? 5[         TextBlockPort#??Z 6   $??Center=?5\      $??18(?5]      $
??10 0q?6*   ?? 5^      -   5        $??70??5_      $??35??5`      $??Center=?5a      $??Center=?5b      $??Center=?5d      $??18(?5e      ??5^      ?? *Port+5e      5f      5g      ?? 5h   
      ButtonSaveChanges#??Z 6   $	??200??5i      $??2??5k      $??Right=?5l      ??5j      ?? *SaveChangesCommand+5l      5m      5n      