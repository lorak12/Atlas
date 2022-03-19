using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Atlas.Areas.Dashboard.Controllers
{
    [Area("Dasboard")]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MushroomController(ApplicationDbContext content)
        {
            _context = content;
        }

        [Route("/Pulpit")]
        public IActionResult Index()
        {
            List<Mushroom> allMushrooms = _context.Mushrooms.ToList(); // odczytanie z tabeli wszystkich rekordów

            List<MushroomsListFirstPageVM> nameOnly = new();  //utworzenie nowej listy PUSTEJ

            foreach (var item in allMushrooms)
            {
                MushroomsListFirstPageVM oneName = new();  //tworzę nowy obiekt typu MushroomListVM, obiekt o nazwie oneName
                oneName.Name = item.Name; //do obiektu oneName do pola Name wpisuję wartość pola Name z obiektu item
                oneName.ID = item.ID.ToString();
                oneMane.Genre = item.Genre;
                oneName.Edibility = item.Edibility.ToString();
                oneName.Occurence = item.Occurence;
                oneName.LatinName = item.LatinName;
                oneName.Kind = item.Kind;
                oneName.Description = item.Description;
                oneName.Family = item.Family;
                nameOnly.Add(oneName);  //dodaję do listy nameOnly obiekt 'oneName' utworzony 2 linijki wcześniej
            }

            return View(nameOnly);  // widok listy grzybów
        }
    }
}
