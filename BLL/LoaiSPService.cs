using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
namespace BLL
{
    public class LoaiSPService
    {
        public List<LoaiSP> GetAll()
        {
            HanghoaModel context = new HanghoaModel();
            return context.LoaiSPs.ToList();
        }
    }
}
