using GraphQLHotChocolate.Models;
using HotChocolate;
using HotChocolate.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLHotChocolate.GraphQL
{
    public class Subscription
    {
        [Subscribe]
        [Topic]
        [GraphQLDescription("The subscription for added platform.")]
        public Post OnPostAdded([EventMessage] Post post)
        {
            return post;
        }

        [Subscribe]
        [Topic]
        [GraphQLDescription("The subscription for added comment.")]
        public Comment OnCommentAdded([EventMessage] Comment comment)
        {
            return comment;
        }

    }
}
