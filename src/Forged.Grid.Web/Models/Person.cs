using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Forged.Grid.Web.Models
{
    public class Person
    {
        [Display(Name = "Id")]
        public Int32 Id { get; set; }

        [Display(Name = "Name")]
        public String Name { get; set; }

        [Display(Name = "Surname")]
        public String Surname { get; set; }


        [Display(Name = "Age")]
        public Int32 Age { get; set; }

        [Display(Name = "Birthday")]
        public DateTime? Birthday { get; set; }

        [Display(Name = "Employed")]
        public Boolean? IsWorking { get; set; }

        [Display(Name = "Marital status")]
        public MaritalStatus? MaritalStatus { get; set; }

        public List<Person> Children { get; set; }

        public Person(Int32 id, String name, String surname)
        {
            Id = id;
            Name = name;
            Surname = surname;
            Children = new List<Person>();
        }
    }
}
