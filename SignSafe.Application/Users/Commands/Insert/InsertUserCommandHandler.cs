using FluentValidation;
using FluentValidation.Results;
using MediatR;
using SignSafe.Domain.Entities;
using SignSafe.Infrastructure.UoW;

namespace SignSafe.Application.Users.Commands.Insert
{
    public class InsertUserCommandHandler : IRequestHandler<InsertUserCommand, InsertUserCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public InsertUserCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<InsertUserCommandResponse> Handle(InsertUserCommand request, CancellationToken cancellationToken)
        {
            var userAlreadyExist = await _unitOfWork.UserRepository.GetByEmail(request.Email);
            if (userAlreadyExist != null)
            {
                var errors = new List<ValidationFailure>
                {
                    new ValidationFailure
                    {
                        PropertyName = "Email",
                        ErrorMessage = string.Format("User already exist for this email {0}", request.Email)
                    }
                };
                throw new ValidationException(errors);
            }

            var user = new User(
                name: request.Name,
                email: request.Email,
                password: request.Password,
                birthDate: request.BirthDate,
                phoneNumber: request.PhoneNumber);

            await _unitOfWork.UserRepository.Insert(user);
            await _unitOfWork.Commit();
            return new InsertUserCommandResponse(user.Id);
        }
    }
}
