   M S B A M L   `   `   `  ����E  Ahalal_bead, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:halal_bead
halal_bead  5   	   =#clr-namespace:halal_bead.ViewModelshalal_bead.ViewModels  5   	   A%clr-namespace:halal_bead.UserControlshalal_bead.UserControls  5   	       halal_bead.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:halal_bead     5   	   :
viewmodels#clr-namespace:halal_bead.ViewModels     5   	   >usercontrols%clr-namespace:halal_bead.UserControls     5   	     � Title$  
MainWindow��5
   	   $	��800��6   $
��1600��6)    � 	Resources 5      )   #halal_bead.ViewModels.MainViewModel   VM��%   &	          5   
   6#   5      .��5      �  � DataContext# [  6    � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   .+�5   
   3   -halal_bead.UserControls.WorkAssignmentControl  -   5          Name$ workAssignControl��5   -   $��0��6q    6H   �� *WorkAssignmentVM+6q   -   'halal_bead.UserControls.FunctionControl  5   
   -   5          Name$ functionControl��5   '   $��1��6c    6@   �� *
FunctionVM+6c   3   -halal_bead.UserControls.TravelingAgentControl  5   
   -   5          Name$ travelingAgentControl��5   -   $��2��6u    6L   �� *TravelingAgentVM+6u   5      5      