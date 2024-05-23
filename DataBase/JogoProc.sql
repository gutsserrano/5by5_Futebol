CREATE OR ALTER PROC Inserir_jogo
    @Campeonato varchar(20),
    @Temporada varchar(10),
    @Mandante varchar(30),
    @Visitante varchar(30),
    @gols_mandante int,
    @gols_visitante int
AS
BEGIN
    
    INSERT INTO dbo.Jogo(campeonato, temp, mandante, visitante, gols_mandante, gols_visitante)
    values (@Campeonato, @Temporada, @Mandante, @Visitante, @gols_mandante, @gols_visitante)

    IF NOT EXISTS (SELECT nome_camp, temporada, nome_equipe FROM dbo.Estatistica WHERE nome_camp = @Campeonato AND temporada = @Temporada AND nome_equipe = @Mandante)
    BEGIN
        INSERT INTO dbo.Estatistica(nome_camp, temporada, nome_equipe, gols_marcados, gols_sofridos, pontuacao)
        VALUES (@Campeonato, @Temporada, @Mandante, 0, 0, 0)
    END

    IF NOT EXISTS (SELECT nome_camp, temporada, nome_equipe FROM dbo.Estatistica WHERE nome_camp = @Campeonato AND temporada = @Temporada AND nome_equipe = @Visitante)
    BEGIN
        INSERT INTO dbo.Estatistica(nome_camp, temporada, nome_equipe, gols_marcados, gols_sofridos, pontuacao)
        VALUES (@Campeonato, @Temporada, @Visitante, 0, 0, 0)
    END

    IF @gols_mandante > @gols_visitante
    BEGIN
        UPDATE dbo.Estatistica
        SET gols_marcados = gols_marcados + @gols_mandante, gols_sofridos = gols_sofridos + @gols_visitante, pontuacao = pontuacao + 3
        WHERE nome_camp = @Campeonato AND temporada = @Temporada AND nome_equipe = @Mandante

        UPDATE dbo.Estatistica
        SET gols_marcados = gols_marcados + @gols_visitante, gols_sofridos = gols_sofridos + @gols_mandante
        WHERE nome_camp = @Campeonato AND temporada = @Temporada AND nome_equipe = @Visitante
    END
    ELSE IF @gols_mandante < @gols_visitante
    BEGIN
        UPDATE dbo.Estatistica
        SET gols_marcados = gols_marcados + @gols_mandante, gols_sofridos = gols_sofridos + @gols_visitante
        WHERE nome_camp = @Campeonato AND temporada = @Temporada AND nome_equipe = @Mandante

        UPDATE dbo.Estatistica
        SET gols_marcados = gols_marcados + @gols_visitante, gols_sofridos = gols_sofridos + @gols_mandante, pontuacao = pontuacao + 5
        WHERE nome_camp = @Campeonato AND temporada = @Temporada AND nome_equipe = @Visitante
    END
    ELSE
    BEGIN
        UPDATE dbo.Estatistica
        SET gols_marcados = gols_marcados + @gols_mandante, gols_sofridos = gols_sofridos + @gols_visitante, pontuacao = pontuacao + 1
        WHERE nome_camp = @Campeonato AND temporada = @Temporada AND nome_equipe = @Mandante

        UPDATE dbo.Estatistica
        SET gols_marcados = gols_marcados + @gols_visitante, gols_sofridos = gols_sofridos + @gols_mandante, pontuacao = pontuacao + 1
        WHERE nome_camp = @Campeonato AND temporada = @Temporada AND nome_equipe = @Visitante
    END

END
GO