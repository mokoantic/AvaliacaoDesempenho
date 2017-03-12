-------------------------------------
--       Roles and Users Tables       
-----------------------------------------

-- Roles
CREATE TABLE Grupo
(
	Gru_Id int identity(1,1) PRIMARY KEY,
	Gru_Descricao nvarchar(300) not null,
	Gru_Ativo bit default 1 not null
);

INSERT INTO Grupo(Gru_Descricao,Gru_Ativo)
     VALUES('System',1)

INSERT INTO Grupo(Gru_Descricao,Gru_Ativo)
     VALUES('Administradores',1)

-- User
CREATE TABLE Utilizador(
	Uti_ID integer identity(1,1) PRIMARY KEY,
	Uti_Gru_Id integer NOT NULL,
	FOREIGN KEY (Uti_Gru_Id) REFERENCES Grupo (Gru_Id)
	on delete cascade
	on update cascade,
	Uti_Login VARCHAR(50) NOT NULL,
	Uti_Password VARCHAR(300) NOT NULL,
	Uti_PrimeiroNome VARCHAR(50),
	Uti_UltimoNome VARCHAR(50),
	Uti_PastaFicheiro VARCHAR(250),
	Uti_NomeFicheiro VARCHAR(250),
	Uti_Email VARCHAR(250) NOT NULL,
	Uti_EmailConfirmado bit,
	Uti_Activo bit default 1 NOT NULL
);