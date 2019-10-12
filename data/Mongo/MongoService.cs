#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Mongo.Interfaces;
using Data.Mongo.Extensions;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Driver;

namespace Data.Mongo
{
    public class MongoService<TDoc> : IMongoService<TDoc>
        where TDoc : class, IMongoDoc<TDoc>
    {
        // private db instances
        private readonly IMongoDatabase _db;
        private readonly IMongoCollection<TDoc> _coll;
        // Get the database connection instance in the constructor
        public MongoService()
        {
            IMongoData<TDoc> _conn =
                new MongoData<TDoc>(
                    typeof(TDoc).Name);
            _db = _conn.Db;
            _coll = _conn.Collection;
        }
        public IMongoDatabase Db => _db;
        public IMongoCollection<TDoc> Coll => _coll;
        public TDoc? AssignId(TDoc? doc)
        {
            if (doc != null)
                doc.Id =
                    GetId(doc) ?? doc.Id;
            return doc;
        }
        public DeleteResult? DeleteOne(
            TDoc? doc) =>
                doc != null
                    ? !string.IsNullOrEmpty(
                        doc.Id)
                        ? _coll.DeleteOne(
                            new BsonDocument(
                                "_id", new ObjectId(
                                    doc.Id)))
                            : null
                        : null;
        public IEnumerable<DeleteResult?>? DeleteMany(
            IEnumerable<TDoc>? docs) =>
            (from TDoc doc
             in docs
             select doc)
            .Select(
                doc => DeleteOne(doc))
            .ToList();
        public IEnumerable<DeleteResult?>? DeleteMany(
            string? field, object? value) =>
            (from
                TDoc? doc
             in
                 GetMany(field, value)
             select
                doc)
            .Select(
                doc =>
                    DeleteOne(doc))
            .AsEnumerable();
        public IEnumerable<TDoc> GetAll() =>
            _coll.Find(
                    f => true).ToList();
        public async Task<IEnumerable<TDoc>> GetAllAsync() =>
            await _coll.Find(
                f => true)
            .ToListAsync()
            .ConfigureAwait(
                false);
        public string? GetId(
            TDoc? doc) =>
            doc != null
                    ? string.IsNullOrEmpty(
                        doc.Id)
                        ? (string)
                            StringObjectIdGenerator
                            .Instance
                            .GenerateId(
                                _coll, doc)
                        : doc.Id
                    : null;
        public TDoc? GetLast() =>
            GetAll()
                .LastOrDefault();
        public IEnumerable<TDoc?>? GetMany(
            IEnumerable<TDoc?>? docs) =>
                    docs?.Any() ?? false
                        ? (from
                                TDoc? doc
                            in
                               docs
                           where
                               doc != null
                           select
                               doc)
                            .Select(
                                doc => GetOne(
                                    doc))
                            .AsEnumerable()
                    : null;
        public IEnumerable<TDoc?>? GetMany(
            IEnumerable<string?>? ids) =>
                ids?.Any() ?? false
                        ? (from
                                string? id
                            in
                               ids
                           where
                               !string.IsNullOrEmpty(
                                   id)
                           select
                               id)
                            .Select(
                                id => GetOne(
                                    id))
                            .AsEnumerable()
                        : null;
        public IEnumerable<TDoc?>? GetMany(
            IDictionary<string, object?>? members) =>
                    members?.Any() ?? false
                        ? _coll.Find(
                            new BsonDocument(
                                members))
                            .ToList()
                        : null;
        public IEnumerable<TDoc?>? GetMany(
            string? field, object? value) =>
            !string.IsNullOrEmpty(field)
                    ? GetMany(
                        new Dictionary
                        <string, object?>()
                    {
                        { field, value }
                    })
                : null;
        public async Task<IEnumerable<TDoc?>?> GetManyAsync(
            IEnumerable<TDoc?>? docs) =>
                docs != null
                    ? docs.Any()
                        ? await Task
                        .FromResult(
                            (from TDoc doc
                            in docs
                             where doc != null
                             select doc)
                           .Select(
                            async doc =>
                                await GetOneAsync(
                                    doc).ConfigureAwait(false))
                           .Cast<TDoc>()).ConfigureAwait(false)
                                : null
                           : null;
        public async Task<IEnumerable<TDoc?>?> GetManyAsync(
            IEnumerable<string?>? ids)
        {
            return
            ids != null
                ? ids.Any()
                    ? await Task
                    .FromResult((
                        from
                            string? id
                        in
                            ids
                        where
                            !string.IsNullOrEmpty(
                                id)
                        select
                            id)
                    .Select(
                        async id =>
                            await GetOneAsync(
                                id)
                    .ConfigureAwait(false))
                    .Cast<TDoc>())
                    .ConfigureAwait(false)
                : null
            : null;
        }
        public TDoc? GetOne(
            TDoc? doc)
        {
            return
            doc != null
                ? GetOne(
                    doc.Id)
            : null;
        }
        public TDoc? GetOne(
            string? field, object? value)
        {
            return
                !string.IsNullOrEmpty(field)
                    ? GetOne(
                        new Dictionary
                        <string, object?>()
                    {
                        { field, value }
                    })
                : null;
        }
        public TDoc? GetOne(
            string? id)
        {
            return
                !string.IsNullOrEmpty(
                    id)
                    ? _coll.Find(
                        new BsonDocument("_id"
                        , new ObjectId(id)))
                        .FirstOrDefault<TDoc>()
                : null;
        }
        public TDoc? GetOne(
            IDictionary<string, object?>? members) =>
                GetMany(
                    members)
                            ?.FirstOrDefault()
                        ?? null;
        public async Task<TDoc?> GetOneAsync(
            TDoc? doc)
        {
            return
                doc != null
                        ? await GetOneAsync(
                            doc.Id)
                        .ConfigureAwait(false)
            : null;
        }
        public async Task<TDoc?> GetOneAsync(
            string? field, object? value)
        {
            return
                !string.IsNullOrEmpty(field)
                    ? await GetOneAsync(
                        new Dictionary<string, object?>()
                        {
                                { field, value }
                        }).ConfigureAwait(false)
                : null;
        }
        public async Task<TDoc?> GetOneAsync(
            string? id)
        {
            return
                !string.IsNullOrEmpty(id)
                    ? await _coll.Find(
                        new BsonDocument("_id"
                            , new ObjectId(id)))
                            .FirstOrDefaultAsync<TDoc>()
                            .ConfigureAwait(false)
                    : null;
        }
        public async Task<TDoc?> GetOneAsync(
            IDictionary<string, object?>? members)
        {
            return
                members != null
                    ? members.Any()
                        ? await _coll.Find(
                            new BsonDocument(
                                members))
                            .FirstOrDefaultAsync()
                            .ConfigureAwait(
                                false)
                        : null
                    : null;
        }
        public IEnumerable<UpdateResult?>? RemoveMany(
            IEnumerable<TDoc?>? docs)
        {
            return
                docs != null
                    ? docs.Any()
                        ? (from
                                TDoc? doc
                            in
                               docs
                           where
                                doc != null
                           select
                                doc)
                            .Select(
                                doc =>
                                    RemoveOne(
                                        doc))
                            .Cast<UpdateResult?>()
                : null
            : null;
        }
        public IEnumerable<UpdateResult?>? RemoveMany(
            IEnumerable<string?>? ids)
        {
            return
               ids != null
                   ? ids.Any()
                       ? (
                       from
                           string? id
                       in
                           ids
                       where
                           !string.IsNullOrEmpty(
                               id)
                       select
                           id)
                       .Select(
                           id =>
                               RemoveOne(
                                   id))
                        .Cast<UpdateResult?>()
                   : null
               : null;
        }
        public async Task<IEnumerable<UpdateResult?>?> RemoveManyAsync(

            IEnumerable<TDoc?>? docs)
        {
            return
                docs != null
                    ? docs.Any()
                        ? await Task.FromResult((
                            from
                                TDoc? doc
                            in
                               docs
                            where
                                 doc != null
                            select
                                 doc)
                            .Select(
                                async doc =>
                                    await
                                    RemoveOneAsync(
                                        doc)
                                    .ConfigureAwait(false))
                            .Cast<UpdateResult>())
                        .ConfigureAwait(false)
                : null
            : null;
        }
        public async Task<IEnumerable<UpdateResult?>?> RemoveManyAsync(

            IEnumerable<string?>? ids)
        {
            return
                ids != null
                    ? ids.Any()
                        ? await Task
                        .FromResult((
                        from
                            string? id
                        in
                            ids
                        where
                            !string.IsNullOrEmpty(id)
                        select
                            id)
                        .Select(
                            async id =>
                                await
                                RemoveOneAsync(
                                    id)
                                .ConfigureAwait(false))
                        .Cast<UpdateResult?>())
                        .ConfigureAwait(false)
                    : null
                : null;
        }
        public UpdateResult? RemoveOne(
            TDoc? doc) =>
                doc != null
                    ? !string.IsNullOrEmpty(
                        doc.Id)
                        ? _coll.UpdateOne(
                            new BsonDocument(
                                "_id", new ObjectId(
                                    doc.Id))
                            , new BsonDocument(
                                "$set", new BsonDocument
                                {
                                    {
                                        nameof(
                                            IMongoDoc<TDoc>
                                            .IsActive)
                                        , false
                                    },
                                    {
                                        nameof(
                                            IMongoDoc<TDoc>
                                            .Modified)
                                        , DateTime.UtcNow
                                    }
                                }))
                        : null
                    : null;
        public UpdateResult? RemoveOne(
            string? id)
        {
            return
                !string.IsNullOrEmpty(
                    id)
                    ? RemoveOne(
                        GetOne(id))
                : null;
        }
        public async Task<UpdateResult?> RemoveOneAsync(
            TDoc? doc)
        {
            return
                doc != null
                    ? !string.IsNullOrEmpty(
                        doc.Id)
                        ? (await _coll.UpdateOneAsync(
                            new BsonDocument(
                                "_id", new ObjectId(
                                    doc.Id)),
                            new BsonDocument(
                                "$set", new BsonDocument
                            {
                                {
                                    nameof(
                                        IMongoDoc<TDoc>
                                        .IsActive)
                                    , false
                                },
                                {
                                    nameof(
                                        IMongoDoc<TDoc>
                                        .Modified)
                                    , DateTime.UtcNow
                                }
                            })).ConfigureAwait(false))
                        : null
                    : null;
        }
        public async Task<UpdateResult?> RemoveOneAsync(
            string? id)
        {
            return
                !string.IsNullOrEmpty(
                    id)
                    ? await RemoveOneAsync(
                        GetOne(
                            id))
                    .ConfigureAwait(false)
                : null;
        }
        public TDoc? Save(
            TDoc? doc)
        {
            TDoc? validated =
                doc != null
                    ? doc.TryValidateDoc(
                        out dynamic? result)
                        ? (TDoc?)result
                    : (TDoc?)result ?? null
                : null;
            if (validated != null)
            {
                if (string.IsNullOrEmpty(
                    validated.Id))
                {
                    validated = validated.AssignId();
                    if (validated != null)
                    {
                        _coll.InsertOne(
                            validated);
                    }
                }
                else
                    _coll.ReplaceOne(
                        new BsonDocument(
                            "_id", new ObjectId(
                                validated.Id))
                            , validated);
            }
            return
                    validated;
        }
        public async Task<TDoc?> SaveAsync(
            TDoc? doc)
        {
            TDoc? validated =
                doc != null
                    ? doc.TryValidateDoc(
                        out dynamic? result)
                        ? (TDoc?)result
                    : (TDoc?)result ?? null
                : null;
            if (validated != null)
            {
                if (string.IsNullOrEmpty(
                    validated.Id))
                {
                    validated = validated.AssignId();
                    if (validated != null)
                    {
                        await _coll.InsertOneAsync(
                            validated)
                            .ConfigureAwait(false);
                    }
                }
                else
                    await _coll.ReplaceOneAsync(
                        new BsonDocument(
                            "_id", new ObjectId(
                                validated.Id))
                            , validated)
                        .ConfigureAwait(false);
            }
            return
                    validated;
        }
        public IEnumerable<TDoc?>? SaveMany(
            IEnumerable<TDoc?>? docs)
        {
            return
                docs != null
                    ? docs.Any()
                        ? (from
                                TDoc? doc
                            in
                               docs
                           where
                                doc != null
                           select
                                doc)
                            .Select(
                                doc =>
                                    Save(doc))
                            .Cast<TDoc?>()
                : null
            : null;
        }
        public async Task<IEnumerable<TDoc?>?> SaveManyAsync(
            IEnumerable<TDoc?>? docs)
        {
            return
                docs != null
                    ? docs.Any()
                        ? await Task.FromResult((
                            from
                                TDoc? doc
                            in
                               docs
                            where
                                 doc != null
                            select
                                 doc)
                            .Select(
                                async doc =>
                                    await
                                    SaveAsync(
                                        doc)
                                    .ConfigureAwait(false))
                            .Cast<TDoc?>())
                        .ConfigureAwait(false)
                : null
            : null;
        }
        public bool TryAssignId(
            TDoc? doc
            , out TDoc? result)
        {
            result =
                AssignId(doc);
            return
                result != null
                    ? true
            : false;
        }
    }
}
