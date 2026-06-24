namespace PetFamily.Contracts.Volunteers;

public sealed record CreateVolunteerRequest(
    string FullName,
    string Email,
    string Description,
    string Phone,
    int ExperienceInYears,
    IEnumerable<RequisiteDto> Requisites,
    IEnumerable<SocialNetworkDto> SocialNetworks);

public sealed record RequisiteDto(string Name, string Description);

public sealed record SocialNetworkDto(string Link, string Title);
