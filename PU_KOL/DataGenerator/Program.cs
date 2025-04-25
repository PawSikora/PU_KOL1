using Bogus;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using DAL;

var options = new DbContextOptionsBuilder<Context>()
    .UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=PU_KOLDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False;")
    .Options;


using var context = new Context(options);

context.Database.ExecuteSqlRaw("DISABLE TRIGGER trg_Student_Historia_Audit ON Student");

context.Historie.RemoveRange(context.Historie);
context.Studenci.RemoveRange(context.Studenci);
context.Grupy.RemoveRange(context.Grupy);
context.SaveChanges();

var rootGroupsFaker = new Faker<Grupa>()
    .RuleFor(g => g.Nazwa, f => f.Commerce.Department())
    .RuleFor(g => g.Studenci, f => new List<Student>());
var rootGroups = rootGroupsFaker.Generate(2);

context.Grupy.AddRange(rootGroups);
context.SaveChanges();

var childGroups = new List<Grupa>();
var childGroupsFaker = new Faker<Grupa>()
    .RuleFor(g => g.Nazwa, f => f.Commerce.Department())
    .RuleFor(g => g.Studenci, f => new List<Student>());

foreach (var root in rootGroups)
{
    var children = childGroupsFaker.Generate(2);
    foreach (var child in children)
    {
        child.ParentID = root.ID;
    }
    childGroups.AddRange(children);
}
context.Grupy.AddRange(childGroups);
context.SaveChanges();


var grandchildGroups = new List<Grupa>();
var grandchildFaker = new Faker<Grupa>()
    .RuleFor(g => g.Nazwa, f => f.Commerce.Department())
    .RuleFor(g => g.Studenci, f => new List<Student>());
foreach (var child in childGroups)
{
    var grandchild = grandchildFaker.Generate();
    grandchild.ParentID = child.ID;
    grandchildGroups.Add(grandchild);
}
context.Grupy.AddRange(grandchildGroups);
context.SaveChanges();

var allGroups = rootGroups.Concat(childGroups).Concat(grandchildGroups).ToList();

var studentFaker = new Faker<Student>()
    .RuleFor(s => s.Imie, f => f.Name.FirstName())
    .RuleFor(s => s.Nazwisko, f => f.Name.LastName())
    .RuleFor(s => s.IDGrupy, f => f.PickRandom(allGroups).ID);
var studenci = studentFaker.Generate(30);
context.Studenci.AddRange(studenci);
context.SaveChanges();


var historiaFaker = new Faker<Historia>()
    .RuleFor(h => h.Imie, f => f.PickRandom(studenci).Imie)
    .RuleFor(h => h.Nazwisko, f => f.PickRandom(studenci).Nazwisko)
    .RuleFor(h => h.IDGrupy, f => f.PickRandom(studenci).IDGrupy)
    .RuleFor(h => h.TypAkcji, f => f.PickRandom(new[] { "edycja", "usuwanie" }))
    .RuleFor(h => h.Data, f => f.Date.Past(1));
var historie = historiaFaker.Generate(20);
context.Historie.AddRange(historie);
context.SaveChanges();

context.Database.ExecuteSqlRaw("ENABLE TRIGGER trg_Student_Historia_Audit ON Student");

Console.WriteLine("Wygenerowano dane testowe.");