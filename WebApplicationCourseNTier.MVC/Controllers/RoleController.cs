using Microsoft.AspNetCore.Mvc;
using WebApplicationCourseNTier.MVC.Models; 
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace WebApplicationCourseNTier.MVC.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

      
        public IActionResult Index()
        {
            var roles = _roleManager.Roles; 
            return View(roles);
        }

     
        public IActionResult Create()
        {
            return View();
        }

       
        [HttpPost]
        public async Task<IActionResult> Create(string roleName)
        {
            if (!string.IsNullOrEmpty(roleName))
            {
                var roleExist = await _roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    var role = new IdentityRole(roleName);
                    var result = await _roleManager.CreateAsync(role);

                    if (result.Succeeded)
                    {
                        TempData["Message"] = "Yeni rol yaradıldı!";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["Error"] = "Rol yaradılarkən xəta baş verdi.";
                    }
                }
                else
                {
                    TempData["Error"] = "Bu adla artıq rol mövcuddur.";
                }
            }
            else
            {
                TempData["Error"] = "Rol adı boş ola bilməz.";
            }
            return View();
        }

    
        [HttpPost]
        public async Task<IActionResult> Delete(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role != null)
            {
                var result = await _roleManager.DeleteAsync(role);
                if (result.Succeeded)
                {
                    TempData["Message"] = "Rol silindi!";
                }
                else
                {
                    TempData["Error"] = "Rol silinərkən xəta baş verdi.";
                }
            }
            else
            {
                TempData["Error"] = "Rol tapılmadı.";
            }
            return RedirectToAction("Index");
        }
    }
}
