
using FluentValidation;

namespace QuizMaster.Core.Model
{
    public class TestCatalogeLecturer
    {
        public int Id { get; set; }
        public int TestCatalogId { get; set; }
        public int LecturerId { get; set; }
        public bool IsDeleTe { get; set; }
    }

    public class TestCatalogeLecturerValidator : AbstractValidator<TestCatalogeLecturer>
    {
        public TestCatalogeLecturerValidator()
        {
            RuleFor(x => x.Id).InclusiveBetween(1, int.MaxValue).WithMessage("Id must be greater than 0.");
            RuleFor(x => x.TestCatalogId).InclusiveBetween(1, int.MaxValue).WithMessage("Id must be greater than 0.");
            RuleFor(x => x.LecturerId).InclusiveBetween(1, int.MaxValue).WithMessage("Id must be greater than 0.");
        }
    }
}
