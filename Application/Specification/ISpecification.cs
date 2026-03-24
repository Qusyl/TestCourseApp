using System.Linq.Expressions;

namespace Application.Specification
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> Filter { get; }
    }
}