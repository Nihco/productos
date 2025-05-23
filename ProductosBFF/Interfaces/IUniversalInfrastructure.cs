﻿using ProductosBFF.Domain.Parameters;
using ProductosBFF.Domain.Universal;
using System.Threading.Tasks;

namespace ProductosBFF.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IUniversalInfrastructure
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ingresoUniversal"></param>
        /// <returns></returns>
        public Task<IngresoUniversalNSD> IngresoUniversal(IngresoUniversal ingresoUniversal);

    }
}