CREATE or ALTER PROC Inserir_Campeonato
    @nome varchar(20),
    @temporada varchar(10)
AS
BEGIN
    INSERT INTO dbo.Campeonato(nome_camp, temporada, status_camp)
    VALUES (@nome, @temporada, 'EM ANDAMENTO');
END
GO