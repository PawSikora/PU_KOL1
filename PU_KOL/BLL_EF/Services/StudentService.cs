using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTOModels;
using BLL.ServiceInterfaces;
using DAL;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace BLL_EF.Services
{
    public class StudentService : IStudentService
    {
        private readonly Context _context;
        private readonly IHistoriaService _historiaService;

        public StudentService(Context context, IHistoriaService historiaService)
        {
            _context = context;
            _historiaService = historiaService;
        }

        public async Task<IEnumerable<StudentDTO>> GetAllStudenci()
        {
            return await _context.Studenci
                .Select(s => new StudentDTO
                {
                    ID = s.ID,
                    Imie = s.Imie,
                    Nazwisko = s.Nazwisko,
                    IDGrupy = s.IDGrupy
                }).ToListAsync();
        }

        public async Task<StudentDTO> GetStudentById(int id)
        {
            var s = await _context.Studenci.FindAsync(id);
            if (s == null) return null;
            return new StudentDTO
            {
                ID = s.ID,
                Imie = s.Imie,
                Nazwisko = s.Nazwisko,
                IDGrupy = s.IDGrupy
            };
        }

        public async Task<StudentDTO> CreateStudent(StudentDTO dto)
        {
            var student = new Student
            {
                Imie = dto.Imie,
                Nazwisko = dto.Nazwisko,
                IDGrupy = dto.IDGrupy
            };
            _context.Studenci.Add(student);
            await _context.SaveChangesAsync();
            return new StudentDTO
            {
                ID = student.ID,
                Imie = student.Imie,
                Nazwisko = student.Nazwisko,
                IDGrupy = student.IDGrupy
            };
        }

        public async Task<StudentDTO> UpdateStudent(int id, StudentDTO dto)
        {
            var s = await _context.Studenci.FindAsync(id);
            if (s == null) return null;

            s.Imie = dto.Imie;
            s.Nazwisko = dto.Nazwisko;
            s.IDGrupy = dto.IDGrupy;

            await _historiaService.AddHistoria(new HistoriaDTO
            {
                Imie = s.Imie,
                Nazwisko = s.Nazwisko,
                IDGrupy = s.IDGrupy,
                TypAkcji = "edycja",
                Data = DateTime.Now
            });

            await _context.SaveChangesAsync();

            return new StudentDTO
            {
                ID = s.ID,
                Imie = s.Imie,
                Nazwisko = s.Nazwisko,
                IDGrupy = s.IDGrupy
            };
        }

        public async Task<bool> DeleteStudent(int id)
        {
            var s = await _context.Studenci.FindAsync(id);
            if (s == null) return false;

            await _historiaService.AddHistoria(new HistoriaDTO
            {
                Imie = s.Imie,
                Nazwisko = s.Nazwisko,
                IDGrupy = s.IDGrupy,
                TypAkcji = "usuwanie",
                Data = DateTime.Now
            });

            _context.Studenci.Remove(s);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
