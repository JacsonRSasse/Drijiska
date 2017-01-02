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
    class ArestaDB
    {
        public static bool setNovaAresta(NpgsqlConnection conexao, Aresta aresta)
        {
            bool incluiu = false;
            try
            {
                String sql = "insert into aresta values(@id, @peso, @ponto1, @ponto2, @visitado, @acumulado)";
                NpgsqlCommand cmd = new NpgsqlCommand(sql, conexao);
                cmd.Parameters.Add("@id", NpgsqlTypes.NpgsqlDbType.Integer).Value = aresta.id_ar;
                cmd.Parameters.Add("@peso", NpgsqlTypes.NpgsqlDbType.Integer).Value = aresta.peso;
                cmd.Parameters.Add("@ponto1", NpgsqlTypes.NpgsqlDbType.Integer).Value = aresta.ponto1;
                cmd.Parameters.Add("@ponto2", NpgsqlTypes.NpgsqlDbType.Integer).Value = aresta.ponto2;
                cmd.Parameters.Add("@visitado", NpgsqlTypes.NpgsqlDbType.Boolean).Value = aresta.visitado;
                cmd.Parameters.Add("@acumulado", NpgsqlTypes.NpgsqlDbType.Integer).Value = aresta.acumulado;
                int valor = cmd.ExecuteNonQuery();
                if(valor == 1)
                {
                    incluiu = true;
                }
            }
            catch(NpgsqlException erro)
            {
                MessageBox.Show("Erro do SQL: " + erro.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro do Sistema: " + erro.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return incluiu;
        }

        public static int getPonto1(NpgsqlConnection conexao, int id)
        {
            int pt1 = 0;
            try
            {
                String sql = "select ponto1 from aresta where id_ar = @id";
                NpgsqlCommand cmd = new NpgsqlCommand(sql, conexao);
                cmd.Parameters.Add("id", NpgsqlTypes.NpgsqlDbType.Integer).Value = id;
                NpgsqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                pt1 = int.Parse(dr.GetString(0));
                dr.Close();
            }
            catch (NpgsqlException erro)
            {
                MessageBox.Show("Erro do SQL: " + erro.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro do Sistema: " + erro.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return pt1;
        }
        public static int getPonto2(NpgsqlConnection conexao, int id)
        {
            int pt2 = 0;
            try
            {
                String sql = "select ponto2 from aresta where id_ar = @id";
                NpgsqlCommand cmd = new NpgsqlCommand(sql, conexao);
                cmd.Parameters.Add("id", NpgsqlTypes.NpgsqlDbType.Integer).Value = id;
                NpgsqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                pt2 = int.Parse(dr.GetString(0));
                dr.Close();
            }
            catch (NpgsqlException erro)
            {
                MessageBox.Show("Erro do SQL: " + erro.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro do Sistema: " + erro.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return pt2;
        }

        public static int getPeso(NpgsqlConnection conexao, int id)
        {
            int peso = 0;
            try
            {
                String sql = "select peso from aresta where id_ar = @id";
                NpgsqlCommand cmd = new NpgsqlCommand(sql, conexao);
                cmd.Parameters.Add("@id", NpgsqlTypes.NpgsqlDbType.Integer).Value = id;
                NpgsqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                peso = int.Parse(dr.GetString(0));
                dr.Close();
            }
            catch (NpgsqlException erro)
            {
                MessageBox.Show("Erro do SQL: " + erro.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro do Sistema: " + erro.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            return peso;
        }

        public static void setExcluiTudo(NpgsqlConnection conexao)
        {
            try
            {
                String sql = "delete from aresta";
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
    }
}
