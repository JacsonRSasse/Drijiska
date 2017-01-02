using MenorCaminho.Classes;
using MenorCaminho.Controle;
using MenorCaminho.FormsExtras;
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

namespace MenorCaminho
{
    public partial class FormPrincipal : Form
    {
        private SolidBrush ponto, ponto2, bor;
        private Graphics g;
        private bool des = false;
        private int cont = 1, contA = 1;

        NpgsqlConnection conexao = null;

        public FormPrincipal()
        {
            conexao = Conexao.getConexao();
            InitializeComponent();
            limpar();
        }
        //carregamento do form
        private void FormPrincipal_Load(object sender, EventArgs e)
        {
            ponto = new SolidBrush(Color.Black);
            ponto2 = new SolidBrush(Color.White);
            bor = new SolidBrush(Color.Gray);
            g = panel1.CreateGraphics();
            FontFamily[] family = FontFamily.Families;
            foreach (FontFamily font in family)
            {
                toolStripComboBox1.Items.Add(font.GetName(1).ToString());
            }
            for(int i = 6; i <= 12; i++)
            {
                toolStripComboBox2.Items.Add(i);
            }
        }
        //pintura dos pontos e cadastro na base de dados
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            des = true;
            if (des)
            {  
                g.FillEllipse(ponto, e.X, e.Y, 20, 20);
                g.DrawString(cont.ToString(), new Font(toolStripComboBox1.Text, Convert.ToInt32(toolStripComboBox2.Text), FontStyle.Regular), ponto2, new PointF(e.X,e.Y));

                Ponto pt = new Ponto();
                pt.id = cont;
                pt.PosX = e.X;
                pt.PosY = e.Y;
                bool incluiu = PontoDB.novoPonto(conexao, pt);
                if (incluiu)
                {
                    cont = cont + 1;
                    preencheComboBox3();
                } else
                {
                    limpar();
                }                
            }
        }
        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            des = false;
        }
        //cadastro em tempo real dos pontos          
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
            this.comboBox1.DataSource = dt;
            this.comboBox1.DisplayMember = "id";
            this.comboBox1.ValueMember = this.comboBox1.DisplayMember;
            dr.Close();
        }

        //botao e função de clear, limpa o painel e o banco de dados, e botao de excluir ponto
        private void btnLimpar_Click(object sender, EventArgs e)
        {
            limpar();
        }
        private void limpar()
        {
            panel1.Refresh();
            ArestaDB.setExcluiTudo(conexao);
            PontoDB.setExcluiTudo(conexao);
            cont = 1;
            contA = 1;
            btnExcluir.Enabled = true;
        }
        private void btnExcluir_Click(object sender, EventArgs e)
        {
            excluiPonto();
            preencheComboBox3();
        }

        private void excluiPonto()
        {
            String n1 = comboBox1.SelectedValue.ToString();
            if(n1 == "0" )
            {

            } else
            {
                int id = int.Parse(n1);
                int x = PontoDB.getX(conexao, id); int y = PontoDB.getY(conexao, id);
                g.FillEllipse(bor, x, y, 20, 20); // <-- exclui o item da tela
                PontoDB.setExcluiPonto(conexao, id);
                comboBox1.Items.Remove(n1);               
            }
            
        }

        //Arestas
        private void novaArestaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(this, "Adicionar nova Aresta?", "Confirmação", MessageBoxButtons.YesNo);

            if (result.ToString().ToUpper() == "NO")
            {
                //to tentando sincronizar os dois
            }
            else
            {
                NovaAresta form = new NovaAresta(conexao, contA);
                form.ShowDialog();
                desenharAresta();
                btnExcluir.Enabled = false;
            }

        }
        private void desenharAresta()
        {
            int pt1 = ArestaDB.getPonto1(conexao, contA);
            int pt2 = ArestaDB.getPonto2(conexao, contA);
            int ptx = PontoDB.getX(conexao, pt1);
            int pty = PontoDB.getY(conexao, pt1);
            int ptx2 = PontoDB.getX(conexao, pt2);
            int pty2 = PontoDB.getY(conexao, pt2);
            int medX = (ptx + ptx2)/2;
            int medY = (pty + pty2)/2;
            
            int peso = ArestaDB.getPeso(conexao, contA);

            g.DrawLine(new Pen(Color.Black), ptx, pty, ptx2, pty2);
            g.DrawString("A:"+contA.ToString() +" Peso: " + peso.ToString(), new Font(toolStripComboBox1.Text, Convert.ToInt32(8), FontStyle.Regular), ponto2, new PointF(medX, medY));

            contA = contA + 1;
        }

        //questionario pra confirmar a saída do programa, caso sim, zerar a base de dados
        private void FechouForm(object sender, FormClosedEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                DialogResult result = MessageBox.Show(this, "Você tem certeza que deseja sair?", "Confirmação", MessageBoxButtons.YesNo);

                if (result.ToString().ToUpper() == "NO")
                {
                    Application.Run(new FormPrincipal());
                } else
                {
                    Application.Exit();
                    limpar();
                }
            }
        }
    }
}
