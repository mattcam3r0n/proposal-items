using System.Collections.Generic;
using proposal_items.Models;

namespace proposal_items.ViewModels
{
    // a view model to be used with the AddProposalItem view
    // this one might not be necessary... a ProposalItem model
    // might be sufficient.
    public class AddProposalItem
    {
        public int ProposalId { get; set; }

        public int ImageId { get; set; }
        
        public Image Image { get; set; }

        public string Caption { get; set; }
    }
}