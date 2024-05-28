CREATE OR ALTER PROC Ver_tabela
    @Campeonato varchar(20),
    @Temporada varchar(10)
AS
BEGIN
    SELECT Equipe.apelido AS Equipe, gols_marcados AS Gols_marcados, gols_sofridos AS Gols_sofridos, pontuacao AS Pontuacao
    FROM Estatistica 
    JOIN Equipe ON Estatistica.nome_equipe = Equipe.nome
    WHERE nome_camp = @Campeonato AND temporada = @Temporada
    ORDER BY pontuacao DESC, (gols_marcados - gols_sofridos) DESC
END
GO

CREATE OR ALTER PROC Cadastrar_equipe_para_campeonato
    @campeonato varchar(20),
    @temporada varchar(10),
    @nome varchar(30),
    @return_value int OUTPUT

    -- Retorno 0 = Insersao realizada com sucesso
    -- Retorno 1 = Time nao existe
    -- Retorno 2 = Campeonato nao existe
    -- Retorno 3 = Campeonato encerrado
    -- Retorno 4 = Campeonato cheio
    -- Retorno 5 = Time ja cadastrado no campeonato
AS
BEGIN

    -- Verifica se o time existe
    IF EXISTS(SELECT nome FROM Equipe WHERE nome = @nome)
    BEGIN
        -- Verifica se o campeonato existe
        IF EXISTS(SELECT nome_camp FROM Campeonato WHERE nome_camp = @campeonato AND temporada = @temporada)
        BEGIN
            -- Verifica se o campeonato esta em andamento
            IF EXISTS(SELECT nome_camp FROM Campeonato WHERE nome_camp = @campeonato AND temporada = @temporada AND (status_camp = 'EM ANDAMENTO' OR status_camp = 'NAO INICIADO'))
            BEGIN
                DECLARE
                    @Times_count int;

                SET @Times_count = (SELECT COUNT(*) FROM dbo.Estatistica WHERE nome_camp = @Campeonato AND temporada = @Temporada);
                
                -- Verifica se o campeonato ja tem 5 times
                IF(@Times_count < 5)
                BEGIN
                    -- Verifica se o time ja esta cadastrado no campeonato
                    IF EXISTS(SELECT nome_camp FROM Estatistica WHERE nome_camp = @campeonato AND temporada = @temporada AND nome_equipe = @nome)
                        SET @return_value = 5
                    ELSE
                    BEGIN
                        INSERT INTO Estatistica(nome_camp, temporada, nome_equipe, gols_marcados, gols_sofridos, pontuacao)
                        VALUES (@campeonato, @temporada, @nome, 0, 0, 0)

                        SET @return_value = 0
                    END
                END
                ELSE
                    SET @return_value = 4
            END
            ELSE
                SET @return_value = 3
        END
        ELSE
            SET @return_value = 2
    END
    ELSE
        SET @return_value = 1

    RETURN;
    
END
GO