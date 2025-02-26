using Microsoft.AspNetCore.Mvc;
using NetCoreMVC.Services;

namespace NetCoreMVC.Controllers
{
    [Route("he-mat-troi")] //-> truy cập theo kiểu /he-mat-troi/...
    public class PlanetController : Controller
    {
        private readonly PlanetService _planetService;
        private readonly ILogger<PlanetController> _logger;

        public PlanetController(PlanetService planetService, ILogger<PlanetController> logger)
        {
            _planetService = planetService;
            _logger = logger;
        }
        
        
        [Route("danh-sach-hanh-tinh.html")] //-> Thiết lập địa chỉ truy cập : /he-mat-troi/danh...
        //[Route("/danh-sach-hanh-tinh")] // Nếu thiết lập như này thì chỉ truy cập được qua đường link này
        public ActionResult Index()
        {
            return View();
        }

        // Mỗi khi truy cập Action -> trong route có tham số tên là action
        [BindProperty(SupportsGet = true, Name = "action")]
        public string Name {get; set;}
        // Ví dụ truy cập vào action Mercury -> Name = Mercury tương đương với PlanetModel
        public IActionResult Mercury(){
            var planet = _planetService.Where(p => p.Name == Name).FirstOrDefault();
            return View("Detail", planet);
        }

        public IActionResult Venus(){
            var planet = _planetService.Where(p => p.Name == Name).FirstOrDefault();
            return View("Detail", planet);
        }

        public IActionResult Earth(){
            var planet = _planetService.Where(p => p.Name == Name).FirstOrDefault();
            return View("Detail", planet);
        }

        public IActionResult Mars(){
            var planet = _planetService.Where(p => p.Name == Name).FirstOrDefault();
            return View("Detail", planet);
        }

        public IActionResult Jupiter(){
            var planet = _planetService.Where(p => p.Name == Name).FirstOrDefault();
            return View("Detail", planet);
        }

        public IActionResult Saturn(){
            var planet = _planetService.Where(p => p.Name == Name).FirstOrDefault();
            return View("Detail", planet);
        }

        public IActionResult Uranus(){
            var planet = _planetService.Where(p => p.Name == Name).FirstOrDefault();
            return View("Detail", planet);
        }

        [Route("sao/[action]", Order = 1, Name = "neptune1")] // phải truy cập theo kiểu /sao/action_name
        [Route("sao/[controller]/[action]", Order = 2, Name = "neptune2")] // phải truy cập theo kiểu /sao/controller_name/action_name
        [Route("[controller]-[action].html", Order = 3, Name = "neptune3")] // phải truy cập theo kiểu /controller_name-action_name.html
        //Có thể khai báo nhiều lần để tạo ra nhiều route, có thể thêm độ ưu tiên bằng order
        public IActionResult Neptune(){
            var planet = _planetService.Where(p => p.Name == Name).FirstOrDefault();
            return View("Detail", planet);
        }

        [Route("hanhtinh/{id:int}")] // Chỉ truy cập được qua đường link này
        public IActionResult PlanetInfo(int id){
            var planet = _planetService.Where(p => p.ID == id).FirstOrDefault();
            return View("Detail", planet);
        }


    }
}

//Action nào 0 có route thì vẫn chịu ảnh hưởng của Route default (MapControllerRoute)
//Nếu thiết lập route ở controller thì phải thiết lập cho action hoặc thêm [action] vào sau