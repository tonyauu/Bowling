using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Bowling.DAL;
using Bowling.Models;
using System.Dynamic;
using PagedList;

namespace Bowling.Controllers
{
    
    public class PeopleController : Controller
    {
        private BowlingContext db = new BowlingContext();

        [Authorize(Users = "redmi@uw.edu")]
        // GET: People
        public ActionResult Index(string sortOrder, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            var people = from p in db.People
                         select p;
            if (!String.IsNullOrEmpty(searchString))
            {
                people = people.Where(p => p.LastName.Contains(searchString)
                                       || p.FirstName.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    people = people.OrderByDescending(p => p.LastName);
                    break;
                default:
                    people = people.OrderBy(p => p.LastName);
                    break;
            }
            
            return View(people.ToList());
        }
        // GET: People/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.People.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        // GET: People/Create
        public ActionResult Create()
        {
            PopulateLaneDropDownList();
            return View();
        }

        private void PopulateLaneDropDownList(object selectLane = null)
        {
            var laneQuery = from l in db.Lanes
                            orderby l.Name
                            select l;
            ViewBag.LaneID = new SelectList(laneQuery, "LaneID", "Name", selectLane);

        }


        // POST: People/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FirstName,LastName,Email, ReservationTime")] Person person, Reserve reserve)
        {
            
            try
            {
                if (ModelState.IsValid)
                {
                    db.People.Add(person);
                    db.Reserves.Add(reserve);
                    db.SaveChanges();
                    return RedirectToAction("Thanks", "Home");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to reserve lane. Try again, and if the problem persists manually write it down and speak with the manager.");
            }
            PopulateLaneDropDownList();
            return View(person);
        }

        // GET: People/Edit/5
        public ActionResult Edit(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.People.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            PopulateLaneDropDownList();
            return View(person);
        }

        // POST: People/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var personToUpdate = db.People.Find(id);
            var reserveToUpdate = db.Reserves.Find(id);
            if(TryUpdateModel(personToUpdate, "", new String[] { "FirstName", "LastName", "Email", "ReservationTime", "LaneName" }))
            {
                try
                {
                    
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (DataException)
                {
                    ModelState.AddModelError("", "Unable to reserve lane. Try again, and if the problem persists manually write it down and speak with the manager.");

                }
                PopulateLaneDropDownList();
            }
            return View(personToUpdate);
        }

        // GET: People/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.People.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        // POST: People/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Person person = db.People.Find(id);
            db.People.Remove(person);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
