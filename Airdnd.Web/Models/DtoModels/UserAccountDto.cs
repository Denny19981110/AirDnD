namespace Airdnd.Web.Models.DtoModels
{
    public class UserAccountDto
    {
        //Dto與一般Model不同是如果要新增，在DTO加額外抓取的東西，類似自訂義
        //for UserAccountpageIndex
        public int Id { get; set; } 
        public string UserName { get; set; }
        public string Email { get; set; }

        

        //給profile用的
        public string AboutMe { get; set; }
        public string Address { get; set; }
        public string Image { get; set; }

    }
}
