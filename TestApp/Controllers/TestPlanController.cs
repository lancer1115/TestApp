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
    public class TestPlanController : Controller
    {
        private TestApplicationEntities db = new TestApplicationEntities();

        // GET: /TestPlan/
        public ActionResult Index(string sortOrder, string searchString)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "Date_desc" : "Date";
            var testplans = from tp in db.TestPlans
                            select tp;
            if (!String.IsNullOrEmpty(searchString))
            {
                testplans = testplans.Where(tp => tp.Name.ToUpper().Contains(searchString.ToUpper()));
            }
            switch (sortOrder)
            {
                case "Name_desc":
                    testplans = testplans.OrderByDescending(tp => tp.Name);
                    break;
                case "Date":
                    testplans = testplans.OrderBy(tp => tp.CreateDate);
                    break;
                case "Date_desc":
                    testplans = testplans.OrderByDescending(tp => tp.CreateDate);
                    break;
                default:
                    testplans = testplans.OrderBy(tp => tp.Name);
                    break;
            }
            return View(testplans.ToList());
        }

        // GET: /TestPlan/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestPlan testplan = db.TestPlans.Find(id);
            if (testplan == null)
            {
                return HttpNotFound();
            }
            return View(testplan);
        }

        // GET: /TestPlan/Create
        public ActionResult Create()
        {
            ViewBag.TestSuiteID = new SelectList(db.TestSuites, "TestSuiteID", "Name");
            return View();
        }

        // POST: /TestPlan/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="TestPlanID,Name,Description,CreateDate,TestSuiteID,FeaturesTested,FeaturesNotTested,Approach,PassCriteria,FailCriteria,SuspensionCriteria,ResumptionCriteria,Deliverables,Responsibilities,Schedule,Risks")] TestPlan testplan)
        {
            if (ModelState.IsValid)
            {
                db.TestPlans.Add(testplan);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TestSuiteID = new SelectList(db.TestSuites, "TestSuiteID", "Name", testplan.TestSuiteID);
            return View(testplan);
        }

        // GET: /TestPlan/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestPlan testplan = db.TestPlans.Find(id);
            if (testplan == null)
            {
                return HttpNotFound();
            }
            ViewBag.TestSuiteID = new SelectList(db.TestSuites, "TestSuiteID", "Name", testplan.TestSuiteID);
            return View(testplan);
        }

        // POST: /TestPlan/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="TestPlanID,Name,Description,CreateDate,TestSuiteID,FeaturesTested,FeaturesNotTested,Approach,PassCriteria,FailCriteria,SuspensionCriteria,ResumptionCriteria,Deliverables,Responsibilities,Schedule,Risks")] TestPlan testplan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(testplan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TestSuiteID = new SelectList(db.TestSuites, "TestSuiteID", "Name", testplan.TestSuiteID);
            return View(testplan);
        }

        // GET: /TestPlan/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestPlan testplan = db.TestPlans.Find(id);
            if (testplan == null)
            {
                return HttpNotFound();
            }
            return View(testplan);
        }

        // POST: /TestPlan/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TestPlan testplan = db.TestPlans.Find(id);
            db.TestPlans.Remove(testplan);
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
