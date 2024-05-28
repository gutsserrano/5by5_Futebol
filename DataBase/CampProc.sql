CREATE or ALTER PROC Inserir_Campeonato
    @nome varchar(20),
    @temporada varchar(10)
AS
BEGIN
    INSERT INTO dbo.Campeonato(nome_camp, temporada, status_camp)
    VALUES (@nome, @temporada, 'NAO INICIADO');
END
GO

CREATE OR ALTER PROC Encerrar_Campeonato
    @nome varchar(20),
    @temporada varchar(10),
    @return_value int OUTPUT
AS
BEGIN

    DECLARE
        @jogos_count int,
        @Times_count int;

    SET @jogos_count = (SELECT COUNT(*) FROM dbo.Jogo WHERE campeonato = @nome AND temp = @temporada);
    SET @Times_count = (SELECT COUNT(*) FROM dbo.Estatistica WHERE nome_camp = @nome AND temporada = @temporada);
    
    IF(@jogos_count > 5)
    BEGIN
        IF((@jogos_count) = ((@Times_count) * (@Times_count) - @Times_count))
        BEGIN
            UPDATE dbo.Campeonato
            SET status_camp = 'ENCERRADO'
            WHERE nome_camp = @nome AND temporada = @temporada
            SET @return_value = 1;
        END
        ELSE
            SET @return_value = 0;
    END
    ELSE
        SET @return_value = 0;

    RETURN @return_value;
END
GO

CREATE OR ALTER PROC Ver_campeao
    @Campeonato varchar(20),
    @Temporada varchar(10)
AS
BEGIN
    IF(SELECT status_camp FROM dbo.Campeonato WHERE nome_camp = @Campeonato AND temporada = @Temporada) = 'ENCERRADO'
    BEGIN
        SELECT nome_equipe, gols_marcados, gols_sofridos, pontuacao
        FROM dbo.Estatistica
        WHERE nome_camp = @Campeonato AND temporada = @Temporada
        ORDER BY pontuacao DESC
    END
END
GO