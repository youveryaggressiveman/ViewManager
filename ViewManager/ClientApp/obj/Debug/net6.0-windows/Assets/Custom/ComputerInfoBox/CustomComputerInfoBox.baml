   M S B A M L   `   `   `  ????D  @ClientApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nulla5clr-namespace:ClientApp.Assets.Custom.ComputerInfoBox'ClientApp.Assets.Custom.ComputerInfoBox  5   	   E'clr-namespace:ClientApp.Properties.LangClientApp.Properties.Lang  5   	   C    =ClientApp.Assets.Custom.ComputerInfoBox.CustomComputerInfoBox   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   Glocal5clr-namespace:ClientApp.Assets.Custom.ComputerInfoBox     5   	   8lang'clr-namespace:ClientApp.Properties.Lang     5   	     ? WindowStyle$
  None=?5	   
    ? Title$  ??5
   	   $	??400??6   $	??500??6   $	??400??5   	   $	??500??6    ? WindowStartupLocation$ CenterScreen=?6(   ??5      ?? 5   
    ?? 
TargetType" ?6   .?5      ??  ?? Property ?@?AllowsTransparency ?? Value$
 True??5      ?? 5       ?@?WindowStyle$
 None=?5      ?? 5       ? ?? 5      ?? 5       ?? 
TargetType" ?6&   .R?5      ? .+?5      ??    	MainColor#???   6%    ?? CornerRadius$ 12??5      $??2q?5   %     ColorHighlight#???  5   %   $??12q?5   %    ?? Padding$ 5q?5   %   5      ?? 5      $??12q?6/   5       5!      5#      5$      .??5&      ? 	 ? RowDefinitions	 5'   
   ?? 5(      $??80 ?6   ?? 5)      ?? 5*      $??50 ?6   .+?5,   
   ?? -   5        
?????? ._?5.      ? 
 ? ColumnDefinitions
 5/      ?? 50      ?? 51      .+?53      ?? -   5         ??Name$ title??53      $
??Left=?54      $??10 0 0 0q?55      $??18(?56      $??2??57      $??Center=?58      $??SemiBold"?59      ?? 5:      -   5         ??Name$ 
exitButton??5=      $??25??5:      $??25??5;      $??1??5<      $??Right=?5?      $	??Top=?5@      $??	0 10 10 0q?5A      ??5B      ?? 5C      " ??6*   .R?5D      ??  ??Name$ Bd??6%   
?????? ._?5F   "   F?  F?Name$ Pt??6'   =????        ?@ @ ? 5  5 ?@  5 ? 5 @ ?@B @B 	 ?? Stretch$ Uniform=?5H   '     AdditionalColor#???  5I   '   $??2??5J   '   5K       ?? Triggers 5L      P? 5M   "    P? Property ?@J?IsMouseOver P? Value$
 True??5N   *   . ?5O   &   ??  ?? 
TargetName$ Bd??6-     Red# [ 5Q   -    ? ???? 5R   &   $ Pt??6-   # ?   5T   -    ? ??5U   #   5W      5Y      ?? 5[      $ 5??6   $	??350??5\      $??0??5]      $
??Left=?5^      $??Center=?5_      $??20 0 0 0q?5`       $ 5q?5a      #???  5c      $??1q?5d      ._?5e      ?? $??Disabled=?5g      $
??Auto=?5h      .??5i      ?? .+?5j      ?? $
??Left=?6(   $??1??5k      #???  5l      $??Center=?5m      $??18(?5n      ?? 5o      $   ClientApp.Properties.Lang.Lang   PcName#?Z 6   $??SemiBold"?6A    ?? 5p      -   5         ?Name$ 	pcNameRun??5p      5q      5r      5s      5t      5u      5v      ?? 5w   
   $??1??6   $??10q?5x      $
??Auto=?5y      $??Disabled=?5z      .??5|      ?? -   5        $??descriptionTextBlock??5|      $??15(?5}      5      ?? 5?   
   -   5        $ pcInfoButton??5?      $??2??5?      $??OK??5?      $??18(?5?       ?? 	IsDefault$
 True??5?      $??10q?5?      $	??100??5?      $??Right=?5?      5?      5?      