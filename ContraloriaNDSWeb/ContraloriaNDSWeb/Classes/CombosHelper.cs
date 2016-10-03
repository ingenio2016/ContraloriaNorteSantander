using ContraloriaNDSWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContraloriaNDSWeb.Classes
{
    public class CombosHelper : IDisposable
    {
        private static ContraloriandsContext db = new ContraloriandsContext();

        public static List<Department> GetDepartments()
        {
            var departments = db.Departments.ToList();
            departments.Add(new Department
            {
                DepartmentId = 0,
                Name = "[Seleccione un Departamento]",

            });
            return departments.OrderBy(x => x.Name).ToList();
        }

        public static List<City> GetCities()
        {
            var cities = db.Cities.ToList();
            cities.Add(new City
            {
                CityId = 0,
                Name = "[Seleccione una Ciudad]",

            });
            return cities.OrderBy(x => x.Name).ToList();
        }

        public static List<Company> GetCompanies()
        {
            var company = db.Companies.ToList();
            company.Add(new Company
            {
                CompanyId = 0,
                Name = "[Seleccione una Contraloría]",

            });
            return company.OrderBy(x => x.Name).ToList();
        }

        public static List<Year> GetYears()
        {
            var year = db.Years.ToList();
            year.Add(new Year
            {
                YearId = 0,
                Name = "[Seleccione un año]",

            });
            return year.OrderBy(x => x.YearId).ToList();
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}