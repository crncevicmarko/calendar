   M S B A M L   `   `   `  ����C  ?Calendar, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:Calendar.ViewCalendar.View  5   	   %    Calendar.View.UserProfileWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:Calendar.View     5   	     � Title$  UserProfileWindow��5   	   $	��400��6#   $	��600��60   .��5	      �  � ColumnDefinitions 5
   
   �� 5      $	��200 �6   6-   �� 5      6!    � RowDefinitions 5   
   �� 5      $
��Auto �6   6,   �� 5      $
��Auto �6   6,   �� 5      $
��Auto �6   6,   �� 5      $
��Auto �6   6,   �� 5      $
��Auto �6   6,   �� 5      6   .+�5   
   �� $��0��6   $��0��6   $��First Name:��6-   $��20(�6C   $��5,5,5,5q�6Q   $��Right=�6b   $��Center=�6�   6�   �� 5   
   $��0��6   $��1��6   $��5,5,5,5q�6/   $��40��6@   $	��300��6L   $
��Left=�6X   ��6s   �� *	FirstName+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�6�   �� 5   
   $��1��6   $��0��6   $��
Last Name:��6-   $��20(�6B   $��5,5,5,5q�6P   $��Right=�6a   $��Center=�6�   6�   �� 5   
   $��1��6   $��1��6   $��5,5,5,5q�6/   $��40��6@   $	��300��6L   $
��Left=�6X   ��6s   �� *
LastName+$ TwoWay=�$ PropertyChanged=�6�   �� 5   
   $��2��6   $��0��6   $��Email:��6-   $��20(�6>   $��5,5,5,5q�6L   $��Right=�6]   $��Center=�6�   6�   �� 5   
   $��2��6   $��1��6   $��5,5,5,5q�6/   $��40��6@   $	��300��6L   $
��Left=�6X   ��6s   �� *Email+$ TwoWay=�$ PropertyChanged=�6�   �� 5   
   $��3��6   $��0��6   $��
User Name:��6-   $��20(�6B   $��5,5,5,5q�6P   $��Right=�6a   $��Center=�6�   6�   �� 5   
   $��3��6   $��1��6   $��5,5,5,5q�6/   $��40��6@   $	��300��6L   $
��Left=�6X   ��6s   �� *
UserName+$ TwoWay=�$ PropertyChanged=�6�   �� 5   
   $��4��6   $��0��6   $��	Password:��6-   $��20(�6A   $��5,5,5,5q�6O   $��Right=�6`   $��Center=�6�   6�   �� 5    
   $��4��6   $��1��6   $��5,5,5,5q�6/   $��40��6@   $	��300��6L   $
��Left=�6X   ��6s   �� *
Password+$ TwoWay=�$ PropertyChanged=�6�   � 5!   
   $��5��6   $��1��6   $	��400��6,   $	��100��68   $	��Top=�6E   $
��Left=�6]   $��0,0,0,0q�6x    5"      �� 5#      6%   �� 5$      6%   .+�5'      �� $��0��6   $��Change��6%   $
��Left=�66   $��50��6Q   $	��150��6�   $��5q�6�   y�6]   �� *UserProfileVisibility+��6�   �� *ChangeCommand+6�   �� 5(      $��0��6   $��Register��6%   $
��Left=�68   $��50��6S   $	��150��6�   $��5q�6�   y�6_   �� *RegistrationVisibility+��6�   �� *RegisterCommand+6�   �� 5)      $��1��6   $��Cancel��6%   $
��Left=�66   $��50��6Q   $	��150��6   ��6]   �� *CancelCommand+6   5*      5+      5,      