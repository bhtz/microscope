using MediatR;

namespace Microscope.Application.Features.Common;

public interface ICommand<T> : IRequest<T>
{

}