﻿-- Tworzenie tabeli Osoby
CREATE TABLE Osoby (
  Id INT PRIMARY KEY IDENTITY(1,1),
  Imie NVARCHAR(255) NOT NULL,
  Nazwisko NVARCHAR(255) NOT NULL,
  Data_urodzenia DATE NOT NULL,
  Plec NVARCHAR(255) NOT NULL
);