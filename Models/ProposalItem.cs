using System;

namespace proposal_items.Models
{
    public class ProposalItem
    {
        public int Id { get; set; }

        public int ProposalId { get; set; }

        public Proposal Proposal { get; set; }

        public int ImageId { get; set; }

        public Image Image { get; set; }
        
        public string Caption { get; set; }



    }
}
