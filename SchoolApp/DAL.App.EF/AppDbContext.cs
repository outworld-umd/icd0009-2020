using System;
using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF {

    public class AppDbContext : IdentityDbContext {

        public DbSet<AbsenceReason> AbsenceReasons { get; set; }
        public DbSet<Dependence> Dependences { get; set; }
        public DbSet<DependenceType> DependenceTypes { get; set; }
        public DbSet<Form> Forms { get; set; }
        public DbSet<FormRole> FormRoles { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<GradeType> GradeTypes { get; set; }
        public DbSet<GradeColumn> GradeColumns { get; set; }
        public DbSet<Homework> Homeworks { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<PersonForm> PersonForms { get; set; }
        public DbSet<PersonGroup> PersonGroups { get; set; }
        public DbSet<Remark> Remarks { get; set; }
        public DbSet<RemarkType> RemarkTypes { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<SubjectGroup> SubjectGroups { get; set; }
        
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    }

}