using Microsoft.AspNetCore.Mvc;
using MVCKontorExpert.BusinessLogic;

public class ProductController : Controller
{
    private readonly IProductData _productData;
    private readonly IParentCategoryData _parentCategoryData;

    public ProductController(IProductData productData, IParentCategoryData parentCategoryData)
    {
        _productData = productData;
        _parentCategoryData = parentCategoryData;
    }

    public async Task<IActionResult> Index(int categoryID)
    {
        // Retrieve products based on categoryID
        var products = await _productData.GetProductsByCategoryID(categoryID);
        return View(products);
    }

}

