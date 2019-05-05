using MongoDB.Bson;
using MongoDB.Bson.Serialization.IdGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebCoreMongo.Models
{
    public class Person
    {
        public ObjectId  _id { get; set; }
        //public ObjectId  Id { get; set; }
        public string Name { get; set; }
    }
}
