#nullable enable
using Data.Mongo.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Mongo.Extensions
{
    public static class IMongoDocExtensions
    {
        public static TDoc? AssignId<TDoc>(
            this TDoc? doc)
        where TDoc : class, IMongoDoc<TDoc> =>
            doc?.Svc.AssignId(doc) ?? null;
        public static void DropCollection<TDoc>(
            this TDoc? doc)
        where TDoc : class, IMongoDoc<TDoc>
        {
            doc?.Svc.Db.DropCollection(
                doc?.GetTypeNames().iface);
        }
        public static IEnumerable<TDoc>? GetAll<TDoc>(
            this TDoc? doc)
        where TDoc : class
            , IMongoDoc<TDoc>
        {
            return
                doc?.Svc.GetAll();
        }
        public static async Task<IEnumerable<TDoc>?> GetAllAsync<TDoc>(
            this TDoc? doc)
        where TDoc : class
            , IMongoDoc<TDoc>
        {
            return
                doc != null
                    ? await
                    doc.Svc
                    .GetAllAsync()
                    .ConfigureAwait(false)
                : null;
        }
        public static TDoc? GetDoc<TDoc>(
            this TDoc? doc)
        where TDoc : class
            , IMongoDoc<TDoc>
        {
            return
                doc?.Svc.GetOne(doc.Id);
        }
        public static TDoc? GetDoc<TDoc>(
            this TDoc? doc
            , string? id)
        where TDoc : class
            , IMongoDoc<TDoc>
        {
            return
                doc?.Svc.GetOne(id);
        }
        public static TDoc? GetDoc<TDoc>(
            this TDoc? doc
            , IDictionary<string, object?>? members)
        where TDoc : class
            , IMongoDoc<TDoc>
        {
            return
                doc != null
                    ? doc.Svc.GetOne(
                        members)
                : throw new
                ArgumentNullException(
                    nameof(
                        doc));
        }
        public static TDoc? GetDoc<TDoc>(
            this TDoc? doc
            , string? field
            , object? value)
        where TDoc : class
            , IMongoDoc<TDoc>
        {
            return
                doc?.Svc.GetOne(
                        field, value);
        }
        public static async Task<TDoc?> GetDocAsync<TDoc>(
            this TDoc? doc)
        where TDoc : class
        , IMongoDoc<TDoc>
        {
            return
                doc != null
                    ? await doc.Svc
                    .GetOneAsync(
                        doc.Id)
                    .ConfigureAwait(
                        false)
                : null;
        }
        public static async Task<TDoc?> GetDocAsync<TDoc>(
            this TDoc? doc, string? id)
        where TDoc : class
            , IMongoDoc<TDoc>
        {
            return
                doc != null
                    ? await doc.Svc
                    .GetOneAsync(
                        id ?? throw new
                        ArgumentNullException(
                            nameof(
                                id)))
                    .ConfigureAwait(
                        false)
                : null;
        }
        public static async Task<TDoc?> GetDocAsync<TDoc>(
            this TDoc? doc
            , IDictionary<string, object?>? members)
        where TDoc : class
            , IMongoDoc<TDoc>
        {
            return
                doc != null
                    ? await doc.Svc
                        .GetOneAsync(
                            members)
                        .ConfigureAwait(
                            false)
                : null;
        }
        public static async Task<TDoc?> GetDocAsync<TDoc>(
            this TDoc? doc
            , string? field
            , object? value)
        where TDoc : class, IMongoDoc<TDoc>
            => doc != null
                ? await doc.Svc
                    .GetOneAsync(
                        field, value)
                    .ConfigureAwait(false)
                : null;
        public static (string iface, string type) GetTypeNames<TDoc>(
            this TDoc? doc)
        where TDoc : class, IMongoDoc<TDoc> =>
            (typeof(TDoc).Name
                , doc?.GetType().Name ?? string.Empty);
        public static TDoc? Save<TDoc>(
            this TDoc? doc)
        where TDoc : class, IMongoDoc<TDoc> =>
            doc?.Svc.Save(doc) ?? null;
        public static async Task<TDoc?> SaveAsync<TDoc>(
            this TDoc? doc)
        where TDoc : class, IMongoDoc<TDoc> =>
            doc != null
                ? await doc.Svc
                .SaveAsync(
                    doc)
                .ConfigureAwait(false)
            : null;
        public static bool TryAssignId<TDoc>(
            this TDoc? doc
            , out TDoc? result)
        where TDoc : class
            , IMongoDoc<TDoc>
        {
            result =
                doc != null
                ? doc.Svc.TryAssignId(
                    doc, out TDoc? assigned)
                    ? assigned
                    : null
                : null;
            return
                !string.IsNullOrEmpty(
                    result?.Id)
                ? true
            : false;
        }
    }
}
