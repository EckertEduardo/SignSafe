using MediatR;
using SignSafe.Domain.Exceptions;
using SignSafe.Infrastructure.UoW;

namespace SignSafe.Application.Users.Commands.ResetPassword
{
    public sealed class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ResetPasswordCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }
        public async Task Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UserRepository.GetByEmail(request.Email)
                ?? throw new NotFoundException("Email", request.Email);

            user.UpdatePassword(request.OtpVerificationCode, request.NewPassword);
            _unitOfWork.UserRepository.Update(user);
            await _unitOfWork.Commit();
        }
    }
}
