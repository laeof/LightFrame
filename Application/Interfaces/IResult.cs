using Application.Common;

namespace Application.Interfaces;

public interface IResult<T>
{
    T Value { get; }
    Error Error { get; }
    bool IsSuccess { get; }
    bool IsFailure { get; }
}