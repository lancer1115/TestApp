using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TestApp.Models;

namespace TestApp.Controllers
{
    public class TestSuiteController : Controller
    {
        private TestApplicationEntities db = new TestApplicationEntities();

        // GET: /TestSuite/
        public ActionResult Index(string sortOrder, string searchString)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "Date_desc" : "Date";
            var testsuites = from ts in db.TestSuites
                           select ts;
            if (!String.IsNullOrEmpty(searchString))
            {
                testsuites = testsuites.Where(ts => ts.Name.ToUpper().Contains(searchString.ToUpper()));
            }
            switch (sortOrder)
            {
                case "Name_desc":
                    testsuites = testsuites.OrderByDescending(ts => ts.Name);
                    break;
                case "Date":
                    testsuites = testsuites.OrderBy(ts => ts.CreateDate);
                    break;
                case "Date_desc":
                    testsuites = testsuites.OrderByDescending(ts => ts.CreateDate);
                    break;
                default:
                    testsuites = testsuites.OrderBy(ts => ts.Name);
                    break;
            }
            return View(testsuites.ToList());
        }

        // GET: /TestSuite/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestSuite testsuite = db.TestSuites.Find(id);
            if (testsuite == null)
            {
                return HttpNotFound();
            }
            return View(testsuite);
        }

        // GET: /TestSuite/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /TestSuite/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="TestSuiteID,Name,Description,CreateDate")] TestSuite testsuite)
        {
            if (ModelState.IsValid)
            {
                db.TestSuites.Add(testsuite);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(testsuite);
        }

        // GET: /TestSuite/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestSuite testsuite = db.TestSuites.Find(id);
            if (testsuite == null)
            {
                return HttpNotFound();
            }
            return View(testsuite);
        }

        // POST: /TestSuite/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="TestSuiteID,Name,Description,CreateDate")] TestSuite testsuite)
        {
            if (ModelState.IsValid)
            {
                db.Entry(testsuite).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(testsuite);
        }

        // GET: /TestSuite/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestSuite testsuite = db.TestSuites.Find(id);
            if (testsuite == null)
            {
                return HttpNotFound();
            }
            return View(testsuite);
        }

        // POST: /TestSuite/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TestSuite testsuite = db.TestSuites.Find(id);
            db.TestSuites.Remove(testsuite);
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
