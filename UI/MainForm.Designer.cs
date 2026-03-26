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
			btnExportarLote = new Button();
			dataGridView1 = new DataGridView();
			cmbArquivos = new ComboBox();
			dataGridView2 = new DataGridView();
			((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
			((System.ComponentModel.ISupportInitialize)dataGridView2).BeginInit();
			SuspendLayout();
			// 
			// txtPasta
			// 
			txtPasta.Location = new Point(12, 11);
			txtPasta.Name = "txtPasta";
			txtPasta.Size = new Size(1227, 23);
			txtPasta.TabIndex = 0;
			// 
			// btnSelecionar
			// 
			btnSelecionar.Location = new Point(1245, 11);
			btnSelecionar.Name = "btnSelecionar";
			btnSelecionar.Size = new Size(150, 26);
			btnSelecionar.TabIndex = 1;
			btnSelecionar.Text = "Selecionar Pasta";
			btnSelecionar.Click += btnSelecionar_Click;
			// 
			// btnProcessar
			// 
			btnProcessar.Location = new Point(1245, 43);
			btnProcessar.Name = "btnProcessar";
			btnProcessar.Size = new Size(150, 23);
			btnProcessar.TabIndex = 2;
			btnProcessar.Text = "Processar";
			btnProcessar.Click += btnProcessar_Click;
			// 
			// btnExportar
			// 
			btnExportar.Location = new Point(12, 72);
			btnExportar.Name = "btnExportar";
			btnExportar.Size = new Size(1383, 30);
			btnExportar.TabIndex = 4;
			btnExportar.Text = "Exportar Excel";
			btnExportar.Click += btnExportar_Click;
			// 
			// btnExportarLote
			// 
			btnExportarLote.Location = new Point(12, 108);
			btnExportarLote.Name = "btnExportarLote";
			btnExportarLote.Size = new Size(1383, 30);
			btnExportarLote.TabIndex = 5;
			btnExportarLote.Text = "Exportar em lote";
			btnExportarLote.Click += btnExportarLote_Click;
			// 
			// dataGridView1
			// 
			dataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			dataGridView1.Location = new Point(12, 153);
			dataGridView1.Name = "dataGridView1";
			dataGridView1.Size = new Size(1383, 270);
			dataGridView1.TabIndex = 6;
			// 
			// cmbArquivos
			// 
			cmbArquivos.DropDownStyle = ComboBoxStyle.DropDownList;
			cmbArquivos.Location = new Point(12, 43);
			cmbArquivos.Name = "cmbArquivos";
			cmbArquivos.Size = new Size(1227, 23);
			cmbArquivos.TabIndex = 3;
			// 
			// dataGridView2
			// 
			dataGridView2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			dataGridView2.Location = new Point(12, 440);
			dataGridView2.Name = "dataGridView2";
			dataGridView2.Size = new Size(1383, 274);
			dataGridView2.TabIndex = 7;
			// 
			// MainForm
			// 
			ClientSize = new Size(1423, 726);
			Controls.Add(dataGridView2);
			Controls.Add(txtPasta);
			Controls.Add(btnSelecionar);
			Controls.Add(btnProcessar);
			Controls.Add(cmbArquivos);
			Controls.Add(btnExportar);
			Controls.Add(btnExportarLote);
			Controls.Add(dataGridView1);
			Name = "MainForm";
			Text = "PDF → Excel";
			((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
			((System.ComponentModel.ISupportInitialize)dataGridView2).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private DataGridView dataGridView2;
	}
}