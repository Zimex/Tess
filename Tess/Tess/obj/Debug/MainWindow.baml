   M S B A M L   `   `   `  ����  Tess    Tess.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  
MainWindow��5   	   $	��766��6   $
��1366��6*   .��5      � -   5         �Name$
 grid��5       � ColumnDefinitions 5   
   �� 5      �� 5      $��0* �6   .+�5
   
   �� $��Center=�6   $
��Auto��62   $��525,10,744,0q�6@   $	��Top=�6V   .+�5      �� -   5         ��Name$ 
loadButton��5      $��
Load image��6)   $��30��6S   $
��Auto��6_   �� 5      -   5        $ startProcessingButton��5      $��Start Processing��64   $��30��6O   {�.  6m   �� 5      -   5        $ startButton��5      $��	Start OCR��6*   $��30��6>   6J   �� 5      -   5        $ sectionButton��5      $��Section��6,   $��30��6>   6J   �� 5      -   5        $ sectionRemoveButton��5      $��Remove section��62   $��30��6K   6W   �� 5      -   5        $ saveTextButton��5      $��	Save Text��6-   $��30��6A   6M   �� 5      -   5        $ clearDictionaryButton��5      $��Clear dictionary��64   $��30                     ��6O   5      �� 5      -	   5        $ showDictionaryButton��5      $��Show dictionary��63   $��30��6M   6Y   5      �� 5   
   -
   5         ��Name$ imageViewer��5      $
��Auto=�6,   $
��Left=�6^   $	��715��6y   $��Bottom=�6�   $	��510��6�   $��	10,0,0,10q�6�   .��5      �� -   5         ��Name$	 img��5      $
��Auto��6:   $
��Auto��6G   6U   5      �� 5   
   -   5         ��Name$ resultTextBox��5      $
��Left=�6*   $	��715��6E   $��
764,10,0,0q�6R    �� TextWrapping$
 Wrap=�6f   $�� ��6z   $	��Top=�6�   $	��584��6�   �� 5   
   -   5        $ processedImageViewer��5      $
��Auto=�65   $��Right=�6g   $	��715��6�   $��	0,10,10,0q�6�   $	��Top=�6�   $	��584��6�   .��5      �� -   5        $ processedImage��5      $
��Auto��6,   $
��Auto��69   5      � 5   
   -   5        $ validationGrid��5      $	��715��6'   $	��586��64   $��Right=�6@   $	��Top=�6\   $��	0,10,10,0q�6t   .+�5      �� -   5        $ saveFirmaEntry��5      $��Zapisz rekord��6-   $��508,263,0,425q�6E   �� 5       -   5        $ validationTextBox��5       $
��Left=�61   $	��154��6L   $
 Wrap=�6Y    �� AcceptsReturn . $�� ��6�   $	��Top=�6�   $	��270��6�   
	 � Tag$		 box��6�   �� 5!      -   5        $��cut all��5!      $
��Left=�6'   $��421,159,0,0q�6B   $	��Top=�6W   $��75��6o   6z   �� 5"      -   5        $��rob sam��5"      $
��Left=�6'   $��421,134,0,0q�6B   $	��Top=�6W   $��75��6o   6z   �� 5#      -   5        $��copy result��5#      $
��Left=�6+   $��501,159,0,0q�6F   $	��Top=�6[   $��75��6s   6~   �� 5$      -   5        
 ��Name$
 parametersListBox��5$      $
��Left=�61    �� SelectionMode$ Multiple=�6L   $	��Top=�6e   $	��130��6}   $	��264��6�   $��	275,0,0,0q�6�   .@�5%      �� .��6   NIP6$   �� 5&      .��6   kod pocztowy6-   �� 5'      .��6   strona6'   �� 5(      .��6   nazwa6&   �� 5)      .��6   miejscowosc6,   �� 5*      .��6   numer ulicy6,   �� 5+      .��6   adres email6,   �� 5,      .��6   ulica6&   �� 5-      .��6   numer klienta6.   �� 5.      .��6   numer konta6,   �� 5/      .��6   data6%   �� 50      .��6   	telefon6(   �� 51      .��6   numer faktury6.   �� 52      .��6   sposob platnosci61   53      � 54      -   5        $ fakturaGrid��54      $
��Left=�6(   $	��300��6C   $��0q�6P   $��Bottom=�6[   $	��240��6v    � RowDefinitions 55      �� 56      $��30 �6$   �� 57      $��30 �6$   �� 58      $��30 �6$   �� 59      $��30 �6$   �� 5;      $��30 �6$   �� 5<      $��30 �6$   �� 5=      $��30 �6$   �� 5>      $��30 �6$   �� 5?      $��30 �6$   �� 5@      $��30 �6$   �� 5A      $��30 �6$    5D      �� 5E      $
��Auto �6'   �� 5F      $��* �6'   �� 5G      $��40 �6'   .+�5I      �� $��numer:��6   $
��Left=�6)   $��0,0,0,0q�6D   $	��Top=�6U   $��0��6m   $
��Auto��6z   �� 5J      $��
zaplacono:��6   $
��Left=�6-   $��0,0,0,0q�6H   $	��Top=�6Y   $��1��6q   $
��Auto��6~   $	 	zaplacono��6�   �� 5K      $��REGON:��6   $
��Left=�6)   $��0,0,0,0q�6D   $	��Top=�6U   $��2��6m   $
��Auto��6z   �� 5L      $��data sprz/wyk:��6   $
��Left=�61   $��0,0,0,0q�6L   $	��Top=�6]   $��3��6u   $
��Auto��6�   �� 5M      $��
data wyst:��6   $
��Left=�6-   $��0,0,0,0q�6H   $	��Top=�6Y   $��4��6q   $
��Auto��6~   �� 5N      $��kwota:��6   $
��Left=�6)   $��0,0,0,0q�6D   $	��Top=�6U   $��5��6m   $
��Auto��6z   $	 razem do zaplaty��6�   �� 5O      $��sposób płatności:��6   $
��Left=�64   $��0,0,0,0q�6O   $	��Top=�6`   $��6��6x   $
��Auto��6�   $	 sposob platnosci��6�   �� 5P      $��termin zaplaty:��6   $
��Left=�62   $��0,0,0,0q�6M   $	��Top=�6^   $��8��6v   $
��Auto��6�   $	 termin zaplaty��6�   �� 5Q      $��netto:��6   $
��Left=�6)   $��0,0,0,0q�6D   $	��Top=�6U   $��7��6m   $
��Auto��6z   �� 5R      $��faks:��6   $
��Left=�6(   $��0,0,0,0q�6C   $	��Top=�6T   $��9��6l   $
��Auto��6y   �� 5T      -   5        $��Zapisz��5T      $��0��6+   $��2��68   ��6a   ��  �� Path$ ID��� 5U      -   5        $��Zapisz��5U      $��1��6+   $��2��68   $
��Left=�6a   $��40��6|   �� 5V      -   5        $��Zapisz��5V      $��2��6+   $��2��68   6H   �� 5W      -   5        $��Zapisz��5W      $��3��6+   $��2��68   6H   �� 5X      -   5        $��Zapisz��5X      $��4��6+   $��2��68   6H   �� 5Y      -   5        $��Zapisz��5Y      $��5��6+   $��2��68   6H   �� 5Z      -   5        $��Zapisz��5Z      $��6��6+   $��2��68   ��6a   �� $ ID��� 5[      -   5        $��Zapisz��5[      $��7��6+   $��2��68   6H   �� 5\      -   5        $��Zapisz��5\      $��8��6+   $��2��68   6H   �� 5]      -    5        $��Zapisz��5]      $��9��6+   $��2��68   6H   �� 5_      -!   5        $ numerFakturaTextBox��5_      $�� ��67   $��0��6?   $��1��6L   $	 numer faktury��6\   �� 5`      -"   5        $ sprzedawcaFakturaTextBox��5`      $�� ��6<   $��1��6D   $��1��6Q   $	 	zaplacono��6a   �� 5a      -#   5        $ REGONTextBox��5a      $�� ��60   $��2��68   $��1��6E   $	 REGON��6U   �� 5b      -$   5        $ dataSprzedazyTextBox��5b      $�� ��68   $��3��6@   $��1��6M   $	 data sprzedazy��6]   �� 5c      -%   5        $# dataWystawieniaFakturaTextBox��5c      $�� ��6A   $��4��6I   $��1��6V   $	 data wystawienia��6f   �� 5d      -&   5        $ kwotaFakturaTextBox��5d      $�� ��67   $��5��6?   $��1��6L   $	 
do zaplaty��6\   �� 5e      -'   5        $# sposobPlatnosciFakturaTextBox��5e      $�� ��6A   $��6��6I   $��1��6V   $	 sposob platnosci��6f   �� 5f      -(   5        $ nettoFakturaTextBox��5f      $�� ��67   $��7��6?   $��1��6L   $	 netto faktura��6\   �� 5g      -)   5        $! terminZaplatyFakturaTextBox��5g      $�� ��6?   $��8��6G   $��1��6T   $	 termin zaplaty��6d   �� 5h      -*   5        $ faksTextBox��5h      $�� ��6/   $��9��67   $��1��6D   $
	 faks��6U   5k      � 5l      -+   5        $ 	towarGrid��5l      $
��Left=�6&   $	��180��6A   $��	0,0,0,330q�6N   $��Bottom=�6a   $	��240��6|    5m      �� 5n      $��30 �6$   �� 5o      $��30 �6$   �� 5p      $��30 �6$   �� 5q      $��30 �6$   �� 5r      $��30 �6$   �� 5s      $��30 �6$    5v      �� 5w      $
��Auto �6'   �� 5x      $��* �6'   �� 5y      $��40 �6'   .+�5{      �� $��nazwa banku:��6   $
��Left=�6/   $��0,0,0,0q�6J   $	��Top=�6[   $��0��6s   $
��Auto��6�   �� 5|      $��nazwa:��6   $
��Left=�6)   $��0,0,0,0q�6D   $	��Top=�6U   $��1��6m   $
��Auto��6z   �� 5}      $��ilosc:��6   $
��Left=�6)   $��0,0,0,0q�6D   $	��Top=�6U   $��2��6m   $
��Auto��6z   �� 5~      $��netto:��6   $
��Left=�6)   $��0,0,0,0q�6D   $	��Top=�6U   $��3��6m   $
��Auto��6z   �� 5      $��brutto:��6   $
��Left=�6*   $��0,0,0,0q�6E   $	��Top=�6V   $��4��6n   $
��Auto��6{   �� 5�      $
��VAT:��6   $
��Left=�6'   $��0,0,0,0q�6B   $	��Top=�6S   $��5��6k   $
��Auto��6x   �� 5�      -,   5        $��Zapisz��5�      $��0��6+   $��2��68   ��6a   �� $ ID��� 5�      --   5        $��Zapisz��5�      $��1��6+   $��2��68   $
��Left=�6a   $��40��6|   �� 5�      -.   5        $��Zapisz��5�      $��2��6+   $��2��68   6H   �� 5�      -/   5        $��Zapisz��5�      $��3��6+   $��2��68   6H   �� 5�      -0   5        $��Zapisz��5�      $��4��6+   $��2��68   6H   �� 5�      -1   5        $��Zapisz��5�      $��5��6+   $��2��68   6H   �� 5�      -2   5        $ nazwaBankuTextBox��5�      $�� ��65   $��0��6=   $��1��6J   $	 nazwa banku��6Z   �� 5�      -3   5        $ nazwaTowarFakturaTextBox��5�      $�� ��6<   $��1��6D   $��1��6Q   $	 nazwa towaru��6a   �� 5�      -4   5        $ iloscTowarTextBox��5�      $�� ��65   $��2��6=   $��1��6J   $	 ilosc towaru��6Z   �� 5�      -5   5        $ nettoTowarTextBox��5�      $�� ��65   $��3��6=   $��1��6J   $	 netto towaru��6Z   �� 5�      -6   5        $ bruttoTowarTextBox��5�      $�� ��66   $��4��6>   $��1��6K   $	 brutto towaru��6[   �� 5�      -7   5        $ 
VATTextBox��5�      $�� ��6.   $��5��66   $��1��6C   $		 VAT��6S   5�      � 5�      -8   5        $ 	firmaGrid��5�      $��Right=�6&   $	��420��6B   $��0q�6O   $��Bottom=�6Z   $	��300��6u    5�      �� 5�      $��30 �6$   �� 5�      $��30 �6$   �� 5�      $��30 �6$   �� 5�      $��30 �6$   �� 5�      $��30 �6$   �� 5�      $��30 �6$   �� 5�      $��30 �6$   �� 5�      $��30 �6$   �� 5�      $��30 �6$   �� 5�      $��30 �6$   �� 5�      $��30 �6$   �� 5�      $��30 �6$   �� 5�      $��30 �6$   �� 5�      $��30 �6$    5�      �� 5�      $
��Auto �6'   �� 5�      $��* �6'   �� 5�      $��40 �6'   .+�5�      �� $��nazwa:��6   $
��Left=�6)   $��0,0,0,0q�6D   $	��Top=�6U   $��0��6m   $
��Auto��6z   �� 5�      $��miejscowość:��6   $
��Left=�6/   $��0,0,0,0q�6J   $	��Top=�6[   $��1��6s   $
��Auto��6�   �� 5�      $��kod pocztowy:��6   $
��Left=�60   $��0,0,0,0q�6K   $	��Top=�6\   $��2��6t   $
��Auto��6�   �� 5�      $��ulica:��6   $
��Left=�6)   $��0,0,0,0q�6D   $	��Top=�6U   $��3��6m   $
��Auto��6z   �� 5�      $��numer:��6   $
��Left=�6)   $��0,0,0,0q�6D   $	��Top=�6U   $��4��6m   $
��Auto��6z   �� 5�      $��e-mail:��6   $
��Left=�6*   $��0,0,0,0q�6E   $	��Top=�6V   $��5��6n   $
��Auto��6{   �� 5�      $��strona:��6   $
��Left=�6*   $��0,0,0,0q�6E   $	��Top=�6V   $��6��6n   $
��Auto��6{   �� 5�      $
��NIP:��6   $
��Left=�6'   $��0,0,0,0q�6B   $	��Top=�6S   $��7��6k   $
��Auto��6x   �� 5�      $��Numer klienta:��6   $
��Left=�61   $��0,0,0,0q�6L   $	��Top=�6]   $��8��6u   $
��Auto��6�   �� 5�      $��Numer konta:��6   $
��Left=�6/   $��0,0,0,0q�6J   $	��Top=�6[   $��9��6s   $
��Auto��6�   �� 5�      $��data1:��6   $
��Left=�6)   $��0,0,0,0q�6D   $	��Top=�6U   $��10��6m   $
��Auto��6{   �� 5�      $��data2:��6   $
��Left=�6)   $��0,0,0,0q�6D   $	��Top=�6U   $��11��6m   $
��Auto��6{   �� 5�      $��	telefon1:��6   $
��Left=�6,   $��0,0,0,0q�6G   $	��Top=�6X   $��12��6p   $
��Auto��6~   �� 5�      $��	telefon2:��6   $
��Left=�6,   $��0,0,0,0q�6G   $	��Top=�6X   $��13��6p   $
��Auto��6~   �� 5�      -9   5        $ saveNazwaButton��5�      $��Zapisz��62   $��0��6C   $��2��6P   $��30��6�   $	��Top=�6�   ��6y   �� $ ID�6�   �� 5�      -:   5        $ saveMiejscowoscButton��5�      $��Zapisz��68   $��1��6I   $��2��6V   6f   �� 5�      -;   5        $ saveKodButton��5�      $��Zapisz��60   $��2��6A   $��2��6N   6^   �� 5�      -<   5        $ saveulicaButton��5�      $��Zapisz��62   $��3��6C   $��2��6P   6`   �� 5�      -=   5        $ saveNumerButton��5�      $��Zapisz��62   $��4��6C   $��2��6P   6`   �� 5�      ->   5        $ saveEmailButton��5�      $��Zapisz��62   $��5��6C   $��2��6P   ��6y   �� $ ID��� 5�      -?   5        $ saveStronaButton��5�      $��Zapisz��63   $��6��6D   $��2��6Q   6a   �� 5�      -@   5        $ saveNIPButton��5�      $��Zapisz��60   $��7��6A   $��2��6N   6^   �� 5�      -A   5        $ saveNumerKlientaButton��5�      $��Zapisz��69   $��8��6J   $��2��6W   6g   �� 5�      -B   5        $ saveNumerKontaButton��5�      $��Zapisz��67   $��9��6H   $��2��6U   6e   �� 5�      -C   5        $ saveData1Button��5�      $��Zapisz��62   $��10��6C   $��2��6Q   6a   �� 5�      -D   5        $ saveData2Button��5�      $��Zapisz��62   $��11��6C   $��2��6Q   6a   �� 5�      -E   5        $ saveTelefon1Button��5�      $��Zapisz��65   $��12��6F   $��2��6T   6d   �� 5�      -F   5        $ saveTelefon2Button��5�      $��Zapisz��65   $��13��6F   $��2��6T   6d   �� 5�      -G   5        $ nazwaTextBox��5�      $�� ��60   $��0��68   $��1��6E   $	 nazwa��6U   �� 5�      -H   5        $ miejscowoscTextBox��5�      $�� ��66   $��1��6>   $��1��6K   $	 miejscowosc��6[   �� 5�      -I   5        $ kodPocztowyTextBox��5�      $�� ��66   $��2��6>   $��1��6K   $	 kod pocztowy��6[   �� 5�      -J   5        $ ulicaTextBox��5�      $�� ��60   $��3��68   $��1��6E   $	 ulica��6U   �� 5�      -K   5        $ numerTextBox��5�      $�� ��60   $��4��68   $��1��6E   $	 numer ulicy��6U   �� 5�      -L   5        $ emailTextBox��5�      $�� ��60   $��5��68   $��1��6E   $	 adres email��6U   �� 5�      -M   5        $ stronaTextBox��5�      $�� ��61   $��6��69   $��1��6F   $	 strona��6V   �� 5�      -N   5        $ 
NIPTextBox��5�      $�� ��6.   $��7��66   $��1��6C   $		 NIP��6S   �� 5�      -O   5        $ numerKlientaTextBox��5�      $�� ��67   $��8��6?   $��1��6L   $	 numer klienta��6\   �� 5�      -P   5        $ numerKontaTextBox��5�      $�� ��65   $��9��6=   $��1��6J   $	 numer konta��6Z   �� 5�      -Q   5        $ data1TextBox��5�      $�� ��60   $��10��68   $��1��6F   $	 data1��6V   �� 5�      -R   5        $ data2TextBox��5�      $�� ��60   $��11��68   $��1��6F   $	 data2��6V   �� 5�      -S   5        $ telefon1TextBox��5�      $�� ��63   $��12��6;   $��1��6I   $	 telefon1��6Y   �� 5�      -T   5        $ telefon2TextBox��5�      $�� ��63   $��13��6;   $��1��6I   $	 telefon2��6Y   5�      �� 5�      $��Towar:��6   $
��Left=�6%   $��
10,172,0,0q�6@   $	��Top=�6T   $��62��6l   $��26��6w   $��14(�6�   �� 5�      $��Faktura:��6   $
��Left=�6'   $��
10,384,0,0q�6B   $	��Top=�6V   $��62��6n   $��26��6y   $��14(�6�   �� 5�      $��Firma:��6   $
��Left=�6%   $��343,264,0,0q�6@   $	��Top=�6U   $��62��6m   $��26��6x   $��14(�6�   �� 5�      -U   5        $ saveTowarEntry��5�      $��Zapisz rekord��6-   $��192,165,316,523q�6E   �� 5�      -V   5        $ SaveFakturaEntry��5�      $��Zapisz rekord��6/   $��192,383,316,305q�6G   �� 5�      -W   5        $��	parametry��5�      $
��Left=�6)   $��421,184,0,0q�6D   $	��Top=�6Y   $��75��6q   6|   �� 5�      -X   5        $��sprz/nab��5�      $
��Left=�6(   $��421,209,0,0q�6C   $	��Top=�6X   $��75��6p   6{   �� 5�      -Y   5        $��naglowek��5�      $
��Left=�6(   $��421,234,0,0q�6C   $	��Top=�6X   $��75��6p   6{   �� 5�      -Z   5        $ wynikiButton��5�      $��wyniki��6+   $
��Left=�6<   $��421,109,0,0q�6W   $	��Top=�6l   $��75��6�   6�   5�      �� 5�   
   $��GroupBox��6   $
��Left=�6%   $��
617,10,0,0q�6@   $	��Top=�6T   $��79��6l   $	��140��6x    J� RenderTransformOrigin$ -0.057,-0.275&�6�   .��5�      �� .+�5�      	� -[   5         	�Name$ radioOCR��5�       d� 	IsChecked$
 trueY�60   .��6\   OCR6a   	� 5�      -\   5        $ radioProcessing��5�      .��6Y   Image Processing6k   	� 5�      -]   5        $ radioValidation��5�      .��6\   
Validation6h   5�      5�      �� 5�   
   $��Image Processing��6   $��614,94,604,485q�6-   $	��140��6E   .��5�      �� .+�5�      	� $
��None��6   $
 TrueY�6-   	� 5�      -^   5        $ imageProcessingSharpening��5�      $��
Sharpening��6A   	� 5�      -_   5        $2 ,imageProcessingIncreaseResolutionRadioButton��5�      $��Increase resolution��6T   	� 5�      -`   5        $# imageProcessingChangeContrast��5�      $��Change Contrast��6E   5�      5�      �� 5�   
   $�� ��6   $
��Left=�6   $��525,699,0,0q�66   $	��Top=�6K   $	��232��6c   �� 5�   
   -a   5        $��Button��5�      $
��Left=�6"   $��617,274,0,0q�6=   $	��Top=�6R   $��75��6j   6u   �� 5�   
   -b   5        $ buildRegexTextBox��5�      $
��Left=�6-   $��57��6H   $��573,314,0,0q�6T   $
 Wrap=�6i   $��TextBox��6}   $	��Top=�6�   $	��163��6�   �� 5�   
   -c   5        $��Buduj wyrazenie��5�      $
��Left=�6+   $��602,387,0,0q�6F   $	��Top=�6[   $	��134��6s   6   �� 5�   
   -d   5        $ sprawdzanieWyrazenTextBox��5�      $
��Left=�65   $	��114��6P   $��573,492,0,0q�6]   $
 Wrap=�6r   $��Płatniości��6�   $	��Top=�6�   $	��163��6�   �� 5�   
   -e   5        $ wyrazenieTextBox��5�      $
��Left=�6,   $��60��6G   $��525,611,0,0q�6S   $
 Wrap=�6h   $��TextBox��6|   $	��Top=�6�   $	��237��6�   �� 5�   
   -f   5        $ szukaj��5�      $��szukaj��6!   $
��Left=�62   $��637,676,0,0q�6M   $	��Top=�6b   $��75��6z   6�   5�      5�      