CREATE OR ALTER PROC Ver_tabela
    @Campeonato varchar(20),
    @Temporada varchar(10)
AS
BEGIN
    SELECT nome_equipe AS Equipe, gols_marcados AS Gols_marcados, gols_sofridos AS Gols_sofridos, pontuacao AS Pontuacao
    FROM Estatistica
    WHERE nome_camp = @Campeonato AND temporada = @Temporada
    ORDER BY pontuacao DESC
END
GO