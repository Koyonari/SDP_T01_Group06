namespace SDP_T01_Group06
{
	public class TechnicalReport : Document
	{


        public TechnicalReport(User owner) : base(owner)
        {
        }

        public override void editDocument()
        {
			Console.WriteLine("What would you like to do?: ");
			Console.WriteLine("1. Add paragraph");
            Console.WriteLine("2. Add code snippet");
			Console.WriteLine("Your option: ");
            string option = Console.ReadLine();
            switch (option)
            {
                case "1":
                    addParagraph();
                    break;
                case "2":
                    addCodeSnippet();
                    break;
                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }
        }

        public override void createBody()
		{
			Console.WriteLine("Created Technical Report body");
		}

		public override void addCodeSnippet()
		{
			Console.Write("Enter code to put in snippet: ");
			string code = Console.ReadLine();
			documentcontent += "\nCode Snippet\n" + code;
		}
	}
}
