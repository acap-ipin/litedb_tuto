   M S B A M L   `   `   `  ����B  >Litedb2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:Litedb.ViewLitedb.View  5   	       Litedb.View.HomePage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:Litedb.View     5   	     N� Title$  HomePage��5   	   $	��430��6   $	��800��6'    � 	Resources 5	       �� 
TargetType �� Property �� Value �� 	Resources$   Litedb.View.AdminboolConverter   convert_admintobool#   Litedb.View.UserboolConverter  convert_usertobool$   Litedb.View.BooladminConverter  convert_booltoadmin��%�  '��       '�� �     '�� t    'G� �    &	  A    &	 T    &	 g    �� 5
   
   " ��6   .�5      ��  � ��$	 5,5q�6'   �� 5       � ��$
 Left=�6/   �� 5       � ��$ 60��6&   �� 5       � ��$ 30��6'   5      �� 5   
   " ��6   .�5      ��  � ��$	 200��6&   �� 5       � ��$ 5,5,5,0q�6'   �� 5       � ��$ Right=�6/   �� 5       � ��$ 30��6'   5      �� 5   
   " ��6   .�5      ��  � ��$ 5,5,5,0q�6'   �� 5       � ��$ 30��6'   �� 5       � ��$	 5,5q�6(   �� 5       � ��
 ���_�6+   �� 5       � ��
 �����6+    5      ���� 5      '��       " ��6   .�5      ��  �@��CornerRadius$ 5��65   5      5       5!      �� 5"   
   " G�6   .�5#      ��  � ��$ 5,5,10,0q�6'   �� 5$       � ��$ 30��6'   5%        5&   
   6#     5'   
   6"     5(   
   6#   5)      .��5*      �  � ColumnDefinitions 5+   
   �� 5,      $
��Auto �6   �� 5-      $��* �6   .+�5/   
   ��  �� Orientation$ Vertical=�6   .+�50      �� -   5         ��Name$ tb_id��50      $y�	Collapsed=�6%   ��52      �� 	 �� ElementName$	 datagrid��
 �� Path$
 SelectedItem.Id� �� Mode$ OneWay=�]� 53      .+�54      �� .��6   Name6   �� 55      -   5        $ tb_name��55      ��56      �� $	 datagrid��$
 SelectedItem.Name�$ OneWay=�57      ]� 58      .+�59      �� .��6   Email6   �� 5:      -   5        $ tb_email��5:      ��5;      �� $	 datagrid��$
 SelectedItem.Email�$ OneWay=�5<      ]� 5=      .+�5>      �� .��6   Phone6   �� 5?      -   5        $ tb_phone��5?      ��5@      �� $	 datagrid��$
 SelectedItem.Phone�$ OneWay=�5A      ]� 5B      .+�5C      �� .��6   
Password6"   �� 5D      -   5         ��Name$ btnshow��5D      $��Right=�6*   .��6~   Show6�   �� 5E      -   5        $ 
tb_pw_show��5E      $y�	Collapsed=�6.   ��5F      �� $	 tb_password��$
 ActualWidth���5G      �� $	 tb_password��$
 Margin�6T   G� 5H      -   5         G�Name$ tb_password��5H      63   5I      ]� 5J      .+�5K      �� $	��5,0q�6   .��6%   Role6+   �� 5L      $ 
Horizontal=�6   $	��5,0q�66   $��Center=�6C   .+�5M      	� -   5         	�Name$ admin��5M   "    d� 	IsChecked 5N   "   �� $	 datagrid��$
 SelectedItem.Admin� �� 	Converter# [  $ OneWay=�.��5O   #   Admin6*   	� 5P      -	   5        $
 user��5P   "   $��10,0,0,0q�60    5Q   "   �� $	 datagrid��$
 SelectedItem.Admin�# [ $ OneWay=�.��5R   #   User6)   �� 5S      -
   5        $ tb_admin��5S      $y�	Collapsed=�60   ��5T      �� $	 admin��$
 	IsChecked�# [ $ OneWay=�5U      5V      �� 5W      -   5        $ 	btninsert��5W      .��6@   Insert6H   � 5X      -   5         �Name$ 
updategrid��5X      $y�Hidden=�6'    5Y      �� 5Z      $��* �6'   �� 5[      $��* �6'   .+�5]      �� -   5        $ 	btnupdate��5]      .��6D   Update6L   �� 5^      -   5        $ 	btndelete��5^      $��1��6,   .��6T   Delete6\   5_      �� 5`      -   5        $ 	btn_excel��5`      .��6?   Import Excel6M   �� 5a      -   5        $ btn_back��5a      $��Bottom=�6'   .��6Y   Back to login6h   5b      ]� 5c   
   $��1��6   .+�5d      �� $��15,0,0,0q�6   $
��Bold"�6*   $	��Top=�6<   Select a user to edit6h   &   System.Windows.Controls.DataGrid  5e      -   5          Name$ datagrid��5e      $��10,10q�6)      
IsReadOnly . 6L   5f      5g      5h      