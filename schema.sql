﻿-- Tabela osób
CREATE TABLE Osoby (
  Id INT PRIMARY KEY IDENTITY(1,1),
  Imie NVARCHAR(255) NOT NULL,
  Nazwisko NVARCHAR(255) NOT NULL,
  Data_urodzenia DATE NOT NULL,
  Plec NVARCHAR(255) NOT NULL
);

-- Tabela atrybutów
CREATE TABLE Atrybuty_Osób (
  Id INT PRIMARY KEY IDENTITY(1,1),
  Osoba_id INT NOT NULL,
  Atrybut NVARCHAR(255) NOT NULL,
  Wartosc NVARCHAR(255) NOT NULL,
  FOREIGN KEY (Osoba_id) REFERENCES Osoby(Id)
);
