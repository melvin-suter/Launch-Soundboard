   M S B A M L   `   `   `  ����L  HLaunch-Soundboard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:Launch_SoundboardLaunch_Soundboard  5   	   "    Launch_Soundboard.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   1localclr-namespace:Launch_Soundboard     5   	   ���#222  � Title$  Launch-Soundboard��5	   	   $	��450��6#   $	��800��60    � 	Resources 5         base_layout �� 
TargetType �� Property �� Value �� BasedOn  base_buttons �� 
TargetType �� Triggers P� Property	 P� Value�� 5   
   %\  &	        '�� �     7   &	 �     7   '��     7  �� 5      " ��6(   .�5      ��  � �� �#3336/   �� 5       � �� �#22260   �� 5       � ��$ 2q�64   �� 5       � �� �#FFF6/   5      �� 5      " ��6   8   6)   �� 5      " ��6)   8   .�5      ��  � �� 5      �� 5      " ��6*   .R�5      �� #��z��6%   #��z��6O   #��z��6{   ._�5   "   �� $��Center=�63   $��Center=�6P   5      5      5       5      P� 5        �@J�IsMouseOver	 . 5!      5#      �� 5$      " ��6   8   6(   5%      .��5(      � -   5        
 �Name$
 
launchGrid��5(       � ColumnDefinitions 5)   
   �� 5*      �� 5+      �� 5,      �� 5-      �� 5.      �� 5/      �� 50      �� 51      �� 52       � RowDefinitions 54   
   �� 55      �� 56      �� 57      �� 58      �� 59      �� 5:      �� 5;      �� 5<      �� 5=      .+�5A   
   �� -   5         ��Name$ 
editButton��5A   =   $��0��6   $��8��6   $
��Edit��6.   6=   �� 5B   
   -   5        $ 
saveButton��5B   >   $��0��6   $��8��6   $��Close��6.   $y�	Collapsed=�6R   5C      5D      