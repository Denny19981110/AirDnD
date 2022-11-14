namespace Airbnb.ViewModel
{
    public class UserProfileViewModel
    {
        public bool IsLoggedIn { get; set; }
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public UserProfileData User { get; set; }

        public class UserProfileData
        {
            public int Id { get; set; }
            public string UserName { get; set; }
            public string UserEmail { get; set; }
            public string AboutMe { get; set; }
            public string Address { get; set; }
            public string Image { get; set; }
        }
    }
}
