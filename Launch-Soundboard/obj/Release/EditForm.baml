   M S B A M L   `   `   `  ���� L  HLaunch-Soundboard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:Launch_SoundboardLaunch_Soundboard       Launch_Soundboard.EditForm   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     1localclr-namespace:Launch_Soundboard       � 
ResizeMode$  NoResize=����#222 � Title$
 Edit��$��149.584��$��315.097�� � 	Resources    base_layout �� 
TargetType �� Property �� Value �� BasedOn  base_textblock  base_textbox  base_comboboxItem  ComboBoxNormalBorderBrush �� Color ! ComboBoxNormalBackgroundBrush # ComboBoxDisabledForegroundBrush # ComboBoxDisabledBackgroundBrush  ComboBoxDisabledBorderBrush �� 
TargetType  	 ComboBoxToggleButtonTemplate	 � ColumnDefinitions
 �� CornerRadius �� Triggers P� Property P� Value �� 
TargetName �� 	ClickMode d� 	IsChecked �� Path �� RelativeSource �� Mode J� IsHitTestVisible �� SelectionBoxItem �� SelectionBoxItemTemplate �� 
IsReadOnly �� 
IsReadOnly � AllowsTransparency �� IsDropDownOpen J� SnapsToDevicePixels � ActualWidth �� MaxDropDownHeight-  'System.Windows.Input.KeyboardNavigation   DirectionalNavigation 
 base_buttons �� Padding  �� Triggers�� %  &	        '�� ?     7   &	 N     '�� �     7  &	 �     '�� <    7  &	 K    '�� �    7  &	 �    &	 �    &	 �    &	 �    &	 �    &		 �    7  7  7  7  7  7  7  '�� �    7 	 7  7  &	
 y	    7   '�� 
    7 
 �� " ��.���  � �� �#333��  � �� �#FFF�� " ��8   �� " ��.���  � �� �#FFF��  � ��$ Center=���  �@��Padding$ 4q��� " ��8   �� " ��.���  � �� �#333��  � �� �#333��  � �� �#FFF��  � ��$ 3q���  � ��$ 1q��� " ��8   �� " ��.���  � �� �#333��  � �� �#FFF�� " ��8   �� $
 #222���� $
 #333���� $
 #888���� $
 #333���� $
 #222���� " d�.R�� 	 �� �� $��20 �.+��� $��Border��$��2��8��  $
 0��$��
1, 1, 1, 1q�8�� �� $��ButtonBorder��$��1��$��
1, 1, 1, 1q����#444$
 
0, 0, 0, 0��$��
0, 0, 0, 0q�8�� F� $��Arrow��$��1��=����        �    �� � 	= ��[ � z �� � z     � 	=  	= 	$��Center=����#444$��Center=� P�  �@J�IsMouseOver . . ��� $ ButtonBorder�� � �� �#333P�  �@d�	IsChecked$
 TrueY�. ��� $ ButtonBorder�� � �� �#333�� $ Arrow�� � �� �#999P�  � {� .  . ��� $ Border��8   � ���� $ ButtonBorder��8   � ���� $ ButtonBorder��8   � ���� 8   � ���� $ Arrow�� � �� �#999�� " ��.���  �@J�SnapsToDevicePixels . ��  �@�OverridesDefaultStyle . ��  � ��$
 Auto=���  � ��$
 Auto=���  � �� . ��  � �� �#fff��  �@�FocusVisualStyle Z� ��  � �� �� " ��.R�� .+�d� $��ToggleButton��$��2��$ Press=�|�.  8��   �� $ IsDropDownOpen� �� *TemplatedParent+$ TwoWay=��� $��ContentSite��$��5, 3, 23, 3q� .  $
��Left=�$��Center=�#��z #��z #��z���� $��PART_EditableTextBox��$��3, 3, 23, 3q�# z $y�Hidden=�
������ $
��Left=�$��Center=�|�. .������ " ��.R��� $��PART_ContentHost��|�.  .��� $��Popup��$��Bottom=�|�.   . #��z $��Slide=�.��� $��DropDown�� . #��z #��z .+��� $��DropDownBorder�����#444$��
0, 0, 0, 0q�$
 0��$��0q�8�� �� $��1q� . .���� $ 	Contained=� P�  �@��HasItems .  . ��� $ DropDownBorder�� � ��$ 95��P�  � {� .  . ��� 8   � ��P�  �@��
IsGrouping . . ���  � �� .  P�  �@��
IsEditable . . ���  �@ 	IsTabStop .  �� $ PART_EditableTextBox�� � y�$ Visible=��� $ ContentSite�� � y�$ Hidden=��� " ��8   .���  � �� �� " ��.R��� #��z�����#222$��1q�$ 5q�._��� $��Center=�$��Center=�  P�  �@J�IsMouseOver . �� " ��8   .��� 	 �� $��* ��� $��2* �! � RowDefinitions! �� $
��auto ��� $
��auto ��� $
��auto ��� �� $
��auto �.+��� $��Sound��$��0��� $��1��	 �� �� $
��auto �.+��� -   " ��Name$" soundBox�� . ���#444�� -   $	��...��$��1���� $��Volume��$��1���� -   $" 	volumeBox��$��1��$��1��$��1��# � 
InputScope$# Number���� $��Color��$��2���� -   $ ��Name$$ colorBox��$��2��$��1���� $��4��% �� Orientation$% 
Horizontal=�$��2��$��Right=�.+��� -   $
��Save���� -   $��Cancle��