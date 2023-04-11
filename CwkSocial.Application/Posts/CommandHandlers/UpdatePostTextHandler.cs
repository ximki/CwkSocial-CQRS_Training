using Cwk.Domain.Aggregates.PostAggregate;
using Cwk.Domain.Exceptions;
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
    public class UpdatePostTextHandler : IRequestHandler<UpdatePostText, OperationResult<Post>>
    {
        private readonly DataContext _ctx;
        public UpdatePostTextHandler(DataContext ctx)
        {
            _ctx = ctx;
        }
        public async Task<OperationResult<Post>> Handle(UpdatePostText request, CancellationToken cancellationToken)
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
                    result.AddError(ErrorCode.UserProfileNotMatchForAction,string.Format(PostsErrorMessages.UserProfileNotMatchForActionMessage,"updates"));
                    return result;
                }
                post.UpdatePostText(request.NewText);

                await _ctx.SaveChangesAsync(cancellationToken);
                result.Payload = post;
               
            }
            catch (PostNotValidException ex)
            {
                ex.ValidationErrors.ForEach(e => result.AddError(ErrorCode.ValidationError, e)); 
            }
            catch (Exception ex)
            {
                result.AddUnknownError(ex.Message);
            }
            return result;

        }
    }
}
