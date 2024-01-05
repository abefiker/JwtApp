namespace JwtApp.Models
{
    public class UserConstants
    {
        public static List<UserModel> Users = new List<UserModel>()
        {
            new UserModel()
            {
                UserName = "Abemelek",Password="@bedu",EmailAdress="kidabemelek@gmail.com",
                Role="Administor",Surname="Amare",GivenName="Abutunut"
            },
            new UserModel()
            {
                UserName = "Abrsh",Password="@brsh",EmailAdress="abrsh6266@gmail.com",
                Role="Seller",Surname="Antennneh",GivenName="Abxo9"
            }
        };
    }
}
