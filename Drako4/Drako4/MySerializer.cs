using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using System.IO;
using System.Runtime.Serialization;

namespace Drako3
{
    class MySerializer<DataType>
    {
        public static async Task SaveData(DataType data,string fileName)
        {
            StorageFolder folder = ApplicationData.Current.LocalFolder;
            StorageFile file = await folder.CreateFileAsync(fileName + ".sav", CreationCollisionOption.ReplaceExisting);
            Stream writeStream = await file.OpenStreamForWriteAsync();
            DataContractSerializer serializer = new DataContractSerializer(typeof(DataType));
            serializer.WriteObject(writeStream, data);
            await writeStream.FlushAsync();
            writeStream.Close();
        }

        public static async Task<DataType> LoadData( string fileName)
        {
            StorageFolder folder = ApplicationData.Current.LocalFolder;
            StorageFile file = await folder.GetFileAsync(fileName+".sav");// (fileName + ".sav", CreationCollisionOption.OpenIfExists);
            Stream readStream = await file.OpenStreamForReadAsync();
            DataContractSerializer serializer = new DataContractSerializer(typeof(DataType));
            DataType data = (DataType)serializer.ReadObject(readStream);
            //if (data is Game) (data as Game).page = null; 
            readStream.Close();
            return data;
        }


    }
}
