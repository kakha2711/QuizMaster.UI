
using FluentValidation;

namespace QuizMaster.Core.Model
{
    public class Lecturer :Person
    {
        //public int Id { get; set; }
        //public bool IsDelete { get; set; } = false;

        //public Subject Subject { get; set; }

        public override string? ToString()
        {
            //, subject: { Subject}
            return $"Id: {Id}, FirsName: {FirsName}, Lastname: {Lastname}, Email: {Email}, PhoneNumber: {PhoneNumber}, PhoneNumber: {PhoneNumber}, Password: {Password}, VerificationCode: {VerificationCode}, IsVerified: {IsVerified}, Role: {Role}, Gender: {Gender}";
        }
    }

    public class LecturerValidator : AbstractValidator<Lecturer>
    {
        public LecturerValidator()
        {
            //RuleFor(x => x.Id).InclusiveBetween(1, int.MaxValue).WithMessage("Id must be greater than 0.");
            //RuleFor(x => x.Subject).NotNull().WithMessage("Subject is required.");
        }
    }
}
