using Cwk.Domain.Aggregates.PostAggregate;
using CwkSocial.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CwkSocial.Application.Posts.Commands
{
    public class UpdatePostText:IRequest<OperationResult<Post>>
    {
        public string NewText { get; set; }
        public Guid PostId { get; set; }   
        public Guid UserProfileId { get; set; }

    }
}
