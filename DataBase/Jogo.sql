CREATE TABLE Jogo
(
    campeonato varchar(20) not null,
    temp varchar(10) not null,
    mandante varchar(30) not null,
    visitante varchar(30) not null,
    gols_mandante int,
    gols_visitante int,
    CONSTRAINT pkjogo PRIMARY KEY (campeonato, temp, mandante, visitante),
    CONSTRAINT fkcamptempjogo FOREIGN KEY (campeonato, temp) REFERENCES Campeonato(nome_camp, temporada),
    CONSTRAINT fkmandantejogo FOREIGN KEY (mandante) REFERENCES Equipe(nome),
    CONSTRAINT fkvisitantejogo FOREIGN KEY (visitante) REFERENCES Equipe(nome)
);