﻿using System.Collections.Generic;
using OdeToFood.Core;
using System.Linq;

namespace OdeToFood.Data
{
    public class SqlRestaurantData : IRestaurantData
    {
        private readonly OdeToFoodDbContext db;

        public SqlRestaurantData(OdeToFoodDbContext db)
        {
            this.db = db;
        }

        public Restaurant Add(Restaurant newRestaurant)
        {
            db.Add(newRestaurant);
            return newRestaurant;
        }

        public int Commit()
        {
            return db.SaveChanges();
        }

        public Restaurant Delete(int id)
        {
            var restaurant = GetById(id);

            if (restaurant != null)
            {
                db.Restaurant.Remove(restaurant);
            }

            return restaurant;
        }

        public Restaurant GetById(int? id)
        {
            return db.Restaurant.Find(id);
        }

        public IEnumerable<Restaurant> GetRestaurantsByName(string name)
        {
            var query = from r in db.Restaurant
                        where (r.Name.ToLower()).Contains(name) || string.IsNullOrEmpty(name)
                        orderby r.Name
                        select r;
            return query;
        }

        public Restaurant Update(Restaurant updatedRestaurant)
        {
            var entity = db.Restaurant.Attach(updatedRestaurant);
            entity.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            return updatedRestaurant;
        }
    }
}
