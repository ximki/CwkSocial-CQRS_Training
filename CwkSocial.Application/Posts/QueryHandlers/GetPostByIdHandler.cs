﻿using Cwk.Domain.Aggregates.PostAggregate;
using CwkSocial.Application.Enums;
using CwkSocial.Application.Models;
using CwkSocial.Application.Posts.Queries;
using CwkSocial.Dal;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CwkSocial.Application.Posts.QueryHandlers
{
    public class GetPostByIdHandler : IRequestHandler<GetPostById, OperationResult<Post>>
    {
        DataContext _ctx;
        public GetPostByIdHandler(DataContext ctx)
        {
            _ctx = ctx;
        }
        public async Task<OperationResult<Post>> Handle(GetPostById request, CancellationToken cancellationToken)
        {
            var result=new OperationResult<Post>();
            var post = await _ctx.Posts.FirstOrDefaultAsync(p => p.PostId == request.PostId);
            if (post is null)
            {
                result.AddError(ErrorCode.NotFound, string.Format(PostsErrorMessages.PostNotFoundMessage, request.PostId));
                return result;
            }
            result.Payload = post;
            return result;
        }
    }
}