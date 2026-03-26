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

		// Percentuais
		ws.Column(3).Style.NumberFormat.Format = "0.00%";
		ws.Column(8).Style.NumberFormat.Format = "0.00%";

		// Índice (alta precisão)
		ws.Column(10).Style.NumberFormat.Format = "0.000000000";

		int ultimaLinha = dados.Count + 1; // +1 por causa do header
		int linhaTotal = ultimaLinha + 1;

		// Label
		ws.Cell(linhaTotal, 1).Value = "TOTAL";

		// Fórmulas
		ws.Cell(linhaTotal, 6).FormulaA1 = $"SUM(F2:F{ultimaLinha})"; // Salário Devido
		ws.Cell(linhaTotal, 7).FormulaA1 = $"SUM(G2:G{ultimaLinha})"; // Salário Contribuição
		ws.Cell(linhaTotal, 9).FormulaA1 = $"SUM(I2:I{ultimaLinha})"; // Devido Segurado
		ws.Cell(linhaTotal, 11).FormulaA1 = $"SUM(K2:K{ultimaLinha})"; // Valor Corrigido

		// Estilo (opcional mas recomendado)
		var totalRow = ws.Row(linhaTotal);
		totalRow.Style.Font.Bold = true;

		// Opcional: linha separadora
		totalRow.Style.Border.TopBorder = XLBorderStyleValues.Thin;

		wb.SaveAs(caminho);
	}
}