using MongoDB.Driver;
using NomuBackend.Services;
using Xceed.Words.NET;
using System.IO;
using System.Collections.Generic;

public class DocsService : IDocService
{
    private readonly IMongoCollection<Docs> _docsCollection;

    public DocsService(IMongoDatabase database)
    {
        _docsCollection = database.GetCollection<Docs>("Docs");
    }

    public void AddDoc(Docs doc) => _docsCollection.InsertOne(doc);

    public Docs GetDocById(string docId) =>
        _docsCollection.Find(d => d.DocId == docId).FirstOrDefault();

    public void UpdateDoc(string docId, Docs updatedDoc)
    {
        var filter = Builders<Docs>.Filter.Eq(d => d.DocId, docId);
        var update = Builders<Docs>.Update
            .Set(d => d.DocType, updatedDoc.DocType)
            .Set(d => d.DocContent, updatedDoc.DocContent)
            .Set(d => d.ProductCatalogue, updatedDoc.ProductCatalogue)
            .Set(d => d.StaffInfo, updatedDoc.StaffInfo)
            .Set(d => d.ContactInfo, updatedDoc.ContactInfo);

        _docsCollection.UpdateOne(filter, update);
    }

    public void RemoveDoc(string docId) =>
        _docsCollection.DeleteOne(d => d.DocId == docId);

    public List<Docs> listDocs(string docType = null, bool sortByDocId = true)
    {
        var filter = docType != null
            ? Builders<Docs>.Filter.Eq(d => d.DocType, docType)
            : Builders<Docs>.Filter.Empty;

        var sort = sortByDocId
            ? Builders<Docs>.Sort.Ascending(d => d.DocId)
            : Builders<Docs>.Sort.Ascending(d => d.DocType);

        return _docsCollection.Find(filter).Sort(sort).ToList();
    }

    
    public void integrateMSWord(string docId, string filePath)
    {
        var doc = GetDocById(docId);
        if (doc != null)
        {
            using (var document = DocX.Create(filePath))
            {
                document.InsertParagraph($"Document ID: {doc.DocId}");
                document.InsertParagraph($"Type: {doc.DocType}");
                document.InsertParagraph($"Content: {doc.DocContent}");
                document.InsertParagraph($"Product Catalogue: {doc.ProductCatalogue}");
                document.InsertParagraph($"Staff Info: {doc.StaffInfo}");
                document.InsertParagraph($"Contact Info: {doc.ContactInfo}");

                document.Save();
            }
        }
    }
}