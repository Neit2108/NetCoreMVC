using Microsoft.AspNetCore.Mvc;
using NetCoreMVC.Services;

namespace NetCoreMVC.Controllers;

public class FirstController : Controller
{
    private readonly ILogger<FirstController> _logger;
    private readonly IHostEnvironment _env;
    private readonly ProductService _productService;

    public FirstController(ILogger<FirstController> logger, IHostEnvironment env, ProductService productService)
    {
        _logger = logger;
        _env = env;
        _productService = productService;
    }
    
    public string Index(){

        _logger.LogInformation("FirstController.Index() called");
        return "I'm first's index";
    }

    public void Nothing(){
        _logger.LogInformation("Nothing Action called");
        Response.Headers.Append("X-Nothing", "Nothing"); // -> Thiet lap header
        
    }

    public object Anything() => new int[]{1,2,3,4,5}; // -> Tra ve 1 array

    public IActionResult Readme(){
        var content = @"Hello my friend,
        
        Welcome to my first controller. This is a simple controller with 3 actions: Index, Nothing and Anything.";
        return Content(content, "text/html");
    }

    public IActionResult Doraemon(){
        string filePath = Path.Combine(_env.ContentRootPath, "Files", "images.jpg");
        var bytes = System.IO.File.ReadAllBytes(filePath);

        return File(bytes, "image/jpg");
    }

    public IActionResult ImagePrice(){
        JsonResult json = new JsonResult(new {
            name = "Doraemon"
            ,price = 1000});

        return json;
    }

    public IActionResult HelloView(string userName){
        if(string.IsNullOrEmpty(userName)){
            userName = "Guest";
        }
        // View(template) - template la duong dan tuyet doi toi view
        // View(template, model) - model la doi tuong du lieu truyen vao view
        return View("HelloView", userName); // ->Razor Engine se tim view co ten HelloView
    }

    public IActionResult ViewProduct(int? id){
        var product = _productService.Where(p => p.Id == id).FirstOrDefault();
        if(product == null){
            TempData["Message"] = "Product not found";
            return Redirect(Url.Action("Index", "Home"));
        }
        // Thiet lap Model cho view tu model
        //return View(product);

        // Thiet lap bang viewdata
        this.ViewData["product"] = product;
        this.ViewData["Title"] = "Product Detail";

        //Gui du lieu sang trang khac
        this.TempData["product"] = product;
        // Doc du lieu o trang khac lan dau se xoa luon
        return View("ViewProduct2");
    }
}