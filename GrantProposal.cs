﻿namespace SDP_T01_Group06
{
	public class GrantProposal : Document
	{
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
