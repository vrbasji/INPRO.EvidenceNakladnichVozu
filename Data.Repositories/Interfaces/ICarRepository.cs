﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Interfaces
{
    public interface ICarRepository
    {
        List<Car> GetAll();

        Car GetById(int carId);

        List<Revision> GetCarRevision(int carId);

        List<Serie> GetAllSeries(int startId, int endId);

        bool ExcludeCar(int carId);

        int AddCar(Car car);

        Car EditCar(Car car);

        Car DeleteCar(int carId);

        List<Car> GetForPages(int start, int end);

        Fault AddFaultToCar(Fault fault, int carId);

        List<Car> FindCars(string query);

        List<Car> GetDashboard();
    }
}
