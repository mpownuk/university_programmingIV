--insert into sprz�t (Nazwa, Opis, StanMagazynowy, Zdjecie) values('K�t�wka', 'urz�dzenie do ci�cia, szlifowania', 3, 
--(select BulkColumn from openrowset(bulk 'C:\Users\Laptop\Desktop\STUDIA\SEMESTR 4\PROGRAMOWANIE IV\RENT_A_TOOL\rent_a_tool_images\Angle-Grinder.jpg', single_blob) as zdjecie));

--insert into sprz�t (nazwa, opis, stanmagazynowy, zdjecie) values ('pi�a �a�cuchowa', 'ci�cie drewna, �cinanie drzew, piel�gnacja ogrodu', 2, 
--(select bulkcolumn from openrowset(bulk 'C:\Users\Laptop\Desktop\STUDIA\SEMESTR 4\PROGRAMOWANIE IV\RENT_A_TOOL\rent_a_tool_images\Chainsaw.jpg', single_blob) as zdjecie));
select * from sprz�t;