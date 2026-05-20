using CSharpFunctionalExtensions;

namespace PetFamily.Domain.Shared.ValueObjects
{
    public sealed class SocialNetwork : ComparableValueObject
    {
        public string Link { get; }
        public string Title { get; }

        private SocialNetwork(string link, string title)
        {
            Link = link;
            Title = title;
        }
        
        public static Result<SocialNetwork, Error> Create(string link, string title)
        {
            if (string.IsNullOrWhiteSpace(link))
                return Error.Validation("social_network.link.empty", $"{nameof(Link)} is not be empty");

            if (string.IsNullOrWhiteSpace(title))
                return Error.Validation("social_network.title.empty", $"{nameof(title)} is not be empty");

            return new SocialNetwork(link, title);
        }
        protected override IEnumerable<IComparable> GetComparableEqualityComponents()
        {
            yield return Link;
            yield return Title;
        }
    }
}
