using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDP_T01_Group06
{
	public class TechnicalReport : Document
	{
		public override void createBody()
		{
			Console.WriteLine("Created Technical Report body");
		}

		public override void addCodeSnippet()
		{
			Console.WriteLine("Adding code snippet...");    
		}
	}
}
