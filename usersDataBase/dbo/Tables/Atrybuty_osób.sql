-- Tworzenie tabeli Atrybuty_osób
CREATE TABLE Atrybuty_osób (
  Id INT PRIMARY KEY IDENTITY(1,1),
  Osoba_id INT NOT NULL,
  Atrybut NVARCHAR(255) NOT NULL,
  Wartosc NVARCHAR(255) NOT NULL,
  FOREIGN KEY (Osoba_id) REFERENCES Osoby(Id)
);