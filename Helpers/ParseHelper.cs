using System.Globalization;

namespace Pdf2Xls.Helpers;

public static class ParseHelper
{
	public static decimal ParseDecimal(string valor)
	{
		if (string.IsNullOrWhiteSpace(valor)) return 0;

		valor = valor.Replace(".", "").Replace(",", ".");
		return decimal.TryParse(valor, NumberStyles.Any, CultureInfo.InvariantCulture, out var result) ? result : 0;
	}

	public static decimal ParsePercent(string valor)
	{
		valor = valor.Replace("%", "").Trim();
		return ParseDecimal(valor);
	}
}