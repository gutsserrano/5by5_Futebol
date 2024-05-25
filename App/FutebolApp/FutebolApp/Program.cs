using Microsoft.Data.SqlClient;
using System.Data;

namespace FutebolApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            InserirJogo();
        }

        static void InserirEquipe()
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

        static void InserirCampeonato()
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

                SqlParameter returnParameter = cmd.Parameters.Add("RetVal", SqlDbType.Int);
                returnParameter.Direction = ParameterDirection.ReturnValue;

                cmd.ExecuteNonQuery();

                int returnValue = Convert.ToInt32(returnParameter.Value);

                if (returnValue == 1)
                {
                    Console.WriteLine("Campeonato Inserido!");

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
                else
                {
                    Console.WriteLine("Não foi possivel inserir este jogo :( Verifique se o campeonato/temporada, e os times estâo cadastrados.");
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
        }
    }
}
