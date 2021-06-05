using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microscope.Application.Features.Storage.Queries;
using Microscope.Domain.Services;

namespace Microscope.Application.QueryHandlers.Storage
{
    public class FilteredContainerQueryHandler : IRequestHandler<FilteredContainerQuery, IEnumerable<FilteredContainerQueryResult>>
    {
        private readonly IStorageService _storageService;

        public FilteredContainerQueryHandler(IStorageService storageService)
        {
            _storageService = storageService;
        }

        public async Task<IEnumerable<FilteredContainerQueryResult>> Handle(FilteredContainerQuery request, CancellationToken cancellationToken)
        {
            var results = await this._storageService.ListContainersAsync();

            return results
                .Select(x => new FilteredContainerQueryResult(x));
        }
    }
}
