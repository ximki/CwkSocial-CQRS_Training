﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cwk.Domain.Exceptions
{
    public class PostCommentNotValidException : NotValidException
    {

        public PostCommentNotValidException() { }
        public PostCommentNotValidException(string message) : base(message) { }
        public PostCommentNotValidException(string message, Exception inner) : base(message, inner) { }
    }
}
