//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;

//namespace StockManagementMVC.Areas.Admin.Controllers
//{
//    [Authorize]
//    [Area("Admin")]
//    public class DashboardController : Controller
//    {
//        public IActionResult Index()
//        {
//            return View();
//        }
//    }
//}




using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockManagementMVC.repository;

namespace StockManagementMVC.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,Nhân Viên")]
    [Area("Admin")]
    public class DashboardController : Controller
    {
        private readonly DataContext _context;

        public DashboardController(DataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // Tổng số sản phẩm
            var totalProducts = await _context.Products.CountAsync();

            // Tổng đơn nhập
            var totalImport = await _context.WarehouseTransactions
                .Where(x => x.TransactionType == "Import")
                .CountAsync();

            // Tổng đơn xuất
            var totalExport = await _context.WarehouseTransactions
                .Where(x => x.TransactionType == "Export")
                .CountAsync();

            // Biểu đồ thống kê theo tháng (tính theo số lượng sản phẩm)
            var monthlyStats = await _context.WarehouseTransactions
                .GroupBy(x => new { x.TransactionType, Month = x.TransactionDate.Month })
                .Select(g => new
                {
                    g.Key.Month,
                    ImportCount = g.Where(x => x.TransactionType == "Import").Sum(x => x.Quantity),
                    ExportCount = g.Where(x => x.TransactionType == "Export").Sum(x => x.Quantity)
                })
                .OrderBy(x => x.Month)
                .ToListAsync();

            ViewBag.TotalProducts = totalProducts;
            ViewBag.TotalImport = totalImport;
            ViewBag.TotalExport = totalExport;
            ViewBag.MonthlyData = monthlyStats;

            return View();
        }
    }
}









