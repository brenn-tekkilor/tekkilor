#nullable enable
using System;
using MongoDB.Driver;
using Data.Mongo.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Data.Mongo
{
    public class MongoData<TDoc> : IMongoData<TDoc>
    where TDoc : class, IMongoDoc<TDoc>
    {
        private static readonly (IMongoClient client, IMongoDatabase db) _conn =
            Connect();
        private readonly IMongoCollection<TDoc> _coll;

        public IMongoDatabase Db => _conn.db;
        public IMongoCollection<TDoc> Collection => _coll;
        public MongoData(string c)
        {
            _coll =
                _conn.db
                .GetCollection<TDoc>(c);
        }
        private static (IMongoClient, IMongoDatabase) Connect()
        {
            IDictionary<string, string> _conn =
                ConnVars(
                    ("MONGO_PROTO", "mongodb://"),
                    ("MONGO_DB", "kroger_test"),
                    ("MONGO_USER", "apptests"),
                    ("MONGO_PWD", "d0tn3t"),
                    ("MONGO_HOST", "localhost"),
                    ("MONGO_PORT", "27017"));
            IMongoClient client =
                new MongoClient(
                    _conn["MONGO_PROTO"] +
                    _conn["MONGO_USER"] + ":" +
                    _conn["MONGO_PWD"] + "@" +
                    _conn["MONGO_HOST"] + ":" +
                    _conn["MONGO_PORT"] + "/" +
                    _conn["MONGO_DB"]);
            IMongoDatabase db =
                client
                .GetDatabase(
                    _conn["MONGO_DB"]);
            return
                (client, db);
        }
        private static IDictionary<string, string> ConnVars(
            params (string key, string value)[] props)
        {
            return
            (
                from
                    (string key, string value) p
                in
                    props
                select
                    p
            )
            .ToDictionary(
                p =>
                    p.key
                , p =>
                    Environment
                    .GetEnvironmentVariable(
                        p.value)
                    ?? p.value);
        }
    }
}