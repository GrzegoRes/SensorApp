﻿using SensorApp.API.Repository.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SensorApp.API.Repository
{
    public interface IRepositorySensor
    {
        Task<IEnumerable<SensorDB>> GetAll();
        Task Create(SensorDB sensor);
        Task<IEnumerable<SensorDB>> GetAllById(int sensorI);
    }
}
