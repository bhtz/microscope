using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IronHasura.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using IronHasura.Models;
using NSwag.Annotations;

namespace IronHasura.Controllers_mvc
{
    [OpenApiIgnore]
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly IronHasuraDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserController(IronHasuraDbContext context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            return View(await this._userManager.Users.ToListAsync());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await this._userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            ViewBag.Roles = await this._userManager.GetRolesAsync(user);
            return View(user);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await this._userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var model = new List<UserRoleViewModel>();
            var userRoles = await this._userManager.GetRolesAsync(user);
            var roles = await this._roleManager.Roles.ToListAsync();

            foreach (var role in roles)
            {
                var item = new UserRoleViewModel() 
                {
                    RoleId = role.Id,
                    RoleName = role.Name
                };

                if(userRoles.Contains(role.Name)) 
                {
                    item.IsSelected = true;
                }
                else 
                {
                    item.IsSelected = false;
                }

                model.Add(item);
            }

            ViewBag.User = user;
            return View(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(IEnumerable<UserRoleViewModel> models, string id)
        {
            var user = await this._userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var roles = await this._userManager.GetRolesAsync(user);
            var result = await this._userManager.RemoveFromRolesAsync(user, roles);

            if(!result.Succeeded)
            {
                ModelState.AddModelError("", "cannot drop roles");
                return View(models);
            }
            
            var selectedRoles = models.Where(x => x.IsSelected).Select(x => x.RoleName);
            result = await this._userManager.AddToRolesAsync(user, selectedRoles);

            if(!result.Succeeded)
            {
                ModelState.AddModelError("", "cannot add selected roles");
                return View(models);
            }
            
            return RedirectToAction("Details", new { id = id });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await this._userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var user = await this._userManager.FindByIdAsync(id);
            await this._userManager.DeleteAsync(user);

            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool UserExists(string id)
        {
            return this._userManager.Users.Any(e => e.Id == id);
        }
    }
}
