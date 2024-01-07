using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationModels
{
	public class DSCResponse
	{
		public int DSCID { get; set; }

		public string Response { get; set; }
		public string DSCCommonName { get; set; }
		public string DSCSerialNo { get; set; }
		public string DSCIssuerCommonName { get; set; }
		public string DSCIssuedDate { get; set; }
		public string DSCExpiredDate { get; set; }
		public string DSCEmail { get; set; }
		public string DSCCountry { get; set; }
		public string DSCCertificateClass { get; set; }
		public string DSCFor { get; set; }

		public int DSCUsedBy { get; set; }
		public int DSCForId { get; set; }
	}
}
