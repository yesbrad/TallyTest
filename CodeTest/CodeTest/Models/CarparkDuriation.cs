using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeTestTallyIT.Models
{
	public class CarparkDuriation
	{
		public string entry { private get; set; }
		public string exit { private get; set; }

		public DateTime EntryTime { get { return DateTime.Parse(entry); } }
		public DateTime ExitTime { get { return DateTime.Parse(exit); } }
	}

}
