namespace RockSchool.WebApi.Options;

public class AuthBehaviorOptions
{
    public const string SectionName = "Auth";

    public bool UseAuthorization { get; set; } = true;
}
