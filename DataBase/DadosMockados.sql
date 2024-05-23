INSERT INTO dbo.Equipe(nome, apelido, data_criacao)
values ('São Paulo Futebol Clube', 'SPFC', '1908/01/01')

INSERT INTO dbo.Equipe(nome, apelido, data_criacao)
VALUES ('Sport Club Corinthians', 'Corinthians', '1910/09/01');

INSERT INTO dbo.Equipe(nome, apelido, data_criacao)
VALUES ('Clube de Regatas do Flamengo', 'Flamengo', '1895/11/17');

INSERT INTO dbo.Equipe(nome, apelido, data_criacao)
VALUES ('Sociedade Esportiva Palmeiras', 'Palmeiras', '1914/08/26');

INSERT INTO dbo.Equipe(nome, apelido, data_criacao)
VALUES ('Santos Futebol Clube', 'Santos', '1912/04/14');

INSERT INTO dbo.Equipe(nome, apelido, data_criacao)
VALUES ('Grêmio Foot-Ball Porto Alegrense', 'Grêmio', '1903/09/15');


EXEC Inserir_Campeonato 'Brasileirao', '2024'
EXEC Inserir_Campeonato 'Copa do Brasil', '2020'
EXEC Inserir_Campeonato 'Libertadores', '2024'
EXEC Inserir_Campeonato 'Champions League', '2024'

EXEC Inserir_jogo 'Brasileirao', '2024', 'São Paulo Futebol Clube', 'Sport Club Corinthians', 3, 2;
EXEC Inserir_jogo 'Brasileirao', '2024', 'São Paulo Futebol Clube', 'Clube de Regatas do Flamengo', 1, 3;
EXEC Inserir_jogo 'Brasileirao', '2024', 'São Paulo Futebol Clube', 'Sociedade Esportiva Palmeiras', 5, 0;

EXEC Inserir_jogo 'Brasileirao', '2024', 'Sociedade Esportiva Palmeiras', 'Sport Club Corinthians', 1, 1;
EXEC Inserir_jogo 'Brasileirao', '2024', 'Sport Club Corinthians', 'São Paulo Futebol Clube', 0, 0;
EXEC Inserir_jogo 'Brasileirao', '2024', 'Sport Club Corinthians', 'Sociedade Esportiva Palmeiras', 0, 0;




EXEC Inserir_jogo 'Copa do Brasil', '2020', 'São Paulo Futebol Clube', 'Sport Club Corinthians', 3, 2;
EXEC Inserir_jogo 'Copa do Brasil', '2020', 'Sociedade Esportiva Palmeiras', 'Sport Club Corinthians', 1, 1;





EXEC Contabilizar_gols 'São Paulo Futebol Clube', 'Brasileirao', '2024'

EXEC Ver_tabela 'Brasileirao', '2024'
EXEC Ver_tabela 'Copa do Brasil', '2020'

SELECT * FROM Jogo ORDER BY mandante;
SELECT * FROM Campeonato
SELECT * FROM Equipe
SELECT * FROM Estatistica ORDER BY pontuacao DESC