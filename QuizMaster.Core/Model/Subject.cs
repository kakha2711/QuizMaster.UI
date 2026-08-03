
using FluentValidation;

namespace QuizMaster.Core.Model
{
    public class Subject
    {
        public int Id { get; set; }
        public string SabjectName { get; set; }
        public string Desctiption { get; set; }
        public bool IsDelete { get; set; } = false;
    }

    public class SubjectValidator : AbstractValidator<Subject>
    {
        public SubjectValidator()
        {
            RuleFor(x => x.Id).InclusiveBetween(1, int.MaxValue).WithMessage("Id must be greater than 0.");
            RuleFor(subject => subject.SabjectName)
                .NotEmpty().WithMessage("Subject name is required.")
                .MaximumLength(50).WithMessage("Subject name cannot exceed 100 characters.");
            RuleFor(subject => subject.Desctiption)
                .NotEmpty().WithMessage("Desctiption name is required.")
                .MaximumLength(200).WithMessage("Description cannot exceed 200 characters.");
        }
    }
}
