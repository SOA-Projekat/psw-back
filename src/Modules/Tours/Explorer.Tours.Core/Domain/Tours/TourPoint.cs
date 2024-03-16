using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Tours.Core.Domain.Tours
{
    public class TourPoint : Entity
    {
        public long TourId { get; init; }
        public Tour? Tour { get; init; }
        public string Name { get; init; }
        public string? Description { get; init; }
        public double Latitude { get; init; }
        public double Longitude { get; init; }
        public string ImageUrl { get; init; }

      

        public TourPoint(int idTour, string name, string? description, double latitude, double longitude, string imageUrl)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Invalid Name.");
            TourId = idTour;
            Name = name;
            Description = description;
            Latitude = latitude;
            Longitude = longitude;
            ImageUrl = imageUrl;
         
        }

        public TourPoint() { }
    }
}
