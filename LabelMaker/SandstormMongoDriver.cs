using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
namespace LabelMaker
{
    public class SandstormMongoDriver
    {
        // Define constant strings to represent the keys of each attribute of a part
        public readonly static String PART_NUMBER_KEY = "Part Number";
        public readonly static String WAVELENGTH_KEY = "Wavelength";
        public readonly static String FORM_FACTOR_KEY = "Form Factor";
        public readonly static String SMM_KEY = "Media";
        public readonly static String TEMP_RANGE_KEY = "Full Temp";
        public readonly static String DISTANCE_KEY = "Distance";
        public readonly static String DISTANCE_UNITS_KEY = "Distance Units";
        public readonly static String DATA_RATE_KEY = "Generic Rate";
        public readonly static String OEM_KEY = "OEM";
        public readonly static String INTERNAL_PART_NUMBER_KEY = "Internal PN";
        public readonly static String PROTOCOL_KEY = "Protocol";
        public readonly static String OBJECT_ID_KEY = "_id";
        public readonly static String PODC_KEY = "PODC Number";
        // Define the connection string for debug use
        public readonly static String CONNECTION_STRING = "mongodb+srv://internus:Sandstone1@sandstonecluster.wbfmx.mongodb.net/Sandstorm?retryWrites=true&w=majority";

        // Define a constant for the number of values in the recents table
        public readonly static int RECENTS_LIMIT = 9;

        // Get the connection string which we will use to connect to Mongo
        public string ConnectionString;

        // The underlying driver that makes all of the connections and requests
        public MongoClient driverClient;

        // Public constructor that establishes connection to the database
        public SandstormMongoDriver(string _ConnectionString)
        {
            // Establish the connection
            ConnectionString = _ConnectionString;
            driverClient = new MongoClient(ConnectionString);
        }

        public BsonDocument GetPartBson(String PartNumberString)
        {
            List<BsonDocument> partDocumentList = GetPartsBson(PartNumberString);

            // If nothing is in the list return a null object 
            if (partDocumentList == null)
            {
                return null;
            }
            if (partDocumentList.Count >= 1)
            {
                return partDocumentList[0];
            }
            else
            {
                return null;
            }
        }

        public async Task<String> AddPartJSON(String jsonString)
        {
            IMongoDatabase mDb = driverClient.GetDatabase("Sandstorm");
            var documentToUpload = BsonSerializer.Deserialize<BsonDocument>(jsonString);
            var collection = mDb.GetCollection<BsonDocument>("Parts");

            // Try saving a part and send a messsage based on success or failure
            try
            {
                await collection.InsertOneAsync(documentToUpload);
            }
            catch (Exception exception)
            {
                return "{\"Status\":\"Fail\",\"Message\":\"" + exception.ToString() + "\"}";
            }
            return "{\"Status\":\"Success\",\"Message\":\"" + "Added Part To Database" + "\"}";

        }



        public async Task<String> AddPODCJSON(String jsonString)
        {
            IMongoDatabase mDb = driverClient.GetDatabase("Sandstorm");
            var documentToUpload = BsonSerializer.Deserialize<BsonDocument>(jsonString);
            documentToUpload["_id"] = new ObjectId();
            var collection = mDb.GetCollection<BsonDocument>("PODC");
            var poNUM = documentToUpload["PODC Number"].AsString;
            var isExist = GetPODCBson(poNUM);
            if (isExist.Count >= 1)
            {
                try
                {
                    BsonDocument derivedBsonDocument = isExist.ElementAt(0);
                    BsonArray derivedBSONArray = (BsonArray)derivedBsonDocument["Part Info"];
                    foreach (var part in (BsonArray)documentToUpload["Part Info"])
                    {
                        derivedBSONArray.Add(part);
                    }
                    derivedBsonDocument["Part Info"] = derivedBSONArray;
                    await RemovePODCJSON(poNUM);
                    await collection.InsertOneAsync(derivedBsonDocument);
                    return "{\"Status\":\"Success\",\"Message\":\"" + "Added Parts To Existing PODC" + "\"}";

                }
                catch (Exception exception)
                {
                    return "{\"Status\":\"Fail\",\"Message\":\"" + exception.ToString() + ":Couldn't Append Parts To Current PODC Object" + "\"}";
                }
            }
            else
            {
                try
                {
                    await collection.InsertOneAsync(documentToUpload);
                }
                catch (Exception exception)
                {
                    return "{\"Status\":\"Fail\",\"Message\":\"" + exception.ToString() + "\"}";
                }
                return "{\"Status\":\"Success\",\"Message\":\"" + "Added PODC to Database" + "\"}";
            }
        }

        public async Task<String> RemovePartPODCJSON(String partNo, String podcNo, String beginSerial)
        {
            IMongoDatabase mDb = driverClient.GetDatabase("Sandstorm");

            var collection = mDb.GetCollection<BsonDocument>("PODC");

            var podc = GetIndvPODCBson(podcNo);
            BsonArray derivedBSONArray = (BsonArray)podc["Part Info"];
            foreach (var part in derivedBSONArray)
            {
                if (part["Part Number"] == partNo && part["Start Serial"] == beginSerial)
                {
                    derivedBSONArray.Remove(part);
                    break;
                }
            }
            podc["Part Info"] = derivedBSONArray;
            var removeFilter = Builders<BsonDocument>.Filter.Eq(doc => doc[PODC_KEY], podcNo);
            try
            {
                await collection.DeleteOneAsync(removeFilter);
                await collection.InsertOneAsync(podc);

            }
            catch (Exception exception)
            {
                return "{\"Status\":\"Fail\",\"Message\":\"" + exception.ToString() + "\"}";
            }
            return "{\"Status\":\"Success\",\"Message\":\"" + "Removed Part From PODC Database" + "\"}";

        }

        public async Task<String> RemovePODCJSON(String removePODC)
        {
            IMongoDatabase mDb = driverClient.GetDatabase("Sandstorm");
            // var documentToRemove = BsonSerializer.Deserialize<BsonDocument>(jsonString);
            var collection = mDb.GetCollection<BsonDocument>("PODC");
            // Try saving a part and send a messsage based on success or failure
            var removeFilter = Builders<BsonDocument>.Filter.Eq(doc => doc[PODC_KEY], removePODC);
            try
            {
                await collection.DeleteOneAsync(removeFilter);
            }
            catch (Exception exception)
            {
                return "{\"Status\":\"Fail\",\"Message\":\"" + exception.ToString() + "\"}";
            }
            return "{\"Status\":\"Success\",\"Message\":\"" + "Removed PODC From PODC Database" + "\"}";

        }

        public async Task<String> RemovePartJSON(String removedPart)
        {
            IMongoDatabase mDb = driverClient.GetDatabase("Sandstorm");
            // var documentToRemove = BsonSerializer.Deserialize<BsonDocument>(jsonString);
            var collection = mDb.GetCollection<BsonDocument>("Parts");
            // Try saving a part and send a messsage based on success or failure
            var removeFilter = Builders<BsonDocument>.Filter.Eq(doc => doc[PART_NUMBER_KEY], removedPart);
            try
            {
                await collection.DeleteOneAsync(removeFilter);
            }
            catch (Exception exception)
            {
                return "{\"Status\":\"Fail\",\"Message\":\"" + exception.ToString() + "\"}";
            }
            return "{\"Status\":\"Success\",\"Message\":\"" + "Removed Part From Parts Database" + "\"}";

        }


        public async Task<String> AddRecentJSON(String jsonString)
        {
            IMongoDatabase mDb = driverClient.GetDatabase("Sandstorm");
            var documentToUpload = BsonSerializer.Deserialize<BsonDocument>(jsonString);
            var collection = mDb.GetCollection<BsonDocument>("Recents");

            // Try saving a part and send a messsage based on success or failure
            try
            {
                await collection.InsertOneAsync(documentToUpload);
            }
            catch (Exception exception)
            {
                return "{\"Status\":\"Fail\",\"Message\":\"" + exception.ToString() + "\"}";
            }
            return "{\"Status\":\"Success\",\"Message\":\"" + "Added Part To Recents Table" + "\"}";

        }

        public async Task<String> RemoveRecentJSON(String removeObjectId)
        {
            IMongoDatabase mDb = driverClient.GetDatabase("Sandstorm");
            // var documentToRemove = BsonSerializer.Deserialize<BsonDocument>(jsonString);
            var collection = mDb.GetCollection<BsonDocument>("Recents");
            var removeFilter = Builders<BsonDocument>.Filter.Eq(doc => doc[OBJECT_ID_KEY], removeObjectId);
            // Try saving a part and send a messsage based on success or failure
            try
            {
                await collection.DeleteOneAsync(removeFilter);
            }
            catch (Exception exception)
            {
                return "{\"Status\":\"Fail\",\"Message\":\"" + exception.ToString() + "\"}";
            }
            return "{\"Status\":\"Success\",\"Message\":\"" + "Removed Part From Recents Table" + "\"}";


        }



        public List<BsonDocument> GetPartsBson(String PartNumberString)
        {
            try
            {
                IMongoDatabase mDb = driverClient.GetDatabase("Sandstorm");
                var collection = mDb.GetCollection<BsonDocument>("Parts");
                var item_result = collection.Find(x => x[PART_NUMBER_KEY] == PartNumberString).ToList();
                return item_result;
            }
            catch (Exception exception)
            {
                return null;
            }

        }

        // Specific to this project
        public List<Accedian.AccedianView.AccedianPart> GetAccedianParts()
        {
            try
            {
                IMongoDatabase mDb = driverClient.GetDatabase("Sandstorm");
                // Build Filter to optimize database retrieval
                var filter = Builders<BsonDocument>.Filter.Eq("OEM", "Accedian");
                var secFilter = Builders<BsonDocument>.Filter.Where(x => x["Secret Description"] != BsonNull.Value);
                var projection = Builders<BsonDocument>.Projection.Exclude("Internal PN")
                    .Exclude("Media")
                    .Exclude("Form Factor")
                    .Exclude("Distance")
                    .Exclude("Distance Units")
                    .Exclude("Wavelength")
                    .Exclude("Protocol")
                    .Exclude("DDM")
                    .Exclude("OEM")
                    .Exclude("Generic Rate")
                    .Exclude("WDM")
                    .Exclude("Simplex / Duplex")
                    .Exclude("Full Wavelength")
                    .Exclude("DWDM Channel")
                    .Exclude("Full Temp")
                    .Exclude("WDM Channel Code")
                    .Exclude("Description No Latch")
                    .Exclude("Full Wavelength");

                var collection = mDb.GetCollection<BsonDocument>("Parts");
                // var item_result = collection.Find(secFilter).Project(projection).ToList();
                var item_result = collection.Find(secFilter).SortBy(bson => bson["Part Number"]).Project(projection).ToCursor();

                List<Accedian.AccedianView.AccedianPart> accedianParts = new List<Accedian.AccedianView.AccedianPart>();

                foreach(BsonDocument doc in item_result.ToEnumerable())
                {
                    var pn = doc["Part Number"];
                    var sd = doc["Secret Description"];
                    var sld = doc["Secret Label Description"];

                    accedianParts.Add(
                        new Accedian.AccedianView.AccedianPart(
                            (string)pn,
                            (string)sd,
                            (string)sld
                            )
                    );
                }

                return accedianParts;
            }
            catch (Exception exception)
            {
                return null;
            }

        }

        // Overload to handle text search
        public List<Accedian.AccedianView.AccedianPart> GetAccedianParts(String searchText)
        {
            try
            {
                IMongoDatabase mDb = driverClient.GetDatabase("Sandstorm");
                // Build Filter to optimize database retrieval
                var filter = Builders<BsonDocument>.Filter.Eq("OEM", "Accedian");
                var secFilter = Builders<BsonDocument>.Filter.Where(x => x["Secret Description"] != BsonNull.Value && x["Part Number"] == searchText);
                var regexFilter = Builders<BsonDocument>.Filter.Regex("Part Number", new BsonRegularExpression(searchText));
                var projection = Builders<BsonDocument>.Projection.Exclude("Internal PN")
                    .Exclude("Media")
                    .Exclude("Form Factor")
                    .Exclude("Distance")
                    .Exclude("Distance Units")
                    .Exclude("Wavelength")
                    .Exclude("Protocol")
                    .Exclude("DDM")
                    .Exclude("OEM")
                    .Exclude("Generic Rate")
                    .Exclude("WDM")
                    .Exclude("Simplex / Duplex")
                    .Exclude("Full Wavelength")
                    .Exclude("DWDM Channel")
                    .Exclude("Full Temp")
                    .Exclude("WDM Channel Code")
                    .Exclude("Description No Latch")
                    .Exclude("Full Wavelength");

                var combinedFilter = secFilter & regexFilter & filter;

                var collection = mDb.GetCollection<BsonDocument>("Parts");
                // var item_result = collection.Find(secFilter).Project(projection).ToList();
                var item_result = collection.Find(combinedFilter).SortBy(bson => bson["Part Number"]).Project(projection).ToCursor();


                
                List<Accedian.AccedianView.AccedianPart> accedianParts = new List<Accedian.AccedianView.AccedianPart>();

                foreach (BsonDocument doc in item_result.ToEnumerable())
                {
                    var pn = doc["Part Number"];
                    var sd = doc["Secret Description"];
                    var sld = doc["Secret Label Description"];

                    accedianParts.Add(
                        new Accedian.AccedianView.AccedianPart(
                            (string)pn,
                            (string)sd,
                            (string)sld
                            )
                    );
                }

                return accedianParts;
            }
            catch (Exception exception)
            {
                return null;
            }

        }



        public List<BsonDocument> GetRecentsBson(int limit)
        {
            IMongoDatabase mDb = driverClient.GetDatabase("Sandstorm");
            var collection = mDb.GetCollection<BsonDocument>("Recents");
            var item_result = collection.Find(bson => true).SortBy(bson => bson[OBJECT_ID_KEY]).ThenByDescending(bson => bson[OBJECT_ID_KEY]).Limit(limit).ToList();
            Console.WriteLine(item_result);
            return item_result;
        }

        public List<BsonDocument> GetPODCBson(String podcNumberString)
        {
            IMongoDatabase mDb = driverClient.GetDatabase("Sandstorm");
            var collection = mDb.GetCollection<BsonDocument>("PODC");
            var item_result = collection.Find(x => x["PODC Number"] == podcNumberString).ToList();
            return item_result;
        }

        public BsonDocument GetIndvPODCBson(String podcNumberString)
        {
            List<BsonDocument> podcDocumentList = GetPODCBson(podcNumberString);

            // If nothing is in the list return a null object 
            if (podcDocumentList.Count >= 1)
            {
                return podcDocumentList[0];
            }
            else
            {
                return null;
            }
        }

        public BsonDocument GetPasswordBson(String form, String manufacturer)
        {
            List<BsonDocument> passwordDocumentList = GetPasswordsBson(manufacturer);
            Regex rgxType = new Regex(@"([A-Za-z]+)(\d+|\+)");
            String type = "";
            String formOptimize = rgxType.Replace(form, type);
            String formFactor = Regex.Replace(formOptimize, @"\s", "");

            if (formFactor.Contains("QSFP"))
            {
                for (int i = 0; i < passwordDocumentList.Count; i++)
                {
                    BsonDocument passwordObj = passwordDocumentList[i];
                    String formCheck = passwordObj["Optic_Type"].ToString();
                    if (formCheck.Contains(formFactor))
                    {
                        return passwordObj;
                    }

                }
                return null;
            }
            else
            {
                for (int i = 0; i < passwordDocumentList.Count; i++)
                {
                    BsonDocument passwordObj = passwordDocumentList[i];
                    String formCheck = passwordObj["Optic_Type"].ToString();
                    if (formCheck.Contains(formFactor) && !formCheck.Contains("Q" + formFactor))
                    {
                        return passwordObj;
                    }

                }
                return null;
            }
        }


        public List<BsonDocument> GetPasswordsBson(String manufacturer)
        {
            IMongoDatabase mDb = driverClient.GetDatabase("Sandstorm");
            var collection = mDb.GetCollection<BsonDocument>("Passwords");
            var item_result = collection.Find(x => x["Manufacturer"] == manufacturer).ToList();
            return item_result;
        }

        public async Task<String> AddPasswordJSON(String passwordSetting)
        {
            IMongoDatabase mDb = driverClient.GetDatabase("Sandstorm");
            var documentToUpload = BsonSerializer.Deserialize<BsonDocument>(passwordSetting);
            var collection = mDb.GetCollection<BsonDocument>("Passwords");

            // Try saving a part and send a messsage based on success or failure
            try
            {
                await collection.InsertOneAsync(documentToUpload);
            }
            catch (Exception exception)
            {
                return "{\"Status\":\"Fail\",\"Message\":\"" + exception.ToString() + "\"}";
            }
            return "{\"Status\":\"Success\",\"Message\":\"" + "Added Password To Database" + "\"}";
        }


        public List<BsonDocument> GetAutoCompleteParts(String partNO)
        {
            IMongoDatabase mDb = driverClient.GetDatabase("Sandstorm");
            var collection = mDb.GetCollection<BsonDocument>("Parts");
            var pipeline = new BsonDocument[] {
              new BsonDocument("$search", new BsonDocument {
                {
                  "autocomplete",
                  new BsonDocument {
                    {
                      "query",
                      partNO
                    },
                    {
                      "path",
                      "Part Number"
                    },
                    {
                        "fuzzy",
                        new BsonDocument
                        {
                            {
                                "prefixLength",
                                1
                            },
                            {
                                "maxEdits",
                                1
                            }
                        }
                    },
                    {
                      "tokenOrder",
                      "sequential"
                    }
                  }
                }
              }),
              new BsonDocument("$sort", new BsonDocument
              {
                  {
                      "score",
                      new BsonDocument("$meta", "textScore")
                  },
                  {
                      "posts",
                      -1
                  }
              }),
              new BsonDocument("$limit", 5)
            };

            var result = collection.Aggregate<BsonDocument>(pipeline).ToList();
            return result;
        }

        public List<BsonDocument> GetAutoCompleteBin(String partNO)
        {
            IMongoDatabase mDb = driverClient.GetDatabase("Sandstorm");
            var collection = mDb.GetCollection<BsonDocument>("BinFiles");
            var pipeline = new BsonDocument[] {
              new BsonDocument("$search", new BsonDocument {
                {
                  "autocomplete",
                  new BsonDocument {
                    {
                      "query",
                      partNO
                    },
                    {
                      "path",
                      "Part Number"
                    },
                    {
                        "fuzzy",
                        new BsonDocument
                        {
                            {
                                "prefixLength",
                                1
                            },
                            {
                                "maxEdits",
                                1
                            }
                        }
                    },
                    {
                      "tokenOrder",
                      "sequential"
                    }
                  }
                }
              }),
              new BsonDocument("$sort", new BsonDocument
              {
                  {
                      "score",
                      new BsonDocument("$meta", "textScore")
                  },
                  {
                      "posts",
                      -1
                  }
              }),
              new BsonDocument("$limit", 5)
            };

            var result = collection.Aggregate<BsonDocument>(pipeline).ToList();
            return result;
        }

        public List<BsonDocument> GetAutoCompletePODC(String podc)
        {
            IMongoDatabase mDb = driverClient.GetDatabase("Sandstorm");
            var collection = mDb.GetCollection<BsonDocument>("PODC");
            var pipeline = new BsonDocument[] {
              new BsonDocument("$search", new BsonDocument {
                {
                  "autocomplete",
                  new BsonDocument {
                    {
                      "query",
                      podc
                    },
                    {
                      "path",
                      "PODC Number"
                    },
                    {
                        "fuzzy",
                        new BsonDocument
                        {
                            {
                                "prefixLength",
                                1
                            },
                            {
                                "maxEdits",
                                1
                            }
                        }
                    },
                    {
                      "tokenOrder",
                      "sequential"
                    }
                  }
                }
              }),
              new BsonDocument("$sort", new BsonDocument
              {
                  {
                      "score",
                      new BsonDocument("$meta", "textScore")
                  },
                  {
                      "posts",
                      -1
                  }
              }),
              new BsonDocument("$limit", 5)
            };

            var result = collection.Aggregate<BsonDocument>(pipeline).ToList();
            return result;
        }

        /*__________________________________________BinFiles Derivations____________________________________________________________________*/


        public List<BsonDocument> GetPartBinBson(String binPartNumberString)
        {
            IMongoDatabase mDb = driverClient.GetDatabase("Sandstorm");
            var collection = mDb.GetCollection<BsonDocument>("BinFiles");
            var item_result = collection.Find(x => x["Part Number"] == binPartNumberString).ToList();
            return item_result;
        }

        public BsonDocument GetIndvPartBinBson(String binPartNumberString)
        {
            List<BsonDocument> binPartDocumentList = GetPartBinBson(binPartNumberString);

            // If nothing is in the list return a null object 
            if (binPartDocumentList.Count >= 1)
            {
                return binPartDocumentList[0];
            }
            else
            {
                return null;
            }
        }

    }

}

