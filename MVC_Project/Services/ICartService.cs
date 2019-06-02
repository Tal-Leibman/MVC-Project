using Microsoft.AspNetCore.Http;
using MVC_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Project.Services
{
    public interface ICartService
    {
        bool RemoveProduct(int id);
        void ClearAllProducts();
        bool AddToCart(long id);

    }
}
