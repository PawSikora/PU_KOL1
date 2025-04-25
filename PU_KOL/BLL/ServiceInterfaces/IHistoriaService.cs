using BLL.DTOModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ServiceInterfaces
{
    public interface IHistoriaService
    {
        Task AddHistoria(HistoriaDTO dto);
        Task<List<HistoriaDTO>> GetHistoriaPage(int pageNumber, int pageSize);

    }
}
