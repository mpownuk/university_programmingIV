   M S B A M L   `   `   `  ����F  BRENT_A_TOOL, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:RENT_A_TOOLRENT_A_TOOL  5   	       RENT_A_TOOL.AddToolWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:RENT_A_TOOL     5   	     � Title$  AddToolWindow��5   	   $	��450��6   $	��400��6,    � 
ResizeMode$ NoResize=�68   .��5
      � $��10q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   �� $��Nazwa:��6   �� 5   
   -   5         ��Name$ NazwaTextBox��5      $��1��6(   $��0,5,0,10q�65   �� 5   
   $��Opis:��6   $��2��6!   �� 5   
   -   5        $ OpisTextBox��5      $��3��6'   $��50��64    �� TextWrapping$
 Wrap=�6@    �� AcceptsReturn$
 True��6T   $��0,5,0,10q�6i   �� 5   
   $��Stan magazynowy:��6   $��4��6,   �� 5   
   -   5        $ StanMagazynowyTextBox��5      $��5��61   $��0,5,0,10q�6>   �� 5   
   -   5         ��Name$ WybierzZdjecieButton��5      $��Wybierz zdjęcie��6/   $��6��6I   $	��0,5q�6y   �� 5    
   -   5         ��Name$ PreviewImage��5       $��7��6&   $	��100��63   $	��100��6@   $��10q�6L   $y�	Collapsed=�6X   �� 5"   
   -   5        $ DodajButton��5"      $��Dodaj sprzęt��6&   $��8��6=   $��0,10,0,0q�6d   5#      5$      