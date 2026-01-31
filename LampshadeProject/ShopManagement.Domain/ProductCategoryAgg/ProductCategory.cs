using _0_Framework.Domain;
using ShopManagement.Domain.ProductAgg;

namespace ShopManagement.Domain.ProductCategoryAgg
{
    public class ProductCategory : EntityBase
    {
        #region properties
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Picture { get; private set; }
        public string PictureAlt { get; private set; }
        public string PictureTitle { get; private set; }
        public string Keywords { get; private set; }
        public string MetaDescription { get; private set; }
        public string Slug { get; private set; }
        public List<Product> Products { get; private set; }
        #endregion

        public ProductCategory()
        {
            Products = new List<Product>();
        }

        #region constractor
        public ProductCategory(string name, string description, string picture,
                                string pictureAlt, string pictureTitle, string keywords,
                                string metaDescription, string slug)
        {
            Name = name;
            Description = description;
            Picture = picture;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            Keywords = keywords;
            MetaDescription = metaDescription;
            Slug = slug;
        }
        #endregion

        #region edit
        public void Edit(string name, string description, string picture,
                                string pictureAlt, string pictureTitle, string keywords,
                                string metaDescription, string slug)
        {
            Name = name;
            Description = description;
            Picture = picture;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            Keywords = keywords;
            MetaDescription = metaDescription;
            Slug = slug;
        }
        #endregion
    }
}
