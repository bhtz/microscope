using System;
using System.Collections.Generic;
using MediatR;

namespace Microscope.Application.Features.Storage.Queries
{
    public class FilteredContainerQuery : IRequest<IEnumerable<FilteredContainerQueryResult>>
    {
        
    }

    public class FilteredContainerQueryResult 
    {
        public string Name { get; set; }

        public FilteredContainerQueryResult(string name)
        {
            this.Name = name;
        }
    }
}
