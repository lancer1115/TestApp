﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TestApp.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class TestApplicationEntities : DbContext
    {
        public TestApplicationEntities()
            : base("name=TestApplicationEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<TestPlan> TestPlans { get; set; }
        public DbSet<TestRun> TestRuns { get; set; }
        public DbSet<TestSuite> TestSuites { get; set; }
    }
}
