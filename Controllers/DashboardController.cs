using DnTech_PBS_UniformManagement.Data;
using DnTech_PBS_UniformManagement.Models.Entities;
using DnTech_PBS_UniformManagement.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DnTech_PBS_UniformManagement.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public DashboardController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // ============================================
        // DASHBOARD INDEX
        // ============================================
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var provinces = await _context.Provinces
                .Include(p => p.HealthAreas)
                    .ThenInclude(h => h.EmployeeHealthAreas)
                .Where(p => p.Active)
                .OrderBy(p => p.Name)
                .ToListAsync();

            var model = new DashboardViewModel
            {
                Provinces = provinces.Select(p => new ProvinceWithAreasViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Code = p.Code,
                    HealthAreasCount = p.HealthAreas.Count(h => h.Active),
                    HealthAreas = p.HealthAreas.Where(h => h.Active).Select(h => new HealthAreaViewModel
                    {
                        Id = h.Id,
                        Name = h.Name,
                        Code = h.Code,
                        ProvinceId = h.ProvinceId,
                        ProvinceName = p.Name,
                        EmployeesCount = h.EmployeeHealthAreas.Count(e => e.Active)
                    }).ToList()
                }).ToList(),
                TotalProvinces = provinces.Count,
                TotalHealthAreas = await _context.HealthAreas.CountAsync(h => h.Active),
                TotalEmployees = await _userManager.Users.CountAsync(u => u.Active),
                TotalUsers = await _userManager.Users.CountAsync()
            };

            return View(model);
        }

        // ============================================
        // PROVINCES
        // ============================================
        [HttpGet]
        [Authorize(Roles = "Administrator,Supervisor")]
        public IActionResult CreateProvince()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Administrator,Supervisor")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateProvince(CreateProvinceViewModel model)
        {
            if (ModelState.IsValid)
            {
                var province = new Province
                {
                    Name = model.Name,
                    Code = model.Code,
                    Active = true
                };

                _context.Provinces.Add(province);
                await _context.SaveChangesAsync();

                TempData["Success"] = "Province created successfully!";
                return RedirectToAction("Index");
            }

            return View(model);
        }

        // ============================================
        // HEALTH AREAS
        // ============================================
        [HttpGet]
        [Authorize(Roles = "Administrator,Supervisor")]
        public async Task<IActionResult> CreateHealthArea(int provinceId)
        {
            var province = await _context.Provinces.FindAsync(provinceId);
            if (province == null)
            {
                return NotFound();
            }

            var model = new CreateHealthAreaViewModel
            {
                ProvinceId = provinceId,
                ProvinceName = province.Name
            };

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator,Supervisor")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateHealthArea(CreateHealthAreaViewModel model)
        {
            if (ModelState.IsValid)
            {
                var healthArea = new HealthArea
                {
                    Name = model.Name,
                    Code = model.Code,
                    ProvinceId = model.ProvinceId,
                    Active = true
                };

                _context.HealthAreas.Add(healthArea);
                await _context.SaveChangesAsync();

                TempData["Success"] = "Health area created successfully!";
                return RedirectToAction("Index");
            }

            var province = await _context.Provinces.FindAsync(model.ProvinceId);
            model.ProvinceName = province?.Name;
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> HealthAreaDetails(int id)
        {
            var healthArea = await _context.HealthAreas
                .Include(h => h.Province)
                .Include(h => h.EmployeeHealthAreas)
                    .ThenInclude(e => e.Employee)
                .FirstOrDefaultAsync(h => h.Id == id);

            if (healthArea == null)
            {
                return NotFound();
            }

            return View(healthArea);
        }

        // ============================================
        // ASSIGN EMPLOYEES
        // ============================================
        [HttpGet]
        [Authorize(Roles = "Administrator,Supervisor")]
        public async Task<IActionResult> AssignEmployee(int healthAreaId)
        {
            var healthArea = await _context.HealthAreas
                .Include(h => h.Province)
                .FirstOrDefaultAsync(h => h.Id == healthAreaId);

            if (healthArea == null)
            {
                return NotFound();
            }

            // Obtener empleados que tienen rol "Employee"
            var employeeRole = await _context.Roles.FirstOrDefaultAsync(r => r.Name == "Employee");
            var employeeIds = await _context.UserRoles
                .Where(ur => ur.RoleId == employeeRole.Id)
                .Select(ur => ur.UserId)
                .ToListAsync();

            var availableEmployees = await _userManager.Users
                .Where(u => employeeIds.Contains(u.Id) && u.Active)
                .ToListAsync();

            ViewBag.Employees = availableEmployees;
            ViewBag.HealthArea = healthArea;

            var model = new AssignEmployeeViewModel
            {
                HealthAreaId = healthAreaId,
                HealthAreaName = $"{healthArea.Name} - {healthArea.Province.Name}"
            };

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator,Supervisor")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AssignEmployee(AssignEmployeeViewModel model)
        {
            if (model.CreateNewEmployee)
            {
                // Crear nuevo empleado
                if (string.IsNullOrEmpty(model.NewEmployeeFullname) ||
                    string.IsNullOrEmpty(model.NewEmployeeEmail) ||
                    string.IsNullOrEmpty(model.NewEmployeePassword))
                {
                    ModelState.AddModelError("", "Please fill all required fields for new employee");
                }
                else
                {
                    var newEmployee = new ApplicationUser
                    {
                        UserName = model.NewEmployeeEmail,
                        Email = model.NewEmployeeEmail,
                        Fullname = model.NewEmployeeFullname,
                        IdCard = model.NewEmployeeIdCard,
                        EmailConfirmed = true,
                        Active = true
                    };

                    var result = await _userManager.CreateAsync(newEmployee, model.NewEmployeePassword);
                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(newEmployee, "Employee");
                        model.EmployeeId = newEmployee.Id;
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }
                }
            }

            if (ModelState.IsValid && !string.IsNullOrEmpty(model.EmployeeId))
            {
                // Verificar si ya está asignado
                var exists = await _context.EmployeeHealthAreas
                    .AnyAsync(e => e.EmployeeId == model.EmployeeId && e.HealthAreaId == model.HealthAreaId);

                if (exists)
                {
                    TempData["Error"] = "Employee is already assigned to this health area";
                    return RedirectToAction("HealthAreaDetails", new { id = model.HealthAreaId });
                }

                var assignment = new EmployeeHealthArea
                {
                    EmployeeId = model.EmployeeId,
                    HealthAreaId = model.HealthAreaId,
                    Active = true
                };

                _context.EmployeeHealthAreas.Add(assignment);
                await _context.SaveChangesAsync();

                TempData["Success"] = "Employee assigned successfully!";
                return RedirectToAction("HealthAreaDetails", new { id = model.HealthAreaId });
            }

            // Reload data if validation fails
            var healthArea = await _context.HealthAreas
                .Include(h => h.Province)
                .FirstOrDefaultAsync(h => h.Id == model.HealthAreaId);

            var employeeRole = await _context.Roles.FirstOrDefaultAsync(r => r.Name == "Employee");
            var employeeIds = await _context.UserRoles
                .Where(ur => ur.RoleId == employeeRole.Id)
                .Select(ur => ur.UserId)
                .ToListAsync();

            ViewBag.Employees = await _userManager.Users
                .Where(u => employeeIds.Contains(u.Id) && u.Active)
                .ToListAsync();
            ViewBag.HealthArea = healthArea;

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator,Supervisor")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveEmployee(string employeeId, int healthAreaId)
        {
            var assignment = await _context.EmployeeHealthAreas
                .FirstOrDefaultAsync(e => e.EmployeeId == employeeId && e.HealthAreaId == healthAreaId);

            if (assignment != null)
            {
                _context.EmployeeHealthAreas.Remove(assignment);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Employee removed from health area";
            }

            return RedirectToAction("HealthAreaDetails", new { id = healthAreaId });
        }




    }
}
