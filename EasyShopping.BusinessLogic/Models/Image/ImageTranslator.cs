using Easyshopping.DataAccess.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyShopping.BusinessLogic.Models
{
    public static class ImageTranslator
    {
        public static ImageDTO ToImageBusiness(this Image image)
        {
            if (image == null) { return null; }

            return new ImageDTO
            {
                Link = image.Link,
                ID = image.ID,
                ProductID = image.ProductID
            };

        }

        public static IList<ImageDTO> ToImageBusiness(this IEnumerable<Image> images)
        {
            if (images == null || !images.Any()) { return null; }

            return images.Select(e => e.ToImageBusiness()).ToList();

        }

        public static Image ToImageEntity(this ImageDTO image)
        {
            if (image == null) { return null; }
            return new Image
            {
                Link = image.Link,
                ID = image.ID,
                ProductID = image.ProductID
            };
        }

        public static IEnumerable<Image> ToImageEntity(this IList<ImageDTO> images)
        {
            if (images == null || !images.Any()) { return null; }
            return images.Select(e => e.ToImageEntity()).ToList();
        }
    }
}
