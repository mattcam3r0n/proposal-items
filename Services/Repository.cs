using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using proposal_items.Models;

namespace proposal_items.Services
{
    // a fake repository, just for testing. does not use EF.
    public class Repository //: IRepository
    {
        private readonly List<Proposal> _proposals;

        private readonly List<Image> _images;

        public Repository()
        {
            _proposals = CreateProposalTestData();
            _images = CreateImageTestData();
        }

        public IQueryable<Proposal> Proposals => _proposals.AsQueryable();

        public IQueryable<Image> Images => _images.AsQueryable();
        
        public void AddProposalItem(int proposalId, ProposalItem proposalItem) {
            // get the proposal that we're going to add the item to
            var proposal = this.Proposals.FirstOrDefault(p => p.Id == proposalId);
            // IGNORE this next line.  It's just manually attaching the actual image object
            // because I'm using a fake repository. EF would do this for you based on
            // the ImageId.
            proposalItem.Image = _images.FirstOrDefault(i => i.Id == proposalItem.ImageId);
            // Add the new item to the proposal.  Possibly SaveChanges() here, if using EF.
            proposal.ProposalItems.Add(proposalItem);
        }

        static List<Proposal> CreateProposalTestData()
        {
            return new List<Proposal>() {
                new Proposal{
                    Id = 1,
                    Name = "Proposal 1",
                    Description = "Proposal 1",
                    ProposalItems = new List<ProposalItem> {
                        new ProposalItem {
                            Id = 1,
                            ImageId = 1,
                            Image = new Image {
                                Id = 1,
                                Filename = "IMG_4580.jpg"
                            },
                            Caption = "Test Image 1",
                        }
                    }
                }
            };
        }

        static List<Image> CreateImageTestData()
        {
            return new List<Image> {
                new Image {
                    Id = 1,
                    Filename = "IMG_4580.jpg"
                },
                new Image {
                    Id = 2,
                    Filename = "IMG_4597.jpg"
                },
                new Image {
                    Id = 3,
                    Filename = "IMG_4599.jpg"
                },
            };
        }
    }
}
