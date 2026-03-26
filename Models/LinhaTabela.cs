namespace Pdf2Xls.Models;

public class LinhaTabela
{
	public string Competencia { get; set; } = string.Empty;
	public decimal SalarioPago { get; set; }
	public decimal Aliquota { get; set; }
	public decimal Teto { get; set; }
	public decimal SalarioDevido { get; set; }
	public decimal ValorCorrigido { get; set; }

	public string Ocorrencia { get; set; } = string.Empty;

	public decimal Aliquota1 { get; set; }

	public decimal TetoSegurado { get; set; }
	public decimal ContribuicaoSocial { get; set; }

	public decimal SalarioContribuicao { get; set; }

	public decimal Aliquota2 { get; set; }

	public decimal DevidoSegurado { get; set; }
	public decimal IndiceCorrecao { get; set; }

}