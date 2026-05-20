using PetFamily.Contracts.Volunteers;

namespace PetFamily.Application.Volunteers.CreateVolunteer;

public sealed record CreateVolunteerCommand(
    string FullName,
    string Email,
    string Description,
    string Phone,
    int ExperienceInYears,
    IEnumerable<RequisiteDto> Requisites,
    IEnumerable<SocialNetworkDto> SocialNetworks)
{
    public static CreateVolunteerCommand FromRequest(CreateVolunteerRequest request) =>
        new(
            request.FullName,
            request.Email,
            request.Description,
            request.Phone,
            request.ExperienceInYears,
            request.Requisites,
            request.SocialNetworks);
}
