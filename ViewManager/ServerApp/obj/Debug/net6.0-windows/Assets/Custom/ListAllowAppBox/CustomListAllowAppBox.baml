   M S B A M L   `   `   `  ????D  @ServerApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nulla5clr-namespace:ServerApp.Assets.Custom.ListAllowAppBox'ServerApp.Assets.Custom.ListAllowAppBox  5   	   E'clr-namespace:ServerApp.Properties.LangServerApp.Properties.Lang  5   
   C    =ServerApp.Assets.Custom.ListAllowAppBox.CustomListAllowAppBox   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   Glocal5clr-namespace:ServerApp.Assets.Custom.ListAllowAppBox     5   	   8lang'clr-namespace:ServerApp.Properties.Lang     5   
     ? WindowStyle$
  None=?5	   
    ? Title$  ??5
   	   $	??400??6   $	??400??6   $	??400??5   	   $	??400??6    ? WindowStartupLocation$ CenterOwner=?6(   ??5      ?? 5   
    ?? 
TargetType" ?6   .?5      ??  ?? Property ?@?AllowsTransparency ?? Value$
 True??5      ?? 5       ?@?WindowStyle$
 None=?5      ?? 5       ? ?? 5      ?? 5       ?? 
TargetType" ?6&   .R?5      ? .+?5      ??    	MainColor#???   6%    ?? CornerRadius$ 12??5      $??2q?5   %     ColorHighlight#???  5   %   $??12q?5   %    ?? Padding$ 5q?5   %   5      ?? 5      $??12q?6/   5       5!      5#      5$      .??5&      ? 	 ? RowDefinitions	 5'   
   ?? 5(      $??80 ?6   ?? 5)      ?? 5*      $??60 ?6   .+?5,   
   ?? -   5        
?????? ._?5.      ? 
 ? ColumnDefinitions
 5/      ?? 50      ?? 51      .+?53      ?? $   ServerApp.Properties.Lang.Lang   ToolTipListAppButton#??Z 6   $??Center=?54      $??10 0 0 0q?55      $??21(?56      $??2??57      $??Center=?58      $??SemiBold"?59      ?? 5:      -   5         ??Name$ 
exitButton??5=      $??25??5:      $??25??5;      $??1??5<      $??Right=?5?      $	??Top=?5@      $??	0 10 10 0q?5A      ??5B      ?? 5C      " ??6*   .R?5D      ??  ??Name$ Bd??6%   
?????? ._?5F   "   F?  F?Name$ Pt??6'   =????        ?@ @ ? 5  5 ?@  5 ? 5 @ ?@B @B 	 ?? Stretch$ Uniform=?5H   '     AdditionalColor#???  5I   '   $??2??5J   '   5K       ?? Triggers 5L      P? 5M   "    P? Property ?@J?IsMouseOver P? Value$
 True??5N   *   . ?5O   &   ??  ?? 
TargetName$ Bd??6-     Red# [ 5Q   -    ? ???? 5R   &   $ Pt??6-   # ?   5T   -    ? ??5U   #   5W      5Y      5Z      5[      ? 5\   
   $??1??6   	 5]      ?? 5^      $??80 ?6    ?? 5_      .+?5a      ??  ?? Orientation$ 
Horizontal=?6   $??Center=?5b      $??5q?5c      $??Center=?5d      .+?5e      ??    InputNameApp#??Z 6   $??18(?5f      $??0 0 10 0q?5g      $??Center=?5h      ?? 5i      -   5         ??Name$ appNameTextBox??5i      $??Center=?5j      $	??150??5k      $??18(?5l      $??0 0 10 0q?5m      ?? 5n      -   5        $ removeButton??5n      $
??🠕??5o      $??10 10 10 10q?5q      $??	10 0 10 0q?5r      $??18(?5s      $??Center=?5t      $??30??5u   !   ?? 5v      -   5        $ 	addButton??5v      $
??🠗??5w      $??
0 10 10 10q?5y      $??	10 0 10 0q?5z      $??18(?5{      $??Center=?5|      $??30??5}      5~      ?? 5      -   5        $??appListView??5      $??20 10q?5?      $??21(?5?      $??1??5?       ? Triggers 5?      :? 5?       :? RoutedEvent$ Loaded??6#   [?5?      ?? 5?      .??5?   "   ?? .??5?   &   W?  ?? TargetProperty$ Opacity?66    W? From 06Z   	 W? To 16c    g? Duration$ 0:0:2D?6j   5?   '   5?   #   5?      5?      5?      5?      ?? 5?   
   -   5        $ saveChangedButton??5?      $??2??5?      $??OK??5?      $??18(?5?       ?? 	IsDefault$
 True??5?      $??10q?5?      5?      5?      