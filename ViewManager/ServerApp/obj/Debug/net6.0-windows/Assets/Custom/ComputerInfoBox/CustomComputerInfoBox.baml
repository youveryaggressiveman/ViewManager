   M S B A M L   `   `   `  ????D  @ServerApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nulla5clr-namespace:ServerApp.Assets.Custom.ComputerInfoBox'ServerApp.Assets.Custom.ComputerInfoBox  5   	   E'clr-namespace:ServerApp.Properties.LangServerApp.Properties.Lang  5   	   =#clr-namespace:ServerApp.ControllersServerApp.Controllers  5   	   C    =ServerApp.Assets.Custom.ComputerInfoBox.CustomComputerInfoBox   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   Glocal5clr-namespace:ServerApp.Assets.Custom.ComputerInfoBox     5   	   8lang'clr-namespace:ServerApp.Properties.Lang     5   	   ;Controllers#clr-namespace:ServerApp.Controllers     5   	     ? WindowStyle$
  None=?5
   
    ? Title$  ??5   	   $	??400??6   $	??500??6   $	??400??5   	   $	??500??6    ? WindowStartupLocation$ CenterOwner=?6(   ??5      ?? 5   
    ?? 
TargetType" ?6   .?5      ??  ?? Property ?@?AllowsTransparency ?? Value$
 True??5      ?? 5       ?@?WindowStyle$
 None=?5      ?? 5       ? ?? 5      ?? 5       ?? 
TargetType" ?6&   .R?5      ? .+?5      ??    	MainColor#???   6%    ?? CornerRadius$ 12??5      $??2q?5   %     ColorHighlight#???  5   %   $??12q?5   %    ?? Padding$ 5q?5   %   ?? 5      $??12q?6/   5      5       5"      5#      .??5%      ? 	 ? RowDefinitions	 5&   
   ?? 5'      $??80 ?6   ?? 5(      ?? 5)      $??50 ?6   .+?5+   
   ?? -   5        
?????? ._?5-      ? 
 ? ColumnDefinitions
 5.      ?? 5/      ?? 50      .+?52      ?? -   5         ??Name$ title??52      $
??Left=?53      $??10 0 0 0q?54      $??18(?55      $??2??56      $??Center=?57      $??SemiBold"?58      ?? 59      -   5         ??Name$ 
exitButton??5<      $??25??59      $??25??5:      $??1??5;      $??Right=?5>      $	??Top=?5?      $??	0 10 10 0q?5@      ??5A      ?? 5B      " ??6*   .R?5C      ??  ??Name$ Bd??6%   
?????? ._?5E   "   F?  F?Name$ Pt??6'   =????        ?@ @ ? 5  5 ?@  5 ? 5 @ ?@B @B 	 ?? Stretch$ Uniform=?5G   '     AdditionalColor#???  5H   '   $??2??5I   '   5J       ?? Triggers 5K      P? 5L   "    P? Property ?@J?IsMouseOver P? Value$
 True??5M   *   . ?5N   &   ??  ?? 
TargetName$ Bd??6-     Red# [ 5P   -    ? ???? 5Q   &   $ Pt??6-   # ?   5S   -    ? ??5T   #   5V      5X      ?? 5Z      $ 5??6   $	??350??5[      $??0??5\      $
??Left=?5]      $??Center=?5^      $??20 0 0 0q?5_       $ 5q?5`      #???  5b      $??1q?5c      ._?5d      ?? $??Disabled=?5f      $
??Auto=?5g      .??5h      ?? .+?5i      ?? $
??Left=?6(   $??1??5j      #???  5k      $??Center=?5l      $??18(?5m      ?? 6'   $   ServerApp.Properties.Lang.Lang   PcName#?Z 6+   $??SemiBold"?6N   ?? 6f   $? ??6j   ?? 6u   -   5         ??Name$ 	pcNameRun??5m   y   6?   5n      5o      5p      5q      5r      ?? 5s   
   $??1??6   $??10q?5t      $
??Auto=?5u      $??Disabled=?5v      .??5x      ?? -   5        $ descriptionTextBlock??5x      $??15(?5z      ??5y      ?? *
S_Answer+ ? DataContext 5|      )   #ServerApp.Controllers.TcpController  5}      5      5?      ?? 5?   
   -   5        $ pcInfoButton??5?      $??2??5?      $??OK??5?      $??18(?5?       ?? 	IsDefault$
 True??5?      $??10q?5?      $	??100??5?      $??Right=?5?      5?      5?      