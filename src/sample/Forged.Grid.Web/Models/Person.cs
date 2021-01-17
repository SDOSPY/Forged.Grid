using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Forged.Grid.Web.Models
{
    public class Person
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Surname")]
        public string Surname { get; set; }


        [Display(Name = "Age")]
        public int Age { get; set; }

        [Display(Name = "Birthday")]
        public DateTime? Birthday { get; set; }

        [Display(Name = "Employed")]
        public bool? IsWorking { get; set; }

        [Display(Name = "Marital status")]
        public MaritalStatus? MaritalStatus { get; set; }

        public List<Person> Children { get; set; }

        public Person(int id, string name, string surname)
        {
            Id = id;
            Name = name;
            Surname = surname;
            Children = new List<Person>();
        }
    }
}
