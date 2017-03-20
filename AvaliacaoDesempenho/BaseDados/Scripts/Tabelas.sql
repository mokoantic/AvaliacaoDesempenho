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

INSERT INTO Utilizador(Uti_Gru_Id, Uti_Login, Uti_Password, Uti_Email, Uti_EmailConfirmado) VALUES (2, 'admin', '7C-87-54-1F-D3-F3-EF-50-16-E1-2D-41-19-00-C8-7A-60-46-A8-E8', 'admin@email.com', 1)

-------------------------------------
--       Tabela Quadrante       
-----------------------------------------

CREATE TABLE Quadrante
(
	Qua_Id integer identity(1,1) PRIMARY KEY,
	Qua_Descricao nvarchar(300) not null,
	Qua_Peso integer not null
);

-------------------------------------
--       Tabela Classificacao       
-----------------------------------------

CREATE TABLE Classificacao
(
	Cla_Id integer identity(1,1) PRIMARY KEY,
	Cla_Qua_Id integer not null,
	FOREIGN KEY (Cla_Qua_Id) REFERENCES Quadrante (Qua_Id)
	on delete cascade
	on update cascade,
	Cla_Descricao nvarchar(300) not null,
	Cla_Cotacao integer not null,
);

-------------------------------------
--       Tabela Questao       
-----------------------------------------

CREATE TABLE Questao
(
	Que_Id integer identity(1,1) PRIMARY KEY,
	Que_Qua_Id integer not null,
	FOREIGN KEY (Que_Qua_Id) REFERENCES Quadrante (Qua_Id)
	on delete cascade
	on update cascade,
	Que_Descricao nvarchar(300) not null,
	Que_Peso integer not null,
);

-------------------------------------
--       Tabela Avaliacao       
-----------------------------------------

CREATE TABLE Avaliacao
(
	Ava_Id integer identity(1,1) PRIMARY KEY,
	Ava_Uti_Id integer not null,
	FOREIGN KEY (Ava_Uti_Id) REFERENCES Utilizador (Uti_Id)
	on delete cascade
	on update cascade,
	Ava_Peso_Quadrante_Um integer not null,
	Ava_Peso_Quadrante_Dois integer not null,
	Ava_Peso_Quadrante_Tres integer not null,
	Ava_Peso_Quadrante_Quatro integer not null,
	Ava_Classificacao_Um integer not null,
	Ava_Classificacao_Dois integer not null,
	Ava_Classificacao_Tres integer not null,
	Ava_Classificacao_Quatro integer not null,
	Ava_Classificacao_Final integer not null
);

-------------------------------------
--       Tabela LinhaAvaliacao       
-----------------------------------------

CREATE TABLE LinhaAvaliacao
(
	LAva_Id integer identity(1,1) PRIMARY KEY,
	LAva_Ava_Id integer not null,
	FOREIGN KEY (LAva_Ava_Id) REFERENCES Avaliacao (Ava_Id)
	on delete cascade
	on update cascade,
	LAva_Qua_Id integer not null,
	FOREIGN KEY (LAva_Qua_Id) REFERENCES Quadrante (Qua_Id)
	on delete cascade
	on update cascade,
	LAva_PesoQuadrante integer not null,
	LAva_Questao nvarchar(300) not null,
	LAva_PesoQuestao integer not null,
	LAva_Classificacao integer not null,
	LAva_Pontos integer not null
);