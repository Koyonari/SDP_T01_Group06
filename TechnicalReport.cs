﻿using SDP_T01_Group06.Composite;
using SDP_T01_Group06.Memento;

namespace SDP_T01_Group06
{
	public class TechnicalReport : Document
	{


        public TechnicalReport(User owner) : base(owner)
        {
        }

        public override void editDocument()
        {
            bool validOption = false;

            while (!validOption)
            {
                selectSection(rootsection, 1);
                Console.WriteLine("What would you like to do?: ");
                Console.WriteLine("1. Add paragraph");
                Console.WriteLine("2. Add code snippet");
                Console.WriteLine("0. Exit");
                Console.Write("Your option: ");
                string option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        addParagraph();
                        validOption = true;
                        break;
                    case "2":
                        addCodeSnippet();
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
            mainSection.add(new DocumentItem("Technical Overview", "Heading"));
            Console.WriteLine("Technical Report body added to document");
        }

        public void addCodeSnippet()
        {
            var codeSection = new DocumentSection("Code Section");
            Console.Write("Enter code snippet: ");
            string code = Console.ReadLine();
            codeSection.add(new DocumentItem(code, "Code"));
            var mainContent = rootsection.getChild(1); // get body section to add component in
            mainContent.add(codeSection);
			Console.WriteLine("Code Snippet added to document");
		}

        //public override DocumentMemento save()
        //{
        //    DocumentSection rootSectionClone = this.rootsection?.Clone() as DocumentSection;
        //    DocumentSection currentSectionClone = this.currentSection?.Clone() as DocumentSection;
        //    return new DocumentMemento(this.documentName, rootSectionClone, currentSectionClone, this.currentState, isEdited);
        //}
    }
}
