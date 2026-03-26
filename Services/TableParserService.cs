using System.Text.RegularExpressions;
using Pdf2Xls.Helpers;
using Pdf2Xls.Models;

namespace Pdf2Xls.Services;

public class TableParserService
{
	public List<LinhaTabela> Parse(string texto)
	{
		var resultado = new List<LinhaTabela>();

		texto = ExtrairBlocoTabela(texto);
		texto = texto.Replace("INDICAÇÃO DE PARCELAS", "\nINDICAÇÃO DE PARCELAS");

		var linhas = Regex.Split(texto, @"(?=\d{2}/\d{4})")
			.Select(x => x.Trim())
			.Where(x => !string.IsNullOrWhiteSpace(x))
			.ToList();

		foreach (var linhaOriginal in linhas)
		{
			var linha = linhaOriginal;

			if (!Regex.IsMatch(linha, @"^\d{2}/\d{4}"))
				continue;

			var antesLimpeza = linha;

			linha = Regex.Replace(linha, @"^(\d{2}/\d{4})\s+\1", "$1");
			linha = Regex.Replace(linha, @"\b\d+,\s+\d{2}/\d{4}", "");
			linha = Regex.Replace(linha, @"(\d+,\d+)\s+0{6,}", "$1");
			linha = Regex.Replace(linha, @"(\d+,\d+)\s+\1", "$1");

			linha = NormalizarLinha(linha);

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

	public List<LinhaTabelaResumo> ParseTabelaResumo(string texto)
	{
		var resultado = new List<LinhaTabelaResumo>();

		var bloco = ExtrairBlocoTabelaResumo(texto);

		if (string.IsNullOrWhiteSpace(bloco))
			return resultado;

		bloco = bloco.Replace("TOTAL", "\nTOTAL");
		bloco = Regex.Replace(bloco, @"INDICAÇÃO DE PARCELAS.*?Contribuição Social sobre Salários Devidos", "", RegexOptions.IgnoreCase | RegexOptions.Singleline);

		var linhas = Regex.Split(bloco, @"(?=\d{2}/\d{4})")
			.Select(x => x.Trim())
			.Where(x => !string.IsNullOrWhiteSpace(x))
			.ToList();

		foreach (var linhaOriginal in linhas)
		{
			if (!Regex.IsMatch(linhaOriginal, @"^\d{2}/\d{4}"))
				continue;

			var linha = linhaOriginal;

			linha = Regex.Replace(linha, @"^(\d{2}/\d{4})\s+\1", "$1");
			linha = Regex.Replace(linha, @"(\d{2}/\d{4})(\d{2}/\d{4})", "$1 $2");
			linha = Regex.Replace(linha, @"(\d+,\d{9})(\d+,\d{2})", "$1 $2");

			linha = NormalizarLinhaResumo(linha);

			var match = Regex.Match(linha, @"
				^(?<comp>\d{2}/\d{4})\s+
				(?<salPago>\d+,\d{2})\s+
				(?<salDevido>\d+,\d{2})\s+
				(?<salContrib>\d+,\d{2})\s+
				(?<aliq>\d+,\d{2})\s+%\s+
				(?<devido>\d+,\d{2})\s+
				(?<indice>\d+,\d{9})\s+
				(?<corrigido>\d+,\d{2})\s+
				(?<juros>\d+,\d{2})\s+
				(?<multa>-|\d+,\d{2})\s+
				(?<total>\d+,\d{2})
			", RegexOptions.IgnorePatternWhitespace);

			if (!match.Success)
				continue;

			resultado.Add(new LinhaTabelaResumo
			{
				Ocorrencia = match.Groups["comp"].Value,
				SalarioPago = ParseHelper.ParseDecimal(match.Groups["salPago"].Value),
				SalarioDevido = ParseHelper.ParseDecimal(match.Groups["salDevido"].Value),
				SalarioContribuicao = ParseHelper.ParseDecimal(match.Groups["salContrib"].Value),
				Aliquota = ParseHelper.ParseDecimal(match.Groups["aliq"].Value),
				DevidoSegurado = ParseHelper.ParseDecimal(match.Groups["devido"].Value),
				IndiceCorrecao = ParseHelper.ParseDecimal(match.Groups["indice"].Value),
				ValorCorrigido = ParseHelper.ParseDecimal(match.Groups["corrigido"].Value),
				Juros = ParseHelper.ParseDecimal(match.Groups["juros"].Value),
				Multa = match.Groups["multa"].Value == "-" ? 0 : ParseHelper.ParseDecimal(match.Groups["multa"].Value),
				Total = ParseHelper.ParseDecimal(match.Groups["total"].Value)
			});
		}

		return resultado
			.GroupBy(x => x.Ocorrencia)
			.Select(g => g.First())
			.ToList();
	}

	private static string ExtrairBlocoTabelaResumo(string texto)
	{
		var inicio = texto.IndexOf(
			"Ocorrência Salário Pago Salário Devido Salário de Contribuição",
			StringComparison.OrdinalIgnoreCase);

		if (inicio < 0)
		{
			inicio = texto.IndexOf(
				"OcorrênciaSalário Pago Salário Devido Salário de Contribuição",
				StringComparison.OrdinalIgnoreCase);
		}

		if (inicio < 0)
		{
			inicio = texto.IndexOf(
				"OcorrênciaSalário PagoSalário DevidoSalário de Contribuição",
				StringComparison.OrdinalIgnoreCase);
		}

		if (inicio < 0)
			return string.Empty;

		var resto = texto.Substring(inicio);

		var fim = resto.IndexOf("eSocial - Evento S-2500", StringComparison.OrdinalIgnoreCase);
		if (fim >= 0)
			resto = resto.Substring(0, fim);

		return resto;
	}

	private static string NormalizarLinhaResumo(string linha)
	{
		// =========================
		// LIMPEZA BÁSICA
		// =========================
		linha = linha.Replace("\n", " ").Replace("\r", " ");
		linha = Regex.Replace(linha, @"\s+", " ").Trim();

		// =========================
		// GARANTIR SEPARAÇÃO DATA
		// =========================
		linha = Regex.Replace(linha, @"(\d{2}/\d{4})(\d)", "$1 $2");

		// =========================
		// AJUSTAR % GRUDADO
		// =========================
		linha = Regex.Replace(linha, @"(\d,\d{2})%", "$1 %");
		linha = Regex.Replace(linha, @"%(\d)", "% $1");

		// =========================
		// SEPARAR ÍNDICE + VALOR
		// Ex: 1,0000000008,20
		// =========================
		linha = Regex.Replace(linha, @"(\d+,\d{6,9})(\d+,\d{2})", "$1 $2");

		// =========================
		// SEPARAR BLOCOS COLADOS
		// Ex: 8,206,54
		// =========================
		int tentativas = 0;
		while (Regex.IsMatch(linha, @"(\d+,\d{2})(\d+,\d{2})") && tentativas < 20)
		{
			linha = Regex.Replace(linha, @"(\d+,\d{2})(\d+,\d{2})", "$1 $2");
			tentativas++;
		}

		// =========================
		// TRATAR HÍFEN GRUDADO
		// Ex: 6,54-14,74
		// =========================
		linha = Regex.Replace(linha, @"(\d+,\d{2})-(\d+,\d{2})", "$1 - $2");

		// =========================
		// GARANTIR ESPAÇO ANTES DO "-"
		// Ex: 6,54-
		// =========================
		linha = Regex.Replace(linha, @"(\d+,\d{2})-", "$1 -");

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