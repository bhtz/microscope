using MediatR;

namespace Microscope.Application.Features.Common;

public interface IQuery<T> : IRequest<T>
{

}