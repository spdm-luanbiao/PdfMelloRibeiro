using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pdf2Xls.Models
{
	public class ResultadoParse
	{
		public List<LinhaTabela> Tabela1 { get; set; } = new();
		public List<LinhaTabela2> Tabela2 { get; set; } = new();
	}
}
