

using Domain.Aggregate.User;


namespace Application.Specification
{
    public class GetUserByIdSpecification : Specification<User>
    {

        public GetUserByIdSpecification(Guid userId) {
            Filter = prod => prod.Id == userId; 
        }
    }
}
