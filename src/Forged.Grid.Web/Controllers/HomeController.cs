using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Forged.Grid.Web.Context;
using Forged.Grid.Web.Models;
using OfficeOpenXml;
using System;
using System.Linq;
using System.Threading;

namespace Forged.Grid.Web.Controllers
{
    public class HomeController : Controller
    {
        private PeopleRepository Repository { get; }

        public HomeController()
        {
            Repository = new PeopleRepository();
        }

        [HttpGet("")]
        public ViewResult Index()
        {
            return View(Repository.GetPeople());
        }

        [HttpGet("installation")]
        public ViewResult Installation()
        {
            return View();
        }

        [HttpGet("ajax")]
        public ActionResult Ajax()
        {
            if (HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                return PartialView("_AjaxGrid", Repository.GetPeople());

            return View();
        }

        [HttpGet("datatables")]
        public ViewResult DataTable()
        {
            return View(Repository.AsDataTable());
        }

        [HttpGet("global-search")]
        public ActionResult GlobalSearch(string search)
        {
            if (HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                return PartialView("_GlobalSearchGrid", Repository.GetPeople(search));

            return View(Repository.GetPeople(search));
        }

        [HttpGet("filter-modes")]
        public ViewResult FilterModes()
        {
            return View(Repository.GetPeople());
        }

        [HttpGet("filtering")]
        public ViewResult Filtering()
        {
            return View(Repository.GetPeople());
        }

        [HttpGet("sorting")]
        public ViewResult Sorting()
        {
            return View(Repository.GetPeople());
        }

        [HttpGet("encoding")]
        public ViewResult Encoding()
        {
            return View(Repository.GetPeople());
        }

        [HttpGet("formatting")]
        public ViewResult Formatting()
        {
            return View(Repository.GetPeople());
        }

        [HttpGet("row-processing")]
        public ViewResult RowProcessing()
        {
            return View(Repository.GetPeople());
        }

        [HttpGet("multiple")]
        public ViewResult Multiple()
        {
            return View(Repository.GetPeople());
        }

        [HttpGet("paging")]
        public ViewResult Paging()
        {
            return View(Repository.GetPeople());
        }

        [HttpGet("manual-processing")]
        public ViewResult ManualProcessing(Int32? rows, Int32? page)
        {
            ViewBag.TotalRows = Repository.GetPeople().Count();

            return View(Repository.GetPeople().Skip((page - 1 ?? 0) * (rows ?? 3)).Take(rows ?? 3));
        }

        [HttpGet("html-attributes")]
        public ViewResult Attributes()
        {
            return View(Repository.GetPeople());
        }

        [HttpGet("partials")]
        public ViewResult Partials()
        {
            return View(Repository.GetPeople());
        }

        [HttpGet("footer")]
        public ViewResult Footer()
        {
            return View(Repository.GetPeople());
        }

        [HttpGet("events")]
        public ActionResult Events()
        {
            if (HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                Thread.Sleep(2000);

                return PartialView("_EventsGrid", Repository.GetPeople());
            }

            return View(Repository.GetPeople());
        }

        [HttpGet("export")]
        public ViewResult Export()
        {
            return View(CreateExportableGrid());
        }

        [HttpGet("export-data")]
        public FileContentResult ExportData()
        {
            return Export(CreateExportableGrid(), "People");
        }

        [HttpGet("source-url")]
        public ActionResult SourceUrl()
        {
            if (HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                return PartialView("_SourceUrlGrid", Repository.GetPeople());

            return View(Repository.GetPeople());
        }

        [HttpGet("localization")]
        public ViewResult Localization()
        {
            return View(Repository.GetPeople());
        }

        [HttpGet("preferences")]
        public ViewResult Preferences()
        {
            return View(Repository.GetPeople());
        }

        [HttpGet("filter-registration")]
        public ViewResult Register()
        {
            return View(Repository.GetPeople());
        }

        [HttpGet("filter-unregistration")]
        public ViewResult Unregister()
        {
            return View(Repository.GetPeople());
        }

        [HttpGet("api")]
        public ViewResult Api()
        {
            return View(Repository.GetPeople());
        }

        private IGrid<Person> CreateExportableGrid()
        {
            IGrid<Person> grid = new Grid<Person>(Repository.GetPeople());
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.Query;

            grid.Columns.Add(model => model.Name);
            grid.Columns.Add(model => model.Surname);

            grid.Columns.Add(model => model.Age);
            grid.Columns.Add(model => model.Birthday).Formatted("{0:d}");
            grid.Columns.Add(model => model.IsWorking);

            grid.Pager = new GridPager<Person>(grid);
            grid.Processors.Add(grid.Pager);
            grid.Processors.Add(grid.Sort);
            grid.Pager.RowsPerPage = 6;

            foreach (IGridColumn<Person> column in grid.Columns)
            {
                column.Filter.IsEnabled = true;
                column.Sort.IsEnabled = true;
            }

            return grid;
        }
        private FileContentResult Export(IGrid grid, String fileName)
        {
            Int32 col = 1;
            using ExcelPackage package = new ExcelPackage();
            ExcelWorksheet sheet = package.Workbook.Worksheets.Add("Data");

            foreach (IGridColumn column in grid.Columns)
            {
                sheet.Cells[1, col].Value = column.Title;
                sheet.Column(col++).Width = 18;

                column.IsEncoded = false;
            }

            foreach (IGridRow<Object> row in grid.Rows)
            {
                col = 1;

                foreach (IGridColumn column in grid.Columns)
                    sheet.Cells[row.Index + 2, col++].Value = column.ValueFor(row);
            }

            return File(package.GetAsByteArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"{fileName}.xlsx");
        }
    }
}
