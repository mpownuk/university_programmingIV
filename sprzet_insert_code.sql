--insert into sprzêt (Nazwa, Opis, StanMagazynowy, Zdjecie) values('K¹tówka', 'urz¹dzenie do ciêcia, szlifowania', 3, 
--(select BulkColumn from openrowset(bulk 'C:\Users\Laptop\Desktop\STUDIA\SEMESTR 4\PROGRAMOWANIE IV\RENT_A_TOOL\rent_a_tool_images\Angle-Grinder.jpg', single_blob) as zdjecie));

--insert into sprzêt (nazwa, opis, stanmagazynowy, zdjecie) values ('pi³a ³añcuchowa', 'ciêcie drewna, œcinanie drzew, pielêgnacja ogrodu', 2, 
--(select bulkcolumn from openrowset(bulk 'C:\Users\Laptop\Desktop\STUDIA\SEMESTR 4\PROGRAMOWANIE IV\RENT_A_TOOL\rent_a_tool_images\Chainsaw.jpg', single_blob) as zdjecie));
select * from sprzêt;