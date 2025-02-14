using MediatR;
using SignSafe.Application.Users.Dtos;
using SignSafe.Data.UoW;

namespace SignSafe.Application.Users.Queries.Get
{
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserDto?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetUserQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<UserDto?> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UserRepository.Get(request.UserId);
            if (user == null)
                return null;

            return new UserDto(user);
        }
    }
}
