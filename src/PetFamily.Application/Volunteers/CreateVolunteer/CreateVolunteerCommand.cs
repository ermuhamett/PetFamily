namespace PetFamily.Application.Volunteers.CreateVolunteer;

public sealed record CreateVolunteerCommand(
    string FullName,
    string Email,
    string Description,
    string Phone,
    int ExperienceInYears,
    IReadOnlyList<RequisiteDto>? Requisites,
    IReadOnlyList<SocialNetworkDto>? SocialNetworks);

public sealed record RequisiteDto(string Name, string Description);

public sealed record SocialNetworkDto(string Link, string Title);
