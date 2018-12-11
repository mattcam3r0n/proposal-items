using System;
using System.Collections.Generic;

namespace proposal_items.Models
{
    public class Proposal
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public List<ProposalItem> ProposalItems { get; set; }

    }
}
