using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MenorCaminho.Controle
{
    class Conexao
    {
        public static NpgsqlConnection getConexao()
        {
            NpgsqlConnection conexao = null;
            try
            {
                conexao = new NpgsqlConnection("Server=localhost;Port=5432;User Id=postgres;Password=postgres;Database=drijiska;");
                conexao.Open();
            }
            catch(NpgsqlException erro)
            {
                DialogResult result = MessageBox.Show("Erro de conexão com o banco" + erro.Message, "Erro", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Error);
                if(result.ToString().ToUpper() == "ABORT")
                {
                    Application.Exit();
                } else
                {
                    if (result.ToString().ToUpper() == "RETRY")
                    {
                        conexao.Open();
                    } else
                    {
                        if(result.ToString().ToUpper() == "IGNORE")
                        {

                        }
                    }
                }
            }
            return conexao;
        }

        public static void fechaConexao(NpgsqlConnection conexao)
        {
            if(conexao != null)
            {
                try
                {
                    conexao.Close();
                }
                catch(NpgsqlException erro)
                {
                    MessageBox.Show("Erro ao fechar a conexão." + erro.Message, "Erro", MessageBoxButtons.OK);
                }
            }
        }
    }
}
