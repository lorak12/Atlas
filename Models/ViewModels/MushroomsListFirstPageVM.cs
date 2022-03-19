using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Atlas.Models.ViewModels
{
    public class MushroomsListFirstPageVM
    {
        public string Name { get; set; }
        public string ID { get; set; }
        public string Family { get; set; }
        public string Genre { get; set; }

        public string Edibility { get; set; }
        public string Kind { get; set; }
        public string Description { get; set; }
        public string LatinName { get; set; }
        public string Occurence { get; set; }
    }
}
