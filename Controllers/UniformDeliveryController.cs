using DnTech_PBS_UniformManagement.Data;
using DnTech_PBS_UniformManagement.Models.Entities;
using DnTech_PBS_UniformManagement.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DnTech_PBS_UniformManagement.Controllers
{
    [Authorize]
    public class UniformDeliveryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UniformDeliveryController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Lista de empleados de un área de salud con sus entregas
        public async Task<IActionResult> Index(int healthAreaId)
        {
            var healthArea = await _context.HealthAreas
                .Include(h => h.Province)
                .FirstOrDefaultAsync(h => h.Id == healthAreaId);

            if (healthArea == null)
            {
                TempData["Error"] = "Área de salud no encontrada.";
                return RedirectToAction("Index", "Dashboard");
            }

            var employees = await _context.EmployeeHealthAreas
                .Where(e => e.HealthAreaId == healthAreaId && e.Active)
                .Select(e => new EmployeeWithUniformsViewModel
                {
                    EmployeeId = e.EmployeeId,
                    HealthAreaId = e.HealthAreaId,
                    FullName = e.Employee != null ? e.Employee.FullName : "",
                    IdCard = e.Employee != null ? e.Employee.IdCard : null,
                    Email = e.Employee != null ? e.Employee.Email : "",
                    Position = e.Employee != null ? e.Employee.Position : null,
                    AssignedAt = e.AssignedAt,
                    LastDeliveryId = e.UniformDeliveries.OrderByDescending(d => d.DeliveryDate).Select(d => d.Id).FirstOrDefault(),
                    LastDeliveryDate = e.UniformDeliveries.OrderByDescending(d => d.DeliveryDate).Select(d => d.DeliveryDate).FirstOrDefault(),
                    NextDeliveryDate = e.UniformDeliveries.OrderByDescending(d => d.DeliveryDate).Select(d => d.NextDeliveryDate).FirstOrDefault(),
                    DeliveryStatus = e.UniformDeliveries.OrderByDescending(d => d.DeliveryDate).Select(d => d.Status).FirstOrDefault() ?? "Sin entrega",
                    DaysUntilNextDelivery = e.UniformDeliveries.OrderByDescending(d => d.DeliveryDate).Select(d => d.DaysUntilNextDelivery).FirstOrDefault(),
                    TotalDeliveries = e.UniformDeliveries.Count
                })
                .ToListAsync();

            ViewBag.HealthAreaName = healthArea.Name;
            ViewBag.HealthAreaCode = healthArea.Code;
            ViewBag.ProvinceName = healthArea.Province?.Name;
            ViewBag.HealthAreaId = healthAreaId;

            return View(employees);
        }

        // GET: Crear nueva entrega
        [Authorize(Roles = "Administrator,Supervisor")]
        public async Task<IActionResult> Create(string employeeId, int healthAreaId)
        {
            var employee = await _context.EmployeeHealthAreas
                .Include(e => e.Employee)
                .Include(e => e.HealthArea)
                .FirstOrDefaultAsync(e => e.EmployeeId == employeeId && e.HealthAreaId == healthAreaId);

            if (employee == null)
            {
                TempData["Error"] = "Empleado no encontrado.";
                return RedirectToAction(nameof(Index), new { healthAreaId });
            }

            var viewModel = new CreateUniformDeliveryViewModel
            {
                EmployeeId = employeeId,
                HealthAreaId = healthAreaId,
                EmployeeName = employee.Employee?.FullName,
                EmployeeIdCard = employee.Employee?.IdCard,
                EmployeePosition = employee.Position,
                HealthAreaName = employee.HealthArea?.Name,
                DeliveryDate = DateTime.Now,
                Items = new List<DeliveryDetailItemViewModel>
                {
                    new DeliveryDetailItemViewModel() // Al menos un item por defecto
                }
            };

            return View(viewModel);
        }

        // POST: Crear nueva entrega
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,Supervisor")]
        public async Task<IActionResult> Create(CreateUniformDeliveryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Validar que haya al menos un item
            if (model.Items == null || !model.Items.Any())
            {
                ModelState.AddModelError("", "Debe agregar al menos una prenda.");
                return View(model);
            }

            // Calcular días hasta próxima entrega
            int? daysUntilNext = null;
            if (model.NextDeliveryDate.HasValue)
            {
                daysUntilNext = (model.NextDeliveryDate.Value - DateTime.Now).Days;
            }

            // Determinar estado
            string status = "Sin entrega";
            if (model.DeliveryDate <= DateTime.Now)
            {
                status = "Entregado";
            }
            else if (model.NextDeliveryDate.HasValue && model.NextDeliveryDate.Value > DateTime.Now)
            {
                status = "Próximo";
            }

            var delivery = new UniformDelivery
            {
                EmployeeId = model.EmployeeId,
                HealthAreaId = model.HealthAreaId,
                DeliveryDate = model.DeliveryDate,
                NextDeliveryDate = model.NextDeliveryDate,
                Status = status,
                Observations = model.Observations,
                DaysUntilNextDelivery = daysUntilNext,
                CreatedAt = DateTime.Now
            };

            _context.UniformDeliveries.Add(delivery);
            await _context.SaveChangesAsync();

            // Agregar detalles
            foreach (var item in model.Items)
            {
                var detail = new DeliveryDetail
                {
                    UniformDeliveryId = delivery.Id,
                    GarmentType = item.GarmentType,
                    Size = item.Size,
                    Quantity = item.Quantity,
                    Notes = item.Notes
                };
                _context.DeliveryDetails.Add(detail);
            }

            await _context.SaveChangesAsync();

            TempData["Success"] = "Entrega de uniformes registrada exitosamente.";
            return RedirectToAction(nameof(Index), new { healthAreaId = model.HealthAreaId });
        }

        // GET: Ver detalles de una entrega
        public async Task<IActionResult> Details(int id)
        {
            var delivery = await _context.UniformDeliveries
                .Include(d => d.Employee)
                    .ThenInclude(e => e!.Employee)
                .Include(d => d.Employee)
                    .ThenInclude(e => e!.HealthArea)
                        .ThenInclude(h => h!.Province)
                .Include(d => d.DeliveryDetails)
                .FirstOrDefaultAsync(d => d.Id == id);

            if (delivery == null)
            {
                TempData["Error"] = "Entrega no encontrada.";
                return RedirectToAction("Index", "Dashboard");
            }

            var viewModel = new UniformDeliveryDetailsViewModel
            {
                DeliveryId = delivery.Id,
                EmployeeId = delivery.EmployeeId,
                HealthAreaId = delivery.HealthAreaId,
                EmployeeName = delivery.Employee?.Employee?.FullName ?? "",
                EmployeeIdCard = delivery.Employee?.Employee?.IdCard,
                EmployeePosition = delivery.Employee?.Position ?? "",
                HealthAreaName = delivery.Employee?.HealthArea?.Name ?? "",
                ProvinceName = delivery.Employee?.HealthArea?.Province?.Name ?? "",
                DeliveryDate = delivery.DeliveryDate,
                NextDeliveryDate = delivery.NextDeliveryDate,
                Status = delivery.Status,
                Observations = delivery.Observations,
                DaysUntilNextDelivery = delivery.DaysUntilNextDelivery,
                CreatedAt = delivery.CreatedAt,
                Details = delivery.DeliveryDetails.Select(d => new DeliveryDetailViewModel
                {
                    Id = d.Id,
                    GarmentType = d.GarmentType,
                    Size = d.Size,
                    Quantity = d.Quantity,
                    Notes = d.Notes
                }).ToList()
            };

            return View(viewModel);
        }

        // GET: Historial de entregas de un empleado
        public async Task<IActionResult> History(string employeeId, int healthAreaId)
        {
            var employee = await _context.EmployeeHealthAreas
                .Include(e => e.Employee)
                .Include(e => e.HealthArea)
                    .ThenInclude(h => h!.Province)
                .FirstOrDefaultAsync(e => e.EmployeeId == employeeId && e.HealthAreaId == healthAreaId);

            if (employee == null)
            {
                TempData["Error"] = "Empleado no encontrado.";
                return RedirectToAction("Index", "Dashboard");
            }

            var deliveries = await _context.UniformDeliveries
                .Where(d => d.EmployeeId == employeeId && d.HealthAreaId == healthAreaId)
                .Include(d => d.DeliveryDetails)
                .OrderByDescending(d => d.DeliveryDate)
                .Select(d => new DeliveryHistoryViewModel
                {
                    Id = d.Id,
                    DeliveryDate = d.DeliveryDate,
                    NextDeliveryDate = d.NextDeliveryDate,
                    Status = d.Status,
                    ItemsCount = d.DeliveryDetails.Count,
                    DaysUntilNextDelivery = d.DaysUntilNextDelivery
                })
                .ToListAsync();

            var viewModel = new EmployeeDeliveriesViewModel
            {
                EmployeeId = employeeId,
                HealthAreaId = healthAreaId,
                EmployeeName = employee.Employee?.FullName ?? "",
                EmployeeIdCard = employee.Employee?.IdCard,
                EmployeePosition = employee.Position,
                HealthAreaName = employee.HealthArea?.Name ?? "",
                ProvinceName = employee.HealthArea?.Province?.Name ?? "",
                Deliveries = deliveries
            };

            return View(viewModel);
        }

        // DELETE: Eliminar entrega
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(int id, int healthAreaId)
        {
            var delivery = await _context.UniformDeliveries.FindAsync(id);

            if (delivery == null)
            {
                TempData["Error"] = "Entrega no encontrada.";
                return RedirectToAction(nameof(Index), new { healthAreaId });
            }

            _context.UniformDeliveries.Remove(delivery);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Entrega eliminada exitosamente.";
            return RedirectToAction(nameof(Index), new { healthAreaId });
        }
    }
}
