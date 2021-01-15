using Forged.Grid.Web.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Forged.Grid.Web.Context
{
    public class PeopleRepository
    {
        public DataTable AsDataTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add(new DataColumn("name") { Caption = "Name" });
            table.Columns.Add(new DataColumn("surname") { Caption = "Surname" });
            table.Columns.Add(new DataColumn("marital-status") { Caption = "Marital status" });
            table.Columns.Add(new DataColumn("age") { Caption = "Age" });
            table.Columns.Add(new DataColumn("birthday") { Caption = "Birthday" });
            table.Columns.Add(new DataColumn("is-working") { Caption = "Is working" });

            foreach (Person person in GetPeople())
            {
                DataRow row = table.NewRow();

                row["name"] = person.Name;
                row["surname"] = person.Surname;
                row["marital-status"] = person.MaritalStatus;

                row["age"] = person.Age;
                row["birthday"] = person.Birthday;
                row["is-working"] = person.IsWorking;

                table.Rows.Add(row);
            }

            return table;
        }
        public IQueryable<Person> GetPeople(String search = "")
        {
            search = (search ?? "").ToLower();
            Int32 currentYear = DateTime.Now.Year;

            return new List<Person>
            {
                new Person(1, "Joe", "Crosswave")
                {
                    MaritalStatus = MaritalStatus.Married,

                    Age = (Int32)(DateTime.Now - new DateTime(currentYear - 32, 01, 05)).TotalDays / 365,
                    Birthday = new DateTime(currentYear - 32, 01, 05),
                    IsWorking = false,

                    Children = new List<Person>
                    {
                        new Person(11, "Katy", "Crosswave")
                        {
                            Age = (Int32)(DateTime.Now - new DateTime(currentYear - 5, 01, 05)).TotalDays / 365,
                            Birthday = new DateTime(currentYear - 5, 01, 05)
                        },
                        new Person(12, "Kate", "Crosswave")
                        {
                            Age = (Int32)(DateTime.Now - new DateTime(currentYear - 5, 01, 05)).TotalDays / 365,
                            Birthday = new DateTime(currentYear - 5, 01, 05)
                        }
                    }
                },
                new Person(2, "Merry", "Lisel")
                {
                    MaritalStatus = MaritalStatus.Widowed,

                    Age = (Int32)(DateTime.Now - new DateTime(currentYear - 42, 05, 06)).TotalDays / 365,
                    Birthday = new DateTime(currentYear - 42, 05, 06)
                },
                new Person(3, "Henry", "Crux")
                {
                    MaritalStatus = MaritalStatus.Single,

                    Age = (Int32)(DateTime.Now - new DateTime(currentYear - 30, 11, 19)).TotalDays / 365,
                    Birthday = new DateTime(currentYear - 30, 11, 19),
                    IsWorking = true
                },
                new Person(4, "Cody", "Jurut")
                {
                    Age = (Int32)(DateTime.Now - new DateTime(currentYear - 50, 08, 11)).TotalDays / 365,
                    Birthday = new DateTime(currentYear - 50, 08, 11),
                    IsWorking = false
                },
                new Person(5, "Simon", "Scranton")
                {
                    MaritalStatus = MaritalStatus.Single,

                    Age = (Int32)(DateTime.Now - new DateTime(currentYear - 35, 10, 10)).TotalDays / 365,
                    Birthday = new DateTime(currentYear - 35, 10, 10)
                },
                new Person(6, "Leena", "Laurent")
                {
                    MaritalStatus = MaritalStatus.Divorced,

                    Age = (Int32)(DateTime.Now - new DateTime(currentYear - 20, 07, 01)).TotalDays / 365,
                    Birthday = new DateTime(currentYear - 20, 07, 01),
                    IsWorking = false
                },
                new Person(7, "Ode", "Cosmides")
                {
                    MaritalStatus = MaritalStatus.Married,

                    Age = (Int32)(DateTime.Now - new DateTime(currentYear - 54, 04, 17)).TotalDays / 365,
                    Birthday = new DateTime(currentYear - 54, 04, 17),
                    IsWorking = true,

                    Children = new List<Person>
                    {
                        new Person(71, "Jake", "Cosmides")
                        {

                            Age = (Int32)(DateTime.Now - new DateTime(currentYear - 6, 07, 14)).TotalDays / 365,
                            Birthday = new DateTime(currentYear - 6, 07, 14)
                        }
                    }
                },
                new Person(8, "Diandra", "Mizner")
                {
                    MaritalStatus = MaritalStatus.Single,

                    Age = (Int32)(DateTime.Now - new DateTime(currentYear - 21, 08, 20)).TotalDays / 365,
                    Birthday = new DateTime(currentYear - 21, 08, 20),
                    IsWorking = false
                },
                new Person(9, "Pete", "Cassel")
                {
                    MaritalStatus = MaritalStatus.Married,

                    Age = (Int32)(DateTime.Now - new DateTime(currentYear - 23, 03, 13)).TotalDays / 365,
                    Birthday = new DateTime(currentYear - 23, 03, 13),
                    IsWorking = false
                },
                new Person(10, "Nicky", "Tremblay")
                {
                    MaritalStatus = MaritalStatus.Married,

                    Age = (Int32)(DateTime.Now - new DateTime(currentYear - 32, 01, 05)).TotalDays / 365,
                    Birthday = new DateTime(currentYear - 32, 01, 05),
                    IsWorking = true,

                    Children = new List<Person>
                    {
                        new Person(101, "Nick", "Tremblay")
                        {
                            Age = (Int32)(DateTime.Now - new DateTime(currentYear - 7, 06, 08)).TotalDays / 365,
                            Birthday = new DateTime(currentYear - 7, 06, 08)
                        },
                        new Person(102, "Nike", "Tremblay")
                        {
                            Age = (Int32)(DateTime.Now - new DateTime(currentYear - 6, 12, 12)).TotalDays / 365,
                            Birthday = new DateTime(currentYear - 6, 12, 12)
                        },
                        new Person(103, "Norbert", "Tremblay")
                        {
                            Age = (Int32)(DateTime.Now - new DateTime(currentYear - 5, 05, 23)).TotalDays / 365,
                            Birthday = new DateTime(currentYear - 5, 05, 23)
                        }
                    }
                },
                new Person(11, "Mary", "Cassel")
                {
                    MaritalStatus = MaritalStatus.Married,

                    Age = (Int32)(DateTime.Now - new DateTime(currentYear - 25, 07, 10)).TotalDays / 365,
                    Birthday = new DateTime(currentYear - 25, 07, 10),
                    IsWorking = true
                }
            }.Where(person =>
                person.Age.ToString().Contains(search) ||
                person.Name.ToLower().Contains(search) ||
                person.Surname.ToLower().Contains(search) ||
                person.Birthday.ToString()!.Contains(search) ||
                person.IsWorking.ToString()!.ToLower().Contains(search) ||
                person.MaritalStatus.ToString()!.ToLower().Contains(search))
            .AsQueryable();
        }
    }
}
