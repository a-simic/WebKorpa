CREATE DATABASE WebKorpa
GO
USE WebKorpa
GO

-- Moze i tabela AspNetUsers
CREATE TABLE Kupac(
KupacId nvarchar(450) NOT NULL PRIMARY KEY,
Ime nvarchar(30) NOT NULL,
Prezime nvarchar(30) NOT NULL,
Drzava nvarchar(30) NOT NULL DEFAULT 'Srbija',
Grad nvarchar(30) NOT NULL DEFAULT 'Beograd',
Adresa nvarchar(100) NOT NULL
);


INSERT INTO Kupac(KupacId, Ime, Prezime, Adresa) VALUES('Kupac001','Mika','Mikic','Glavna 5');
INSERT INTO Kupac(KupacId, Ime, Prezime, Adresa) VALUES('Kupac002','Laza','Lazic','Prvomajska 8');

--SELECT * FROM Kupac;

CREATE TABLE Porudzbina(
	PorudzbinaId int IDENTITY(1,1) PRIMARY KEY,
	KupacId nvarchar(450) NOT NULL FOREIGN KEY REFERENCES Kupac(KupacId),
	DatumKupovine  datetime NOT NULL DEFAULT GETDATE()
 );


 INSERT INTO Porudzbina(KupacId) VALUES('Kupac001');
 INSERT INTO Porudzbina(KupacId) VALUES('Kupac002');

--SELECT * FROM Porudzbina;

CREATE TABLE Kategorija(
KategorijaId int IDENTITY(1,1) PRIMARY KEY,
Naziv nvarchar(100) NOT NULL
);

SET IDENTITY_INSERT Kategorija ON
INSERT INTO Kategorija(KategorijaId, Naziv) VALUES(1,'Vodeni sportovi')
INSERT INTO Kategorija(KategorijaId, Naziv) VALUES(2,'Fudbal')
INSERT INTO Kategorija(KategorijaId, Naziv) VALUES(3,'Sah')
SET IDENTITY_INSERT Kategorija OFF


--SELECT * FROM Kategorija

CREATE TABLE Proizvod(
	ProizvodId int IDENTITY(1,1) PRIMARY KEY,
	KategorijaId int FOREIGN KEY REFERENCES Kategorija(KategorijaId) NOT NULL,
	Naziv nvarchar(100) NOT NULL,	
	Cena decimal(10, 2) NOT NULL,
	Opis nvarchar(100) NULL
);

INSERT INTO Proizvod (Cena, KategorijaId, Naziv, Opis) VALUES (CAST(34275.00 AS Decimal(10, 2)), 1, 'Kajak', 'Camac za jednu osobu')
INSERT INTO Proizvod (Cena, KategorijaId, Naziv, Opis) VALUES (CAST(2348.95 AS Decimal(10, 2)), 1, 'Pojas za spasavanje', 'Pojas za zastitu na vodi')
INSERT INTO Proizvod (Cena, KategorijaId, Naziv, Opis) VALUES (CAST(3459.50 AS Decimal(10, 2)), 2, 'Fudbalska lopta', 'FIFA-oficijelna tezina i dimenzije')
INSERT INTO Proizvod (Cena, KategorijaId, Naziv, Opis) VALUES (CAST(1234.95 AS Decimal(10, 2)), 2, 'Korner zastava', 'Zastava za obelezavanje kornera')
INSERT INTO Proizvod (Cena, KategorijaId, Naziv, Opis) VALUES (CAST(4795.00 AS Decimal(10, 2)), 2, 'Dres', 'Teget beli dres sa oznakom tima')
INSERT INTO Proizvod (Cena, KategorijaId, Naziv, Opis) VALUES (CAST(1687.00 AS Decimal(10, 2)), 3, 'Sat za merenje vremena', 'Meri vreme izmedju poteza')
INSERT INTO Proizvod (Cena, KategorijaId, Naziv, Opis) VALUES (CAST(2934.95 AS Decimal(10, 2)), 3, 'Stolica za sah', 'Udobna stolica za sah')
INSERT INTO Proizvod (Cena, KategorijaId, Naziv, Opis) VALUES (CAST(1867.00 AS Decimal(10, 2)), 3, 'Sahovska tabla', ' Sahovska tabla za celu porodicu')
INSERT INTO Proizvod (Cena, KategorijaId, Naziv, Opis) VALUES (CAST(1245.00 AS Decimal(10, 2)), 3, 'Pozlaceni kralj', 'Pozlacena figura kralj')
	
 --SELECT * FROM Proizvod;

CREATE TABLE Stavka(
	StavkaId int IDENTITY(1,1) NOT NULL PRIMARY KEY,
	PorudzbinaId int NOT NULL FOREIGN KEY REFERENCES Porudzbina(Porudzbinaid) ,
	ProizvodId int NOT NULL FOREIGN KEY REFERENCES Proizvod(Proizvodid),
	Kolicina int NOT NULL,
	CONSTRAINT UQ_Stavka UNIQUE(PorudzbinaId,ProizvodId)
	);
	



INSERT Stavka ( Kolicina, PorudzbinaId, ProizvodId) VALUES ( 2, 1, 1)
INSERT Stavka ( Kolicina, PorudzbinaId, ProizvodId) VALUES ( 1, 1, 2)

INSERT Stavka ( Kolicina, PorudzbinaId, ProizvodId) VALUES ( 1, 2, 2)
INSERT Stavka ( Kolicina, PorudzbinaId, ProizvodId) VALUES ( 1, 2, 3)


--SELECT * FROM Stavka