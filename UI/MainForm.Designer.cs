namespace Pdf2Xls.UI
{
	partial class MainForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private System.Windows.Forms.TextBox txtPasta;
		private System.Windows.Forms.Button btnSelecionar;
		private System.Windows.Forms.Button btnProcessar;
		private System.Windows.Forms.Button btnExportar;
		private System.Windows.Forms.DataGridView dataGridView1;
		private System.Windows.Forms.ComboBox cmbArquivos;
		private System.Windows.Forms.Button btnExportarLote;

		private void InitializeComponent()
		{
			txtPasta = new TextBox();
			btnSelecionar = new Button();
			btnProcessar = new Button();
			btnExportar = new Button();
			btnExportarLote = new Button(); // NOVO
			dataGridView1 = new DataGridView();
			cmbArquivos = new ComboBox();

			((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
			SuspendLayout();

			// txtPasta
			txtPasta.Location = new Point(12, 12);
			txtPasta.Name = "txtPasta";
			txtPasta.Size = new Size(600, 23);

			// btnSelecionar
			btnSelecionar.Location = new Point(620, 12);
			btnSelecionar.Name = "btnSelecionar";
			btnSelecionar.Size = new Size(150, 23);
			btnSelecionar.Text = "Selecionar Pasta";
			btnSelecionar.Click += btnSelecionar_Click;

			// btnProcessar
			btnProcessar.Location = new Point(620, 35);
			btnProcessar.Name = "btnProcessar";
			btnProcessar.Size = new Size(150, 28);
			btnProcessar.Text = "Processar";
			btnProcessar.Click += btnProcessar_Click;

			// cmbArquivos
			cmbArquivos.DropDownStyle = ComboBoxStyle.DropDownList;
			cmbArquivos.Location = new Point(12, 40);
			cmbArquivos.Name = "cmbArquivos";
			cmbArquivos.Size = new Size(600, 23);

			// btnExportar
			btnExportar.Location = new Point(12, 69);
			btnExportar.Name = "btnExportar";
			btnExportar.Size = new Size(758, 30);
			btnExportar.Text = "Exportar Excel";
			btnExportar.Click += btnExportar_Click;

			// btnExportarLote (NOVO)
			btnExportarLote.Location = new Point(12, 105);
			btnExportarLote.Name = "btnExportarLote";
			btnExportarLote.Size = new Size(758, 30);
			btnExportarLote.Text = "Exportar em lote";
			btnExportarLote.Click += btnExportarLote_Click;

			// dataGridView1
			dataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			dataGridView1.Location = new Point(12, 141); // desceu
			dataGridView1.Name = "dataGridView1";
			dataGridView1.Size = new Size(760, 353);

			// MainForm
			ClientSize = new Size(800, 504);
			Controls.Add(txtPasta);
			Controls.Add(btnSelecionar);
			Controls.Add(btnProcessar);
			Controls.Add(cmbArquivos);
			Controls.Add(btnExportar);
			Controls.Add(btnExportarLote); // NOVO
			Controls.Add(dataGridView1);

			Name = "MainForm";
			Text = "PDF → Excel";

			((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion
	}
}