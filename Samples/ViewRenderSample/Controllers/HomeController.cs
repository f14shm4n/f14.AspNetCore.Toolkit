using f14.AspNetCore.Mvc;
using f14.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;
using ViewRenderSample.Models;

namespace ViewRenderSample.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> RenderView([FromServices] IViewRenderer viewRender)
        {
            ViewData["Type"] = 1;
            string result = await viewRender.RenderAsync(HttpContext, "TestView", new TestViewModel { Title = "Test view" }, ViewData);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> RenderCustomView([FromServices] IWebHostEnvironment env, [FromServices] IViewRenderer viewRender)
        {
            ViewData["Type"] = 2;
            string result = await viewRender.RenderAsync(HttpContext, "~/CustomViews/CustomTestView.cshtml", new TestViewModel { Title = "Test view" }, ViewData, true);
            return Content(result);
        }

        [HttpGet("test-action/{id}/{name}")]
        public IActionResult TestAction(int id, string name)
        {
            return Ok($"Test Action: [{id}, {name}]");
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
