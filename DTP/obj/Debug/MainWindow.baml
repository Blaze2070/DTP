   M S B A M L   `   `   `  ����>  :DTP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:DTPDTP  5   	       DTP.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   #localclr-namespace:DTP     5   	     � Title$'  !Учет происшествий��5   	   $	��700��6#   $
��1050��60    � WindowStartupLocation$ CenterScreen=�5	   	   ���#f6f7fb.��5      �  � RowDefinitions 5   
   �� 5      $��64 �6   �� 5      $��* �6   .+�5   
   �� ���#3F5FBF$��0��6&   $��64��63    �� CornerRadius$ 	0,0,18,18��6?   ._�5      ]� $
��24,0q�6   .+�5      ��  �� Orientation$ 
Horizontal=�6   $��Center=�66   $
��Left=�6Q   .+�5      A� $��36��6   $��36��6)   ���#f2f7fe$��0,0,14,0q�6D   $��Center=�6V   �� 5      $'��!УЧЕТ ПРОИСШЕСТВИЙ��6    $��Center=�69   
�������$��22(�5   3   $
��Bold"�5       $��Segoe UI)�62   5      �� 5      -   5        $��+��5      $��38��6%   $��38��60   $��20(�6<   $��0,0,8,0q�5      ���#71a0ff
�������$
��Bold"�5      ���#3F5FBF � Cursor$
 Hand��5       � ToolTip$/ )Добавить происшествие��6'   $��Right=�5      $��1q�65   5       5!      5"      �� 5$   
   $��1��6   
�������$ 16��61   $��18,12,18,18q�6C   $��1q�6X   ���#c0cad6._�5%      �  5&      �� 5'      $
��Auto �6$   �� 5(      $��* �6$   .+�5+      &   System.Windows.Controls.DataGrid     AutoGenerateColumns .  $��1��5,      $��	18,2,18,8q�6(      	RowHeight$ 40Q�6;   	   HeadersVisibility$	 Column=�5-      
   CanUserAddRows
 .     CanUserDeleteRows .  ��5+      �� *AccidentList+   
IsReadOnly 5.   5   �� *
IsReadOnly+   SelectionMode 5/      �� *SelectionMode+ �� SelectedItem 50      �� *SelectedAccident+   Columns 51      0  *System.Windows.Controls.DataGridTextColumn  52      ,  &System.Windows.Controls.DataGridColumn   Header$ Дата��6-      Width5  /System.Windows.Controls.DataGridLengthConverter$	 150 6v   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6;   �� *Date_and_Time+ �� StringFormat$ 
dd.MM.yyyy��6v     53      $ 
Место��6-   $	 180 6Y    6<   �� *
Location+6Y     54      $ Тип��6-   $ 80 6\    6:   �� *Accident_Type+6\     55      $ Описание ��6-   $ * 6a    6A   �� *Description+6a   4  .System.Windows.Controls.DataGridTemplateColumn  57      $  ��61   $ 90 6;      CellTemplate 58      �� 59   "   .R�5:   &   �� $ 
Horizontal=�61   $��Center=�6K   .+�5;   *   �� -   5        $��✏️��5;   1   $��30��5<   1   $��30��6=   5=   1   �� 5@   *   -   5        $
��🗑��5@   1   $��30��5A   1   $��30��6=   5B   1   5D   '   5E   #   5G      5I      5J      5K      5L      5M      