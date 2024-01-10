using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminPanelModels
{
	public class MessageEF
	{
		public string Satus { get; set; }
		public string Msg { get; set; }
		public string Value { get; set; }
		public string Data { get; set; }
		public string UserName { get; set; }
		public string Password { get; set; }

		//---added by suresh
		public int? Id { get; set; }

		private static MessageEF instance = null;
		public static MessageEF GetInstance()
		{
			if (instance == null)
			{
				instance = new MessageEF();
			}
			return instance;
		}
	}
}
