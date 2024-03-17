using Explorer.BuildingBlocks.Core.Domain;
//using Explorer.Tours.Core.Domain.ShoppingCarts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain.Tours
{
    public class Tour : Entity
    {
        public string Name { get; private set; }

        public DifficultyLevel DifficultyLevel { get; private set; }

        public string Description { get; private set; }

        //public List<string> Tags { get; private set; }


        public TourStatus Status { get;  set; }

        public int Price { get; private set; }

        public int UserId { get; private set; }

        public List<Equipment> Equipments { get; } = new List<Equipment> ();

       // public DateTime? PublishedDateTime { get; private set; }

		//public DateTime? ArchivedDateTime { get; private set; }


		public ICollection<TourPoint> TourPoints { get; } = new List<TourPoint>();

        //public ICollection<TourCharacteristic> TourCharacteristics { get; } = new List<TourCharacteristic>();
        //public ICollection<TourReview> TourReviews { get; }= new List<TourReview>();

        public Tour(string name, DifficultyLevel difficultyLevel, string? description, int userId)
        {
            if (string.IsNullOrWhiteSpace(name) || userId == 0) throw new ArgumentException("Field empty.");

            if (!Enum.IsDefined(typeof(DifficultyLevel), difficultyLevel))
            {
                throw new ArgumentException("Invalid DifficultyLevel value.");
            }
            Name = name;
            DifficultyLevel = difficultyLevel;
            Description = description;
            Status = TourStatus.Draft;
            //PublishedDateTime = null; 
            //ArchivedDateTime = null;
            Price = 0;
            UserId = userId;
            


        }

        public void UpdateTour(string name, DifficultyLevel difficultyLevel, string? description, int userId, TourStatus status)
        {
            Name=name;
			DifficultyLevel = difficultyLevel;
            Description = description;
            UserId = userId;
            Status = status;


		}
        /*
        public void setCharacteristic(double distance, double duration, TransportType transportType)
        {
            TourCharacteristic characteristic = TourCharacteristics.FirstOrDefault(c => c.TransportType == transportType);
            if (characteristic != null) 
            {
                TourCharacteristics.Remove(characteristic);
            }
            TourCharacteristics.Add(new TourCharacteristic(distance, duration, transportType));
             
        }

        public void Publish(Tour tour)
        {
            if (Status != TourStatus.Draft)
            {
                throw new InvalidOperationException("Only draft tours can be published.");
            }

            tour.Status= TourStatus.Published;
            tour.PublishedDateTime = DateTime.UtcNow;



        }

        public void Archive(Tour tour)
        {
            if(Status != TourStatus.Published)
            {

				throw new InvalidOperationException("Only published tours can be archived.");
			}

            tour.Status = TourStatus.Archived;
            tour.ArchivedDateTime = DateTime.UtcNow;
		}
        */


    }


}
