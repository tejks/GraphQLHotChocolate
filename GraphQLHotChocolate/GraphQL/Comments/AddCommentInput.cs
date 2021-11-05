using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLHotChocolate.GraphQL.Comments
{
    public record AddCommentInput(Guid UserId, Guid PostId, string Title);
}
