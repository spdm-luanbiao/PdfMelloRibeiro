using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Pdf2Xls.UI
{
	public partial class MainForm : Form
	{
		public MainForm()
		{
			InitializeComponent();
			AplicarTemaDark();
		}

		private List<string> _arquivos = new();
		private List<Models.LinhaTabela> _dados = new();
		private List<Models.LinhaTabelaResumo> _dadosResumo = new();

		private void btnSelecionar_Click(object sender, EventArgs e)
		{
			using var fbd = new FolderBrowserDialog();

			if (fbd.ShowDialog() == DialogResult.OK)
			{
				txtPasta.Text = fbd.SelectedPath;

				_arquivos = Helpers.FileHelper.ObterPdfs(txtPasta.Text);

				cmbArquivos.DataSource = null;
				cmbArquivos.DataSource = _arquivos
					.Select(x => new
					{
						Nome = Path.GetFileName(x),
						Caminho = x
					})
					.ToList();

				cmbArquivos.DisplayMember = "Nome";
				cmbArquivos.ValueMember = "Caminho";
			}
		}

		private void btnProcessar_Click(object sender, EventArgs e)
		{
			if (cmbArquivos.SelectedItem == null)
			{
				MessageBox.Show("Selecione um arquivo!");
				return;
			}

			var arquivo = cmbArquivos.SelectedValue.ToString(); 
			var processador = new Core.ProcessadorPdf();

			_dados = processador.Processar(arquivo);
			_dadosResumo = processador.ProcessarResumo(arquivo);

			dataGridView1.DataSource = null;
			dataGridView1.DataSource = _dados;

			if (_dados.Any())
			{
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

			dataGridView2.DataSource = null;
			dataGridView2.DataSource = _dadosResumo;

			if (_dadosResumo.Any())
			{
				dataGridView2.Columns["Ocorrencia"].HeaderText = "Ocorrência";
				dataGridView2.Columns["SalarioPago"].HeaderText = "Salário Pago";
				dataGridView2.Columns["SalarioDevido"].HeaderText = "Salário Devido";
				dataGridView2.Columns["SalarioContribuicao"].HeaderText = "Salário Contribuição";
				dataGridView2.Columns["Aliquota"].HeaderText = "Alíquota";
				dataGridView2.Columns["DevidoSegurado"].HeaderText = "Devido Segurado";
				dataGridView2.Columns["IndiceCorrecao"].HeaderText = "Índice Correção";
				dataGridView2.Columns["ValorCorrigido"].HeaderText = "Valor Corrigido";
				dataGridView2.Columns["Juros"].HeaderText = "Juros";
				dataGridView2.Columns["Multa"].HeaderText = "Multa";
				dataGridView2.Columns["Total"].HeaderText = "Total";

				dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
			}
		}

		private void btnExportar_Click(object sender, EventArgs e)
		{
			if ((_dados == null || !_dados.Any()) && (_dadosResumo == null || !_dadosResumo.Any()))
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

				export.ExportarExcelCompleto(_dados, _dadosResumo, sfd.FileName);

				MessageBox.Show("Exportado com sucesso!");
			}
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
					var resumo = processador.ProcessarResumo(arquivo);

					if (!dados.Any() && !resumo.Any())
						continue;

					var nomeArquivo = Path.GetFileNameWithoutExtension(arquivo);
					var caminhoFinal = Path.Combine(pastaDestino, $"{nomeArquivo}.xlsx");

					export.ExportarExcelCompleto(dados, resumo, caminhoFinal);

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

		private void AplicarTemaDark()
		{
			this.BackColor = Color.FromArgb(30, 30, 30);
			this.ForeColor = Color.White;

			foreach (Control ctrl in this.Controls)
			{
				AplicarTemaControle(ctrl);
			}
		}

		private void AplicarTemaControle(Control ctrl)
		{
			switch (ctrl)
			{
				case TextBox txt:
					txt.BackColor = Color.FromArgb(45, 45, 45);
					txt.ForeColor = Color.White;
					txt.BorderStyle = BorderStyle.FixedSingle;
					break;

				case ComboBox cmb:
					cmb.BackColor = Color.FromArgb(45, 45, 45);
					cmb.ForeColor = Color.White;
					break;

				case Button btn:
					btn.BackColor = Color.FromArgb(60, 60, 60);
					btn.ForeColor = Color.White;
					btn.FlatStyle = FlatStyle.Flat;
					btn.FlatAppearance.BorderColor = Color.FromArgb(80, 80, 80);
					btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(80, 80, 80);
					btn.FlatAppearance.MouseDownBackColor = Color.FromArgb(100, 100, 100);
					break;

				case DataGridView dgv:
					dgv.BackgroundColor = Color.FromArgb(30, 30, 30);
					dgv.EnableHeadersVisualStyles = false;

					dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(45, 45, 45);
					dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

					dgv.DefaultCellStyle.BackColor = Color.FromArgb(30, 30, 30);
					dgv.DefaultCellStyle.ForeColor = Color.White;

					dgv.RowsDefaultCellStyle.BackColor = Color.FromArgb(30, 30, 30);
					dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(40, 40, 40);

					dgv.GridColor = Color.FromArgb(60, 60, 60);
					break;
			}

			foreach (Control child in ctrl.Controls)
			{
				AplicarTemaControle(child);
			}
		}

		private void btnAbrirModel_Click(object sender, EventArgs e)
		{
			try
			{
				var caminho = Path.Combine(Application.StartupPath,"Resources", "modelo.pdf");

				if (!File.Exists(caminho))
				{
					MessageBox.Show("Modelo não encontrado!");
					return;
				}

				System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
				{
					FileName = caminho,
					UseShellExecute = true
				});
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Erro ao abrir modelo: {ex.Message}");
			}
		}
	}
}