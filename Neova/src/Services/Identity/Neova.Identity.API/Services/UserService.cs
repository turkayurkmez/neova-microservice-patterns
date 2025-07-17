using Neova.Identity.API.Domains;

namespace Neova.Identity.API.Services
{
    public class UserService
    {
        public User ValidateUser(string username, string password)
        {
            // Burada kullanıcı doğrulama işlemi yapılabilir.
            // Örnek olarak basit bir kontrol yapıyoruz.
            // Burada veritabanından kullanıcı bilgilerini çekebilirsiniz....


            if (username == "turkay" && password == "123")
            {
                return new User
                {
                    Id = "1",
                    UserName = "turkay",
                    Password = "123",
                    Email = ""
                };
            }

            return null;
        }
    }
}
