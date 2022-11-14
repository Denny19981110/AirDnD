namespace Airdnd.ViewModel
{
    public class UserWishListViewModel
    {

        public bool IsLoggedIn { get; set; }
        public UserProfileData WishList { get; set; }

        public class UserProfileData
        {
            public int ListId { get; set; }
            public string ImgPath { get; set; }
            public string ListName { get; set; }
        }
    }
}
