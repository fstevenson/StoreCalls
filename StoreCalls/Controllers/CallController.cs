using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StoreCalls.Models;
using StoreCalls.DAL;

namespace StoreCalls.Controllers
{
    public class CallController : Controller
    {
        private CallContext db = new CallContext();

        //
        // GET: /Call/
        [HttpGet]
        public ActionResult Index()
        {
            //var calls = db.Calls.Include(c => c.Employee).Include(c => c.Category);
            //return View(calls.ToList());
            var program = new SelectList(new[] { "Morning", "Afternoon" });
            List<SelectListItem> catDb = new List<SelectListItem>();
            var categories = db.Categories.Select(x => new SelectListItem() { Text = x.CategoryName, Value = x.CategoryName });
            foreach (var c in categories)
            {
                catDb.Add(new SelectListItem() { Text = c.Text, Value = c.Value });
            }
            
            //var categoriesList = new SelectList(new[] { "Sport", "Police" });

            SelectList tmp = new SelectList(catDb, "Value", "Text", null);
            ViewBag.ProgramList = program;
            ViewBag.CategoriesList = tmp;
            return View();
        }

        [HttpPost]
        public JsonResult Index(Models.Registration registration)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var aux = (from ca in db.Categories
                               where ca.CategoryName.Equals(registration.Categories)
                               select ca).Single();
                    Person person = new Person(registration.PhoneNumber, registration.Name, registration.LastName, registration.Address1, registration.Address2, registration.PostCode);
                    
                    
                    // Check if a person already exists on the table with that phone number.
                    var checkPerson = db.Persons.Any(p => p.PhoneNumber == person.PhoneNumber);
                    if (checkPerson)
                    {
                        Person tmpPerson = db.Persons.Single(p => p.PhoneNumber == person.PhoneNumber);

                        if (TryUpdateModel(tmpPerson, new string[] { "Name", "LastName", "Address1", "Address2", "PostCode" }))
                        {
                            try
                            {
                                db.Entry(tmpPerson).State = EntityState.Modified;
                                db.SaveChanges();
                                Call call = new Call(aux.CategoryId, 1, registration.Program, registration.Comments, registration.Positive, tmpPerson);
                                db.Calls.Add(call);
                                db.SaveChanges();
                            }
                            catch (DataException)
                            {
                                return Json(new { Comments = "error with the update" });
                            }
                        }
                        
                    }
                    else
                    {
                        Call call = new Call(aux.CategoryId, 1, registration.Program, registration.Comments, registration.Positive, person);
                        db.Calls.Add(call);
                        db.SaveChanges();
                    }


                    //employee.ForEach(s => context.Employees.AddOrUpdate(p => p.LastName, s));
                    //db.Calls.Add(call); //this works
                    //db.Calls.AddOrUpdate(c => c.comments);
                    


                    //db.SaveChanges();
                    
                    string message = string.Format("message '{0}'", registration.Comments);
                    registration.Comments = message;
                    //string message = string.Format("message '{0}'", call.Caller.Name);
                    return Json(registration);
                }
                catch (Exception e)
                {
                    throw e;
                }

            }
            string error = "error";
            return Json(error);
        }
        [HttpPost]
        public JsonResult Complete(Models.Registration registration)
        {
            var aux = db.Persons.Any(p => p.PhoneNumber == registration.PhoneNumber);
            if (aux)
            {
                Person tmpPerson = db.Persons.Single(p => p.PhoneNumber == registration.PhoneNumber);

                registration.Name = tmpPerson.Name;
                registration.LastName = tmpPerson.LastName;
                registration.Address1 = tmpPerson.Address1;
                registration.Address2 = tmpPerson.Address2;
                registration.PostCode = tmpPerson.PostCode;
                return Json(registration);
            }
            return Json(false);
        }

        [HttpPost]
        public JsonResult Historical(Models.Registration registration)
        {
            var aux = db.Persons.Any(p => p.PhoneNumber == registration.PhoneNumber);
            if (aux)
            {
                Person tmpPerson = db.Persons.Single(p => p.PhoneNumber == registration.PhoneNumber);

                var bCalls = db.Calls.Any(c => c.Person.PersonId == tmpPerson.PersonId);

                if (bCalls)
                {
                    var calls = (from ca in db.Calls
                                 where ca.Person.PersonId == tmpPerson.PersonId
                                 select ca);

                    List<Call> lCall = new List<Call>();
                    foreach (var c in calls)
                    {
                        Call call = new Call(c.Program, c.Comments, c.Positive);
                        lCall.Add(call);
                    }
                    //return this.Json(lCall);
                    return Json(new { Name = tmpPerson.Name, LastName = tmpPerson.LastName, Address1 = tmpPerson.Address1, Address2 = tmpPerson.Address2, PostCode = tmpPerson.PostCode, Calls = lCall });
                }
                else
                    return Json(false);
               
            }
            return Json(false);
        }


        //
        // GET: /Call/Details/5

        public ActionResult Details(long id = 0)
        {
            Call call = db.Calls.Find(id);
            if (call == null)
            {
                return HttpNotFound();
            }
            return View(call);
        }

        //
        // GET: /Call/Create

        public ActionResult Create()
        {
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "Name");
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName");
            return View();
        }

        //
        // POST: /Call/Create

        [HttpPost]
        public ActionResult Create(Call call)
        {
            if (ModelState.IsValid)
            {
                db.Calls.Add(call);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "Name", call.EmployeeId);
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", call.CategoryId);
            return View(call);
        }

        //
        // GET: /Call/Edit/5

        public ActionResult Edit(long id = 0)
        {
            Call call = db.Calls.Find(id);
            if (call == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "Name", call.EmployeeId);
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", call.CategoryId);
            return View(call);
        }

        //
        // POST: /Call/Edit/5

        [HttpPost]
        public ActionResult Edit(Call call)
        {
            if (ModelState.IsValid)
            {
                db.Entry(call).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "Name", call.EmployeeId);
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", call.CategoryId);
            return View(call);
        }

        //
        // GET: /Call/Delete/5

        public ActionResult Delete(long id = 0)
        {
            Call call = db.Calls.Find(id);
            if (call == null)
            {
                return HttpNotFound();
            }
            return View(call);
        }

        //
        // POST: /Call/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            Call call = db.Calls.Find(id);
            db.Calls.Remove(call);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}