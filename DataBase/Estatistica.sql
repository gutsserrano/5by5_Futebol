CREATE TABLE Estatistica
(
    nome_camp varchar(20) not null,
    temporada varchar(10) not null,
    nome_equipe varchar(30) not null,
    gols_marcados int,
    gols_sofridos int,
    pontuacao int,
    CONSTRAINT pkestatistica PRIMARY KEY (nome_camp, temporada, nome_equipe),
    CONSTRAINT fkcampestatistica FOREIGN KEY (nome_camp, temporada) REFERENCES Campeonato(nome_camp, temporada),
    CONSTRAINT fkequipeestatistica FOREIGN KEY (nome_equipe) REFERENCES Equipe(nome)
);