﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using CrudMVCCodeFirst.Data;
using CrudMVCCodeFirst.Models;

namespace CrudMVCCodeFirst.Controllers
{
    public class LaunchController : Controller
    {
        private LaunchContext db = new LaunchContext();

        // GET: Launch
        public ActionResult Index()
        {
            return View(db.Launches.ToList());
        }

        // GET: Launch/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LaunchEntry launchEntry = db.Launches.Find(id);
            if (launchEntry == null)
            {
                return HttpNotFound();
            }
            return View(launchEntry);
        }

        //// GET: Launch/Create
        public ActionResult Create()
        {
            var user = this.User;
            return View();
        }

        // POST: Launch/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,LaunchInfo,PostedByUserName")] LaunchEntry launchEntry)
        {
            launchEntry.PostedByUserName = this.User.Identity.Name;

            try
            {               

                if (ModelState.IsValid)
                {
                    db.Launches.Add(launchEntry);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Unable to save");
            }

            return View(launchEntry);
        }

        // GET: Launch/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LaunchEntry launchEntry = await db.Launches.FindAsync(id);

            if (launchEntry == null)
            {
                return HttpNotFound();
            }

            return View(launchEntry);
        }

        // POST: Launch/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,LaunchInfo,PostedByUserName")] LaunchEntry launchEntry)
        {
            launchEntry.PostedByUserName = this.User.Identity.Name;

            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(launchEntry).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                ModelState.AddModelError("", "Unable to Save");
            }
            return View(launchEntry);
        }

        // GET: Launch/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LaunchEntry launchEntry = db.Launches.FirstOrDefault(launches => launches.Id == id);

            return View(launchEntry);
        }

        // POST: Launch/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                LaunchEntry launchEntry = db.Launches.Find(id);
                if (launchEntry == null)
                {
                    return HttpNotFound();
                }
                db.Launches.Remove(launchEntry);
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                return View("Error");
            }
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
