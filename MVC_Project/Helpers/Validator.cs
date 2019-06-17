using MVC_Project.Models;

namespace MVC_Project.Helpers
{
    public static class Validator
    {
        public static bool Validate(this Product product)
        {
            return product != null
                && !string.IsNullOrEmpty(product.Title)
                && !string.IsNullOrEmpty(product.ShortDescription)
                && !string.IsNullOrEmpty(product.LongDescription)
                && product.Price > 0;
        }

        public static bool Validate(this ProductAddition product)
        {
            return product != null
                && !string.IsNullOrEmpty(product.Title)
                && !string.IsNullOrEmpty(product.ShortDescription)
                && !string.IsNullOrEmpty(product.LongDescription)
                && product.Price > 0;
        }

        public static bool Validate(this RegisterUser registeringUser)
        {
            return registeringUser != null
                && !string.IsNullOrEmpty(registeringUser.UserName)
                && !string.IsNullOrEmpty(registeringUser.Password)
                && !string.IsNullOrEmpty(registeringUser.Email)
                && !string.IsNullOrEmpty(registeringUser.FirstName)
                && !string.IsNullOrEmpty(registeringUser.LastName)
                && registeringUser.Password == registeringUser.ConfirmPassword
                && registeringUser.BirthDate != null;
        }
    }
}
