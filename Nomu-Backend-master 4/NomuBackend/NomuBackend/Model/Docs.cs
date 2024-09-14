public class Docs
{
    public string DocId { get; set; }
    public string DocType { get; set; }
    public string DocContent { get; set; }
    public string ProductCatalogue { get; set; }
    public string StaffInfo { get; set; }
    public string ContactInfo { get; set; }
}
// Below are CRUD operations that can be done using above fields

// public void InsertDoc(Docs doc)
// {
//     var collection = _database.GetCollection<Docs>("Docs");
//     collection.InsertOne(doc);
// }
// public Docs GetDocById(string docId)
// {
//     var collection = _database.GetCollection<Docs>("Docs");
//     return collection.Find(d => d.DocId == docId).FirstOrDefault();
// }
// public void UpdateDoc(string docId, Docs updatedDoc)
// {
//     var collection = _database.GetCollection<Docs>("Docs");
//     var filter = Builders<Docs>.Filter.Eq(d => d.DocId, docId);
//     var update = Builders<Docs>.Update
//                     .Set(d => d.DocType, updatedDoc.DocType)
//                     .Set(d => d.DocContent, updatedDoc.DocContent)
//                     .Set(d => d.ProductCatalogue, updatedDoc.ProductCatalogue)
//                     .Set(d => d.StaffInfo, updatedDoc.StaffInfo)
//                     .Set(d => d.ContactInfo, updatedDoc.ContactInfo);

//     collection.UpdateOne(filter, update);
// }
// public void DeleteDoc(string docId)
// {
//     var collection = _database.GetCollection<Docs>("Docs");
//     collection.DeleteOne(d => d.DocId == docId);
// }
