
using FluentValidation;
using System.Security.Cryptography.X509Certificates;

namespace QuizMaster.Core.Model
{
    public class Student : Person
    {
        //public int Id { get; set; }
        public double Grade { get; set; }
        //public bool IsDelete { get; set; } = false;
        //public List<Subject> Subjects { get; set; }

       public int CompareTo(Student? other)
        {
            if (other == null) return 1;
            return this.Grade.CompareTo(other.Grade);
        }

        public override string? ToString()
        {
            //, subject: { string.Join(",", Subjects) }
            return $"Id: {Id}, FirsName: {FirsName}, Lastname: {Lastname}, Email: {Email}, PhoneNumber: {PhoneNumber}, PhoneNumber: {PhoneNumber}, Password: {Password}, VerificationCode: {VerificationCode}, IsVerified: {IsVerified}, Role: {Role}, Gender: {Gender}, Grade: {Grade}";
        }
    }

    public class StudentValidator : AbstractValidator<Student>
    {
        public StudentValidator()
        {
            //RuleFor(student => student.Id).InclusiveBetween(1, int.MaxValue).WithMessage("Id must be greater than 0.");
            //RuleFor(student => student.Subjects)
            //    .NotNull()
            //    .WithMessage("Last name is required.")
            //    .Must(x => x.Any())
            //    .WithMessage("At least one Subject must be specified.");

            //RuleForEach(student => student.Subjects).ChildRules(subject =>
            //    {
            //    subject.RuleFor(x => x.SabjectName)
            //           .NotEmpty()
            //           .Matches(@"[a-zA-Z]")
            //           .WithMessage("Subject-ის სახელი უნდა შეიცავდეს მხოლოდ ასოებს.");
            //});


        }
    }
}
