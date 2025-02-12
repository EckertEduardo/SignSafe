using FluentValidation;
using MediatR;
using MediatR.Pipeline;

namespace SignSafe.Application.Behaviors
{
    public class ValidationPreProcessor<TRequest> : IRequestPreProcessor<TRequest>
        where TRequest : IRequest
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationPreProcessor(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public Task Process(TRequest request, CancellationToken cancellationToken)
        {
            var context = new ValidationContext<TRequest>(request);
            var failures = _validators
                .Select(v => v.Validate(context))
                .SelectMany(result => result.Errors)
                .Where(error => error != null)
                .ToList();

            if (failures.Count != 0)
            {
                throw new ValidationException(failures);
            }
            return Task.CompletedTask;
        }
    }

}
