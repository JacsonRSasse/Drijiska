using MenorCaminho.Classes;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MenorCaminho.Controle
{
    class PontoDB
    {
        public static bool novoPonto(NpgsqlConnection conexao, Ponto ponto)
        {
            bool incluiu = false;
            try
            {
                String sql = "insert into ponto values(@id, @posX, @posY)";
                NpgsqlCommand cmd = new NpgsqlCommand(sql, conexao);
                cmd.Parameters.Add("@id", NpgsqlTypes.NpgsqlDbType.Integer).Value = ponto.id;
                cmd.Parameters.Add("posX", NpgsqlTypes.NpgsqlDbType.Integer).Value = ponto.PosX;
                cmd.Parameters.Add("@posY", NpgsqlTypes.NpgsqlDbType.Integer).Value = ponto.PosY;
                int v = cmd.ExecuteNonQuery();
                if ( v == 1 )
                {
                    incluiu = true;
                }
            }
            catch(NpgsqlException erro)
            {
                MessageBox.Show("Erro do SQL: " + erro.Message, "Erro de inserção!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch(Exception erro)
            {
                MessageBox.Show("Erro do Sistema: " + erro.Message, "Erro Fatal", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return incluiu;
        }

        public static void setExcluiTudo(NpgsqlConnection conexao)
        {
            try
            {
                String sql = "delete from ponto";
                NpgsqlCommand cmd = new NpgsqlCommand(sql, conexao);
                cmd.ExecuteNonQuery();
            }
            catch (NpgsqlException erro)
            {
                MessageBox.Show("Erro do SQL: " + erro.Message, "Erro de exclusão!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro do Sistema: " + erro.Message, "Erro Fatal", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public static void setExcluiPonto(NpgsqlConnection conexao, int id)
        {
            try
            {
                String sql = "delete from ponto where id = @id";
                NpgsqlCommand cmd = new NpgsqlCommand(sql, conexao);
                cmd.Parameters.Add("@id", NpgsqlTypes.NpgsqlDbType.Integer).Value = id;
                cmd.ExecuteNonQuery();
            }
            catch (NpgsqlException erro)
            {
                MessageBox.Show("Erro do SQL: " + erro.Message, "Erro de exclusão!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro do Sistema: " + erro.Message, "Erro Fatal", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static int getQuant(NpgsqlConnection conexao)
        {
            int qtd = 0;
            try
            {
                String sql = "select COUNT(id) from ponto";
                NpgsqlCommand cmd = new NpgsqlCommand(sql, conexao);
                NpgsqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                qtd = int.Parse(dr.GetString(0));
                dr.Close();                
            }
            catch (NpgsqlException erro)
            {
                MessageBox.Show("Erro do SQL: " + erro.Message, "Erro de SQL!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro do Sistema: " + erro.Message, "Erro Fatal", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return qtd;
        }

        public static int getX(NpgsqlConnection conexao, int id)
        {
            int x = 0;
            try
            {
                String sql = "select posx from ponto where id = @id";
                NpgsqlCommand cmd = new NpgsqlCommand(sql, conexao);
                cmd.Parameters.Add("@id", NpgsqlTypes.NpgsqlDbType.Integer).Value = id;
                NpgsqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                x = int.Parse(dr.GetString(0));
                dr.Close();
            }
            catch (NpgsqlException erro)
            {
                MessageBox.Show("Erro do SQL: " + erro.Message, "Erro de SQL!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro do Sistema: " + erro.Message, "Erro Fatal", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return x;
        }

        public static int getY(NpgsqlConnection conexao, int id)
        {
            int y = 0;
            try
            {
                String sql = "select posy from ponto where id = @id";
                NpgsqlCommand cmd = new NpgsqlCommand(sql, conexao);
                cmd.Parameters.Add("@id", NpgsqlTypes.NpgsqlDbType.Integer).Value = id;
                NpgsqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                y = int.Parse(dr.GetString(0));
                dr.Close();
            }
            catch (NpgsqlException erro)
            {
                MessageBox.Show("Erro do SQL: " + erro.Message, "Erro de SQL!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro do Sistema: " + erro.Message, "Erro Fatal", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return y;
        }
       
    }
}
