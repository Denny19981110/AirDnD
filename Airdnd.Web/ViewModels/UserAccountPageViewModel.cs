using Airbnb.ViewData;

namespace Airbnb.ViewModel
{
    public class UserAccountPageViewModel
    {
        public bool IsLoggedIn { get; set; }
        public UserAccountPageData User { get; set; }

        public class UserAccountPageData
        {
            public int Id { get; set; }
            public string UserName { get; set; }
            public string UserEmail { get; set; }
            public string Image { get; set; }
        }
    }
}
