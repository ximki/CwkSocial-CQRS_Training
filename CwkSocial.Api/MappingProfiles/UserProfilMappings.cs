namespace CwkSocial.Api.MappingProfiles
{
    public class UserProfilMappings:Profile
    {
        public UserProfilMappings()
        {
            
            CreateMap<UserProfileCreateUpdate, UpdateUserProfileBasicInfo>();
            CreateMap<UserProfile, UserProfileResponse>();
            CreateMap<BasicInfo, BasicInformation>();
        }
    }
}
