using SDP_T01_Group06.Composite;
using SDP_T01_Group06.Memento;

namespace SDP_T01_Group06
{
    public class GrantProposal : Document
	{

        public GrantProposal(User owner) : base(owner)
        {
        }
        public override void editDocument()
        {
            bool validOption = false;
            while (!validOption)
            {
                //Console.WriteLine("\n=== Current Document State ===");
                //Console.WriteLine($"Root section children count: {rootsection.children.Count}");
                //Console.WriteLine($"Current section: {(currentSection?.SectionName ?? "null")}");
                //if (currentSection != null)
                //{
                //    Console.WriteLine("Current section children:");
                //    foreach (var child in currentSection.children)
                //    {
                //        if (child is DocumentSection ds)
                //            Console.WriteLine($"- Section: {ds.SectionName}");
                //        else if (child is DocumentItem di)
                //            Console.WriteLine($"- Item: {di.Content}");
                //    }
                //}
                //Console.WriteLine($"History count: {history.GetMementos().Count}");
                //Console.WriteLine("========================\n");

                bool exit = selectSection(rootsection, 1);
                if (exit)
                {
                    validOption = true;
                    return;
                }
                Console.WriteLine("What would you like to do?: ");
                Console.WriteLine("1. Add paragraph");
                Console.WriteLine("2. Add budget breakdown");
                Console.Write("Your option: ");
                string option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        //Console.WriteLine("\n=== Before Creating Paragraph Memento ===");
                        DocumentMemento pSnapshot = createMemento();
                        //Console.WriteLine($"Memento created with root children count: {pSnapshot.RootSectionClone.children.Count}");
                        history.AddMemento(pSnapshot);

                        //Console.WriteLine("\n=== Adding Paragraph ===");
                        addParagraph();

                        //Console.WriteLine("\n=== After Adding Paragraph ===");
                        //Console.WriteLine($"Current section children count: {currentSection.children.Count}");
                        break;

                    case "2":
                        //Console.WriteLine("\n=== Before Creating Budget Memento ===");
                        DocumentMemento bSnapshot = createMemento();
                        //Console.WriteLine($"Memento created with root children count: {bSnapshot.RootSectionClone.children.Count}");
                        history.AddMemento(bSnapshot);

                        //Console.WriteLine("\n=== Adding Budget ===");
                        addBudgetBreakdown();

                        //Console.WriteLine("\n=== After Adding Budget ===");
                        //Console.WriteLine($"Current section children count: {currentSection.children.Count}");
                        break;

                    case "0":
                        validOption = true;
                        break;
                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }
            }
        }

        public override void createBody()
		{
			var mainSection = new DocumentSection("Main Content");
			rootsection.add(mainSection);
			mainSection.add(new DocumentItem("Proposal Overview", "Heading"));
			Console.WriteLine("Grant Proposal body added to document");
		}

        public void addBudgetBreakdown()
        {
            DocumentSection budgetSection = new DocumentSection("Budget Section");
            Console.Write("Enter budget details: ");
            string budget = Console.ReadLine();
            budgetSection.add(new DocumentItem(budget, "Budget"));
            DocumentComponent mainContent = rootsection.getChild(1); // get body section to insert
            mainContent.add(budgetSection);
			Console.WriteLine("Budget Breakdown added to document");
		}

        //public override DocumentMemento createMemento()
        //{
        //    //Console.WriteLine("In gp.cs:" + this.rootsection.SectionName);
        //    DocumentSection rootSectionClone = this.rootsection?.Clone() as DocumentSection;
        //    //Console.WriteLine("in gp.cs222: "+rootSectionClone.SectionName);
        //    DocumentSection currentSectionClone = this.currentSection?.Clone() as DocumentSection;
        //    return new DocumentMemento(this.documentName, rootSectionClone, currentSectionClone, this.currentState, isEdited);
        //}
    }
}