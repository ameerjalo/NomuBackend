using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;

namespace NomuBackend.Services
{
    public interface IDocService
    {
        void AddDoc(Docs doc);
        Docs GetDocById(string docId);
        void UpdateDoc(string docId, Docs updatedDoc);
        void RemoveDoc(string docId);
        List<Docs> listDocs(string docType = null, bool sortByDocId = true); 
        void integrateMSWord(string docId, string filePath); 
    }
}