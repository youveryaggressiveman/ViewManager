   M S B A M L   `   `   `  ????D  @ServerApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullW0clr-namespace:ServerApp.Assets.Custom.MessageBox"ServerApp.Assets.Custom.MessageBox  5   	   9    3ServerApp.Assets.Custom.MessageBox.CustomMessageBox   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   Blocal0clr-namespace:ServerApp.Assets.Custom.MessageBox     5   	     ? WindowStyle$
  None=?5   	    ? Title$  ??5	   	   $	??200??6   $	??350??6   $	??200??5
   	   $	??350??6    ? WindowStartupLocation$ CenterOwner=?6(   ??5      ?? 5   
    ?? 
TargetType" ?6   .?5      ??  ?? Property ?@?AllowsTransparency ?? Value$
 True??5      ?? 5       ?@?WindowStyle$
 None=?5      ?? 5       ? ?? 5      ?? 5       ?? 
TargetType" ?6&   .R?5      ? .+?5      ??    	MainColor#???   6%    ?? CornerRadius$ 12??5      $??2q?5   %     ColorHighlight#???  5   %   $??12q?5   %    ?? Padding$ 5q?5   %   5      ?? 5      $??12q?6/   5      5       5"      5#      .??5%      ? 	 ? RowDefinitions	 5&   
   ?? 5'      $??35 ?6   ?? 5(      ?? 5)      $??45 ?6   .+?5+   
   ?? -   5        
?????? ._?5-      ? 
 ? ColumnDefinitions
 5.      ?? 5/      ?? 50      .+?52      ?? -   5         ??Name$ title??52      $
??Left=?53      $??10 0 0 0q?54      $??18(?55      $??Center=?56      $??SemiBold"?57      ?? 58      -   5         ??Name$ 
exitButton??5;      $??25??58      $??25??59      $??1??5:      $??Right=?5=      $	??Top=?5>      $??	0 10 10 0q?5?      ??5@      ?? 5A      " ??6*   .R?5B      ??  ??Name$ Bd??6%   
?????? ._?5D   "   F?  F?Name$ Pt??6'   =????        ?@ @ ? 5  5 ?@  5 ? 5 @ ?@B @B 	 ?? Stretch$ Uniform=?5F   '     AdditionalColor#???  5G   '   $??2??5H   '   5I       ?? Triggers 5J      P? 5K   "    P? Property ?@J?IsMouseOver P? Value$
 True??5L   *   . ?5M   &   ??  ?? 
TargetName$ Bd??6-     Red# [ 5O   -    ? ???? 5P   &   $ Pt??6-   # ?   5R   -    ? ??5S   #   5U      5W      5X      5Y      ? 5\   
   $??1??6   
 5]      ?? 5^      $
??auto ?6#   ?? 5_      .+?5a      ?? -   5        $??image??5a      $??50??5b      $??Center=?5c      $??	15 5 15 5q?5d      $??50??5e      ?? 5f      -   5        $??description??5k      $??1??5f      $??5q?5g      $??Center=?5h      $
??Wrap=?5i      $??15(?5j      5k      5m      ? 5n   
   $??2??6   
 5o      ?? 5p      $	??230 ?6#   ?? 5q      .+?5s      ?? -   5        $??secondButton??5v      $??
10 10 5 10q?5s      $??80??5t      $??15(?5u      $??Right=?5x      ?? 5y      -   5        $??firstButton??5      $??
5 10 10 10q?5y      $??80??5{      $??1??5|      $??15(?5}       ?? 	IsDefault$
 True??5~      $??Right=?5?      5?      5?      5?      