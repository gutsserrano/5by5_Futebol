using Microsoft.Data.SqlClient;
using System.Data;

namespace FutebolApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int option;
            do
            {
                option = Menu();

                switch (option)
                {
                    case 1:
                        CadastrarEquipe();
                        break;
                    case 2:
                        VerEquipesCadastradas();
                        break;
                    case 3:
                        CadastrarCampeonato();
                        break;
                    case 4:
                        VerCampeonatos();
                        break;
                    case 5:
                        MenuCampeonatos();
                        break;
                }
            } while(option != 0);
        }

        static void CadastrarEquipe()
        {
            Banco conn = new Banco();
            SqlConnection conexaosql = new SqlConnection(conn.Caminho());
            conexaosql.Open();
            SqlCommand cmd = new SqlCommand();


            try
            {
                cmd.CommandText = "INSERT INTO dbo.Equipe(nome, apelido, data_criacao) VALUES (@nome, @apelido, @data_criacao)";

                Titulo(">>>>>Cadastrar Equipe<<<<<");
                cmd.Parameters.AddWithValue("@nome", SqlDbType.VarChar).Value = LerString("Digite o Nome completo da Equipe: ");
                cmd.Parameters.AddWithValue("@apelido", System.Data.SqlDbType.VarChar).Value = LerString("Digite o apelido da Equipe: ");
                cmd.Parameters.AddWithValue("@data_criacao", System.Data.SqlDbType.Date).Value = LerData("Data da criação da equipe: ");

                cmd.Connection = conexaosql;
                cmd.ExecuteNonQuery();
            } catch (Exception e)
            {
                Console.WriteLine("\nMensagem da Exception:");
                Console.WriteLine(e.ToString());
                Console.ReadKey();
            }
            finally
            {
                conexaosql.Close();
            }
        }

        static void VerEquipesCadastradas()
        {
            Banco conn = new Banco();
            SqlConnection conexaosql = new SqlConnection(conn.Caminho());
            conexaosql.Open();
            SqlCommand cmd = new SqlCommand();


            try
            {
                cmd.CommandText = "SELECT nome, apelido, data_criacao FROM Equipe ORDER BY nome";

                cmd.Connection = conexaosql;
                var returnValue = cmd.ExecuteReader();

                if (!returnValue.HasRows)
                {
                    Console.WriteLine("Nenhum Time cadastrado");
                    Console.ReadKey();
                    //throw new Exception("Não há registros"); //Poderia retornar uma Exception
                }
                else
                {
                    Titulo($">>>>>Times Cadastrados<<<<<");
                    Console.WriteLine($"             Nome             |     apelido   |   Data de Criação");
                    Console.WriteLine($"                              |               |");
                    int posicao = 1;
                    while (returnValue.Read())
                    {
                        string nome = returnValue["nome"].ToString();
                        string apelido = returnValue["apelido"].ToString();
                        DateTime data = DateTime.Parse(returnValue["data_criacao"].ToString());

                        Console.WriteLine($"{nome.PadRight(30)}| {apelido.PadRight(14)}| {data.ToString().PadRight(10)}");
                        posicao++;
                    }
                    Console.ReadKey();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("\nMensagem da Exception:");
                Console.WriteLine(e.ToString());
                Console.ReadKey();
            }
            finally
            {
                conexaosql.Close();
            }
        }

        static void CadastrarCampeonato()
        {
            Banco conn = new Banco();
            SqlConnection conexaosql = new SqlConnection(conn.Caminho());
            conexaosql.Open();

            try
            {
                SqlCommand cmd = new SqlCommand("[dbo].[Inserir_campeonato]", conexaosql);
                cmd.CommandType = CommandType.StoredProcedure;
                Titulo(">>>>>Cadastrar Campeonato<<<<<");
                cmd.Parameters.Add(new SqlParameter("@nome", SqlDbType.VarChar)).Value = LerString("Digite o nome do Campeonato: ");
                cmd.Parameters.Add(new SqlParameter("@temporada", SqlDbType.VarChar)).Value = LerString("Digite a temporada: ");
                cmd.ExecuteNonQuery(); //Retorna a linha que foi executada

                Console.WriteLine("Campeonato Inserido!");
                Console.ReadKey();
            }
            catch (Exception e)
            {
                Console.WriteLine("\nMensagem da Exception:");
                Console.WriteLine(e.ToString());
                Console.ReadKey();
            }
            finally
            {
                conexaosql.Close();
            }
        }

        static void VerCampeonatos()
        {
            Banco conn = new Banco();
            SqlConnection conexaosql = new SqlConnection(conn.Caminho());
            conexaosql.Open();
            SqlCommand cmd = new SqlCommand();


            try
            {
                cmd.CommandText = "SELECT nome_camp, temporada, status_camp FROM Campeonato ORDER BY nome_camp, temporada, status_camp";

                cmd.Connection = conexaosql;
                var returnValue = cmd.ExecuteReader();

                if (!returnValue.HasRows)
                {
                    Console.WriteLine("Nenhum Campeonato cadastrado");
                    Console.ReadKey();
                    //throw new Exception("Não há registros"); //Poderia retornar uma Exception
                }
                else
                {
                    Titulo($">>>>>Campeonatos Cadastrados<<<<<");
                    Console.WriteLine($"     Campeonato     |  temporada   |  Situação");
                    Console.WriteLine($"                    |              |");
                    int posicao = 1;
                    while (returnValue.Read())
                    {
                        string nome = returnValue["nome_camp"].ToString();
                        string apelido = returnValue["temporada"].ToString();
                        string situacao = returnValue["status_camp"].ToString();

                        Console.WriteLine($"{nome.PadRight(20)}| {apelido.PadRight(13)}| {situacao}");
                        posicao++;
                    }
                    Console.ReadKey();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("\nMensagem da Exception:");
                Console.WriteLine(e.ToString());
                Console.ReadKey();
            }
            finally
            {
                conexaosql.Close();
            }
        }

        static void InserirJogo()
        {
            Banco conn = new Banco();
            SqlConnection conexaosql = new SqlConnection(conn.Caminho());
            conexaosql.Open();

            string mandante, visitante;
            int gols_mandante, gols_visitante;

            try
            {
                SqlCommand cmd = new SqlCommand("[dbo].[Inserir_jogo]", conexaosql);
                cmd.CommandType = CommandType.StoredProcedure;
                Titulo(">>>>>Cadastrar Jogo<<<<<");
                
                cmd.Parameters.Add(new SqlParameter("@campeonato", SqlDbType.VarChar)).Value = LerString("Digite o nome do Campeonato: ");
                cmd.Parameters.Add(new SqlParameter("@temporada", SqlDbType.VarChar)).Value = LerString("Digite a temporada: ");

                mandante = LerString("Digite o nome do time da casa: ");
                cmd.Parameters.Add(new SqlParameter("@mandante", SqlDbType.VarChar)).Value = mandante;

                visitante = LerString("Digite o nome do time visitante: ");
                cmd.Parameters.Add(new SqlParameter("@visitante", SqlDbType.VarChar)).Value = visitante;

                gols_mandante = LerInt("Quantos gols o time da casa fez? ");
                cmd.Parameters.Add(new SqlParameter("@gols_mandante", SqlDbType.Int)).Value = gols_mandante;

                gols_visitante = LerInt("Quantos gols o time visitante fez? ");
                cmd.Parameters.Add(new SqlParameter("@gols_visitante", SqlDbType.Int)).Value = gols_visitante;

                cmd.Parameters.Add("@status_insersao", SqlDbType.Int).Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();

                int returnValue = Convert.ToInt32(cmd.Parameters["@status_insersao"].Value);

                if (returnValue == 0)
                {
                    Titulo(">>>>>Placar Final<<<<<");
                    Console.WriteLine($"{mandante} {gols_mandante} X {gols_visitante} {visitante}");
                    Console.WriteLine();

                    if (gols_mandante > gols_visitante)
                    {
                        Console.WriteLine($"{mandante} venceu a partida e anotou 3 pontos!");
                    }else if(gols_visitante > gols_mandante)
                    {
                        Console.WriteLine($"{visitante} venceu a partida e anotou 5 pontos!");
                    }
                    else
                    {
                        Console.WriteLine("Empate, ambos os times anotaram 1 ponto!");
                    }
                }
                else if (returnValue == 1)
                {
                    Console.WriteLine("Não foi possivel inserir este jogo. O campeonato já foi encerrado ou não está cadastrado.");
                }
                else if(returnValue == 2) 
                {
                    Console.WriteLine("Não foi possivel inserir este jogo. O(s) time(s) não existe(em).");
                }
                else if (returnValue == 3)
                {
                    Console.WriteLine("Não foi possivel inserir este jogo. Os times devem ser diferentes.");
                }
                else
                {
                    Console.WriteLine("Não foi possivel inserir este jogo. Time(s) não cadastrado(s) no campeonato.");
                }

                Console.ReadKey();
            }
            catch (Exception e)
            {
                Console.WriteLine("\nMensagem da Exception:");
                Console.WriteLine(e.ToString());
                Console.ReadKey();
            }
            finally
            {
                conexaosql.Close();
            }
        }

        static void InserirEquipeAoCampeonato(string camp, string temp)
        {
            Banco conn = new Banco();
            SqlConnection conexaosql = new SqlConnection(conn.Caminho());
            conexaosql.Open();

            try
            {
                SqlCommand cmd = new SqlCommand("[dbo].[Cadastrar_equipe_para_campeonato]", conexaosql);
                cmd.CommandType = CommandType.StoredProcedure;
                Titulo(">>>>>Inserir equipe ao campeonato<<<<<");

                cmd.Parameters.Add(new SqlParameter("@campeonato", SqlDbType.VarChar)).Value = camp;
                cmd.Parameters.Add(new SqlParameter("@temporada", SqlDbType.VarChar)).Value = temp;
                cmd.Parameters.Add(new SqlParameter("@nome", SqlDbType.VarChar)).Value = LerString("Digite o nome da equipe a ser adicionada: ");


                cmd.Parameters.Add("@return_value", SqlDbType.Int).Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();

                int returnValue = Convert.ToInt32(cmd.Parameters["@return_value"].Value);

                if (returnValue == 0)
                {
                    Titulo("**Equipe Inserida com sucesso!**");
                }
                else if (returnValue == 1)
                {
                    Console.WriteLine("\nNão foi possivel adicionar este time. Time não cadastrado");
                }
                else if (returnValue == 2)
                {
                    Console.WriteLine("\nNão foi possivel adicionar este time. Campeonato não cadastrado");
                }
                else if (returnValue == 3)
                {
                    Console.WriteLine("\nNão foi possivel adicionar este time. Campeonato encerrado");
                }
                else if (returnValue == 4)
                {
                    Console.WriteLine("\nNão foi possivel adicionar este time. Campeonato cheio");
                }
                else
                {
                    Console.WriteLine("\nNão foi possivel adicionar este time. Time já cadastrado no campeonato");
                }

                Console.ReadKey();
            }
            catch (Exception e)
            {
                Console.WriteLine("\nMensagem da Exception:");
                Console.WriteLine(e.ToString());
                Console.ReadKey();
            }
            finally
            {
                conexaosql.Close();
            }
        }

        static void MenuInsertEquipe()
        {
            string camp, temp;

            Titulo(">>>>>Inserir equipe ao campeonato<<<<<");
            camp = LerString("Digite o nome do campeonato: ");
            temp = LerString("Digite a temporada: ");

            Console.Clear();
            do
            {
                InserirEquipeAoCampeonato(camp, temp);
                Console.Clear();
            } while (LerString("Deseja adicionar mais um Time ?\n 1 - SIM\n0 - NÃO") == "1");
        }

        static void VerTabela()
        {
            Banco conn = new Banco();
            SqlConnection conexaosql = new SqlConnection(conn.Caminho());
            conexaosql.Open();
            string camp;
            string temporada;
            try
            {
                SqlCommand cmd = new SqlCommand("[dbo].[Ver_tabela]", conexaosql);
                cmd.CommandType = CommandType.StoredProcedure;
                Titulo(">>>>>Tabela de classificação<<<<<");
                camp = LerString("Digite o nome do Campeonato: ");
                temporada = LerString("Digite a temporada: ");
                cmd.Parameters.Add(new SqlParameter("@campeonato", SqlDbType.VarChar)).Value = camp;
                cmd.Parameters.Add(new SqlParameter("@temporada", SqlDbType.VarChar)).Value = temporada;
                var returnValue = cmd.ExecuteReader();

                if (!returnValue.HasRows)
                {
                    Console.WriteLine("Campeonato ou Temporada não localizado");
                    Console.ReadKey();
                    //throw new Exception("Não há registros"); //Poderia retornar uma Exception
                }
                else
                {
                    Titulo($">>>>>{camp} - {temporada}<<<<<");
                    Console.WriteLine("\n\nCampeonato Localizado:");
                    Console.WriteLine($"   Posição   |     Equipe    |   Gols Marcados   |   Gols Sofridos   |   Pontuação");
                    Console.WriteLine($"             |               |                   |                   |");
                    int posicao = 1;
                    while (returnValue.Read())
                    {
                        string equipe = returnValue["Equipe"].ToString();
                        int gols_marcados = int.Parse(returnValue["Gols_marcados"].ToString());
                        int gols_sofridos = int.Parse(returnValue["Gols_sofridos"].ToString());
                        int pontuacao = int.Parse(returnValue["Pontuacao"].ToString());
                        Console.WriteLine($"{posicao.ToString().PadRight(13)}|{equipe.PadRight(15)}|{gols_marcados.ToString().PadRight(19)}|{gols_sofridos.ToString().PadRight(19)}|{pontuacao}");
                        posicao++;
                    }
                    Console.ReadKey();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("\nMensagem da Exception:");
                Console.WriteLine(e.ToString());
                Console.ReadKey();
            }
            finally
            {
                conexaosql.Close();
            }
        }

        static void EncerrarCampeonato()
        {
            Banco conn = new Banco();
            SqlConnection conexaosql = new SqlConnection(conn.Caminho());
            conexaosql.Open();

            string mandante, visitante;
            int gols_mandante, gols_visitante;

            try
            {
                SqlCommand cmd = new SqlCommand("[dbo].[Encerrar_campeonato]", conexaosql);
                cmd.CommandType = CommandType.StoredProcedure;
                Titulo(">>>>>Encerrar Campeonato<<<<<");

                cmd.Parameters.Add(new SqlParameter("@nome", SqlDbType.VarChar)).Value = LerString("Digite o nome do Campeonato: ");
                cmd.Parameters.Add(new SqlParameter("@temporada", SqlDbType.VarChar)).Value = LerString("Digite a temporada: ");

                cmd.Parameters.Add("@return_value", SqlDbType.Int).Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();

                int returnValue = Convert.ToInt32(cmd.Parameters["@return_value"].Value);

                if (returnValue == 1)
                {
                    Console.WriteLine("\nCampeonato encerrado com sucesso!");
                }
                else
                {
                    Console.WriteLine("\nNão foi possível encerrar este campeonato pois nem todos os times jogaram seus jogos de IDA e VOLTA.");
                }

                Console.ReadKey();
            }
            catch (Exception e)
            {
                Console.WriteLine("\nMensagem da Exception:");
                Console.WriteLine(e.ToString());
                Console.ReadKey();
            }
            finally
            {
                conexaosql.Close();
            }
        }

        static void VerCampeao()
        {
            Banco conn = new Banco();
            SqlConnection conexaosql = new SqlConnection(conn.Caminho());
            conexaosql.Open();
            string camp;
            string temporada;
            try
            {
                SqlCommand cmd = new SqlCommand("[dbo].[Ver_campeao]", conexaosql);
                cmd.CommandType = CommandType.StoredProcedure;
                Titulo(">>>>>Ver Campeão<<<<<");
                camp = LerString("Digite o nome do Campeonato: ");
                temporada = LerString("Digite a temporada: ");
                cmd.Parameters.Add(new SqlParameter("@campeonato", SqlDbType.VarChar)).Value = camp;
                cmd.Parameters.Add(new SqlParameter("@temporada", SqlDbType.VarChar)).Value = temporada;

                var returnValue = cmd.ExecuteReader();

                if (!returnValue.HasRows)
                {
                    Console.WriteLine("Campeonato não encerrado");
                }
                else
                {
                    returnValue.Read();
                    Console.WriteLine();
                    Console.WriteLine($"Time Campeão: {returnValue["nome_equipe"]}\nGols Marcados: {returnValue["gols_marcados"]}\nGols Sofridos: {returnValue["gols_sofridos"]}\nPontuação: {returnValue["pontuacao"]}");

                }

            
            }
            catch (Exception e)
            {
                Console.WriteLine("\nMensagem da Exception:");
                Console.WriteLine(e.ToString());
                Console.ReadKey();
            }
            finally
            {
                conexaosql.Close();
            }
            
            Console.ReadKey();
        }

        static int Menu()
        {
            int option;

            do
            {
                Titulo(">>>>>Campeonato de Futebol<<<<<");
                option = LerInt(
                    "1 - Cadastrar Time\n" +
                    "2 - Ver times cadastrados\n" +
                    "3 - Cadastrar Campeonato\n" +
                    "4 - Ver Campeonatos cadastrados\n" +
                    "5 - Manipular Campeonatos\n" +
                    "0 - Sair"
                    );
            } while (option < 0 || option > 5);

            return option;
        }

        static void MenuCampeonatos()
        {
            int option;

            do
            {
                do
                {
                    Titulo(">>>>>Área Campeonatos<<<<<");
                    option = LerInt(
                        "1 - Adicionar Times a um Campeonato\n" +
                        "2 - Inserir jogo a um Campeonato\n" +
                        "3 - Ver Tabela de um Campeonato\n" +
                        "4 - Encerrar um Campeonato\n" +
                        "5 - Ver Campeão de um Campeonato\n" +
                        "0 - Voltar"
                        );
                } while (option < 0 || option > 5);

                switch (option)
                {
                    case 1:
                        MenuInsertEquipe();
                        break;
                    case 2:
                        InserirJogo();
                        break;
                    case 3:
                        VerTabela();
                        break;
                    case 4:
                        EncerrarCampeonato();
                        break;
                    case 5:
                        VerCampeao();
                        break;
                }
            } while (option != 0);
        }

        static string LerString(string msg)
        {
            string texto;
            do
            {
                Console.WriteLine(msg);
                texto = Console.ReadLine();

                if (texto == "")
                {
                    Console.WriteLine("Não é possivel cadastrar dados vazios!");
                    Console.ReadKey();
                }
            } while (texto == "");

            return texto;
        }

        static int LerInt(string msg)
        {
            int input;
            bool conversao;

            do
            {
                Console.WriteLine(msg);
                conversao = int.TryParse(Console.ReadLine(), out input);

                if (!conversao)
                {
                    Console.WriteLine("\nDigite um numero!\n");
                    Console.ReadKey();
                }
            } while (!conversao);

            return input;
        }

        static string LerData(string msg)
        {
            DateOnly data;
            bool conversao;

            do
            {
                Console.WriteLine(msg);
                conversao = DateOnly.TryParse(Console.ReadLine(), out data);

                if (!conversao)
                {
                    Console.WriteLine("\nDigite uma data válida!\n");
                    Console.ReadKey();
                }
            }while(!conversao);

            return data.ToString();
        }

        static void Titulo(string msg)
        {
            Console.Clear();
            Console.WriteLine(msg);
            Console.WriteLine();
        }
    }
}
