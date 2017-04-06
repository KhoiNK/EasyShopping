using Easyshopping.DataAccess.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyShopping.BusinessLogic.Models
{
    public static class ProducerTranslator
    {
        public static ProducerDTO ToProducerBusiness(this Producer producer)
        {
            if (producer == null) { return null; }

            return new ProducerDTO
            {
                ID = producer.ID,
                Name = producer.Name,
                StartDate = producer.StartDate,
                EndDate = producer.EndDate,
                Description = producer.Description,
                StatusID = producer.Status_ID,
            };

        }

        public static IList<ProducerDTO> ToProducerBusiness(this IEnumerable<Producer> producers)
        {
            if (producers == null || !producers.Any()) { return null; }

            return producers.Select(e => e.ToProducerBusiness()).ToList();

        }

        public static Producer ToProducerEntity(this ProducerDTO producer)
        {
            if (producer == null) { return null; }
            return new Producer
            {
                ID = producer.ID,
                Name = producer.Name,
                StartDate = producer.StartDate,
                EndDate = producer.EndDate,
                Description = producer.Description,
                Status_ID = producer.StatusID
            };
        }

        public static IEnumerable<Producer> ToProducerEntity(this IList<ProducerDTO> producers)
        {
            if (producers == null || !producers.Any()) { return null; }
            return producers.Select(e => e.ToProducerEntity()).ToList();
        }
    }
}

