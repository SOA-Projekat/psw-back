using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Public.Administration
{
    public interface ITourStatisticsService
    {
        public int GetNumberOfPurchaseByAuthor(int authorId);
        public List<TourDTO> GetPurchasedToursByAuthor(int authorId);

        public int GetNumberOfCompletedByAuthor(int authorId);

        public int GetNumberOfStartedByAuthor(int authorId);

        public int GetNumberOfPurchaseByTour(int authorId, int tourId);

        public int GetNumberOfStartedByTour(int authorId, int tourId);
        public int GetNumberOfCompletedByTour(int authorId, int tourId);

       // public List<double> GetVisitedTourPointPercentage(int authorId,int tourId);

        //public List<double> GetTourPointEncounterPercentage(int authorId, int tourId);
       // public List<int> GetMaxPercentage(int authorId);

    }
}
