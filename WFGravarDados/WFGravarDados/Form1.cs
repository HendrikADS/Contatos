using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using MySql.Data.MySqlClient;

namespace WFGravarDados
{
    public partial class Form1 : Form
    {
        MySqlConnection Conexao;
        string data_source = "Server=localhost;User=root;Database=db_agenda;Password=@Hendrik1979;";      

        public Form1()
        {
            InitializeComponent();

            lstContatos.View = View.Details;//Detalhamento da estrutura do listview
            lstContatos.AllowColumnReorder = true;//Permite alterar o tamanho da colona
            lstContatos.FullRowSelect = true;//permite selecionar ma linha
            lstContatos.GridLines = true;//cria linhas de grade

            //Definicao das colunas no ListView

            lstContatos.Columns.Add("id", 30, HorizontalAlignment.Left);
            lstContatos.Columns.Add("NOME", 150, HorizontalAlignment.Left);
            lstContatos.Columns.Add("EMAIL", 150, HorizontalAlignment.Left);
            lstContatos.Columns.Add("TELEFONE", 80, HorizontalAlignment.Left);
            //add novos campos
            lstContatos.Columns.Add("ENDERECO", 150, HorizontalAlignment.Left);
            lstContatos.Columns.Add("CEP", 50, HorizontalAlignment.Left);
            lstContatos.Columns.Add("BAIRRO", 150, HorizontalAlignment.Left);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        //BOTAO ENVIAR
        private void btnEnviar_Click(object sender, EventArgs e)
        {
            try
            {                             
                // Criar Conexao
                Conexao = new MySqlConnection(data_source);
                string sql = "INSERT INTO contatos (nome, email, telefone, endereco, cep, bairro) " +
                 "VALUES " +
                 $"('{txtNome.Text}', '{txtEmail.Text}', '{txtTelefone.Text}', " +
                 $"'{txtEndereco.Text}', '{txtCep.Text}', '{txtBairro.Text}')";


                // Executar o comando Insert

                MySqlCommand comando = new MySqlCommand(sql, Conexao);
                Conexao.Open();
                comando.ExecuteReader();
                MessageBox.Show("Inclusão Efetuada");
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (Conexao != null && Conexao.State == ConnectionState.Open)// outra opcao
                {
                    Conexao.Close();
                }
            }

        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                string q = "'%" + txtBuscar.Text + "%'";

                //Criar Conexao
                Conexao = new MySqlConnection(data_source);
                
                string sql = "SELECT * " +
                             "FROM contatos WHERE nome LIKE " + q +
                             "OR email LIKE " + q;

                //Executar Comando de Busca

                MySqlCommand comando = new MySqlCommand(sql, Conexao);

                Conexao.Open();

                //cria um litor de informaçoes(com o nome reader)

                MySqlDataReader reader = comando.ExecuteReader();

                //Limpa list View

                lstContatos.Items.Clear();

                //while percorre todos os resultados que forem encontrado no BD

                while (reader.Read()) ;

                //cria um vetor para acomodar os campos colunas (tabela)

                string[] linha =
                {
                    reader.GetString(0),
                    reader.GetString(1),
                    reader.GetString(2),
                    reader.GetString(3),
                    reader.GetString(4),
                    reader.GetString(5),
                    reader.GetString(6),
                };
                var linha_listview = new ListViewItem(linha);
                lstContatos.Items.Add(linha_listview);

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Conexao.Close();
            }    
    
        }
        private void lstContatos_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void txtTelefone_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void LimparCampos()
        {
            txtNome.Clear();
            txtEmail.Clear();
            txtTelefone.Clear();
            txtEndereco.Clear();
            txtCep.Clear();
            txtBairro.Clear();
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            LimparCampos();
        }
    }
}
