   M S B A M L   `   `   `  ���� A  =VMFWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:VMFWPF.CustomVMFWPF.Custom  -clr-namespace:VMFWPF.CustomVMFWPF.Custom      VMFWPF.MainWindow   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     -localclr-namespace:VMFWPF.Custom     +srcclr-namespace:VMFWPF.Custom     -     � WindowStyle$
  None=� � AllowsTransparency .  � Title$ 
MainWindow��$	��600��$	��800��   CustomWindowControlTemplate#���    � 	Resources   CustomToggerButtonStyle �� 
TargetType �� Property �� Value �� 
TargetType �� Path	 �� RelativeSource
 J� SnapsToDevicePixels �� Triggers P� Property P� Value �� 
TargetName �� Angle �� CenterX �� CenterY T� X T� Y  TreeItemStyle � RowDefinitions �� Orientation d� 	IsChecked �� TextAlignment  CustomButtonControlTemplate �� StrokeDashArray J� Effect3  -System.Windows.Media.Effects.DropShadowEffect   
BlurRadius   ShadowDepth   	Direction   Color � Cursor  ListViewItemButton �Name
  � Tag! � ToolTip  Custom_MenuItem��%  &	       &	 �    7  &	 �    &	  s    &	 �	    &	 ]    �� " d�.���  � ��
 ���� ��  � ��$ 32����  � �� �� " d�.R�� ���� $ 
Background�	 �� *TemplatedParent+.+�F� $��
ToggerPath��
�������$��Center=�$��Center=�
 . ��B� $��M 0,0 L 5,5 L 0,10 ZC� P�  �@d�	IsChecked$
 TrueY�. ��� $ 
ToggerPath�� � ��
 ������� $ 
ToggerPath�� � z� V� .~��� $ 45Q�$ 5Q�$ 5Q�T� $ 5Q�$ 5Q��� " Q�.���  � �� �� " Q�.R��  �� $��30 ��� $
��Auto �.+��� $��
SelectBack��$��-100000,0,-100000,0q����� $ 
Background�	 �� *TemplatedParent+�� $	��bor��
������ $y�Visible=�._��� $ 
Horizontal=�$��20��$��Stretch=�.+�d� $��Toggle��8��  $��5,0,0,0q� �� $ 
IsExpanded�	 �� *TemplatedParent+�� $��16��$��16��$��Custom/Floder.png���� 
�������$��Center=�#��z��$
 Left=�$	��5,0q�$
��Wrap=�$��CharacterEllipsis=��� $��1���� $��SubItems��$��2��$y�	Collapsed=�$��	20,0,10,0q� P�  �@Q�
IsExpanded . . ��� $ SubItems�� � y�$ Visible=�P�  �@��HasItems .  . ��� $ Toggle�� � y�$ Hidden=�P�  �@Q�
IsSelected . . ��� $ 
SelectBack�� � �� �	#993CB1DC�� " ��.R��� $��Border��
������ 
�������._�� .+��� $��
FocuesRect��$y�Hidden=�
���   �$��1��$	 1 2R�
 . �� #��z��#��z��$��Center=�$��Center=� P�  �@J�IsMouseOver . . ��� $ Border�� � �� �	#FF636370P�  � �� . . ��� $ Border�� � ��
 �  ��P�  �@J�IsKeyboardFocused . . ��� $ 
FocuesRect�� � y�$ Visible=��� " �.R�� $��OuterBorder��$��10q� �� $��26 ��� $��* �.+��� $��WindowBorder��
���  ��$��0q�$��2�����	#FF3A3A3A   $ 10Q�$ 0Q�$ 0Q�$ 	#FF008BFF���� $��TopBar��
������ ._��� $��Title��$��Stretch=�$��Center=�#��z #��z��$��5,1,1,0q��� $ 
Horizontal=�$��Right=�.+��� $	��Min��$��_��$��Right=�$��Stretch=�$��26��#���  �� $	��Max��$	��口��$��Right=�$��Stretch=�$��26��#���  �� $��Close��$��X��$��Right=�$��Stretch=�$��26��#���  �� $��LeftSide��$ SizeWE��
������ $
��Left=�$��5��$��2���� $��	RightSide��$ SizeWE��
������ $��Right=�$��5��$��2���� $��TopSide��$ SizeNS��
������ $��Stretch=�$��5��$	��Top=�$��0���� $��
ButtomSide��$ SizeNS��
������ $��Stretch=�$��5��$��Bottom=�$��1���� $��1��.u��� �� " ��.���  � ��
 �  ����  � �� �� .R�� $ ConPanel��
������   �� $ NodePtr�.+��� $��68��$	��130��$��68��$
��5,10q�! �� $ FileName�.+��� $��68��$��68��$��68��$��68��$��UniformToFill=����� $ 	TypeImage��� 
������ $
��Wrap=�$��WordEllipsis=�$��62��$��Center=�
����������� $ FileName��� �� " m�.���  � �� �� .R�� #��z��.+��� 
��������� #��z���� #��z��#��z��$��Center=�$��Center=�.��� -   $��	UIContext�����	#FF2D2D30$��5,0,5,5q�" � ColumnDefinitions" �� $	��200 �$��50���� $��5 ��� $��* �$��50�� �� $��30 ��� $��60 ��� $��* ��� $��26 �.+�R� -   # R�Name$# treeView��$��5,5,0,5q�$��2��#��[ $��Center=�$��Center=����	#FF252526��Z� �� -   $ ��Name$$ gridSplitter��$��1��$��2��$��Stretch=�$	��0,5q�$��Stretch=�$��Stretch=����	#FF333337�� -   % ��Name$
% View��$��2��$��0,5,5,5q�$��2�����	#FF1A1A1A$��Disabled=�$	��Top=�& J� 	AllowDrop& . ' �� SelectionMode$' Single=���Z� ( � ContextMenu( �� .@�m� -   $��新建文件夹��m� -   ) m�Name$) delete��$��删除��m� -   $) rename��$��	重命名������ .R�� * K� IsItemsHost* . + � Orientation$+ 
Horizontal=����� .R��� -	   #��[ �� -
   , ��Name$, button��$��<��$��2��$
��Left=�$��	10,31,0,0q�$��1��$	��Top=�$��75��$! <���� -   $, button1��$
��Open��$
��Left=�$��	99,31,0,0q�$��1��$	��Top=�$��75��$��2���� -   $, button2��$��Close��$
��Left=�$��
179,31,0,0q�$��1��$	��Top=�$��75��$��2���� -   $, button3��$��Export��$��2��$
��Left=�$��
271,31,0,0q�$��1��$	��Top=�$��75��o� -   - o�Name$
- menu��#��[ $��3�����	#FF2D2D30$��Center=�$��Center=�.@�m� $��文件��$��2q����	#FF2D2D30
�������$��60��$��30��#��[ .@�m� $��打开�����	#FF2D2D30m� $��新建�����	#FF2D2D30�� m� $��	格式化�����	#FF2D2D30m� $��编辑��$��2q�
�������$��60��$��30��