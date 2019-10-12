#nullable enable
using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Mongo.Interfaces;
using MongoDB.Driver;

namespace Data.Mongo.Interfaces
{
    public interface IMongoService<TDoc>
        where TDoc : class, IMongoDoc<TDoc>
    {
        IMongoCollection<TDoc> Coll { get; }
        IMongoDatabase Db { get; }
        TDoc? AssignId(TDoc? doc);
        IEnumerable<TDoc> GetAll();
        Task<IEnumerable<TDoc>> GetAllAsync();
        string? GetId(
            TDoc? doc);
        IEnumerable<TDoc?>? GetMany(
            IEnumerable<string?>? ids);
        IEnumerable<TDoc?>? GetMany(
            IEnumerable<TDoc?>? docs);
        Task<IEnumerable<TDoc?>?> GetManyAsync(
            IEnumerable<string?>? ids);
        Task<IEnumerable<TDoc?>?> GetManyAsync(
            IEnumerable<TDoc?>? docs);
        TDoc? GetOne(
            IDictionary<string, object?>? members);
        TDoc? GetOne(
            string? field, object? value);
        TDoc? GetOne(
            string? id);
        TDoc? GetOne(
            TDoc? doc);
        Task<TDoc?> GetOneAsync(
            IDictionary<string, object?>? members);
        Task<TDoc?> GetOneAsync(
            string? field, object? value);
        Task<TDoc?> GetOneAsync(
            string? id);
        Task<TDoc?> GetOneAsync(
            TDoc? doc);
        IEnumerable<UpdateResult?>? RemoveMany(
            IEnumerable<string?>? ids);
        IEnumerable<UpdateResult?>? RemoveMany(
            IEnumerable<TDoc>? docs);
        Task<IEnumerable<UpdateResult?>?> RemoveManyAsync(
            IEnumerable<string?>? ids);
        Task<IEnumerable<UpdateResult?>?> RemoveManyAsync(
            IEnumerable<TDoc?>? docs);
        UpdateResult? RemoveOne(
            TDoc? doc);
        UpdateResult? RemoveOne(
            string? id);
        Task<UpdateResult?> RemoveOneAsync(
            TDoc? doc);
        Task<UpdateResult?> RemoveOneAsync(
            string? id);
        TDoc? Save(
            TDoc? doc);
        Task<TDoc?> SaveAsync(
            TDoc? doc);
        IEnumerable<TDoc?>? SaveMany(
            IEnumerable<TDoc?>? docs);
        Task<IEnumerable<TDoc?>?> SaveManyAsync(
            IEnumerable<TDoc?>? docs);
        bool TryAssignId(
            TDoc? doc
            , out TDoc? result);
    }
}