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
    public class TestRunController : Controller
    {
        private TestApplicationEntities db = new TestApplicationEntities();

        // GET: /TestRun/
        public ActionResult Index(string sortOrder, string searchString)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "Date_desc" : "Date";
            ViewBag.StatusSortParm = sortOrder == "Status" ? "Status_desc" : "Status";
            var testruns = from tr in db.TestRuns
                           select tr;
            if (!String.IsNullOrEmpty(searchString))
            {
                testruns = testruns.Where(tr => tr.Name.ToUpper().Contains(searchString.ToUpper()));
            }
            switch (sortOrder)
            {
                case "Name_desc":
                    testruns = testruns.OrderByDescending(tr => tr.Name);
                    break;
                case "Date":
                    testruns = testruns.OrderBy(tr => tr.RunDate);
                    break;
                case "Date_desc":
                    testruns = testruns.OrderByDescending(tr => tr.RunDate);
                    break;
                case "Status":
                    testruns = testruns.OrderBy(tr => tr.Status);
                    break;
                case "Status_desc":
                    testruns = testruns.OrderByDescending(tr => tr.Status);
                    break;
                default:
                    testruns = testruns.OrderBy(tr => tr.Name);
                    break;
            }
            return View(testruns.ToList());
        }

        // GET: /TestRun/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestRun testrun = db.TestRuns.Find(id);
            if (testrun == null)
            {
                return HttpNotFound();
            }
            return View(testrun);
        }

        // GET: /TestRun/Create
        public ActionResult Create()
        {
            ViewBag.TestPlanID = new SelectList(db.TestPlans, "TestPlanID", "Name");
            return View();
        }

        // POST: /TestRun/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="TestRunID,Name,TestPlanID,RunDate,Description,TestSteps,ExpectedResults,Status")] TestRun testrun)
        {
            if (ModelState.IsValid)
            {
                db.TestRuns.Add(testrun);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TestPlanID = new SelectList(db.TestPlans, "TestPlanID", "Name", testrun.TestPlanID);
            return View(testrun);
        }

        // GET: /TestRun/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestRun testrun = db.TestRuns.Find(id);
            if (testrun == null)
            {
                return HttpNotFound();
            }
            ViewBag.TestPlanID = new SelectList(db.TestPlans, "TestPlanID", "Name", testrun.TestPlanID);
            return View(testrun);
        }

        // POST: /TestRun/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="TestRunID,Name,TestPlanID,RunDate,Description,TestSteps,ExpectedResults,Status")] TestRun testrun)
        {
            if (ModelState.IsValid)
            {
                db.Entry(testrun).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TestPlanID = new SelectList(db.TestPlans, "TestPlanID", "Name", testrun.TestPlanID);
            return View(testrun);
        }

        // GET: /TestRun/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestRun testrun = db.TestRuns.Find(id);
            if (testrun == null)
            {
                return HttpNotFound();
            }
            return View(testrun);
        }

        // POST: /TestRun/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TestRun testrun = db.TestRuns.Find(id);
            db.TestRuns.Remove(testrun);
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
