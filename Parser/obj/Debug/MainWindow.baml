   M S B A M L   `   `   `  ����A  =Parser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:ParserParser  5   	   X TXceed.Wpf.Toolkit, Version=4.0.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f4P:clr-namespace:Xceed.Wpf.Toolkit;assembly=Xceed.Wpf.ToolkitXceed.Wpf.Toolkit 5   	       Parser.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   &localclr-namespace:Parser     5   	   NwpfTool:clr-namespace:Xceed.Wpf.Toolkit;assembly=Xceed.Wpf.Toolkit     5   	     � Title$  
MainWindow��5
   	   $	��450��6   $	��800��6)   .��5      �  � RowDefinitions 5   
   �� 5      $	��90* �6   6+   �� 5      $	��10* �6   6+    � ColumnDefinitions 5   
   �� 5      $	��60* �6   $	��480��6+   6<   �� 5      $	��40* �6   6-   .+�5   
   &   System.Windows.Controls.DataGrid  $��0��6   $��0��6    $��	10,10,0,0q�5         CanUserAddRows .     AutoGenerateColumns .   �� SelectedItem 5   0   �� *SelectedItem+��6X   �� *News+   Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ Название��6%   1  +System.Windows.Controls.DataGridBoundColumn   Binding 67   �� 	 �� Path$	 Header�5      #  Xceed.Wpf.Toolkit.RichTextBox  5   
   $��0��6   $��1��6+   $��2��6;   $��0,0,0,0q�6f   
   Text
 6L   �� *
NewsView+6f   ]� 5   
   ��. $��1��6)   $��0��66   .+�5       �� $
��Left=�6   $
��Left=�6+   $��Загрузить��5!   4   $��20��5"      $��70��6!   $��10 10 10 10q�6,   ��5!      �� *
LoadButton+5"   ,   �� 5#      $
��Left=�6   $��
5,10,10,10q�6,   $
��Left=�6@   $��20��5$       �� TextWrapping$
 Wrap=�6   $	��240��6E   ��6-   �� *Search+6E   �� 5%      $
��Left=�6   $��
Поиск��5&   2   $��
5,10,10,10q�6B   $��50��6V   $��20��6a   ��6   �� *SearchButton+6a   �� 5'      $
��Left=�6   $��
Сброс��5(   1   $��
5,10,10,10q�6A   $��50��6U   $��20��6`   ��6   �� *ResetButton+6`   5*      5-      5.      