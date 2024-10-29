using Microsoft.AspNetCore.Mvc;

namespace Example.Controllers;

[Controller]
public class SimpleController : Controller
{
    [Route("/test/{id}")]
    [HttpGet]
    public IActionResult Do(int id, string appendedValue)
    {
        return Json("test");
    }
}