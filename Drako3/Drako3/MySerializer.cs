using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using System.IO;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Drako3
{
    class MySerializer<DataType>
    {
        public static async Task SaveData(DataType data,string fileName)
        {
            StorageFolder folder = ApplicationData.Current.LocalFolder;
            StorageFile file = await folder.CreateFileAsync(fileName + ".sav", CreationCollisionOption.ReplaceExisting);
            using (Stream writeStream = await file.OpenStreamForWriteAsync())
            {
                DataContractSerializer serializer = new DataContractSerializer(typeof(DataType));
                serializer.WriteObject(writeStream, data);
                await writeStream.FlushAsync();
            }
        }

        public static async Task<DataType> LoadData( string fileName)
        {
            StorageFolder folder = ApplicationData.Current.LocalFolder;
            StorageFile file = await folder.GetFileAsync(fileName+".sav");// (fileName + ".sav", CreationCollisionOption.OpenIfExists);
           using( Stream readStream = await file.OpenStreamForReadAsync())
           {
               DataContractSerializer serializer = new DataContractSerializer(typeof(DataType));
               DataType data;

               data = (DataType)serializer.ReadObject(readStream);

               return data;
           }
           
           
            
                //if (data is Game) (data as Game).page = null; 
            
        }


    }
}
