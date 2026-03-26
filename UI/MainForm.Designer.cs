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
		private Button btnModelo;
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			txtPasta = new TextBox();
			btnSelecionar = new Button();
			btnProcessar = new Button();
			btnExportar = new Button();
			btnExportarLote = new Button();
			dataGridView1 = new DataGridView();
			cmbArquivos = new ComboBox();
			dataGridView2 = new DataGridView();
			pictureBox1 = new PictureBox();
			label1 = new Label();
			btnAbrirModel = new Button();
			((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
			((System.ComponentModel.ISupportInitialize)dataGridView2).BeginInit();
			((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
			SuspendLayout();
			// 
			// txtPasta
			// 
			txtPasta.Location = new Point(192, 17);
			txtPasta.Name = "txtPasta";
			txtPasta.Size = new Size(1047, 23);
			txtPasta.TabIndex = 0;
			// 
			// btnSelecionar
			// 
			btnSelecionar.Location = new Point(1245, 17);
			btnSelecionar.Name = "btnSelecionar";
			btnSelecionar.Size = new Size(150, 26);
			btnSelecionar.TabIndex = 1;
			btnSelecionar.Text = "Selecionar Pasta";
			btnSelecionar.Click += btnSelecionar_Click;
			// 
			// btnProcessar
			// 
			btnProcessar.Location = new Point(1245, 49);
			btnProcessar.Name = "btnProcessar";
			btnProcessar.Size = new Size(150, 23);
			btnProcessar.TabIndex = 2;
			btnProcessar.Text = "Processar";
			btnProcessar.Click += btnProcessar_Click;
			// 
			// btnExportar
			// 
			btnExportar.Location = new Point(192, 78);
			btnExportar.Name = "btnExportar";
			btnExportar.Size = new Size(622, 30);
			btnExportar.TabIndex = 4;
			btnExportar.Text = "Exportar Individual";
			btnExportar.Click += btnExportar_Click;
			// 
			// btnExportarLote
			// 
			btnExportarLote.Location = new Point(830, 78);
			btnExportarLote.Name = "btnExportarLote";
			btnExportarLote.Size = new Size(565, 30);
			btnExportarLote.TabIndex = 5;
			btnExportarLote.Text = "Exportar Todos";
			btnExportarLote.Click += btnExportarLote_Click;
			// 
			// dataGridView1
			// 
			dataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			dataGridView1.Location = new Point(12, 140);
			dataGridView1.Name = "dataGridView1";
			dataGridView1.Size = new Size(1383, 270);
			dataGridView1.TabIndex = 6;
			// 
			// cmbArquivos
			// 
			cmbArquivos.DropDownStyle = ComboBoxStyle.DropDownList;
			cmbArquivos.Location = new Point(192, 49);
			cmbArquivos.Name = "cmbArquivos";
			cmbArquivos.Size = new Size(1047, 23);
			cmbArquivos.TabIndex = 3;
			// 
			// dataGridView2
			// 
			dataGridView2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			dataGridView2.Location = new Point(12, 416);
			dataGridView2.Name = "dataGridView2";
			dataGridView2.Size = new Size(1383, 274);
			dataGridView2.TabIndex = 7;
			// 
			// pictureBox1
			// 
			pictureBox1.BackgroundImage = Properties.Resources.logo_spdm;
			pictureBox1.BackgroundImageLayout = ImageLayout.Center;
			pictureBox1.Location = new Point(12, 4);
			pictureBox1.Margin = new Padding(0);
			pictureBox1.Name = "pictureBox1";
			pictureBox1.Size = new Size(174, 127);
			pictureBox1.TabIndex = 8;
			pictureBox1.TabStop = false;
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Location = new Point(755, 702);
			label1.Name = "label1";
			label1.Size = new Size(518, 15);
			label1.TabIndex = 9;
			label1.Text = "Sistema para conversão de PDF para XLS para os arquivos da MELLO RIBEIRO, conforme modelo: ";
			// 
			// btnAbrirModel
			// 
			btnAbrirModel.Location = new Point(1275, 697);
			btnAbrirModel.Name = "btnAbrirModel";
			btnAbrirModel.Size = new Size(75, 23);
			btnAbrirModel.TabIndex = 10;
			btnAbrirModel.Text = "Modelo";
			btnAbrirModel.UseVisualStyleBackColor = true;
			btnAbrirModel.Click += btnAbrirModel_Click;
			// 
			// MainForm
			// 
			ClientSize = new Size(1423, 726);
			Controls.Add(btnAbrirModel);
			Controls.Add(label1);
			Controls.Add(pictureBox1);
			Controls.Add(dataGridView2);
			Controls.Add(txtPasta);
			Controls.Add(btnSelecionar);
			Controls.Add(btnProcessar);
			Controls.Add(cmbArquivos);
			Controls.Add(btnExportar);
			Controls.Add(btnExportarLote);
			Controls.Add(dataGridView1);
			Icon = (Icon)resources.GetObject("$this.Icon");
			Name = "MainForm";
			Text = "PDF → Excel | Mello Ribeiro";
			((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
			((System.ComponentModel.ISupportInitialize)dataGridView2).EndInit();
			((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private DataGridView dataGridView2;
		private PictureBox pictureBox1;
		private Label label1;
		private Button btnAbrirModel;
	}
}