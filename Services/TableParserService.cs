using System.Text.RegularExpressions;
using Pdf2Xls.Helpers;
using Pdf2Xls.Models;

namespace Pdf2Xls.Services;

public class TableParserService
{
	public List<LinhaTabela> Parse(string texto)
	{
		var resultado = new List<LinhaTabela>();

		Console.WriteLine("===== INÍCIO PARSER =====");
		
		texto = ExtrairBlocoTabela(texto);
		texto = texto.Replace("INDICAÇÃO DE PARCELAS", "\nINDICAÇÃO DE PARCELAS");

		var linhas = Regex.Split(texto, @"(?=\d{2}/\d{4})")
			.Select(x => x.Trim())
			.Where(x => !string.IsNullOrWhiteSpace(x))
			.ToList();

		Console.WriteLine($"Total de linhas após split: {linhas.Count}");

		foreach (var linhaOriginal in linhas)
		{
			Console.WriteLine($"\nORIGINAL: {linhaOriginal}");

			var linha = linhaOriginal;

			if (!Regex.IsMatch(linha, @"^\d{2}/\d{4}"))
				continue;

			var antesLimpeza = linha;

			linha = Regex.Replace(linha, @"^(\d{2}/\d{4})\s+\1", "$1");
			linha = Regex.Replace(linha, @"\b\d+,\s+\d{2}/\d{4}", "");
			linha = Regex.Replace(linha, @"(\d+,\d+)\s+0{6,}", "$1");
			linha = Regex.Replace(linha, @"(\d+,\d+)\s+\1", "$1");

			Console.WriteLine($"DEPOIS LIMPEZA: {linha}");

			linha = NormalizarLinha(linha);

			Console.WriteLine($"NORMALIZADA: {linha}");

			var match = Regex.Match(linha, @"
				^(?<comp>\d{2}/\d{4})\s+
				(?<salPago>\d+,\d{2})\s+
				(?<aliq1>\d+,\d{2})\s+%\s+
				(?<teto>\d+,\d{2})\s+
				(?<contrib>\d+,\d{2})\s+
				(?<salDevido>\d+,\d{2})\s+
				(?<salContrib>\d+,\d{2})\s+
				(?<aliq2>\d+,\d{2})\s+%\s+
				(?<devido>\d+,\d{2})\s+
				(?<indice>\d+,\d{2})\s+
				(?<corrigido>\d+,\d{2})
				", RegexOptions.IgnorePatternWhitespace);

			if (!match.Success)
			{
				Console.WriteLine(">> IGNORADO (regex não casou)");
				continue;
			}

			try
			{
				resultado.Add(new LinhaTabela
				{
					Ocorrencia = match.Groups["comp"].Value,
					SalarioPago = ParseHelper.ParseDecimal(match.Groups["salPago"].Value),
					Aliquota1 = ParseHelper.ParseDecimal(match.Groups["aliq1"].Value),
					TetoSegurado = ParseHelper.ParseDecimal(match.Groups["teto"].Value),
					ContribuicaoSocial = ParseHelper.ParseDecimal(match.Groups["contrib"].Value),
					SalarioDevido = ParseHelper.ParseDecimal(match.Groups["salDevido"].Value),
					SalarioContribuicao = ParseHelper.ParseDecimal(match.Groups["salContrib"].Value),
					Aliquota2 = ParseHelper.ParseDecimal(match.Groups["aliq2"].Value),
					DevidoSegurado = ParseHelper.ParseDecimal(match.Groups["devido"].Value),
					IndiceCorrecao = ParseHelper.ParseDecimal(match.Groups["indice"].Value),
					ValorCorrigido = ParseHelper.ParseDecimal(match.Groups["corrigido"].Value)
				});

				Console.WriteLine(">> ✅ OK");
			}
			catch (Exception ex)
			{
				Console.WriteLine($">> ERRO: {ex.Message}");
			}
		}

		Console.WriteLine($"===== TOTAL: {resultado.Count} =====");

		return resultado
			.GroupBy(x => x.Ocorrencia)
			.Select(g => g.OrderByDescending(x => x.IndiceCorrecao).First())
			.ToList();
	}

	private static string NormalizarLinha(string linha)
	{
		linha = linha.Replace("\n", " ").Replace("\r", " ");
		linha = Regex.Replace(linha, @"\s+", " ").Trim();

		linha = Regex.Replace(linha, @"(\d{2}/\d{4})(\d)", "$1 $2");

		int tentativas = 0;
		while (Regex.IsMatch(linha, @"(\d+,\d{2})(\d+,\d{2})") && tentativas < 10)
		{
			linha = Regex.Replace(linha, @"(\d+,\d{2})(\d+,\d{2})", "$1 $2");
			tentativas++;
		}

		linha = Regex.Replace(linha, @"(\d+,\d{6,})(\d+,\d{2})", "$1 $2");

		linha = Regex.Replace(linha, @"(\d,\d{2})%", "$1 %");
		linha = Regex.Replace(linha, @"%(\d)", "% $1");

		return linha;
	}

	private static string ExtrairBlocoTabela(string texto)
	{
		var inicio = texto.IndexOf("BASE PARA OS SAL", StringComparison.OrdinalIgnoreCase);

		if (inicio < 0)
			return texto;

		return texto.Substring(inicio);
	}
}