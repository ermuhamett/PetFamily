namespace PetFamily.Domain.Shared.ValueObjects;

public sealed class SocialNetworkDetails()
{
    public List<SocialNetwork> SocialNetworks { get; private set; } = [];
}
