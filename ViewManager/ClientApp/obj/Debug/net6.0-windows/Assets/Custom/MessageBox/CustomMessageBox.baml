   M S B A M L   `   `   `  ????D  @ClientApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullW0clr-namespace:ClientApp.Assets.Custom.MessageBox"ClientApp.Assets.Custom.MessageBox  5   	   9    3ClientApp.Assets.Custom.MessageBox.CustomMessageBox   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   Blocal0clr-namespace:ClientApp.Assets.Custom.MessageBox     5   	     ? WindowStyle$
  None=?5   	    ? Title$  ??5	   	   $	??200??5
   	   $	??350??5   	   $	??200??5   	   $	??350??5   	    ? WindowStartupLocation$ CenterScreen=?5   	   ??5      ?? 5   
    ?? 
TargetType" ?6   .?5      ??  ?? Property ?@?AllowsTransparency ?? Value$
 True??5      ?? 5       ?@?WindowStyle$
 None=?5      ?? 5       ? ?? 5      ?? 5       ?? 
TargetType" ?6&   .R?5      ? .+?5      ??    	MainColor#???   6%    ?? CornerRadius$ 12??5   %   $??2q?5   %     ColorHighlight#???  5   %   $??12q?5   %    ?? Padding$ 5q?5   %   5!      ?? 5"      $??12q?6/   5#      5$      5&      5'      .??5)      ? 	 ? RowDefinitions	 5*   
   ?? 5+      $??35 ?6   ?? 5,      ?? 5-      $??45 ?6   .+?5/   
   ?? -   5        
?????? ._?51      ? 
 ? ColumnDefinitions
 52      ?? 53      ?? 54      .+?56      ?? -   5         ??Name$ title??56      $
??Left=?57      $??10 0 0 0q?58      $??18(?59      $??Center=?5:      $??SemiBold"?5;      ?? 5<      -   5         ??Name$ 
exitButton??5?      $??25??5<      $??25??5=      $??1??5>      $??Right=?5A      $	??Top=?5B      $??	0 10 10 0q?5C      ??5D      ?? 5E      " ??6*   .R?5F      ??  ??Name$ Bd??6%   
?????? ._?5H   "   F?  F?Name$ Pt??6'   =????        ?@ @ ? 5  5 ?@  5 ? 5 @ ?@B @B 	 ?? Stretch$ Uniform=?5J   '     AdditionalColor#???  5K   '   $??2??5L   '   5M       ?? Triggers 5N      P? 5O   "    P? Property ?@J?IsMouseOver P? Value$
 True??5P   *   . ?5Q   &   ??  ?? 
TargetName$ Bd??6-     Red# [ 5S   -    ? ???? 5T   &   $ Pt??6-   # ?   5V   -    ? ??5W   #   5Y      5[      5\      5]      ? 5`   
   $??1??6   
 5a      ?? 5b      $
??auto ?6#   ?? 5c      .+?5e      ?? -   5        $??image??5e      $??50??5f      $??Center=?5g      $??	15 5 15 5q?5h      $??50??5i      ?? 5j      -   5        $??description??5o      $??1??5j      $??5q?5k      $??Center=?5l      $
??Wrap=?5m      $??15(?5n      5o      5q      ? 5r   
   $??2??6   
 5s      ?? 5t      $	??230 ?6#   ?? 5u      .+?5w      ?? -   5        $??secondButton??5z      $??
10 10 5 10q?5w      $??80??5x      $??15(?5y      $??Right=?5|       ? Cursor$
 Hand??5}      ?? 5~      -   5        $??firstButton??5?      $??
5 10 10 10q?5~      $??80??5?      $??1??5?      $??15(?5?       ?? 	IsDefault$
 True??5?      $??Right=?5?      5?      5?      5?      