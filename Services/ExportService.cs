using ClosedXML.Excel;
using Pdf2Xls.Models;

namespace Pdf2Xls.Services;

public class ExportService
{
	public void ExportarExcel(List<LinhaTabela> dados, string caminho)
	{
		using var wb = new XLWorkbook();
		var ws = wb.Worksheets.Add("Dados");

		ws.Cell(1, 1).Value = "Ocorrência";
		ws.Cell(1, 2).Value = "Salário Pago";
		ws.Cell(1, 3).Value = "Alíquota 1";
		ws.Cell(1, 4).Value = "Teto Segurado";
		ws.Cell(1, 5).Value = "Contribuição Social";
		ws.Cell(1, 6).Value = "Salário Devido";
		ws.Cell(1, 7).Value = "Salário Contribuição";
		ws.Cell(1, 8).Value = "Alíquota 2";
		ws.Cell(1, 9).Value = "Devido Segurado";
		ws.Cell(1, 10).Value = "Índice Correção";
		ws.Cell(1, 11).Value = "Valor Corrigido";

		for (int i = 0; i < dados.Count; i++)
		{
			var d = dados[i];

			ws.Cell(i + 2, 1).Value = d.Ocorrencia;
			ws.Cell(i + 2, 2).Value = d.SalarioPago;
			ws.Cell(i + 2, 3).Value = d.Aliquota1 / 100;
			ws.Cell(i + 2, 4).Value = d.TetoSegurado;
			ws.Cell(i + 2, 5).Value = d.ContribuicaoSocial;
			ws.Cell(i + 2, 6).Value = d.SalarioDevido;
			ws.Cell(i + 2, 7).Value = d.SalarioContribuicao;
			ws.Cell(i + 2, 8).Value = d.Aliquota2 / 100; 
			ws.Cell(i + 2, 9).Value = d.DevidoSegurado;
			ws.Cell(i + 2, 10).Value = d.IndiceCorrecao;
			ws.Cell(i + 2, 11).Value = d.ValorCorrigido;
		}

		ws.Columns().AdjustToContents();

		var decimalFormat = "#,##0.00";

		ws.Column(2).Style.NumberFormat.Format = decimalFormat; // Salário Pago
		ws.Column(4).Style.NumberFormat.Format = decimalFormat; // Teto
		ws.Column(5).Style.NumberFormat.Format = decimalFormat; // Contribuição
		ws.Column(6).Style.NumberFormat.Format = decimalFormat; // Salário Devido
		ws.Column(7).Style.NumberFormat.Format = decimalFormat; // Salário Contribuição
		ws.Column(9).Style.NumberFormat.Format = decimalFormat; // Devido
		ws.Column(11).Style.NumberFormat.Format = decimalFormat; // Corrigido

		ws.Column(3).Style.NumberFormat.Format = "0.00%";
		ws.Column(8).Style.NumberFormat.Format = "0.00%";

		ws.Column(10).Style.NumberFormat.Format = "0.000000000";

		int ultimaLinha = dados.Count + 1;
		int linhaTotal = ultimaLinha + 1;

		ws.Cell(linhaTotal, 1).Value = "TOTAL";

		ws.Cell(linhaTotal, 6).FormulaA1 = $"SUM(F2:F{ultimaLinha})"; // Salário Devido
		ws.Cell(linhaTotal, 7).FormulaA1 = $"SUM(G2:G{ultimaLinha})"; // Salário Contribuição
		ws.Cell(linhaTotal, 9).FormulaA1 = $"SUM(I2:I{ultimaLinha})"; // Devido Segurado
		ws.Cell(linhaTotal, 11).FormulaA1 = $"SUM(K2:K{ultimaLinha})"; // Valor Corrigido

		var totalRow = ws.Row(linhaTotal);
		totalRow.Style.Font.Bold = true;

		totalRow.Style.Border.TopBorder = XLBorderStyleValues.Thin;

		wb.SaveAs(caminho);
	}

	public void ExportarExcelResumo(List<LinhaTabelaResumo> dados, string caminho)
	{
		using var wb = new XLWorkbook();
		var ws = wb.Worksheets.Add("Resumo");

		ws.Cell(1, 1).Value = "Ocorrência";
		ws.Cell(1, 2).Value = "Salário Pago";
		ws.Cell(1, 3).Value = "Salário Devido";
		ws.Cell(1, 4).Value = "Salário Contribuição";
		ws.Cell(1, 5).Value = "Alíquota";
		ws.Cell(1, 6).Value = "Devido Segurado";
		ws.Cell(1, 7).Value = "Índice Correção";
		ws.Cell(1, 8).Value = "Valor Corrigido";
		ws.Cell(1, 9).Value = "Juros";
		ws.Cell(1, 10).Value = "Multa";
		ws.Cell(1, 11).Value = "Total";

		for (int i = 0; i < dados.Count; i++)
		{
			var d = dados[i];

			ws.Cell(i + 2, 1).Value = d.Ocorrencia;
			ws.Cell(i + 2, 2).Value = d.SalarioPago;
			ws.Cell(i + 2, 3).Value = d.SalarioDevido;
			ws.Cell(i + 2, 4).Value = d.SalarioContribuicao;
			ws.Cell(i + 2, 5).Value = d.Aliquota / 100;
			ws.Cell(i + 2, 6).Value = d.DevidoSegurado;
			ws.Cell(i + 2, 7).Value = d.IndiceCorrecao;
			ws.Cell(i + 2, 8).Value = d.ValorCorrigido;
			ws.Cell(i + 2, 9).Value = d.Juros;
			ws.Cell(i + 2, 10).Value = d.Multa;
			ws.Cell(i + 2, 11).Value = d.Total;
		}

		ws.Columns().AdjustToContents();

		var decimalFormat = "#,##0.00";
		ws.Column(2).Style.NumberFormat.Format = decimalFormat;
		ws.Column(3).Style.NumberFormat.Format = decimalFormat;
		ws.Column(4).Style.NumberFormat.Format = decimalFormat;
		ws.Column(6).Style.NumberFormat.Format = decimalFormat;
		ws.Column(8).Style.NumberFormat.Format = decimalFormat;
		ws.Column(9).Style.NumberFormat.Format = decimalFormat;
		ws.Column(10).Style.NumberFormat.Format = decimalFormat;
		ws.Column(11).Style.NumberFormat.Format = decimalFormat;

		ws.Column(5).Style.NumberFormat.Format = "0.00%";
		ws.Column(7).Style.NumberFormat.Format = "0.000000000";

		wb.SaveAs(caminho);
	}

	public void ExportarExcelCompleto(
	List<LinhaTabela> tabela1,
	List<LinhaTabelaResumo> tabela2,
	string caminho)
	{
		using var wb = new XLWorkbook();

		var ws1 = wb.Worksheets.Add("Dados");

		ws1.Cell(1, 1).Value = "Ocorrência";
		ws1.Cell(1, 2).Value = "Salário Pago";
		ws1.Cell(1, 3).Value = "Alíquota 1";
		ws1.Cell(1, 4).Value = "Teto Segurado";
		ws1.Cell(1, 5).Value = "Contribuição Social";
		ws1.Cell(1, 6).Value = "Salário Devido";
		ws1.Cell(1, 7).Value = "Salário Contribuição";
		ws1.Cell(1, 8).Value = "Alíquota 2";
		ws1.Cell(1, 9).Value = "Devido Segurado";
		ws1.Cell(1, 10).Value = "Índice Correção";
		ws1.Cell(1, 11).Value = "Valor Corrigido";

		for (int i = 0; i < tabela1.Count; i++)
		{
			var d = tabela1[i];

			ws1.Cell(i + 2, 1).Value = d.Ocorrencia;
			ws1.Cell(i + 2, 2).Value = d.SalarioPago;
			ws1.Cell(i + 2, 3).Value = d.Aliquota1 / 100;
			ws1.Cell(i + 2, 4).Value = d.TetoSegurado;
			ws1.Cell(i + 2, 5).Value = d.ContribuicaoSocial;
			ws1.Cell(i + 2, 6).Value = d.SalarioDevido;
			ws1.Cell(i + 2, 7).Value = d.SalarioContribuicao;
			ws1.Cell(i + 2, 8).Value = d.Aliquota2 / 100;
			ws1.Cell(i + 2, 9).Value = d.DevidoSegurado;
			ws1.Cell(i + 2, 10).Value = d.IndiceCorrecao;
			ws1.Cell(i + 2, 11).Value = d.ValorCorrigido;
		}

		ws1.Columns().AdjustToContents();

		var decimalFormat = "#,##0.00";

		ws1.Column(2).Style.NumberFormat.Format = decimalFormat;
		ws1.Column(4).Style.NumberFormat.Format = decimalFormat;
		ws1.Column(5).Style.NumberFormat.Format = decimalFormat;
		ws1.Column(6).Style.NumberFormat.Format = decimalFormat;
		ws1.Column(7).Style.NumberFormat.Format = decimalFormat;
		ws1.Column(9).Style.NumberFormat.Format = decimalFormat;
		ws1.Column(11).Style.NumberFormat.Format = decimalFormat;

		ws1.Column(3).Style.NumberFormat.Format = "0.00%";
		ws1.Column(8).Style.NumberFormat.Format = "0.00%";
		ws1.Column(10).Style.NumberFormat.Format = "0.000000000";

		if (tabela1.Any())
		{
			int ultimaLinha = tabela1.Count + 1;
			int linhaTotal = ultimaLinha + 1;

			ws1.Cell(linhaTotal, 1).Value = "TOTAL";

			ws1.Cell(linhaTotal, 6).FormulaA1 = $"SUM(F2:F{ultimaLinha})";
			ws1.Cell(linhaTotal, 7).FormulaA1 = $"SUM(G2:G{ultimaLinha})";
			ws1.Cell(linhaTotal, 9).FormulaA1 = $"SUM(I2:I{ultimaLinha})";
			ws1.Cell(linhaTotal, 11).FormulaA1 = $"SUM(K2:K{ultimaLinha})";

			var totalRow = ws1.Row(linhaTotal);
			totalRow.Style.Font.Bold = true;
			totalRow.Style.Border.TopBorder = XLBorderStyleValues.Thin;
		}

		var ws2 = wb.Worksheets.Add("Resumo");

		ws2.Cell(1, 1).Value = "Ocorrência";
		ws2.Cell(1, 2).Value = "Salário Pago";
		ws2.Cell(1, 3).Value = "Salário Devido";
		ws2.Cell(1, 4).Value = "Salário Contribuição";
		ws2.Cell(1, 5).Value = "Alíquota";
		ws2.Cell(1, 6).Value = "Devido Segurado";
		ws2.Cell(1, 7).Value = "Índice Correção";
		ws2.Cell(1, 8).Value = "Valor Corrigido";
		ws2.Cell(1, 9).Value = "Juros";
		ws2.Cell(1, 10).Value = "Multa";
		ws2.Cell(1, 11).Value = "Total";

		for (int i = 0; i < tabela2.Count; i++)
		{
			var d = tabela2[i];

			ws2.Cell(i + 2, 1).Value = d.Ocorrencia;
			ws2.Cell(i + 2, 2).Value = d.SalarioPago;
			ws2.Cell(i + 2, 3).Value = d.SalarioDevido;
			ws2.Cell(i + 2, 4).Value = d.SalarioContribuicao;
			ws2.Cell(i + 2, 5).Value = d.Aliquota / 100;
			ws2.Cell(i + 2, 6).Value = d.DevidoSegurado;
			ws2.Cell(i + 2, 7).Value = d.IndiceCorrecao;
			ws2.Cell(i + 2, 8).Value = d.ValorCorrigido;
			ws2.Cell(i + 2, 9).Value = d.Juros;
			ws2.Cell(i + 2, 10).Value = d.Multa;
			ws2.Cell(i + 2, 11).Value = d.Total;
		}

		ws2.Columns().AdjustToContents();

		ws2.Column(2).Style.NumberFormat.Format = decimalFormat;
		ws2.Column(3).Style.NumberFormat.Format = decimalFormat;
		ws2.Column(4).Style.NumberFormat.Format = decimalFormat;
		ws2.Column(6).Style.NumberFormat.Format = decimalFormat;
		ws2.Column(8).Style.NumberFormat.Format = decimalFormat;
		ws2.Column(9).Style.NumberFormat.Format = decimalFormat;
		ws2.Column(10).Style.NumberFormat.Format = decimalFormat;
		ws2.Column(11).Style.NumberFormat.Format = decimalFormat;

		ws2.Column(5).Style.NumberFormat.Format = "0.00%";
		ws2.Column(7).Style.NumberFormat.Format = "0.000000000";

		if (tabela2.Any())
		{
			int ultimaLinha = tabela2.Count + 1;
			int linhaTotal = ultimaLinha + 1;

			ws2.Cell(linhaTotal, 1).Value = "TOTAL";

			ws2.Cell(linhaTotal, 4).FormulaA1 = $"SUM(D2:D{ultimaLinha})"; // Salário Contribuição
			ws2.Cell(linhaTotal, 6).FormulaA1 = $"SUM(F2:F{ultimaLinha})"; // Devido Segurado
			ws2.Cell(linhaTotal, 8).FormulaA1 = $"SUM(H2:H{ultimaLinha})"; // Valor Corrigido
			ws2.Cell(linhaTotal, 9).FormulaA1 = $"SUM(I2:I{ultimaLinha})"; // Juros
			ws2.Cell(linhaTotal, 11).FormulaA1 = $"SUM(K2:K{ultimaLinha})"; // Total

			var totalRow = ws2.Row(linhaTotal);
			totalRow.Style.Font.Bold = true;
			totalRow.Style.Border.TopBorder = XLBorderStyleValues.Thin;
		}

		wb.SaveAs(caminho);
	}
}