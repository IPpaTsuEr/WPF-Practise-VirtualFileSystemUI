   M S B A M L   `   `   `  ����J  FFrameLessWindow, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullJ6clr-namespace:FrameLessWindow;assembly=FrameLessWindowFrameLessWindow  5   	   N JFrameSlecteListView, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullV>clr-namespace:FrameSlecteListView;assembly=FrameSlecteListViewFrameSlecteListView 5	   	   D @SearchBox, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null8*clr-namespace:SearchBox;assembly=SearchBox	SearchBox 5
   	   E AVMControls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;,clr-namespace:VMControls;assembly=VMControls
VMControls 5   	   A =VMFWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:VMFWPFVMFWPF 5   	      VMFWPF.VMWindow   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   c	 _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity 	 5   	   h
 dMicrosoft.Expression.Interactions, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bie9http://schemas.microsoft.com/expression/2010/interactions 
 5   	   ?fl6clr-namespace:FrameLessWindow;assembly=FrameLessWindow 
 5   	   Gfs>clr-namespace:FrameSlecteListView;assembly=FrameSlecteListView 
 5	   	   3sb*clr-namespace:SearchBox;assembly=SearchBox 
 5
   	   5eb,clr-namespace:VMControls;assembly=VMControls 
 5   	    localclr-namespace:VMFWPF 
 5   	     � Title$  虚拟磁盘系统��5         IconForeground#���   5      $	��450��5      $	��800��6"   $	��200��5      $	��200��6%    � 
ResizeMode$ CanResizeWithGrip=�5      %   FrameLessWindow.FrameLessWindow   ToolBarHeight$ 32 �5   	      StateBarHeight$ 32 �6   ���	#FF007ACC   ShadowColor$ 	#FF007ACC��5      ���	#FF2D2D30 � 	Resources 5       �� 
TargetType �� Property �� Value	 �� 
TargetType
 J� RenderTransformOrigin �� Angle � AllowsTransparency � HorizontalOffset � VerticalOffset �� ElementName �� Mode � 	StaysOpen �� RelativeSource �� Mode  IconBackground �� DataType  VMControls.VMNode � ColumnDefinitions  StringTypeToIconConvert �� 	Converter �� ConverterParameter  DefaluteSmallIcon �� FallbackValue  IconText �� Triggers ]� 
Conditions �� 
SourceName �� Property �� Value �� 
TargetName P� 
SourceName  P� Property! P� Value��%�  '��       7  7  7  �� 5   
   " ��6   .�5      ��  � ��# �   6+   �� 5       � �� 5      �� 5      "	 ��6&   .R�5       � .+�5!      �� ._�5"   "   d� $��display_popup��6/   
������ $
 0.5,0.5&�5#   /   $��0q�6P   |�. z�5$   &   �� 5%   *   $ 0Q�6:   6F   .��5'   &   �� #��z��60   $��>��6Z   6f   5(   #   5)      � 5*      $��Bottom=�6$    . $ 16��6Q   $ 8��6g   |�. ��5+   !   �� *	IsChecked+$ display_popup��$ TwoWay=� 5,   !   �� *	IsFocused+ �� $
 Self=�.��5-   "   �� $��4q�6)   $��0q�64   ._�5.   &   �� #��z��6/   #���  5/   /   $	��200��50   /   $��0q�6;   $
��Auto=�51   /   ��52   *   �� 53   .   "  6;   (��       *, +).R�54   2   �  55   6   �� 56   :   $
��Auto �6K   �� 57   :   .+�59   6   �� $��20��6<   ��6H   �� 8   $ 0��8  �� 5:   6   $��1��6@   8�� $��12(�6�   $��Normal"�6�   ��6P   �� *Name+6�   5;   3   5=   /   5?   '   5@   #   5A      5B       5D      ]� 5E       5F   "   �� 5G   &   $ display_popup��6T    �@J�	IsFocused .  6q   �� 5H   &   $ display_popup��6S    �@J�	IsFocused .  6p   ./�5J   "   �� $ display_popup��6L    �@d�	IsChecked$ FalseY�6>   6i   5K      P� 5N      $ display_popup��6H     �@d�	IsChecked$
! TrueY�6;   . �5O   "   �� $ display_popup��6D    � z� 5P   &   �� 5Q   *   $ 90Q�6:   6G   5S   #   5T      5V      5X      5Y      5Z      "   TitleBarContext" 5\      �� 5]   
   $��虚拟磁盘系统��6   $��Center=�6"   $
��10,0q�6=   
�������6K   #   ToolBarExContent# 5`      o� 5a   
   #���   6   
������ 
���   �$��0,0,0,1q�6j   .@�5b      m� $��文件��6   $��32��6#   .@�5c      m� $��打开��6   $ m� CommandParameter$
$ Open��5f      {�5d      �� *DataContext.OpenSuccesed+  EqualToBool# [ $ False��% m� Command% 5e      �� *DataContext.SystemCommand+5f   5   �� 5g        CustomSeparator#��[ 6   m� 5h      $��新建��6   $$ 	CreateNew��5j      % 5i      �� *DataContext.SystemCommand+5k      m� 5l      $��	格式化��6   $$ Format��5o      {�5m      �� *DataContext.OpenSuccesed+# [ $
 True��% 5n      �� *DataContext.SystemCommand+5p      �� 5q      #��[ 6   m� 5r      $��关闭��6   $$ Close��5u      {�5s      �� *DataContext.OpenSuccesed+# [ $
 True��% 5t      �� *DataContext.SystemCommand+5u   6   �� 5v      #��[ 6   m� 5w      $��退出��6   $$ Exite��5y      % 5x      �� *DataContext.SystemCommand+5y   6   5z      m� 5|      $��编辑��6   .@�5}      m� $��导出到...��6   {�5~      �� *DataContext.CurrentNode+# [ $ VM��% 5      �� *DataContext.NodeCommand+$ 5�      `� 5�        DoNothingMultiConvert& `� 	Converter#& [ 6'   .1�5�      �� ' �� Source$' Export��6&   68   �� 5�      $ 	Main_List��6'   ( �� Path$( SelectedItems�6?   6V   5�      5�      �� 5�      #��[ 6   m� 5�      $��	导入...��6   {�5�      �� *DataContext.CurrentNode+# [ $ VM��.@�5�      m� $��文件��6   $$ Inport��6W   % 6+   �� *DataContext.NodeCommand+6W   m� 5�      $��	文件夹��6   $$ InportFloders��6X   % 6,   �� *DataContext.NodeCommand+6X   5�      5�      5�      .��5�      � ) � RowDefinitions) 5�   
   �� 5�      $��32 �6   6*   �� 5�      6   .+�5�   
   �  5�      �� 5�      $
��Auto �6#   62   �� 5�      6%   �� 5�      $
��Auto �6#   62   .+�5�      �� * �� Orientation$* 
Horizontal=�6   .+�5�      ��   
IconButton#��[ 6   $��GoBack��5�       
	 GoBack#��[	 5�      ��5�      �� *DataContext.NodeCommand+5�   =   �� 5�      #��[ 6   $��	GoForward��5�      #��[	 5�      $
 0.5,0.5&�6;   ��5�      �� *DataContext.NodeCommand+z�5�      �� 5�      $	 180Q�6*   68   5�      �� 5�      #��[ 6   $
��GoUp��5�       
 GoUp#��[
 5�      ��5�      �� *DataContext.NodeCommand+5�   ;   5�      � 5�      $��1��6   
������  5�      �� 5�      $
��Auto �6'   66   �� 5�      6)   �� 5�      $
��Auto �6'   66   .+�5�      � $y�Hidden=�6   $��2��62   + � Maximum$+ 1Q�6F   , � Value$	, 0.8Q�6R   6`   �� 5�      $* 
Horizontal=�6   .+�5�      �� $��24��6   ��6'   �� *DataContext.CurrentNode+# [ $ 0��6�   �� 5�      $y�Hidden=�6   66   5�      � 5�      -   5        $��History_List��5�   )   $��1��6    5�      �� 5�      6-   �� 5�      $
��Auto �6+   6:   �� 5�      $
��Auto �6+   6:   .+�5�      �� -   5        $��Path_Viewer��5�      
������ $��Stretch=�5�      $��Stretch=�5�      $��0q�5�      ��5�      �� *DataContext.CurrentNode+  NodeToPathView# [ . 	 (System.Windows.Interactivity.Interaction-   Triggers- 5�      / 	 )System.Windows.Interactivity.EventTrigger  5�      .   	EventName$. Loaded��6-   3 	 -System.Windows.Interactivity.EventTriggerBase/   
SourceName$/ Path_Viewer��6@   . 	 (System.Windows.Interactivity.TriggerBase0   Actions.0 5�   "   6 	 0System.Windows.Interactivity.InvokeCommandAction  1   Command1 68   �� *DataContext.ProcessCommand+2   CommandParameter2 5�   8   �� $ Path_Viewer��5�      ��5�      �� 5�      .R�5�   "   �� 3 K� IsItemsHost3 . $* 
Horizontal=�6@   
������ 6t   5�      ��5�      �� 5�      "  6+   .R�5�   "     VMControls.ExButton  -   5        
������ $y�Visible=�5�   .   #���   5�   .   4   DisplayName4 5�   .   �� *Name+5   ChildrenSource5 5�   .   �� *
Children+  PathViewDisplayFloderOnly# [ 6   ChildVisiable6 5�   &   `� 5   *      PathViewChildrenListVisiable#& [ 67   .1�5  .   �� 69   �� 5  .   $( CurrentNode�66    6I   �� 7 �� AncestorType$7 WindowL�6�   5  +   5  #   5     5     d� 5     -   5        $��Open_HistoryList��5  #   $��1��5  #   $��24��63   $��0q�5  #   $
 0.5,0.5&�5  #   z�5     �� 5     $	 180Q�6.   6<   ��5     �� 5      
 PullUp#��[ 6(   #��[ 5  &   
������ 5  &   5     � 5     $��1��6    .  .  $	 -35��5!     $��Bottom=�5$  Q   ��5     �� $( 	IsChecked�$ Open_HistoryList��$ TwoWay=�
8 � Tag8 5"     �� *PlacementTarget.ActualWidth+ �� $
 Self=�9 � DataContext9 5#     �� *PlacementTarget.DataContext+ �� $
 Self=�: � PlacementTarget: 5$     �� $ History_List��.��5%     ��   HistoryListDataStyle#��[ 5&  "     HistoryListItemStyle#��[ 5'  "   $	��300��5(  "   ; �� SelectionMode$; Single=�62   #���  5)  "   #���   5*  "   $��1,0,1,1q�5+  "   ��5%  "   �� *DataContext.History+��5,  "   �� $	( Tag� �� $7 PopupL�- 5-       5.  "   $. SelectionChanged��61   .0 5/  &   >	 
 8Microsoft.Expression.Interactivity.Core.CallMethodAction	  < 	  
MethodName$#< History_List_SelectionChanged��6:   = 	  TargetObject= 6e   �� 6�   50  #   52     53     �� 55     $��2��6   #��[ 6-     Refreash#��[ 56     $��Refresh��58     ��57     �� *DataContext.NodeCommand+58     59     5;     
  SearchBox.SearchBox
  5=     $��2��6   $	��200��6+   
������ > 
  CornerRadius$> 0��6P   ? 
  RealTimeSearch? .  #���   5?     @ 
  
SearchIcon@ 5@     �� 5A     #��[ 6     
 Search#��[ 6B   $��14(�6a   $��Light"�6o   6�   - 5C       5D     $. OnSearchClick��6%   .0 5E       1 60   �� *DataContext.NodeCommand+ �� $7 WindowL�2 5F     `� 5G  "   #& [ 6/   .1�5H  &   �� $' Search��6.   �� 5I  &   $( 
SearchText�6.    6@   �� "7 
 6�   5J  #   5L     5M     5O     5P     � 5R  
   $��1��6    5S     �� 5T     $	��200 �6#   61   �� 5U     $��3 �6#   6/   �� 5V     6%   .+�5Y     R� -   5        $��	Side_List��5Y     $��0q�5Z     
�������  SideListDataStyle#��[ 5]       SideListItemStyle#��[ 5^     A %� IsVirtualizingA . B %� VirtualizationMode$B 	Recycling=�5`     ���	#FF1E1E1E��5\     �� *DataContext.IndexViewTree+- 5b       5c     $. SelectedItemChanged��6%   $/ 	Side_List��6E   .0 5d       1 60   �� *DataContext.NodeCommand+ �� $7 WindowL�2 5e     `� 5f  "   #& [ 6/   .1�5g  &   �� $' SideSelected��6.   �� 5h  &   $( SelectedItem�6.   $ 	Side_List��6B   6]   5i  #   5k     5l     5n     �� 5p     $��1��6   $��0q�6+   $��Stretch=�5q     $��Stretch=�69   ���	#FF3E3E426n   /  )FrameSlecteListView.FrameSelectedListView  5s     -   5        $��	Main_List��5s  '   $��2��68   C   FrameSelectModeC . D J� 	AllowDropD . |�.   MainListStyle_#��[ 5w  '     MainListItemStyle#��[ 5y  '     MainListItemSelecter#��[ 5z  '   ���	#FF1E1E1E��5x  '   �� *CurrentNode.Children+- 5{       5|     $
. Drop��6%   $/ 	Main_List��66   .0 5}     	  $< Main_List_Drop��6.   = 6J   �� 6f   5~     5�     5�     5�     E   StateBarExContentE 5�     � 5�  
    5�     �� 5�     $
��Auto �6#   62   �� 5�     6%   �� 5�     $
��Auto �6#   62   .+�5�     �� $��2��6   6+   5�     5�     