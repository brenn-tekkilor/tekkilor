#nullable enable
using data.mongodb.extensions;
using data.mongodb.interfaces;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Driver;
using MongoDB.Driver.Core;
using System;
using System.Collections.Generic;
using System.Resources;

namespace data.mongodb
{
    public class Db
    {
        private static readonly string? User = Environment.GetEnvironmentVariable("DB_USER");
        private static readonly string? Pwd = Environment.GetEnvironmentVariable("DB_PWD");
        private static readonly string? Name = Environment.GetEnvironmentVariable("DB_NAME");
        private static readonly string? Server = Environment.GetEnvironmentVariable("DB_SERVER");
        private static readonly string? Port = Environment.GetEnvironmentVariable("DB_PORT");
        private const string resourceFile = @".\Properties\Resources.resources";
        private static Db? Instance { get; set; }
        private string ConnectString { get; set; }
        private MongoClient Client { get; set; }
        private IMongoDatabase Dbase { get; set; }
        public static Db GetInstance()
        {
            if (Db.Instance == null)
            {
                Instance = new Db();
            }
            return Db.Instance;
        }
        private Db()
        {
            ConnectString = CreateConnectString();
            Client = new MongoClient(ConnectString);
            Dbase = Client.GetDatabase(Name);
        }
        private static string CreateConnectString()
        {
            using System.Resources.ResourceSet resources = new ResourceSet(resourceFile);
            if (User != null && Pwd != null && Name != null
                && Server != null && Port != null)
                return "mongodb://" + User
                    + ':' + Pwd + '@' + Server
                    + ':' + Port + '/' + Name;
            else
                throw new NullReferenceException(resources.GetString("_err_null_ref"));
        }
        public static IMongoCollection<IMongoDoc> GetCollection(string v)
        {
            return GetInstance().Dbase.GetCollection<IMongoDoc>(v);
        }
        public static IMongoCollection<IMongoDoc> GetCollection(IMongoDoc? d)
        {
            if (d != null)
                return GetCollection(d.GetColName());
            throw new ArgumentNullException(nameof(d));
        }
        public static void DropCollection(string v)
        {
            GetInstance().Dbase.DropCollection(v);
        }
        public static IMongoCollection<IMongoDoc> NewCollection(string v)
        {
            DropCollection(v);
            GetInstance().Dbase.CreateCollection(v);
            return GetCollection(v);
        }
        public static IMongoDoc SetDocId(IMongoDoc? d)
        {
            if (d != null)
            {
                if (d.Id == null)
                            d.Id = (string)StringObjectIdGenerator.Instance.GenerateId(GetCollection(d), d);
                return d;
            }
            throw new ArgumentNullException(nameof(d));
        }
        public static string? GetDocIdString(IMongoDoc? d)
        {
            return d != null
                ? d.Id ?? (string)StringObjectIdGenerator.Instance.GenerateId(
                        GetCollection(d), d)
                : throw new ArgumentNullException(nameof(d));
        }
        public static IFindFluent<IMongoDoc, IMongoDoc> FindAll(IMongoCollection<IMongoDoc> c)
        {
            return c.Find<IMongoDoc>(new BsonDocument());

        }
        public static IFindFluent<IMongoDoc, IMongoDoc> FindAll(string v)
        {
            return FindAll(GetCollection(v));
        }
        public static IFindFluent<IMongoDoc, IMongoDoc> FindAll(IMongoDoc d)
        {
            return FindAll(GetCollection(d));
        }
        public static FilterDefinition<IMongoDoc> BuildFilterById(string v)
        {
            FilterDefinitionBuilder<IMongoDoc> builder = Builders<IMongoDoc>.Filter;
            FilterDefinition<IMongoDoc> filter = builder.Eq(nameof(IMongoDoc.Id), v);
            return filter;
        }
        public static FilterDefinition<IMongoDoc> BuildFilterById(IMongoDoc d)
        {
            if (d != null && d.Id == null)
                return BuildFilterById(d.Save());
            else if (d != null && d.Id != null)
                return BuildFilterById(d.Id);
            else
                throw new ArgumentNullException(nameof(d));
        }
        public static FilterDefinition<IMongoDoc> BuildFilterByDoc(IMongoDoc d)
        {
            FilterDefinitionBuilder<IMongoDoc> builder = Builders<IMongoDoc>.Filter;
            List<FilterDefinition<IMongoDoc>> filterList = new List<FilterDefinition<IMongoDoc>>();
            foreach (BsonElement e in d.ToBsonDocument().Elements)
            {
                if (e.Name == "_id" || e.Name == "Id")
                    continue;
                filterList.Add(builder.Eq(e.Name, e.Value));
            }
            FilterDefinition<IMongoDoc> filter = builder.And(filterList);
            return filter;

        }
        public static FilterDefinition<IMongoDoc> BuildFilterByField(string v, object o)
        {
            FilterDefinitionBuilder<IMongoDoc> builder = Builders<IMongoDoc>.Filter;
            FilterDefinition<IMongoDoc> filter = builder.Eq(v, o);
            return filter;
        }
        public static IMongoDoc? FindById(IMongoCollection<IMongoDoc> c, string v)
        {
            return (IMongoDoc)c.Find<IMongoDoc>(BuildFilterById(v)).FirstOrDefault<IMongoDoc>();
        }
        public static IMongoDoc? FindById(IMongoDoc? d)
        {
            if (d != null)
            {
                if (d.Id != null)
                    return FindById(GetCollection(d), d.Id);
                if (d.Id == null)
                    return d.Save();
            }
            throw new ArgumentNullException(nameof(d));
        }
        public static IMongoDoc? GetDocByUniqueField(IMongoCollection<IMongoDoc> c, string v, object o)
        {
            return (IMongoDoc)c.Find<IMongoDoc>(BuildFilterByField(v, o)).FirstOrDefault<IMongoDoc>();
        }
        public static IMongoDoc? GetDocByUniqueField(IMongoDoc d, string v)
        {
            return GetDocByUniqueField(GetCollection(d), v, d.ToBsonDocument<IMongoDoc>().GetValue(v));
        }
        public static bool IsDocDuplicate(IMongoDoc d)
        {
            return GetCollection(d).Find<IMongoDoc>(BuildFilterByDoc(d)).CountDocuments() >= 1 ? true : false;
        }
        public static IMongoDoc? FindDocEqualTo(IMongoDoc? d)
        {
            return d != null ? IsDocDuplicate(d)
                ? GetCollection(d).Find<IMongoDoc>(
                    BuildFilterByDoc(d)).FirstOrDefault<IMongoDoc>()
                    : null : throw new ArgumentNullException(nameof(d));
        }
        public static IMongoDoc FindLast(IMongoCollection<IMongoDoc> c)
        {
            return c.FindSync<IMongoDoc>(
                q => q.Id is string
                , new FindOptions<IMongoDoc, IMongoDoc>()
                {
                    Limit = 1
                    ,
                    Sort = Builders<IMongoDoc>.Sort.Descending(
                        q => q.Id)
                }).First<IMongoDoc>();
        }
        public static IMongoDoc FindLast(IMongoDoc d)
        {
            return FindLast(GetCollection(d));
        }
        public static IMongoDoc Save(IMongoDoc d)
        {
            if (d != null)
            {
                if (d.Id == null)
                {
                    d = SetDocId(d);
                    GetCollection(d).InsertOne(d);
                }
                else
                    GetCollection(d).OfType<typeof d>.FindOneAndReplace<IMongoDoc>(q => q.Id == d.Id, d);                    
                return d.
            }
            throw new ArgumentNullException(nameof(d));
        }

    }
}