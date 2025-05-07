using Bogus;
using SignSafe.Domain.Entities;

namespace Tests.Fakers.Users
{
    public class UserFaker : Faker<User>
    {
        public UserFaker()
        {
            CustomInstantiator(f => new User(
                f.Person.FullName,
                f.Person.Email,
                f.Person.UserName,
                f.Person.DateOfBirth,
                f.Person.Phone
                ));
            //RuleFor(x => x.Id, f => FakerExtensions.GetId());
            //RuleFor(x => x.Name, f => f.Person.FullName);
            //RuleFor(x => x.Email, f => f.Person.Email);
            //RuleFor(x => x.Password, f => f.Person.UserName);
            //RuleFor(x => x.PhoneNumber, f => f.Person.Phone);
            //RuleFor(x => x.BirthDate, f => f.Person.DateOfBirth);
            //RuleFor(x => x.Roles, f => UserRoles.Standard.GetDescription());
            //RuleFor(x => x.Deleted, f => false);
        }
    }
}