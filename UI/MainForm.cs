using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pdf2Xls.UI
{
	public partial class MainForm : Form
	{
		public MainForm()
		{
			InitializeComponent();
		}

		private List<string> _arquivos = new();
		private List<Models.LinhaTabela> _dados = new();

		private void btnSelecionar_Click(object sender, EventArgs e)
		{
			using var fbd = new FolderBrowserDialog();

			if (fbd.ShowDialog() == DialogResult.OK)
			{
				txtPasta.Text = fbd.SelectedPath;

				_arquivos = Helpers.FileHelper.ObterPdfs(txtPasta.Text);

				cmbArquivos.DataSource = null;
				cmbArquivos.DataSource = _arquivos;
			}
		}
		private void btnProcessar_Click(object sender, EventArgs e)
		{
			if (cmbArquivos.SelectedItem == null)
			{
				MessageBox.Show("Selecione um arquivo!");
				return;
			}

			var arquivo = cmbArquivos.SelectedItem.ToString();

			var processador = new Core.ProcessadorPdf();

			_dados = processador.Processar(arquivo);

			dataGridView1.DataSource = null;
			dataGridView1.DataSource = _dados;

			dataGridView1.Columns["Ocorrencia"].HeaderText = "Ocorrência";
			dataGridView1.Columns["SalarioPago"].HeaderText = "Salário Pago";
			dataGridView1.Columns["Aliquota1"].HeaderText = "Alíquota";
			dataGridView1.Columns["TetoSegurado"].HeaderText = "Teto Segurado";
			dataGridView1.Columns["ContribuicaoSocial"].HeaderText = "Contribuição Social";
			dataGridView1.Columns["SalarioDevido"].HeaderText = "Salário Devido";
			dataGridView1.Columns["SalarioContribuicao"].HeaderText = "Salário de Contribuição";
			dataGridView1.Columns["Aliquota2"].HeaderText = "Alíquota";
			dataGridView1.Columns["DevidoSegurado"].HeaderText = "Devido Segurado";
			dataGridView1.Columns["IndiceCorrecao"].HeaderText = "Índice Correção";
			dataGridView1.Columns["ValorCorrigido"].HeaderText = "Valor Corrigido";

			dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
		}

		private void btnExportarLote_Click(object sender, EventArgs e)
		{
			if (_arquivos == null || !_arquivos.Any())
			{
				MessageBox.Show("Nenhum arquivo encontrado!");
				return;
			}

			using var fbd = new FolderBrowserDialog();

			if (fbd.ShowDialog() != DialogResult.OK)
				return;

			var pastaDestino = fbd.SelectedPath;

			var processador = new Core.ProcessadorPdf();
			var export = new Services.ExportService();

			int sucesso = 0;
			int erro = 0;

			foreach (var arquivo in _arquivos)
			{
				try
				{
					var dados = processador.Processar(arquivo);

					if (dados == null || !dados.Any())
						continue;

					var nomeArquivo = Path.GetFileNameWithoutExtension(arquivo);
					var caminhoFinal = Path.Combine(pastaDestino, $"{nomeArquivo}.xlsx");

					export.ExportarExcel(dados, caminhoFinal);

					sucesso++;
				}
				catch (Exception ex)
				{
					erro++;
					Console.WriteLine($"Erro ao processar {arquivo}: {ex.Message}");
				}
			}

			MessageBox.Show($"Exportação concluída!\n\nSucesso: {sucesso}\nErros: {erro}");
		}

		private void btnExportar_Click(object sender, EventArgs e)
		{
			if (_dados == null || !_dados.Any())
			{
				MessageBox.Show("Nada para exportar!");
				return;
			}

			var sfd = new SaveFileDialog
			{
				Filter = "Excel (*.xlsx)|*.xlsx"
			};

			if (sfd.ShowDialog() == DialogResult.OK)
			{
				var export = new Services.ExportService();
				export.ExportarExcel(_dados, sfd.FileName);

				MessageBox.Show("Exportado com sucesso!");
			}
		}
	}
}
