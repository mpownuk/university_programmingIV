   M S B A M L   `   `   `  ����  RENT_A_TOOL     RENT_A_TOOL.RentToolWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  Wypożycz narzędzie��5   	   $	��400��6$   $	��400��61    � 
ResizeMode$ NoResize=�6=   .��5      � -   5         �Name$ RentGrid��5      $��10q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5	      $
��Auto �6   �� 5
      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   �� -   5         ��Name$ ToolNameText��5      $
��Bold"�6*   $��18(�6<   $��Center=�5      $��	0,10,0,20q�61   �� 5   
   $��1��6    �� Orientation$ 
Horizontal=�6"   $��10q�6;   .+�5      �� $"��Wybierz datę wypożyczenia:��6   $��Center=�6:   $	��180��6U   (  "System.Windows.Controls.DatePicker  5      -   5          Name$ RentDatePicker��5      5      �� 5   
   $��2��6   $ 
Horizontal=�6"   $��10q�6;   .+�5      �� $��Wybierz datę zwrotu:��6   $��Center=�64   $	��180��6O     5      -   5        $ ReturnDatePicker��5      5      �� 5   
   -   5        $��Potwierdź wypożyczenie��5      $	��200��62   $��40��6>   $��Center=�5      $��4��6.   $��10q�6;   5      5       5!      