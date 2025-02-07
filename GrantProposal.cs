﻿using SDP_T01_Group06.Composite;

namespace SDP_T01_Group06
{
    public class GrantProposal : Document
	{
        public GrantProposal(User owner) : base(owner)
        {
        }
        public override void editDocument()
        {
            bool validOption = false; // Flag to control the loop

            while (!validOption) // Loop until a valid option is entered
            {
                selectSection(rootsection, 1);
                Console.WriteLine("What would you like to do?: ");
                Console.WriteLine("1. Add paragraph");
                Console.WriteLine("2. Add budget breakdown");
                Console.Write("Your option: ");
                string option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        addParagraph();
                        validOption = true; // Set flag to true to exit the loop
                        break;
                    case "2":
                        addBudgetBreakdown();
                        validOption = true; // Set flag to true to exit the loop
                        break;
                    case "0":
                        return; // Exit the entire editDocument function
                    default:
                        Console.WriteLine("Invalid option.");
                        break; // Loop continues because validOption is still false
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

        public override void addBudgetBreakdown()
        {
            DocumentSection budgetSection = new DocumentSection("Budget Section");
            Console.Write("Enter budget details: ");
            string budget = Console.ReadLine();
            budgetSection.add(new DocumentItem(budget, "Budget"));
            DocumentComponent mainContent = rootsection.getChild(1); // get body section to insert
            mainContent.add(budgetSection);
        }
    }
}