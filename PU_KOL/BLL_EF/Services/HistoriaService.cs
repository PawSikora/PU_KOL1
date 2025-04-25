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
    public class HistoriaService : IHistoriaService
    {
        private readonly Context _context;

        public HistoriaService(Context context)
        {
            _context = context;
        }

        public async Task AddHistoria(HistoriaDTO dto)
        {
            var historia = new Historia
            {
                Imie = dto.Imie,
                Nazwisko = dto.Nazwisko,
                IDGrupy = dto.IDGrupy,
                TypAkcji = dto.TypAkcji,
                Data = dto.Data
            };
            _context.Historie.Add(historia);
            await _context.SaveChangesAsync();
        }

        public async Task<List<HistoriaDTO>> GetHistoriaPage(int pageNumber, int pageSize)
        {
            return await _context.Historie
                .OrderByDescending(h => h.Data)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(h => new HistoriaDTO
                {
                    ID = h.ID,
                    Imie = h.Imie,
                    Nazwisko = h.Nazwisko,
                    IDGrupy = h.IDGrupy,
                    TypAkcji = h.TypAkcji,
                    Data = h.Data
                })
                .ToListAsync();
        }
        }
}
