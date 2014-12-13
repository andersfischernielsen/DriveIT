using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Results;
using DriveIT.Models;

namespace DriveIT.Web.Models
{
    public class CarCommentViewModel
    {
        public CarDto Car;
        public IEnumerable<CommentDto> Comments;
        public IEnumerable<ContactRequestDto> ContactRequest;
    }
}