 using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using sqlapp.Models;
using sqlapp.Services;

namespace sqlapp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IProductService _productService;

        public IEnumerable<Product> Products { get; set; }

        public IndexModel(IProductService productService)
        {
            _productService = productService;
        }

        public void OnGet()
        {
            Products = _productService.GetProducts();
        }
    }
}