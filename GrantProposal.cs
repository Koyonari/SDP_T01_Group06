namespace SDP_T01_Group06
{
	public class GrantProposal : Document
	{
        public GrantProposal(User owner) : base(owner)
        {
        }
        public override void editDocument()
        {
            Console.WriteLine("What would you like to do?: ");
            Console.WriteLine("1. Create body");
            Console.WriteLine("2. Add budeget breakdown");
            Console.WriteLine("Your option: ");
            string option = Console.ReadLine();
            switch (option)
            {
                case "1":
                    createBody();
                    break;
                case "2":
                    addBudgetBreakdown();
                    break;
                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }
        }

        public override void createBody()
		{
			Console.WriteLine("Created Grant Proposal body");
		}

		public override void addBudgetBreakdown()
		{
			Console.WriteLine("Adding budget breakdown table...");
		}
	}
}
