USE DBFutebol;
GO

CREATE OR ALTER PROC Inserir_jogo
    @Campeonato varchar(20),
    @Temporada varchar(10),
    @Mandante varchar(30),
    @Visitante varchar(30),
    @gols_mandante int,
    @gols_visitante int,
    @status_insersao int OUTPUT

    -- Retorno 0 = Insersao realizada com sucesso
    -- Retorno 1 = Insersao nao realizada, campeonato esta encerrado ou não está cadastrado
    -- Retorno 2 = Insersao nao realizada, times nao existem
    -- Retorno 3 = Insersao nao realizada, times iguais
    -- Retorno 4 = Insersao nao realizada, time(s) nao cadastrado(s) no campeonato
    
AS
BEGIN

    -- Verifica se o campeonato está em andamento
    IF EXISTS(SELECT nome_camp, temporada, status_camp FROM dbo.Campeonato WHERE nome_camp = @Campeonato AND temporada = @Temporada AND (status_camp = 'EM ANDAMENTO' OR status_camp = 'NAO INICIADO'))
    BEGIN

        -- Verifica se ambos os times existem
        IF EXISTS(SELECT nome FROM dbo.Equipe WHERE nome = @Mandante) AND EXISTS(SELECT nome FROM dbo.Equipe WHERE nome = @Visitante)
        BEGIN

            IF (SELECT nome FROM dbo.Equipe WHERE nome = @Mandante) != @Visitante 
            BEGIN
                
                IF EXISTS(SELECT nome_camp, temporada, nome_equipe FROM dbo.Estatistica WHERE nome_camp = @Campeonato AND temporada = @Temporada AND nome_equipe = @Mandante) AND EXISTS(SELECT nome_camp, temporada, nome_equipe FROM dbo.Estatistica WHERE nome_camp = @Campeonato AND temporada = @Temporada AND nome_equipe = @Visitante)
                BEGIN
                    IF(SELECT status_camp FROM dbo.Campeonato WHERE nome_camp = @Campeonato AND temporada = @Temporada) = 'NAO INICIADO'
                    BEGIN
                        UPDATE dbo.Campeonato
                        SET status_camp = 'EM ANDAMENTO'
                        WHERE nome_camp = @Campeonato AND temporada = @Temporada
                    END

                    INSERT INTO dbo.Jogo(campeonato, temp, mandante, visitante, gols_mandante, gols_visitante)
                    values (@Campeonato, @Temporada, @Mandante, @Visitante, @gols_mandante, @gols_visitante)

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

                    SET @status_insersao = 0;
                END
                ELSE
                    SET @status_insersao = 4;
            END
            ELSE
                SET @status_insersao = 3;
        END
        ELSE
            SET @status_insersao = 2;
    END
    ELSE
        SET @status_insersao = 1;

    
    RETURN;
END
GO