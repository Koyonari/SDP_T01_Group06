using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDP_T01_Group06
{
	public abstract class Document
	{
		public void assembleDocument()
		{
			addHeader();
			addFooter();
			createBody();
		}

		public void addHeader()
		{
			Console.WriteLine("Header added to document");
		}
		public void addFooter()
		{
			Console.WriteLine("Footer added to document");
		}

		public abstract void createBody();
	}
}
