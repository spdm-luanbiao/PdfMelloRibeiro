using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pdf2Xls.Models
{
	public class LinhaTabela2
	{
		public string Ocorrencia { get; set; } = string.Empty;
		public decimal SalarioPago { get; set; }
		public decimal SalarioDevido { get; set; }
		public decimal SalarioContribuicao { get; set; }
		public decimal Aliquota { get; set; }
		public decimal DevidoSegurado { get; set; }
		public decimal IndiceCorrecao { get; set; }
		public decimal ValorCorrigido { get; set; }
		public decimal Juros { get; set; }
		public decimal Multa { get; set; }
		public decimal Total { get; set; }
	}
}
