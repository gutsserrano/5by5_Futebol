CREATE TABLE Campeonato
(
    nome_camp varchar(20) not null,
    temporada varchar(10) not null,
    status_camp varchar(12),
    CONSTRAINT pkcamp PRIMARY KEY (nome_camp, temporada)
);