
using System.Linq.Expressions;


namespace Application.Specification
{
    public abstract class Specification<T> : ISpecification<T>
    {
       public Expression<Func<T,  bool>> Filter { get; protected set; }
    }
}
