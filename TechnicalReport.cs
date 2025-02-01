namespace SDP_T01_Group06
{
	public class TechnicalReport : Document
	{
        public TechnicalReport(User owner) : base(owner)
        {
        }

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
