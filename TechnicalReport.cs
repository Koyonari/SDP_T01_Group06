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
			Console.WriteLine("1. Create body");
            Console.WriteLine("2. Add code snippet");
			Console.WriteLine("Your option: ");
            string option = Console.ReadLine();
            switch (option)
            {
                case "1":
                    createBody();
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
			Console.WriteLine("Adding code snippet...");    
		}
	}
}
