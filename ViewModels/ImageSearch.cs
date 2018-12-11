using System.Collections.Generic;
using proposal_items.Models;

namespace proposal_items.ViewModels
{
    // a view model to be used by the ImageSearch view/controller action.
    // holds the proposal id, search text, and the images that match the search text.
    public class ImageSearch
    {
        public int ProposalId { get; set; }
        public string SearchString { get; set; }

        public IEnumerable<Image> Images { get; set; }
    }
}