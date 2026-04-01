using CSharpFunctionalExtensions;

namespace PetFamily.Domain.Common.ValueObjects
{
    public record PetStatus
    {
        public Status Value { get; }

        private PetStatus(Status value) 
        {
            Value = value;
        }
        
        public static Result<PetStatus> Create(Status value)
        {
            return Result.Success(new PetStatus(value));
        }
        public enum Status
        {
            NeedHelp = 1,
            HomeSeeking = 2,
            FoundHome = 3
        }
    }
}
