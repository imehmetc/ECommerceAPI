using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Domain.Enums
{
    public enum OrderStatus
    {
        New = 1,            // Yeni Sipariş
        Approved,           // Onaylandı
        Shipped,            // Kargoda
        InBranch,           // Şubede
        DistributionPhase,  // Dağıtım Aşaması
        Delivered           //Teslim edilmiş 
    }
}
