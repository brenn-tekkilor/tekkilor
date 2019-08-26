#nullable enable
using org.common.retail.merchandise.interfaces;
namespace org.common.retail.merchandise
{
    public class Category : ACategory
    {
        private static readonly Db DB = Db.GET_INSTANCE();
        public IIdGenerator DB_ID { get; } = StringObjectIdGenerator.Instance;
        public string DB_C { get; } = Environment.GetEnvironmentVariable("CATEGORY_COLLECTION");
        public string Id { get; set; }
        public CategoryFunction? Function { get; set; }
        public CategoryLevel? Level { get; set; }
        public IItemSellingRule? ItemSellingRule { get; set; }
        public string Name { get; set; }
        public string? URL { get; set; }
        public string? Description { get; set; }
        public Occasions? Occasion { get; set; }
        public SalesRestrictionType? Restriction { get; set; }
        public Dictionary<string, ACategory> Children { get; set; }
        public new Category(string name, string? url, string? description
                                                            , CategoryFunction? function
                                                            , CategoryLevel? level, Occasions? occasion
                                                            , SalesRestrictionType? restriction)
        {
            Id = (string)DB_ID.GenerateId(DB.GetCollection(DB_C), this);
            Name = name;
            URL = url;
            Description = description;
            Function = function;
            Level = level;
            Occasion = occasion;
            Restriction = restriction;
            Children = new Dictionary<string, Category>();
        }

        public new Category Save()
        {
            return (Category)DB.Save(this);
        }
        public new void AddChild(Category c)
        {
            if (c.Id == null)
                AddChild((Category)c.Save());
            if (c.Id != null)
                Children.Add(c.Id, c);
            DB.ReplaceOne(this);

        }
    }
}
