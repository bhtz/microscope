using GraphQL;
using GraphQL.Types;
using GraphQL.Utilities;
using System;

namespace Microscope.GraphQL
{
    public class MicroscopeSchema : Schema
    {
        public MicroscopeSchema(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            Query = serviceProvider.GetRequiredService<RootQuery>();
            Mutation = serviceProvider.GetRequiredService<RootMutation>();
        }
    }
}