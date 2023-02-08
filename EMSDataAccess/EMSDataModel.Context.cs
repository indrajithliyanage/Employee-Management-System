﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EMSDataAccess
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class EMSDBEntities : DbContext
    {
        public EMSDBEntities()
            : base("name=EMSDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<EmployeeType> EmployeeTypes { get; set; }
        public virtual DbSet<LeaveApply> LeaveApplies { get; set; }
        public virtual DbSet<LeaveType> LeaveTypes { get; set; }
    
        public virtual ObjectResult<DepartmentSearchSP_Result> DepartmentSearchSP(Nullable<int> deptID, string deptName)
        {
            var deptIDParameter = deptID.HasValue ?
                new ObjectParameter("DeptID", deptID) :
                new ObjectParameter("DeptID", typeof(int));
    
            var deptNameParameter = deptName != null ?
                new ObjectParameter("DeptName", deptName) :
                new ObjectParameter("DeptName", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<DepartmentSearchSP_Result>("DepartmentSearchSP", deptIDParameter, deptNameParameter);
        }
    
        public virtual ObjectResult<EmployeeSearchSP_Result> EmployeeSearchSP(Nullable<int> empID, string firstName)
        {
            var empIDParameter = empID.HasValue ?
                new ObjectParameter("EmpID", empID) :
                new ObjectParameter("EmpID", typeof(int));
    
            var firstNameParameter = firstName != null ?
                new ObjectParameter("FirstName", firstName) :
                new ObjectParameter("FirstName", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<EmployeeSearchSP_Result>("EmployeeSearchSP", empIDParameter, firstNameParameter);
        }
    
        public virtual ObjectResult<Employee> EmpSearchF(Nullable<int> empID, string firstName)
        {
            var empIDParameter = empID.HasValue ?
                new ObjectParameter("EmpID", empID) :
                new ObjectParameter("EmpID", typeof(int));
    
            var firstNameParameter = firstName != null ?
                new ObjectParameter("FirstName", firstName) :
                new ObjectParameter("FirstName", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Employee>("EmpSearchF", empIDParameter, firstNameParameter);
        }
    
        public virtual ObjectResult<Employee> EmpSearchF(Nullable<int> empID, string firstName, MergeOption mergeOption)
        {
            var empIDParameter = empID.HasValue ?
                new ObjectParameter("EmpID", empID) :
                new ObjectParameter("EmpID", typeof(int));
    
            var firstNameParameter = firstName != null ?
                new ObjectParameter("FirstName", firstName) :
                new ObjectParameter("FirstName", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Employee>("EmpSearchF", mergeOption, empIDParameter, firstNameParameter);
        }
    
        public virtual ObjectResult<Department> DeptSearchF(Nullable<int> deptID, string deptName)
        {
            var deptIDParameter = deptID.HasValue ?
                new ObjectParameter("DeptID", deptID) :
                new ObjectParameter("DeptID", typeof(int));
    
            var deptNameParameter = deptName != null ?
                new ObjectParameter("DeptName", deptName) :
                new ObjectParameter("DeptName", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Department>("DeptSearchF", deptIDParameter, deptNameParameter);
        }
    
        public virtual ObjectResult<Department> DeptSearchF(Nullable<int> deptID, string deptName, MergeOption mergeOption)
        {
            var deptIDParameter = deptID.HasValue ?
                new ObjectParameter("DeptID", deptID) :
                new ObjectParameter("DeptID", typeof(int));
    
            var deptNameParameter = deptName != null ?
                new ObjectParameter("DeptName", deptName) :
                new ObjectParameter("DeptName", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Department>("DeptSearchF", mergeOption, deptIDParameter, deptNameParameter);
        }
    }
}