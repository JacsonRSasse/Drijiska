using MenorCaminho.Classes;
using MenorCaminho.Controle;
using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MenorCaminho.FormsExtras
{
    public partial class NovaAresta : Form
    {
        internal NpgsqlConnection conexao = null;
        int id;

        public NovaAresta(NpgsqlConnection conexao, int id)
        {
            InitializeComponent();
            this.conexao = conexao;
            this.id = id;
            preencheComboBox3();
        }

        private void preencheComboBox3()
        {
            String sql = "select id from ponto";
            NpgsqlCommand cmd = new NpgsqlCommand(sql, conexao);
            NpgsqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            DataRow row = dt.NewRow();
            row["id"] = "0";
            dt.Rows.InsertAt(row, 0);
            this.comboBoxPt1.DataSource = dt;
            this.comboBoxPt1.DisplayMember = "id";
            this.comboBoxPt1.ValueMember = this.comboBoxPt1.DisplayMember;
            dr.Close();

            String sql2 = "select id from ponto";
            NpgsqlCommand cmd2 = new NpgsqlCommand(sql2, conexao);
            NpgsqlDataReader dr2 = cmd2.ExecuteReader();
            DataTable dt2 = new DataTable();
            dt2.Load(dr2);
            DataRow row2 = dt2.NewRow();
            row2["id"] = "0";
            dt2.Rows.InsertAt(row2, 0);
            this.comboBoxPt2.DataSource = dt2;
            this.comboBoxPt2.DisplayMember = "id";
            this.comboBoxPt2.ValueMember = this.comboBoxPt2.DisplayMember;
            dr2.Close();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            int v1 = int.Parse(comboBoxPt1.SelectedValue.ToString());
            int v2 = int.Parse(comboBoxPt2.SelectedValue.ToString());
            int peso = int.Parse(textBoxPeso.Text);

            if (v1 == 0 || v2 == 0)
            {
                MessageBox.Show("Impossível criar uma aresta no ponto 0,\nfavor mudar os valores.", "Falha: Valores Neutros", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else{
                if (v1 == v2)
                {
                    MessageBox.Show("Impossível criar uma aresta ligando o mesmo ponto,\nfavor mudar os valores.", "Falha: Valores Iguais", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    Aresta aresta = new Aresta();
                    aresta.id_ar = id;
                    aresta.peso = peso;
                    aresta.ponto1 = v1;
                    aresta.ponto2 = v2;
                    aresta.acumulado = 0;
                    aresta.visitado = false;
                    bool incluiu = ArestaDB.setNovaAresta(conexao, aresta);
                    if (incluiu)
                    {
                        MessageBox.Show("Aresta Incluída");
                        Close();
                    }
                }
            }            
        }
    }
}
