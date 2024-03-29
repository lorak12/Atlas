﻿using Atlas.Data;
using Atlas.Models;
using Atlas.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Atlas.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    public class MushroomController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MushroomController(ApplicationDbContext content)
        {
            _context = content;
        }

        [Route("/Pulpit/Grzyby")]
        // lista grzybów, które są w bazie
        public IActionResult Index()
        {
            List<Mushroom> allMushrooms = _context.Mushrooms.ToList(); // odczytanie z tabeli wszystkich rekordów

            List<MushroomsListVM> nameOnly = new();  //utworzenie nowej listy PUSTEJ

            foreach (var item in allMushrooms)
            {
                MushroomsListVM oneName = new();  //tworzę nowy obiekt typu MushroomListVM, obiekt o nazwie oneName
                oneName.Name = item.Name; //do obiektu oneName do pola Name wpisuję wartość pola Name z obiektu item
                oneName.ID = item.ID.ToString();
                nameOnly.Add(oneName);  //dodaję do listy nameOnly obiekt 'oneName' utworzony 2 linijki wcześniej
            }

            return View(nameOnly);  // widok listy grzybów
        }

        [Route("/Pulpit/Grzyby/Usun/{id}")]
        // usuwanie z bazy
        public IActionResult Delete(string id)
        {
            Mushroom mushroom = _context.Mushrooms.FirstOrDefault(x => x.ID == Guid.Parse(id)); // wybierz z tabeli Mushrooms pierwszego napotkanego grzyba o ID takim jak podane w akcji

            _context.Mushrooms.Remove(mushroom);
            _context.SaveChanges();

            return View(); // potwierdzenie usunięcia z bazy
        }

        [Route("/Pulpit/Grzyby/Nowy")]
        // utworzenie nowego wpisu - dodanie grzyba do bazy
        public IActionResult Create()
        {
            return View(); // pusty formularz do wypełnienia
        }

        [HttpPost]
        [Route("/Pulpit/Grzyby/Nowy")]
        // utworzenie nowego wpisu - dodanie grzyba do bazy
        public IActionResult Create(MushroomVM model)
        {
            Mushroom newMushroom = new();
            newMushroom.ID = Guid.NewGuid(); //nowy identyfikator typu GUID
            newMushroom.Name = model.Name;
            newMushroom.Description = model.Description;
            newMushroom.Edibility = model.Edibility;
            newMushroom.Family = model.Family;
            newMushroom.Genre = model.Genre;
            newMushroom.Kind = model.Kind;
            newMushroom.LatinName = model.LatinName;
            newMushroom.Occurence = model.Occurence;
            newMushroom.Create = DateTime.Now;

            _context.Mushrooms.Add(newMushroom);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
            //return View(); 

        }




        [Route("/Pulpit/Grzyby/Edycja/{id}")]
        // aktualizacja wpisu, w bazie
        public IActionResult Update(string id)
        {
            Mushroom mushroom = _context.Mushrooms.FirstOrDefault(x => x.ID == Guid.Parse(id));

            MushroomVM model = new()
            {
                Description = mushroom.Description,
                Edibility = mushroom.Edibility,
                Family = mushroom.Family,
                Genre = mushroom.Genre,
                Kind = mushroom.Kind,
                LatinName = mushroom.LatinName,
                Name = mushroom.Name,
                Occurence = mushroom.Occurence,
                ID = id
            };


            return View(model); // wypełniony formularz z danymi wybranego grzyba
        }

        [HttpPost]
        [Route("/Pulpit/Grzyby/Edycja")]
        public IActionResult Update(MushroomVM model)
        {

            Mushroom mushroom = _context.Mushrooms.FirstOrDefault(x => x.ID == Guid.Parse(model.ID));

            mushroom.Family = model.Family;
            mushroom.Description = model.Description;
            mushroom.Edibility = model.Edibility;
            mushroom.Genre = model.Genre;
            mushroom.Kind = model.Kind;
            mushroom.Name = model.Name;
            mushroom.LatinName = model.LatinName;
            mushroom.Occurence = model.Occurence;

            _context.Mushrooms.Update(mushroom);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        
    }
}
