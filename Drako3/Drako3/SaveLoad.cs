using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Windows.Storage;
using System.Runtime.Serialization;

namespace Drako3
{
    class SaveLoad
    {
        private static async void SaveGameState(string name, Game game)
        {
           
            StorageFolder folder = ApplicationData.Current.LocalFolder;
            StorageFile file = await folder.CreateFileAsync(name+".sav", CreationCollisionOption.ReplaceExisting);
            Stream writeStream = await file.OpenStreamForWriteAsync();
             DataContractSerializer serializer = new DataContractSerializer(typeof(Game));
             serializer.WriteObject(writeStream, game);
            using(StreamWriter streamWriter=new StreamWriter(writeStream))
            {

                streamWriter.WriteAsync("");
            }
            //dobrac się 

        }
    }
}
