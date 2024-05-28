USE DBFutebol;
GO

CREATE TABLE Equipe
(
    nome varchar(30) not null,
    apelido varchar(15) not null,
    data_criacao DATE not null,
    CONSTRAINT pkequipe PRIMARY KEY (nome)
);