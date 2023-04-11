using Cwk.Domain.Aggregates.PostAggregate;
using CwkSocial.Application.Enums;
using CwkSocial.Application.Models;
using CwkSocial.Application.Posts.Commands;
using CwkSocial.Dal;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CwkSocial.Application.Posts.CommandHandlers
{
    public class DeletePostHandler : IRequestHandler<DeletePost, OperationResult<Post>>
    {
        private readonly DataContext _ctx;
        public DeletePostHandler(DataContext ctx)
        {
            _ctx = ctx;
        }
        public async Task<OperationResult<Post>> Handle(DeletePost request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<Post>();
            try
            {
                var post = await _ctx.Posts.FirstOrDefaultAsync(p => p.PostId == request.PostId,cancellationToken);
                if (post is null)
                {
                    result.AddError(ErrorCode.NotFound,string.Format(PostsErrorMessages.PostNotFoundMessage,request.PostId));
                    return result;
                }
                if (post.UserProfileId!=request.UserProfileId)
                {
                    result.AddError(ErrorCode.UserProfileNotMatchForAction, string.Format(PostsErrorMessages.UserProfileNotMatchForActionMessage, "deletes"));
                    return result;
                }
                _ctx.Posts.Remove(post);
                await _ctx.SaveChangesAsync(cancellationToken);
                result.Payload = post;
            }

            catch (Exception ex)
            {
                result.AddUnknownError(ex.Message);
            }
            return result;

        }
    }
}
